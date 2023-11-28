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
    
    public partial class Cursos
    {
        public Cursos()
        {
            this.Cursos_Excepciones_Usuarios = new HashSet<Cursos_Excepciones_Usuarios>();
            this.Cursos_Fechas = new HashSet<Cursos_Fechas>();
            this.aspnet_Roles = new HashSet<aspnet_Roles>();
        }
    
        public int id_curso { get; set; }
        public string nombre_curso { get; set; }
        public string descripcion_curso { get; set; }
        public string emailcuerpo_curso { get; set; }
        public string concideraciones_curso { get; set; }
        public Nullable<bool> activo_curso { get; set; }
        public Nullable<bool> bajalogica_curso { get; set; }
        public Nullable<System.DateTime> fechadesde_curso { get; set; }
        public Nullable<System.DateTime> fechahasta_curso { get; set; }
        public Nullable<System.Guid> CreacionUsuario { get; set; }
        public Nullable<System.DateTime> CreacionFecha { get; set; }
        public Nullable<System.Guid> UltimaActualizacionUsuario { get; set; }
        public Nullable<System.DateTime> UltimaActualizacionFecha { get; set; }
    
        public virtual ICollection<Cursos_Excepciones_Usuarios> Cursos_Excepciones_Usuarios { get; set; }
        public virtual ICollection<Cursos_Fechas> Cursos_Fechas { get; set; }
        public virtual ICollection<aspnet_Roles> aspnet_Roles { get; set; }
    }
}
