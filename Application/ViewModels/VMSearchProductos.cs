using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using Domain.Interfaces.Services.ApiServices.Productos;
using System.Collections.ObjectModel;

namespace ApplicationLayer.ViewModels
{
    public class VMSearchProductos : ViewModelBase
    {
        private readonly IProductoService _productosService = null!;
        private ObservableCollection<ProductoDTO> _productosEncontrados = new();
        private ObservableCollection<ProductoDTO> _productosSeleccionados = new();
        public VMSearchProductos(IProductoService productosService)
        {
            _productosService = productosService;
        }

        public VMSearchProductos() { }

        public ObservableCollection<ProductoDTO> ProductosEncontrados
        {
            get => _productosEncontrados;
            set
            {
                _productosEncontrados = value;
                OnPropertyChanged(nameof(ProductosEncontrados));
            }
        }

        public ObservableCollection<ProductoDTO> ProductosSeleccionados
        {
            get => _productosSeleccionados;
            set
            {
                _productosSeleccionados = value;
                OnPropertyChanged(nameof(ProductosSeleccionados));
            }
        }

        public async Task BuscarProductosPorNombre(string nombre)
        {
            var productos = await _productosService.GetProductosByNameAsync<ProductoDTO>(nombre);
            foreach(var producto in productos)
            {
                if (!ProductosSeleccionados.Contains(producto))
                {
                    ProductosEncontrados.Add(producto);
                }
            }
            OnCollectionChanged(nameof(ProductosEncontrados));
        }

        public void EliminarProductoSeleccionado(ProductoDTO producto)
        {
            ProductosSeleccionados.Remove(producto);
            OnCollectionChanged(nameof(ProductosSeleccionados));
        }
    }
}
