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

        public async Task<TDoc> PostDocumentAndMovementsSDK<TDoc, TMov>(TDoc documento, List<TMov> movimientos)
        {
            // Crear el payload para enviar al API, simulando la estructura de la API ( DocumentoConMovimientosDTO )
            var payload = new
            {
                Documento = documento,
                Movimientos = movimientos
            };

            // Serializar el payload a JSON
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            // Enviar el payload al API
            var response = await _client.PostAsync("/DocumentosSDK", content);

            //Retornar desempaquetado
            return await ApiTools.DeserializeResponse<TDoc>(response);
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

        public async Task<List<T>> GetDocumentosPendientes<T>()
        {
            var response = await _client.GetAsync("/Pendientes");
            return await ApiTools.DeserializeResponse<List<T>>(response);
        }

        public async Task PutDocumento<T>(T documento)
        {
            var content = new StringContent(JsonConvert.SerializeObject(documento), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/Pendientes", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
