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
    
    public partial class Encomienda_bk
    {
        public int id_encomienda { get; set; }
        public System.DateTime FechaEncomienda { get; set; }
        public int nroEncomiendaconsejo { get; set; }
        public int id_consejo { get; set; }
        public int id_profesional { get; set; }
        public string ZonaDeclarada { get; set; }
        public int id_tipotramite { get; set; }
        public int id_tipoexpediente { get; set; }
        public int id_subtipoexpediente { get; set; }
        public int id_estado { get; set; }
        public string CodigoSeguridad { get; set; }
        public string Observaciones_plantas { get; set; }
        public string Observaciones_rubros { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public bool Pro_teatro { get; set; }
    }
}
