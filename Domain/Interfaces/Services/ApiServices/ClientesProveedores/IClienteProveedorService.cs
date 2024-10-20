namespace Domain.Interfaces.Services.ApiServices.ClientesProveedores
{
    public interface IClienteProveedorService
    {
        Task<List<T>> GetClientesProveedoresByNameAsync<T>(string name);
    }
}
