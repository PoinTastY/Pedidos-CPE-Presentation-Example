namespace Domain.Interfaces.Services.ApiServices.Productos
{
    public interface IProductoService
    {
        /// <summary>
        /// Search for products by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns>products that product.CNOMBREPRODUCTO contains the provided name</returns>
        Task<List<T>> GetProductosByNameAsync<T>(string name);
    }
}
