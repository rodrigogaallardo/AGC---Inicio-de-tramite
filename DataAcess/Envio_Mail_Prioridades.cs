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
    
    public partial class Envio_Mail_Prioridades
    {
        public int ID_Prioridad { get; set; }
        public System.TimeSpan Hora_Desde { get; set; }
        public System.TimeSpan Hora_Hasta { get; set; }
        public int Tiempo_Reenvio { get; set; }
        public string Obervacion { get; set; }
        public Nullable<System.DateTime> Ultima_Ejecucion { get; set; }
    }
}
