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
    
    public partial class SSIT_Solicitudes_AvisoCaducidad
    {
        public int id_aviso { get; set; }
        public int id_solicitud { get; set; }
        public int id_email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> fechaNotificacionSSIT { get; set; }
    
        public virtual Emails Emails { get; set; }
        public virtual SSIT_Solicitudes SSIT_Solicitudes { get; set; }
    }
}