using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasRubrosDTO
	{
		public int IdTransferenciaRubro { get; set; }
		public int IdSolicitud { get; set; }
		public string CodidoRubro { get; set; }
		public string DescripcionRubro { get; set; }
		public bool EsAnterior { get; set; }
		public int IdTipoActividad { get; set; }
		public int IdTipoDocumentoReq { get; set; }
		public decimal SuperficieHabilitar { get; set; }
		public int? IdImpactoAmbiental { get; set; }
		public DateTime CreateDate { get; set; }

        public string TipoActividadNombre { get; set; }
        public string TipoDocumentoDescripcion { get; set; }
        public double? LocalVenta { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get;set; }
        public string DocRequerida { get; set; }

        public TipoActividadDTO TipoActividad { get; set; }
        public ImpactoAmbientalDTO ImpactoAmbiental { get;set;}
        public TipoDocumentacionRequeridaDTO TipoDocumentacionRequerida { get; set; }
	}				
}


