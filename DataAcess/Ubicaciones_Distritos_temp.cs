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
    
    public partial class Ubicaciones_Distritos_temp
    {
        public int id_ubicacion_temp { get; set; }
        public int IdDistrito { get; set; }
        public Nullable<int> IdZona { get; set; }
        public Nullable<int> IdSubZona { get; set; }
        public int Id { get; set; }
    
        public virtual Ubicaciones_CatalogoDistritos Ubicaciones_CatalogoDistritos { get; set; }
        public virtual Ubicaciones_temp Ubicaciones_temp { get; set; }
    }
}
