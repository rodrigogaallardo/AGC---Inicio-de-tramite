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
    
    public partial class SSIT_Solicitudes_Ubicaciones_Mixturas
    {
        public int id_solicitudubicacionmixtura { get; set; }
        public int id_solicitudubicacion { get; set; }
        public int IdZonaMixtura { get; set; }
    
        public virtual SSIT_Solicitudes_Ubicaciones SSIT_Solicitudes_Ubicaciones { get; set; }
        public virtual Ubicaciones_ZonasMixtura Ubicaciones_ZonasMixtura { get; set; }
    }
}