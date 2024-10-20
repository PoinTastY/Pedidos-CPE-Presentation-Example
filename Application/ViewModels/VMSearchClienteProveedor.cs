using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using Domain.Interfaces.Services.ApiServices.ClientesProveedores;
using System.Collections.ObjectModel;

namespace ApplicationLayer.ViewModels
{
    public class VMSearchClienteProveedor : ViewModelBase
    {
        private readonly IClienteProveedorService _clienteProveedorService = null!;
        private ObservableCollection<ClienteProveedorDTO> _clienteProveedores = new();
        public ObservableCollection<ClienteProveedorDTO> ClientesProveedoresEncontrados
        {
            get { return _clienteProveedores; }
            set { _clienteProveedores = value; OnCollectionChanged(nameof(ClientesProveedoresEncontrados)); }
        }

        private ClienteProveedorDTO _clienteProveedorSeleccionado = new();
        public ClienteProveedorDTO ClienteProveedorSeleccionado
        {
            get { return _clienteProveedorSeleccionado; }
            set { _clienteProveedorSeleccionado = value; OnPropertyChanged(nameof(_clienteProveedorSeleccionado)); }
        }

        public VMSearchClienteProveedor(IClienteProveedorService clienteProveedorService)
        {
            _clienteProveedorService = clienteProveedorService;
        }

        public VMSearchClienteProveedor() { }

        public async Task SearchClienteProveedorAsync(string search)
        {
            var clientesProveedores = await _clienteProveedorService.GetClientesProveedoresByNameAsync<ClienteProveedorDTO>(search);
            ClientesProveedoresEncontrados = new ObservableCollection<ClienteProveedorDTO>(clientesProveedores);
            OnCollectionChanged(nameof(ClientesProveedoresEncontrados));
        }
    }
}
