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
    
    public partial class GrupoConsejos
    {
        public GrupoConsejos()
        {
            this.ConsejoProfesional = new HashSet<ConsejoProfesional>();
            this.ConsejoProfesional_RolesPermitidos = new HashSet<ConsejoProfesional_RolesPermitidos>();
            this.Rel_Usuarios_GrupoConsejo = new HashSet<Rel_Usuarios_GrupoConsejo>();
        }
    
        public int id_grupoconsejo { get; set; }
        public string nombre_grupoconsejo { get; set; }
        public string descripcion_grupoconsejo { get; set; }
        public string logo_impresion_grupoconsejo { get; set; }
        public string logo_pantalla_grupoconsejo { get; set; }
    
        public virtual ICollection<ConsejoProfesional> ConsejoProfesional { get; set; }
        public virtual ICollection<ConsejoProfesional_RolesPermitidos> ConsejoProfesional_RolesPermitidos { get; set; }
        public virtual ICollection<Rel_Usuarios_GrupoConsejo> Rel_Usuarios_GrupoConsejo { get; set; }
    }
}
