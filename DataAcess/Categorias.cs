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
