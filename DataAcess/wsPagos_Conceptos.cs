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