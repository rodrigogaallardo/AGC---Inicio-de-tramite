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
    
    public partial class TiposDePlanos
    {
        public TiposDePlanos()
        {
            this.Encomienda_Planos = new HashSet<Encomienda_Planos>();
        }
    
        public int id_tipo_plano { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Nullable<bool> requiere_detalle { get; set; }
        public string extension { get; set; }
        public Nullable<int> tamanio_max_mb { get; set; }
        public string acronimo_SADE { get; set; }
    
        public virtual ICollection<Encomienda_Planos> Encomienda_Planos { get; set; }
    }
}
