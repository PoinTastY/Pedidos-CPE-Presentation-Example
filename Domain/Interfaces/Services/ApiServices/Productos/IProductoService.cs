namespace Domain.Interfaces.Services.ApiServices.Productos
{
    public interface IProductoService
    {
        /// <summary>
        /// Gets the products by their ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<List<T>> GetProductosByIdsSQLAsync<T>(List<int> ids);

        /// <summary>
        /// Ask for te products by id filterig CPE requirements
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<List<T>> GetProductosByIdListCPESQLAsync<T>(List<int> ids);

        /// <summary>
        /// Search for products by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns>products that product.CNOMBREPRODUCTO contains the provided name</returns>
        Task<List<T>> GetProductosByNameAsync<T>(string name);
    }
}
