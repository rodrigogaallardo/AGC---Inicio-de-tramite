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
    
    public partial class SolicitudRubro
    {
        public int NroSolicitud { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool EsAnterior { get; set; }
        public int id_tipoactividad { get; set; }
        public int id_tipodocreq { get; set; }
        public decimal Superficie { get; set; }
        public Nullable<int> id_ImpactoAmbiental { get; set; }
    
        public virtual Solicitud Solicitud { get; set; }
    }
}
