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
    
    public partial class Transf_Solicitudes_Notificaciones
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
