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
    
    public partial class SSIT_Solicitudes_RubrosCN
    {
        public int IdSolicitudRubro { get; set; }
        public int IdSolicitud { get; set; }
        public int IdRubro { get; set; }
        public string CodigoRubro { get; set; }
        public string NombreRubro { get; set; }
        public int IdTipoActividad { get; set; }
        public decimal SuperficieHabilitar { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual SSIT_Solicitudes SSIT_Solicitudes { get; set; }
        public virtual TipoActividad TipoActividad { get; set; }
        public virtual RubrosCN RubrosCN { get; set; }
    }
}