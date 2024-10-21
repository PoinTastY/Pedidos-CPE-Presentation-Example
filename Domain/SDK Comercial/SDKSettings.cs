﻿namespace Domain.SDK_Comercial
{
    public class SDKSettings
    {
        public string CodConcepto { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string CodigoCteProv { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string ServerUri { get; set; } = string.Empty;
        public string CodigoAlmacen { get; set; } = string.Empty;
        public int CIDVALORCLASIFICACION1 { get; set; }
        public int CIDVALORCLASIFICACION2 { get; set; }
        public int CIDVALORCLASIFICACION3 { get; set; }
        public int CIDVALORCLASIFICACION4 { get; set; }
        public int CIDVALORCLASIFICACION5 { get; set; }
        public int CIDVALORCLASIFICACION6 { get; set; }
        public bool FiltrarClasif1 { get; set; }
        public bool FiltrarClasif2 { get; set; }
        public bool FiltrarClasif3 { get; set; }
        public bool FiltrarClasif4 { get; set; }
        public bool FiltrarClasif5 { get; set; }
        public bool FiltrarClasif6 { get; set; }
        public string FiltrarClasif1Value { get; set; } = "ignore";
        public string FiltrarClasif2Value { get; set; } = "ignore";
        public string FiltrarClasif3Value { get; set; } = "ignore";
        public string FiltrarClasif4Value { get; set; } = "ignore";
        public string FiltrarClasif5Value { get; set; } = "ignore";
        public string FiltrarClasif6Value { get; set; } = "ignore";

        public SDKSettings() { }
    }
}
