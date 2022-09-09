using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITDocumentosAdjuntosDTO
    {
        public int id_docadjunto { get; set; }
        public int id_solicitud { get; set; }
		public int id_tdocreq { get; set; }
        public string tdocreq_detalle { get; set; }
        public int id_tipodocsis { get; set; }
		public int id_file { get; set; }
        public bool generadoxSistema { get; set; }
        public DateTime CreateDate{ get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateUser { get; set; }
        public string nombre_archivo { get; set; }
        public string nombre_tdocreq { get; set; }
        public DateTime? fechaPresentado { get; set; }

        public string url { get; set; }

        public virtual TiposDeDocumentosSistemaDTO TiposDeDocumentosSistemaDTO { get; set; }
        public virtual TiposDeDocumentosRequeridosDTO TiposDeDocumentosRequeridosDTO { get; set; }
    }
}


