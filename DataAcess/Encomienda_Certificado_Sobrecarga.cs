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
    
    public partial class Encomienda_Certificado_Sobrecarga
    {
        public Encomienda_Certificado_Sobrecarga()
        {
            this.Encomienda_Sobrecarga_Detalle1 = new HashSet<Encomienda_Sobrecarga_Detalle1>();
        }
    
        public int id_sobrecarga { get; set; }
        public int id_encomienda_datoslocal { get; set; }
        public int id_tipo_certificado { get; set; }
        public int id_tipo_sobrecarga { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Encomienda_Tipos_Certificados_Sobrecarga Encomienda_Tipos_Certificados_Sobrecarga { get; set; }
        public virtual Encomienda_Tipos_Sobrecargas Encomienda_Tipos_Sobrecargas { get; set; }
        public virtual ICollection<Encomienda_Sobrecarga_Detalle1> Encomienda_Sobrecarga_Detalle1 { get; set; }
        public virtual Encomienda_DatosLocal Encomienda_DatosLocal { get; set; }
    }
}