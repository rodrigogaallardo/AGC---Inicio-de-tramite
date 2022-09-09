using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaDocumentosAdjuntosDTO
    {
		public int id_docadjunto { get; set; }
		public int id_encomienda { get; set; }
		public int id_tdocreq { get; set; }
		public int id_tipodocsis { get; set; }
        public int id_file { get; set; }
        public bool generadoxSistema { get; set; }
        public string nombre_archivo { get; set; }
        public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
        public string url { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<Guid> UpdateUser { get; set; }
        public DateTime? fechaPresentado { get; set; }

        public virtual TiposDeDocumentosSistemaDTO TiposDeDocumentosSistemaDTO { get; set; }
        public virtual TiposDeDocumentosRequeridosDTO TiposDeDocumentosRequeridosDTO { get; set; }
	}				
}


