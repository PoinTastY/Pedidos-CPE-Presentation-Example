using Domain.Interfaces.Services.ApiServices.Documentos;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services.API.Documentos
{
    public class DocumentoService : IDocumentoService
    {
        private readonly HttpClient _client;

        public DocumentoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> PostDocumentAndMovementsSDK<T>(T documento, List<T> movimientos)
        {
            // Crear el payload para enviar al API, simulando la estructura de la API ( DocumentoConMovimientosDTO )
            var payload = new
            {
                Document = documento,
                Movements = movimientos
            };

            // Serializar el payload a JSON
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            // Enviar el payload al API
            var response = await _client.PostAsync("/Documentos", content);

            //Retornar desempaquetado
            return await ApiTools.DeserializeResponse<T>(response);
        }

        public async Task<int> PostPendingDocumentAndMovementsPostgres<TDoc, TMov>(TDoc pedido, List<TMov> movimientos)
        {
            var payload = new
            {
                Documento = pedido,
                Movimientos = movimientos
            };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Pendientes", content);

            return await ApiTools.DeserializeResponse<int>(response);
        }
    }
}
