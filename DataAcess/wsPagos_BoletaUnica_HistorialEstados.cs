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
    
    public partial class wsPagos_BoletaUnica_HistorialEstados
    {
        public int id { get; set; }
        public int id_pago_BU { get; set; }
        public Nullable<System.DateTime> FechaPago_BU { get; set; }
        public string TrazaPago_BU { get; set; }
        public int EstadoPago_BU { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
    
        public virtual wsPagos_BoletaUnica wsPagos_BoletaUnica { get; set; }
        public virtual wsPagos_BoletaUnica_Estados wsPagos_BoletaUnica_Estados { get; set; }
    }
}
