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
    
    public partial class RULH_Log
    {
        public int id_log { get; set; }
        public string Sistema { get; set; }
        public string Tipo { get; set; }
        public int id_solicitud { get; set; }
        public System.DateTime fecha { get; set; }
        public string Mensaje { get; set; }
    }
}