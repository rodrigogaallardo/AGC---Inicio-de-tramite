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
    
    public partial class Niveles_Agrupamiento
    {
        public Niveles_Agrupamiento()
        {
            this.Transf_DocumentosAdjuntos = new HashSet<Transf_DocumentosAdjuntos>();
        }
    
        public int id { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<Transf_DocumentosAdjuntos> Transf_DocumentosAdjuntos { get; set; }
    }
}
