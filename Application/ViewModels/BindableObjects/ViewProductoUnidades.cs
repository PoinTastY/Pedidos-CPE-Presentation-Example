using ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ViewModels.BindableObjects
{
    public class ViewProductoUnidades
    {
        public ProductoDTO Producto { get; set; } = new ProductoDTO();
        public double Unidades { get; set; }
        public double Surtidas { get; set; }
    }
}
