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
