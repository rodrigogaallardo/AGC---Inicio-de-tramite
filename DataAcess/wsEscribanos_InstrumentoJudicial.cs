//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class wsEscribanos_InstrumentoJudicial
    {
        public int id_insjud { get; set; }
        public int id_derecho_ocupacion { get; set; }
        public Nullable<System.DateTime> fecha_testimonio_insjud { get; set; }
        public string juzgado_insjud { get; set; }
        public string secretaria_insjud { get; set; }
        public string jurisdiccion_insjud { get; set; }
        public string autos_insjud { get; set; }
        public Nullable<System.DateTime> fecha_pago_impsellos_insjud { get; set; }
        public string Banco_pago_insjud { get; set; }
        public Nullable<decimal> monto_pago_insjud { get; set; }
    
        public virtual wsEscribanos_DerechoOcupacion wsEscribanos_DerechoOcupacion { get; set; }
    }
}