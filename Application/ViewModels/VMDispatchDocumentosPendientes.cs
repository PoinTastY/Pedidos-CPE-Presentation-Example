using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using ApplicationLayer.ViewModels.BindableObjects;
using Domain.Entities.Interface;
using Domain.Interfaces.Services.ApiServices.Documentos;
using Domain.Interfaces.Services.ApiServices.Movimientos;
using Domain.Interfaces.Services.ApiServices.Productos;
using System.Collections.ObjectModel;

namespace ApplicationLayer.ViewModels
{
    public class VMDispatchDocumentosPendientes : ViewModelBase
    {
        private readonly IDocumentoService _documentoService = null!;
        private readonly IMovimientoService _movimientoService = null!;
        private readonly IProductoService _productoService = null!;
        private ObservableCollection<Documento> _documentosPendientes = new();
        private Documento _documentoSeleccionado = null!;
        private List<Movimiento> _movimientos = new();
        private List<ProductoDTO> _productos = new();
        private ObservableCollection<ViewProductoUnidades> _productosUnidades = new();

        public VMDispatchDocumentosPendientes(IDocumentoService documentoService, IMovimientoService movimientoService, IProductoService productoService)
        {
            _documentoService = documentoService;
            _movimientoService = movimientoService;
            _productoService = productoService;
        }

        public VMDispatchDocumentosPendientes() { }

        public ObservableCollection<Documento> DocumentosPendientes
        {
            get => _documentosPendientes;
            set
            {
                _documentosPendientes = value;
                OnPropertyChanged(nameof(DocumentosPendientes));
            }
        }

        public List<Movimiento> Movimientos
        {
            get => _movimientos;
        }

        public List<ProductoDTO> Productos
        {
            get => _productos;
        }

        public ObservableCollection<ViewProductoUnidades> ProductosUnidades
        {
            get => _productosUnidades;
            set
            {
                _productosUnidades = value;
                OnPropertyChanged(nameof(ProductosUnidades));
            }
        }

        public async Task LoadDocumentosPendientes()
        {
            DocumentosPendientes = new ObservableCollection<Documento>(await _documentoService.GetDocumentosPendientes<Documento>());
            OnPropertyChanged(nameof(DocumentosPendientes));
            return;
        }

        public Documento DocumentoSeleccionado
        {
            get => _documentoSeleccionado;
            set
            {
                _documentoSeleccionado = value;
                OnPropertyChanged(nameof(DocumentoSeleccionado));
            }
        }

        public async Task FetchMovimientosAndProductos()
        {
            try
            {
                if (DocumentoSeleccionado == null)
                {
                    return;
                }
                _movimientos = new(await _movimientoService.GetMovimientosByDocumentoId(DocumentoSeleccionado.IdInterfaz));
                if(_movimientos.Count == 0)
                {
                    throw new Exception("No se encontraron movimientos para el documento seleccionado, algo anda mal");
                }
                _productos = new(await _productoService.GetProductosByCodigos<ProductoDTO>(_movimientos.Select(m => m.CodigoProducto).ToList()));
                _productosUnidades = new();
                foreach (var producto in _productos) {
                    var productMatch = _movimientos.First(m => m.CodigoProducto == producto.CCODIGOPRODUCTO);
                    _productosUnidades.Add(new ViewProductoUnidades { Producto = producto, Unidades = productMatch.Unidades, Surtidas = productMatch.Surtidas });
                }
                OnCollectionChanged(nameof(ProductosUnidades));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los movimientos y productos del documento ({ex.Message})", ex);
            }
        }

        public async Task SaveDocumentAndMovementsOnSDK()
        {
            //validate if all movements have units
            if (_movimientos.Any(m => m.Surtidas == 0))
            {
                throw new Exception("No se puede guardar un movimiento con 0 unidades");
            }
            DocumentoSeleccionado.Impreso = true;
            var docDTO = new DocumentDTO(DocumentoSeleccionado);
            var movDTO = new List<MovimientoDTO>();
            foreach (var movimiento in Movimientos)
            {
                movimiento.Unidades = movimiento.Surtidas;
                movDTO.Add(new MovimientoDTO(movimiento));
            }
             
            try
            {
                var resultDTO = await _documentoService.PostDocumentAndMovementsSDK<DocumentDTO, MovimientoDTO>(docDTO, movDTO);
                DocumentoSeleccionado.IdContpaqiSQL = resultDTO.CIDDOCUMENTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar el documento y movimientos en el SDK ({ex.Message})", ex);
            }

            try
            {
                await _documentoService.PutDocumento(DocumentoSeleccionado);
                await LoadDocumentosPendientes();
                DocumentoSeleccionado = null!;
                _productosUnidades = new();
                OnCollectionChanged(nameof(ProductosUnidades));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el documento en la base de datos ({ex.Message})", ex);
            }
        }
    }
}
