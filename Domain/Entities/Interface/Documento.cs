using Domain.SDK_Comercial;

namespace Domain.Entities.Interface
{
    public class Documento
    {
        public int IdInterfaz { get; set; } = 0;
        public int IdContpaqiSQL { get; set; } = 0;
        public double Folio { get; set; } = 0;
        public int NumMoneda { get; set; } = 0;
        public double TipoCambio { get; set; } = 0;
        public double Importe { get; set; } = 0;
        public double DescuentoDoc1 { get; set; } = 0;
        public double DescuentoDoc2 { get; set; } = 0;
        public int SistemaOrigen { get; set; } = 0;
        public string CodConcepto { get; set; }//
        public string Serie { get; set; }//
        /// <summary>
        /// Format: "MM/dd/yyyy"
        /// </summary>
        public string Fecha { get; set; } = string.Empty; //REQUIRED
        public string CodigoCteProv { get; set; }//
        public string RazonSocial { get; set; } = string.Empty;//
        public string CodigoAgente { get; set; } = string.Empty;
        public string Referencia { get; set; }//
        public int Afecta { get; set; } = 0;
        public double Gasto1 { get; set; } = 0;
        public double Gasto2 { get; set; } = 0;
        public double Gasto3 { get; set; } = 0;
        public string Observaciones { get; set; } = string.Empty;
        public string TextoExtra1 { get; set; } = string.Empty;
        public string TextoExtra2 { get; set; } = string.Empty;
        public string TextoExtra3 { get; set; } = string.Empty;
        public bool Surtido { get; set; } = false;
        public bool Impreso { get; set; } = false;

        public Documento(string CodConcepto, string Serie,  string CodigoCteProv, string Referencia)
        {
            this.CodConcepto = CodConcepto;
            this.Serie = Serie;
            this.CodigoCteProv = CodigoCteProv;
            this.Referencia = Referencia;
        }
        public Documento(SDKSettings settings)
        {
            CodConcepto = settings.CodConcepto;
            Serie = settings.Serie;
            CodigoCteProv = settings.CodigoCteProv;
            Referencia = settings.Referencia;
        }

        public Documento() { }
    }
}
