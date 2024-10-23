using Domain.Entities.Interface;
using Domain.Interfaces.Services.ApiServices.Movimientos;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services.API.Movimientos
{
    public class MovimientoService : IMovimientoService
    {
        private readonly HttpClient _client;

        public MovimientoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Movimiento>> GetMovimientosByDocumentoId(int idDocumento)
        {
            var response = await _client.GetAsync($"/Movimientos/ByDocumentoId?documentoId={idDocumento}");
            return await ApiTools.DeserializeResponse<List<Movimiento>>(response);
        }

        public async Task UpdateMovimientos(List<Movimiento> movimientos)
        {
            var content = new StringContent(JsonConvert.SerializeObject(movimientos), Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("/Movimientos", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al actualizar los movimientos: {response.ReasonPhrase}");
            }
        }
    }
}
