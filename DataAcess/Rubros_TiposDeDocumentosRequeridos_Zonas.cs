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
    
    public partial class Rubros_TiposDeDocumentosRequeridos_Zonas
    {
        public int id_rubtdocreqzona { get; set; }
        public int id_rubro { get; set; }
        public int id_tdocreq { get; set; }
        public bool obligatorio_rubtdocreq { get; set; }
        public string codZonaHab { get; set; }
        public System.Guid CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Zonas_Habilitaciones Zonas_Habilitaciones { get; set; }
        public virtual TiposDeDocumentosRequeridos TiposDeDocumentosRequeridos { get; set; }
        public virtual Rubros Rubros { get; set; }
    }
}
