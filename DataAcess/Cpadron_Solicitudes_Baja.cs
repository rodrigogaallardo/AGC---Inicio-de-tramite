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
    
    public partial class Cpadron_Solicitudes_Baja
    {
        public int id_baja { get; set; }
        public int id_cpadron { get; set; }
        public int id_tipo_motivo_baja { get; set; }
        public System.DateTime fecha_baja { get; set; }
        public string observaciones { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
    
        public virtual CPadron_Solicitudes CPadron_Solicitudes { get; set; }
        public virtual TiposMotivoBaja TiposMotivoBaja { get; set; }
    }
}
