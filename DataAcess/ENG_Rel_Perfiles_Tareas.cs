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
    
    public partial class ENG_Rel_Perfiles_Tareas
    {
        public int id_perfiltarea { get; set; }
        public int id_perfil { get; set; }
        public int id_tarea { get; set; }
    
        public virtual ENG_Tareas ENG_Tareas { get; set; }
        public virtual SGI_Perfiles SGI_Perfiles { get; set; }
    }
}
