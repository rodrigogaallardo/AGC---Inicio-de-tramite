//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class wsPagos_Conceptos
    {
        public int id_pagoconcepto { get; set; }
        public int id_pago { get; set; }
        public int cod_concepto_1 { get; set; }
        public int cod_concepto_2 { get; set; }
        public int cod_concepto_3 { get; set; }
        public string descripcion_concepto { get; set; }
        public int cantidad_concepto { get; set; }
        public decimal importe_concepto { get; set; }
        public Nullable<System.Guid> ConceptoID { get; set; }
        public Nullable<int> Vigencia { get; set; }
        public Nullable<decimal> valor_para_reglas { get; set; }
    
        public virtual wsPagos wsPagos { get; set; }
    }
}
