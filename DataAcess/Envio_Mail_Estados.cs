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
    
    public partial class Envio_Mail_Estados
    {
        public Envio_Mail_Estados()
        {
            this.Envio_Mail = new HashSet<Envio_Mail>();
        }
    
        public int id_estado { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<Envio_Mail> Envio_Mail { get; set; }
    }
}
