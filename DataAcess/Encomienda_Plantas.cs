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
    
    public partial class Encomienda_Plantas
    {
        public Encomienda_Plantas()
        {
            this.Encomienda_ConformacionLocal = new HashSet<Encomienda_ConformacionLocal>();
            this.Encomienda_Sobrecarga_Detalle1 = new HashSet<Encomienda_Sobrecarga_Detalle1>();
        }
    
        public int id_encomiendatiposector { get; set; }
        public int id_encomienda { get; set; }
        public int id_tiposector { get; set; }
        public string detalle_encomiendatiposector { get; set; }
    
        public virtual ICollection<Encomienda_ConformacionLocal> Encomienda_ConformacionLocal { get; set; }
        public virtual TipoSector TipoSector { get; set; }
        public virtual ICollection<Encomienda_Sobrecarga_Detalle1> Encomienda_Sobrecarga_Detalle1 { get; set; }
        public virtual Encomienda Encomienda { get; set; }
    }
}
