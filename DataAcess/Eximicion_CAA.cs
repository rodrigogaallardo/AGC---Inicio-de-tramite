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
    
    public partial class Eximicion_CAA
    {
        public int id_eximicion_caa { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int id_solicitud { get; set; }
        public int id_tipo_tramite { get; set; }
        public bool eximido { get; set; }
        public System.Guid CreateUser { get; set; }
    
        public virtual SSIT_Solicitudes SSIT_Solicitudes { get; set; }
    }
}
