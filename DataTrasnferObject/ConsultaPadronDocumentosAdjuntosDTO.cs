using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronDocumentosAdjuntosDTO
	{
		public int Id { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdTipodocumentoRequerido { get; set; }
		public string TipodocumentoRequeridoDetalle { get; set; }
		public int IdTipoDocumentoSistema { get; set; }
		public int IdFile { get; set; }
		public bool GeneradoxSistema { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? UpdateDate { get; set; }
		public Guid? UpdateUser { get; set; }
		public string NombreArchivo { get; set; }

        //public string Tipo { get; set; }

        public TiposDeDocumentosRequeridosDTO TiposDeDocumentosRequeridos { get; set; }
        public TiposDeDocumentosSistemaDTO TiposDeDocumentosSistema { get; set; }
    }				
}


