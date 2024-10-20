using Domain.Interfaces.Services.ApiServices.Productos;

namespace Infrastructure.Services.API.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _client;

        public ProductoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<T>> GetProductosByNameAsync<T>(string name)
        {
            var response = await _client.GetAsync($"/Productos/ByNombre?nombre={Uri.EscapeDataString(name)}");

            return await ApiTools.DeserializeResponse<List<T>>(response);
        }
    }
}
