//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transf_DocumentosAdjuntos
    {
        public int id_docadjunto { get; set; }
        public int id_solicitud { get; set; }
        public int id_tdocreq { get; set; }
        public string tdocreq_detalle { get; set; }
        public int id_tipodocsis { get; set; }
        public int id_file { get; set; }
        public bool generadoxSistema { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.Guid> UpdateUser { get; set; }
        public string nombre_archivo { get; set; }
        public int id_agrupamiento { get; set; }
    
        public virtual TiposDeDocumentosRequeridos TiposDeDocumentosRequeridos { get; set; }
        public virtual TiposDeDocumentosSistema TiposDeDocumentosSistema { get; set; }
        public virtual Transf_Solicitudes Transf_Solicitudes { get; set; }
        public virtual Niveles_Agrupamiento Niveles_Agrupamiento { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
