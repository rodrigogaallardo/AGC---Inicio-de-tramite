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
    
    public partial class Zonas_Habilitaciones
    {
        public Zonas_Habilitaciones()
        {
            this.Rel_ZonasPlaneamiento_ZonasHabilitaciones = new HashSet<Rel_ZonasPlaneamiento_ZonasHabilitaciones>();
            this.RubrosZonasCondiciones = new HashSet<RubrosZonasCondiciones>();
            this.Rubros_CircuitoAtomatico_Zonas = new HashSet<Rubros_CircuitoAtomatico_Zonas>();
            this.Rubros_TiposDeDocumentosRequeridos_Zonas = new HashSet<Rubros_TiposDeDocumentosRequeridos_Zonas>();
        }
    
        public int id_zonahabilitaciones { get; set; }
        public string CodZonaHab { get; set; }
        public string DescripcionZonaHab { get; set; }
        public string CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual ICollection<Rel_ZonasPlaneamiento_ZonasHabilitaciones> Rel_ZonasPlaneamiento_ZonasHabilitaciones { get; set; }
        public virtual ICollection<RubrosZonasCondiciones> RubrosZonasCondiciones { get; set; }
        public virtual ICollection<Rubros_CircuitoAtomatico_Zonas> Rubros_CircuitoAtomatico_Zonas { get; set; }
        public virtual ICollection<Rubros_TiposDeDocumentosRequeridos_Zonas> Rubros_TiposDeDocumentosRequeridos_Zonas { get; set; }
    }
}
