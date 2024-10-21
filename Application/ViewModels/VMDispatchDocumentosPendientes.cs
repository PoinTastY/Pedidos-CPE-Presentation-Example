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
                _movimientos = new(await _movimientoService.GetMovimientosByDocumentoId(DocumentoSeleccionado.IdInterfaz));
                if(_movimientos.Count == 0)
                {
                    throw new Exception("No se encontraron movimientos para el documento seleccionado, algo anda mal");
                }
                var productos = await _productoService.GetProductosByCodigos<ProductoDTO>(_movimientos.Select(m => m.CodigoProducto).ToList());
                _productosUnidades = new();
                foreach (var producto in productos) {
                    _productosUnidades.Add(new ViewProductoUnidades { Producto = producto, Unidades = _movimientos.First(m => m.CodigoProducto == producto.CCODIGOPRODUCTO).Unidades });
                }
                OnCollectionChanged(nameof(ProductosUnidades));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los movimientos y productos del documento ({ex.Message})", ex);
            }
        }
    }
}
