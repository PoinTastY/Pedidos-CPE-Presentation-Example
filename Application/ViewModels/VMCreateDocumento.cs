using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using Domain.Entities.Interface;
using Domain.Interfaces.Repos;
using Domain.Interfaces.Services.ApiServices.Documentos;
using Domain.SDK_Comercial;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ViewModels
{
    public class VMCreateDocumento : ViewModelBase
    {
        private readonly IDocumentoService _documentoService = null!;
        private readonly SDKSettings _settings = null!;
        private Documento _documento = null!;
        private ClienteProveedorDTO _clienteProveedorSeleccionado = null!;
        public Documento Documento
        {
            get => _documento;
            set
            {
                _documento = value;
                OnPropertyChanged(nameof(Documento));
            }
        }
        private ObservableCollection<ProductoDTO> _productos = new();
        public ObservableCollection<ProductoDTO> Productos
        {
            get => _productos;
            set
            {
                _productos = value;
                OnPropertyChanged(nameof(Productos));
            }
        }

        public ClienteProveedorDTO ClienteProveedorSeleccionado
        {
            get => _clienteProveedorSeleccionado;
            set
            {
                _clienteProveedorSeleccionado = value;
                OnPropertyChanged(nameof(ClienteProveedorSeleccionado));
            }
        }

        public VMCreateDocumento(IDocumentoService documentoService, SDKSettings settings)
        {
            _documentoService = documentoService;
            _settings = settings;
            _documento = new Documento(settings);
            _documento.Fecha = DateTime.Now.ToString("MM/dd/yyyy");
            _clienteProveedorSeleccionado = new ClienteProveedorDTO();
            _clienteProveedorSeleccionado.CRAZONSOCIAL = "Seleccionar Socio";

        }
        public VMCreateDocumento() { }

        public async Task EnviarDocumentoMovimientos()
        {
            if(_clienteProveedorSeleccionado.CCODIGOCLIENTE == null)
            {
                throw new Exception("Debe seleccionar un cliente valido");
            }
            var movimientos = new List<Movimiento>();
            foreach (var producto in Productos)
            {
                movimientos.Add(new Movimiento(producto.CCODIGOPRODUCTO, "2", 10, Documento.Referencia));
            }

            Documento.IdInterfaz = await _documentoService.PostPendingDocumentAndMovementsPostgres<Documento, Movimiento>(Documento, movimientos);
            OnPropertyChanged(nameof(Documento));
        }
    }
}
