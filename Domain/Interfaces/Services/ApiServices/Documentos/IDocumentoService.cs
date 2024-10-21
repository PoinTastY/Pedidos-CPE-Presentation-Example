using Domain.Entities.Interface;

namespace Domain.Interfaces.Services.ApiServices.Documentos
{
    public interface IDocumentoService
    {
        /// <summary>
        /// Posts a document and its movements to the SDK
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentDTO"></param>
        /// <param name="movementsDTO"></param>
        /// <returns></returns>
        Task<T> PostDocumentAndMovementsSDK<T>(T documentDTO, List<T> movementsDTO);

        /// <summary>
        /// 
        /// Agrega un documento pendiente y sus movimientos a la base de datos postgres
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="movementsDTO"></param>
        /// <returns>Id del documento creado</returns>
        Task<int> PostPendingDocumentAndMovementsPostgres<TDoc, TMov>(TDoc documento, List<TMov> movimientos);

        /// <summary>
        /// Gets pending documents to work with them
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> GetDocumentosPendientes<T>();

    }
}