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
    
    public partial class Documento
    {
        public Documento()
        {
            this.SolicitudDocumento = new HashSet<SolicitudDocumento>();
        }
    
        public int Id { get; set; }
        public int IdTipoVerificacion { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public bool Requerido { get; set; }
    
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual TipoVerificacion TipoVerificacion { get; set; }
        public virtual ICollection<SolicitudDocumento> SolicitudDocumento { get; set; }
    }
}
