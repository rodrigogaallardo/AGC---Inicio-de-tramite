using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasDocumentosAdjuntosDTO
	{
		public int Id { get; set; }
		public int IdSolicitud { get; set; }
		public int IdTipoDocumentoRequerido { get; set; }
		public string TipoDocumentoRequeridoDetalle { get; set; }
		public int IdTipoDocsis { get; set; }
		public int IdFile { get; set; }
		public bool GeneradoxSistema { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? UpdateDate { get; set; }
		public Guid? UpdateUser { get; set; }
		public string NombreArchivo { get; set; }
		public int IdAgrupamiento { get; set; }

        public byte[] Documento { get; set; }

        public string url { get; set; }
        public TiposDeDocumentosRequeridosDTO TipoDocumentoRequerido { get; set; }
    }				
}


