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
    
    public partial class Encomienda_Tipos_Certificados_Sobrecarga
    {
        public Encomienda_Tipos_Certificados_Sobrecarga()
        {
            this.Encomienda_Certificado_Sobrecarga = new HashSet<Encomienda_Certificado_Sobrecarga>();
        }
    
        public int id_tipo_certificado { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<Encomienda_Certificado_Sobrecarga> Encomienda_Certificado_Sobrecarga { get; set; }
    }
}
