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
    
    public partial class TiposDeUbicacion
    {
        public TiposDeUbicacion()
        {
            this.SubTiposDeUbicacion = new HashSet<SubTiposDeUbicacion>();
        }
    
        public int id_tipoubicacion { get; set; }
        public string descripcion_tipoubicacion { get; set; }
        public Nullable<bool> RequiereSMP { get; set; }
    
        public virtual ICollection<SubTiposDeUbicacion> SubTiposDeUbicacion { get; set; }
    }
}
