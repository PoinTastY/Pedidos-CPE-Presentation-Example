using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using Domain.Interfaces.Services.ApiServices.Productos;
using Domain.SDK_Comercial;
using System.Collections.ObjectModel;

namespace ApplicationLayer.ViewModels
{
    public class VMSearchProductos : ViewModelBase
    {
        private readonly IProductoService _productosService = null!;
        private ObservableCollection<ProductoDTO> _productosEncontrados = new();
        private ObservableCollection<ProductoDTO> _productosSeleccionados = new();
        private readonly SDKSettings _settings = null!;
        private readonly List<bool> _filterRequirements = new();
        private readonly List<int> _valoresClasificaciones = new();
        private readonly List<string> _accionesClasificaciones = new();
        public VMSearchProductos(IProductoService productosService, SDKSettings settings)
        {
            _productosService = productosService;
            _settings = settings;

            //load
            _filterRequirements.Add(settings.FiltrarClasif1);
            _filterRequirements.Add(settings.FiltrarClasif2);
            _filterRequirements.Add(settings.FiltrarClasif3);
            _filterRequirements.Add(settings.FiltrarClasif4);
            _filterRequirements.Add(settings.FiltrarClasif5);
            _filterRequirements.Add(settings.FiltrarClasif6);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION1);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION2);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION3);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION4);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION5);
            _valoresClasificaciones.Add(settings.CIDVALORCLASIFICACION6);
            _accionesClasificaciones.Add(settings.FiltrarClasif1Value);
            _accionesClasificaciones.Add(settings.FiltrarClasif2Value);
            _accionesClasificaciones.Add(settings.FiltrarClasif3Value);
            _accionesClasificaciones.Add(settings.FiltrarClasif4Value);
            _accionesClasificaciones.Add(settings.FiltrarClasif5Value);
            _accionesClasificaciones.Add(settings.FiltrarClasif6Value);
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
                var needsFilter = FiltrarProducto(producto);
                if (needsFilter)
                    continue;
                if (!ProductosSeleccionados.Contains(producto))
                {
                    ProductosEncontrados.Add(producto);
                }
            }
            OnCollectionChanged(nameof(ProductosEncontrados));
        }

        private bool FiltrarProducto(ProductoDTO producto)
        {
            var index = 0;
            foreach(var needFilter in _filterRequirements)
            {
                if (needFilter)
                {
                    return AccionFiltro(_accionesClasificaciones[index], _valoresClasificaciones[index], producto, index);
                }
                index++;
            }
            return false;
        }

        private bool AccionFiltro(string filtro, int valor, ProductoDTO producto, int indice)
        {
            // Construimos el nombre de la propiedad basado en el índice
            string nombrePropiedad = $"CIDVALORCLASIFICACION{indice + 1}";

            // Reflection to get the property value
            var tipoProducto = producto.GetType();
            var propiedad = tipoProducto.GetProperty(nombrePropiedad);

            if (propiedad == null)
                throw new Exception($"La propiedad {nombrePropiedad} no existe en ProductoDTO.");

            var valorPropiedad = (int)propiedad.GetValue(producto);

            // Validate filter
            if (filtro == "ignore")
                throw new Exception("Incongruencia en la lógica de filtro de SDKSettings.json");

            //deben estar invertidos, think about it
            if (filtro == "equal")
                return valor != valorPropiedad;

            if (filtro == "not")
                return valor == valorPropiedad;

            throw new Exception($"Incongruencia en la lógica de filtro de SDKSettings.json, no se pudo aplicar ningún filtro. Filto solicitado:{filtro}");
        }


        public void EliminarProductoSeleccionado(ProductoDTO producto)
        {
            ProductosSeleccionados.Remove(producto);
            OnCollectionChanged(nameof(ProductosSeleccionados));
        }
    }
}
