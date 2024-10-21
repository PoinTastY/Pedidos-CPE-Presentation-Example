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

        public async Task<List<T>> GetProductosByCodigos<T>(List<string> codigos)
        {
            var query = string.Join("&", codigos.Select(c => $"codigos={Uri.EscapeDataString(c)}"));

            var response = await _client.GetAsync($"/Productos/ByCodigos?{query}");

            return await ApiTools.DeserializeResponse<List<T>>(response);
        }
    }
}
