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
    
    public partial class Rel_Usuarios_GrupoConsejo
    {
        public System.Guid userid { get; set; }
        public int id_grupoconsejo { get; set; }
        public int id_rel_usugrucon { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual GrupoConsejos GrupoConsejos { get; set; }
    }
}
