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
    
    public partial class ASR_Rel_Empresas_Profesionales
    {
        public int id_rel_emp_usu { get; set; }
        public int id_empasc { get; set; }
        public int id_profesional { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public int id_estado_aceptacion { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Profesional Profesional { get; set; }
    }
}
