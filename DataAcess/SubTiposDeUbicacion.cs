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
    
    public partial class SubTiposDeUbicacion
    {
        public SubTiposDeUbicacion()
        {
            this.EncomiendaExt_Ubicaciones = new HashSet<EncomiendaExt_Ubicaciones>();
            this.SSIT_Solicitudes_Ubicaciones = new HashSet<SSIT_Solicitudes_Ubicaciones>();
            this.CPadron_Ubicaciones = new HashSet<CPadron_Ubicaciones>();
            this.Encomienda_Ubicaciones = new HashSet<Encomienda_Ubicaciones>();
            this.Transf_Ubicaciones = new HashSet<Transf_Ubicaciones>();
            this.Ubicaciones = new HashSet<Ubicaciones>();
            this.SolicitudPartida = new HashSet<SolicitudPartida>();
            this.Ubicaciones_Historial_Cambios = new HashSet<Ubicaciones_Historial_Cambios>();
        }
    
        public int id_subtipoubicacion { get; set; }
        public string descripcion_subtipoubicacion { get; set; }
        public int id_tipoubicacion { get; set; }
        public bool habilitado { get; set; }
    
        public virtual TiposDeUbicacion TiposDeUbicacion { get; set; }
        public virtual ICollection<EncomiendaExt_Ubicaciones> EncomiendaExt_Ubicaciones { get; set; }
        public virtual ICollection<SSIT_Solicitudes_Ubicaciones> SSIT_Solicitudes_Ubicaciones { get; set; }
        public virtual ICollection<CPadron_Ubicaciones> CPadron_Ubicaciones { get; set; }
        public virtual ICollection<Encomienda_Ubicaciones> Encomienda_Ubicaciones { get; set; }
        public virtual ICollection<Transf_Ubicaciones> Transf_Ubicaciones { get; set; }
        public virtual ICollection<Ubicaciones> Ubicaciones { get; set; }
        public virtual ICollection<SolicitudPartida> SolicitudPartida { get; set; }
        public virtual ICollection<Ubicaciones_Historial_Cambios> Ubicaciones_Historial_Cambios { get; set; }
    }
}
