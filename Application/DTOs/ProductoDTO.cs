using Domain.Entities.ContpaqiComercial;

namespace ApplicationLayer.DTOs
{
    public class ProductoDTO
    {
        public int CIDPRODUCTO { get; set; }
        public string CCODIGOPRODUCTO { get; set; } = null!;
        public string CNOMBREPRODUCTO { get; set; } = null!;
        public string? CDESCRIPCIONPRODUCTO { get; set; }
        public string CTEXTOEXTRA1 { get; set; } = null!;
        public string CTEXTOEXTRA2 { get; set; } = null!;
        public string CTEXTOEXTRA3 { get; set; } = null!;
        public double CPRECIO1 { get; set; }
        public string CCLAVESAT { get; set; } = null!;
        public int CIDVALORCLASIFICACION1 { get; set; }
        public int CIDVALORCLASIFICACION2 { get; set; }
        public int CIDVALORCLASIFICACION3 { get; set; }
        public int CIDVALORCLASIFICACION4 { get; set; }
        public int CIDVALORCLASIFICACION5 { get; set; }
        public int CIDVALORCLASIFICACION6 { get; set; } // para pesar solo se usan los productos en los que este campo no sea 0
        public ProductoDTO() { }
        public ProductoDTO(ProductoSQL producto)
        {
            CIDPRODUCTO = producto.CIDPRODUCTO;
            CCODIGOPRODUCTO = producto.CCODIGOPRODUCTO;
            CNOMBREPRODUCTO = producto.CNOMBREPRODUCTO;
            CDESCRIPCIONPRODUCTO = producto.CDESCRIPCIONPRODUCTO;
            CTEXTOEXTRA1 = producto.CTEXTOEXTRA1;
            CTEXTOEXTRA2 = producto.CTEXTOEXTRA2;
            CTEXTOEXTRA3 = producto.CTEXTOEXTRA3;
            CPRECIO1 = producto.CPRECIO1;
            CCLAVESAT = producto.CCLAVESAT;
        }
    }
}
