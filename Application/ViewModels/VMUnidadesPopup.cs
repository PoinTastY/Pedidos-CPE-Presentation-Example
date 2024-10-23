using ApplicationLayer.ViewModels.BindableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ViewModels
{
    public class VMUnidadesPopup
    {
        private ViewProductoUnidades _productoUnidades = new();
        public ViewProductoUnidades ProductoUnidades
        {
            get => _productoUnidades;
            set
            {
                _productoUnidades = value;
            }
        }
        public VMUnidadesPopup() { }
        public VMUnidadesPopup(ViewProductoUnidades productoUnidades)
        {
            _productoUnidades = productoUnidades;
        }
    }
}
