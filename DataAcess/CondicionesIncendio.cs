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
    
    public partial class CondicionesIncendio
    {
        public CondicionesIncendio()
        {
            this.RubrosDepositosCN = new HashSet<RubrosDepositosCN>();
            this.RubrosCN = new HashSet<RubrosCN>();
        }
    
        public int idCondicionIncendio { get; set; }
        public string codigo { get; set; }
        public Nullable<decimal> superficie { get; set; }
        public Nullable<decimal> superficieSubsuelo { get; set; }
    
        public virtual ICollection<RubrosDepositosCN> RubrosDepositosCN { get; set; }
        public virtual ICollection<RubrosCN> RubrosCN { get; set; }
    }
}
