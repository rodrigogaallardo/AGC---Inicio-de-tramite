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
    
    public partial class CPadron_Ubicaciones_PropiedadHorizontal
    {
        public int id_cpadronprophorizontal { get; set; }
        public Nullable<int> id_cpadronubicacion { get; set; }
        public Nullable<int> id_propiedadhorizontal { get; set; }
    
        public virtual CPadron_Ubicaciones CPadron_Ubicaciones { get; set; }
        public virtual Ubicaciones_PropiedadHorizontal Ubicaciones_PropiedadHorizontal { get; set; }
    }
}