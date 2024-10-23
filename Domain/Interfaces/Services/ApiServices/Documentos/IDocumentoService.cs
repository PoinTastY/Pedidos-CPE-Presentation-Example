using Domain.Entities.Interface;

namespace Domain.Interfaces.Services.ApiServices.Documentos
{
    public interface IDocumentoService
    {
        /// <summary>
        /// !!!must be DTOS!!! posts a document and its movements to the SDK
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="documentDTO"></param>
        /// <param name="movementsDTO"></param>
        Task<TDoc> PostDocumentAndMovementsSDK<TDoc, TMov>(TDoc documento, List<TMov> movimientos);

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
        Task<List<T>> GetDocumentosPendientes<T>();

        /// <summary>
        /// Puts a document in the database (updates)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documento"></param>
        Task PutDocumento<T>(T documento);
    }
}