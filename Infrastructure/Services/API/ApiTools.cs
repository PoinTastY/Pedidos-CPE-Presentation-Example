using ApplicationLayer.DTOs;
using Newtonsoft.Json;

namespace Infrastructure.Services.API
{
    public static class ApiTools
    {
        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
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
                throw new Exception($"Parece que no tuvimos una respuesta exitosa para:{response.RequestMessage.RequestUri.LocalPath}: " + response.ReasonPhrase);
            }
        }
    }
}
