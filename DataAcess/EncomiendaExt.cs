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
    
    public partial class EncomiendaExt
    {
        public EncomiendaExt()
        {
            this.EncomiendaExt_Empresas = new HashSet<EncomiendaExt_Empresas>();
            this.EncomiendaExt_Titulares_PersonasFisicas = new HashSet<EncomiendaExt_Titulares_PersonasFisicas>();
            this.EncomiendaExt_Titulares_PersonasJuridicas = new HashSet<EncomiendaExt_Titulares_PersonasJuridicas>();
            this.EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas = new HashSet<EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas>();
            this.EncomiendaExt_Ubicaciones = new HashSet<EncomiendaExt_Ubicaciones>();
        }
    
        public int id_encomienda { get; set; }
        public int nroTramite { get; set; }
        public int nroEncomiendaconsejo { get; set; }
        public System.DateTime FechaEncomienda { get; set; }
        public int id_consejo { get; set; }
        public int id_profesional { get; set; }
        public string CodigoSeguridad { get; set; }
        public int TipoTramite { get; set; }
        public int id_estado { get; set; }
        public bool Bloqueada { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public string NroDGROC { get; set; }
        public string MotivoRechazo { get; set; }
        public Nullable<int> id_file { get; set; }
    
        public virtual ConsejoProfesional ConsejoProfesional { get; set; }
        public virtual ICollection<EncomiendaExt_Empresas> EncomiendaExt_Empresas { get; set; }
        public virtual EncomiendaExt_Estados EncomiendaExt_Estados { get; set; }
        public virtual ICollection<EncomiendaExt_Titulares_PersonasFisicas> EncomiendaExt_Titulares_PersonasFisicas { get; set; }
        public virtual ICollection<EncomiendaExt_Titulares_PersonasJuridicas> EncomiendaExt_Titulares_PersonasJuridicas { get; set; }
        public virtual ICollection<EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas> EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public virtual ICollection<EncomiendaExt_Ubicaciones> EncomiendaExt_Ubicaciones { get; set; }
        public virtual Profesional Profesional { get; set; }
    }
}