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
    
    public partial class Categorias
    {
        public Categorias()
        {
            this.Rubros = new HashSet<Rubros>();
        }
    
        public int id_categoria { get; set; }
        public int cod_categoria { get; set; }
        public string nom_categoria { get; set; }
        public Nullable<int> cod_categoria_padre { get; set; }
    
        public virtual ICollection<Rubros> Rubros { get; set; }
    }
}
