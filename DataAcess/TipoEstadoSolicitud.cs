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
    
    public partial class TipoEstadoSolicitud
    {
        public TipoEstadoSolicitud()
        {
            this.Solicitud = new HashSet<Solicitud>();
            this.SSIT_Solicitudes_Nuevas = new HashSet<SSIT_Solicitudes_Nuevas>();
            this.Transf_Solicitudes = new HashSet<Transf_Solicitudes>();
            this.SSIT_Solicitudes = new HashSet<SSIT_Solicitudes>();
            this.TransicionEstadoSolicitud = new HashSet<TransicionEstadoSolicitud>();
            this.TransicionEstadoSolicitud1 = new HashSet<TransicionEstadoSolicitud>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Solicitud> Solicitud { get; set; }
        public virtual ICollection<SSIT_Solicitudes_Nuevas> SSIT_Solicitudes_Nuevas { get; set; }
        public virtual ICollection<Transf_Solicitudes> Transf_Solicitudes { get; set; }
        public virtual ICollection<SSIT_Solicitudes> SSIT_Solicitudes { get; set; }
        public virtual ICollection<TransicionEstadoSolicitud> TransicionEstadoSolicitud { get; set; }
        public virtual ICollection<TransicionEstadoSolicitud> TransicionEstadoSolicitud1 { get; set; }
    }
}