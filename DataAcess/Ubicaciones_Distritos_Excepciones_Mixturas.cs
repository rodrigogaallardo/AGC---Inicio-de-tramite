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
    
    public partial class Ubicaciones_Distritos_Excepciones_Mixturas
    {
        public int IdExcepcionMixtura { get; set; }
        public int IdDistrito { get; set; }
        public Nullable<int> IdZona { get; set; }
        public Nullable<int> IdSubZona { get; set; }
        public int IdZonaMixtura { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Ubicaciones_CatalogoDistritos Ubicaciones_CatalogoDistritos { get; set; }
        public virtual Ubicaciones_CatalogoDistritos_Subzonas Ubicaciones_CatalogoDistritos_Subzonas { get; set; }
        public virtual Ubicaciones_CatalogoDistritos_Zonas Ubicaciones_CatalogoDistritos_Zonas { get; set; }
        public virtual Ubicaciones_ZonasMixtura Ubicaciones_ZonasMixtura { get; set; }
    }
}
