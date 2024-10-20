using Domain.Interfaces.Services.ApiServices.ClientesProveedores;

namespace Infrastructure.Services.API.ClientesProveedores
{
    public class ClienteProveedorService : IClienteProveedorService
    {
        private readonly HttpClient _client;
        public ClienteProveedorService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<T>> GetClientesProveedoresByNameAsync<T>(string name)
        {
            var response = await _client.GetAsync($"ClienteProveedor/ByNombre?nombre={Uri.EscapeDataString(name)}");

            var list = await ApiTools.DeserializeResponse<List<T>>(response);
            if(list.Count <= 0)
            {
                throw new Exception("No se encontraron clientes o proveedores con ese nombre");
            }
            return list;
        }

    }
}
