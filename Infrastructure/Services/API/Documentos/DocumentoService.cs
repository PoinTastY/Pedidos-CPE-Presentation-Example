using Application.DTOs;
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

        public async Task<T> PutDocumentAndMovements<T>(T documento, List<T> movimientos)
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
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> GetDocumentoByConceptoSerieAndFolioSDKAsync<T>(string codConcepto, string serie, string folio)
        {
            try
            {
                var response = await _client.GetAsync($"/getDocumentByConceptoFolioAndSerieSDK/{codConcepto}/{serie}/{folio}");

                return await DeserializeResponse<T>(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el documento: {ex.Message} (Inner: {ex.InnerException})");
            }
        }

        public async Task<T> GetDocumentByIdSDKAsync<T>(int idDocumento)
        {

            var response = await _client.GetAsync($"/getDocumentByIdSDK/{idDocumento}");

            return await DeserializeResponse<T>(response);
        }

        public async Task<List<T>> GetPedidosByFechaSerieCPESQL<T>(DateTime fechaInicio, DateTime fechaFin, string serie)
        {
            try
            {
                // Formatear las fechas en formato ISO 8601 para que el API las pueda interpretar
                string fechaInicioFormatted = fechaInicio.ToString("yyyy-MM-ddTHH:mm:ss");
                string fechaFinFormatted = fechaFin.ToString("yyyy-MM-ddTHH:mm:ss");

                string url = $"/getPedidosByFechaSerieCPESQL/{Uri.EscapeDataString(fechaInicioFormatted)}/{Uri.EscapeDataString(fechaFinFormatted)}/{Uri.EscapeDataString(serie)}";
                var response = await _client.GetAsync(url);

                return await DeserializeResponse<List<T>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de documentos: " + ex.Message);
            }
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content) ?? throw new Exception("No se pudo deserializar la respuesta del API");

                var dataString = apiResponse.Data.ToString() ?? throw new Exception("No se pudo obtener el Data String de la respuesta del api");
                var document = JsonConvert.DeserializeObject<T>(dataString) ?? throw new Exception("No se pudo deserializar Data de la respuesta del api");
                return document;

            }
            else
            {
                throw new Exception("Parece que no tuvimos una respuesta Exitosa :c: " + response.ReasonPhrase);
            }
        }
    }
}
