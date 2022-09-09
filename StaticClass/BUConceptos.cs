using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClass
{
    public class BUConcepto
    {

        public int idPagoConcepto { get; set; }
        public int IdPago { get; set; }
        public int CodConcepto1 { get; set; }
        public int CodConcepto2 { get; set; }
        public int CodConcepto3 { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal ValorDetalle { get; set; }
        public bool? AdmiteReglas { get; set; }
        public Guid ItemID { get; set; }

        public string ConceptoFormatoBU
        {
            get
            {
                return string.Format("{0:00}.{1:00}.{2:00}", this.CodConcepto1, this.CodConcepto2, this.CodConcepto3);
            }

        }
        public string ConceptoFormatoPE
        {
            get
            {
                return string.Format("{0:00}{1:00}{2:00}", this.CodConcepto1, this.CodConcepto2, this.CodConcepto3);
            }

        }
    }
    public class BUConceptoException : Exception
    {
        public BUConceptoException(string mensaje)
            : base(mensaje, new Exception())
        {
        }
    }
}


