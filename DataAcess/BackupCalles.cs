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
    
    public partial class BackupCalles
    {
        public int id_calle { get; set; }
        public int Codigo_calle { get; set; }
        public string NombreOficial_calle { get; set; }
        public Nullable<int> AlturaIzquierdaInicio_calle { get; set; }
        public Nullable<int> AlturaIzquierdaFin_calle { get; set; }
        public Nullable<int> AlturaDerechaInicio_calle { get; set; }
        public Nullable<int> AlturaDerechaFin_calle { get; set; }
        public string NombreAnterior_calle { get; set; }
        public string TipoCalle_calle { get; set; }
        public string Observaciones_calle { get; set; }
        public Nullable<int> Longitud_calle { get; set; }
        public string NombreGeografico_calle { get; set; }
        public string CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public System.DateTime FechaBackup { get; set; }
    }
}
