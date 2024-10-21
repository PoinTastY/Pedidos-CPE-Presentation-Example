using Domain.Entities.Interface;

namespace Domain.Interfaces.Services.ApiServices.Movimientos
{
    public interface IMovimientoService
    {
        /// <summary>
        /// Gets the movements of a document by its id
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<List<Movimiento>> GetMovimientosByDocumentoId(int idDocumento);

        /// <summary>
        /// Updates the movement with the provided id, with the provided unidades
        /// </summary>
        /// <param name="idMovimiento"></param>
        /// <param name="unidades"></param>
        /// <returns>Completed task and the message form the api</returns>
        Task UpdateMovimientos(List<Movimiento> movimientos);
    }
}
