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
    
    public partial class Cargos
    {
        public Cargos()
        {
            this.SSIT_Solicitudes_Firmantes_PersonasFisicas = new HashSet<SSIT_Solicitudes_Firmantes_PersonasFisicas>();
            this.SSIT_Solicitudes_Firmantes_PersonasJuridicas = new HashSet<SSIT_Solicitudes_Firmantes_PersonasJuridicas>();
            this.Transf_Firmantes_PersonasFisicas = new HashSet<Transf_Firmantes_PersonasFisicas>();
            this.Transf_Firmantes_PersonasJuridicas = new HashSet<Transf_Firmantes_PersonasJuridicas>();
        }
    
        public int id_cargo { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<SSIT_Solicitudes_Firmantes_PersonasFisicas> SSIT_Solicitudes_Firmantes_PersonasFisicas { get; set; }
        public virtual ICollection<SSIT_Solicitudes_Firmantes_PersonasJuridicas> SSIT_Solicitudes_Firmantes_PersonasJuridicas { get; set; }
        public virtual ICollection<Transf_Firmantes_PersonasFisicas> Transf_Firmantes_PersonasFisicas { get; set; }
        public virtual ICollection<Transf_Firmantes_PersonasJuridicas> Transf_Firmantes_PersonasJuridicas { get; set; }
    }
}
