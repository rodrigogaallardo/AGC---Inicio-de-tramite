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
    
    public partial class Transf_Solicitudes_Notificaciones1
    {
        public int id_notificacion { get; set; }
        public int id_solicitud { get; set; }
        public int id_email { get; set; }
        public System.DateTime createDate { get; set; }
        public Nullable<System.DateTime> fechaNotificacionSSIT { get; set; }
        public Nullable<int> Id_NotificacionMotivo { get; set; }
    
        public virtual SSIT_Solicitudes_Notificaciones_motivos SSIT_Solicitudes_Notificaciones_motivos { get; set; }
    }
}
