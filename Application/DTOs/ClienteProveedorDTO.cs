namespace ApplicationLayer.DTOs
{
    public class ClienteProveedorDTO
    {
        public int CIDCLIENTEPROVEEDOR { get; set; }
        public string CCODIGOCLIENTE { get; set; } = null!;
        public string CRAZONSOCIAL { get; set; } = null!;
        public string CRFC { get; set; } = null!;
        public int CIDVALORCLASIFCLIENTE1 { get; set; }
        public int CIDVALORCLASIFCLIENTE2 { get; set; }
        public int CIDVALORCLASIFCLIENTE3 { get; set; }
        public int CIDVALORCLASIFCLIENTE4 { get; set; }
        public int CIDVALORCLASIFCLIENTE5 { get; set; }
        public int CIDVALORCLASIFCLIENTE6 { get; set; }
        public int CTIPOCLIENTE { get; set; }
        public int CESTATUS { get; set; }
        public double CLIMITECREDITOCLIENTE { get; set; }
        public int CIDVALORCLASIFPROVEEDOR1 { get; set; }
        public int CIDVALORCLASIFPROVEEDOR2 { get; set; }
        public int CIDVALORCLASIFPROVEEDOR3 { get; set; }
        public int CIDVALORCLASIFPROVEEDOR4 { get; set; }
        public int CIDVALORCLASIFPROVEEDOR5 { get; set; }
        public int CIDVALORCLASIFPROVEEDOR6 { get; set; }
        public string CTEXTOEXTRA1 { get; set; } = null!;
        public string CTEXTOEXTRA2 { get; set; } = null!;
        public string CTEXTOEXTRA3 { get; set; } = null!;
        public string CEMAIL1 { get; set; } = null!;
        public string CEMAIL2 { get; set; } = null!;
        public string CEMAIL3 { get; set; } = null!;
        public string CMETODOPAG { get; set; } = null!;
        public string CUSOCFDI { get; set; } = null!;
        public string CREGIMFISC { get; set; } = null!;
        public ClienteProveedorDTO() { }
    }
}
