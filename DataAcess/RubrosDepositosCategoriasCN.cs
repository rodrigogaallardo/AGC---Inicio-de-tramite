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
    
    public partial class RubrosDepositosCategoriasCN
    {
        public RubrosDepositosCategoriasCN()
        {
            this.RubrosDepositosCN = new HashSet<RubrosDepositosCN>();
        }
    
        public int IdCategoriaDeposito { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<RubrosDepositosCN> RubrosDepositosCN { get; set; }
    }
}
