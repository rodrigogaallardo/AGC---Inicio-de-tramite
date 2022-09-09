using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaRubrosDTO
	{
		public int IdEncomiendaRubro { get; set; }
		public int IdEncomienda { get; set; }
		public string CodigoRubro { get; set; }
		public string DescripcionRubro { get; set; }
		public bool? EsAnterior { get; set; }
		public int IdTipoActividad { get; set; }
		public int IdTipoDocumentoRequerido { get; set; }
		public decimal SuperficieHabilitar { get; set; }
		public int? IdImpactoAmbiental { get; set; }
		public DateTime CreateDate { get; set; }

        public string TipoActividadNombre { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public string TipoDocumentoDescripcion {get;set;}
        public double? LocalVenta { get; set; }


        public bool? TieneDeposito { get; set; }
        public Nullable<decimal> SupMinCargaDescarga { get; set; }
        public Nullable<decimal> SupMinCargaDescargaRefII { get; set; }
        public Nullable<decimal> SupMinCargaDescargaRefV { get; set; }
        public bool? OficinaComercial { get; set; }

        public virtual ImpactoAmbientalDTO ImpactoAmbientalDTO { get; set; }
        public virtual TipoDocumentacionRequeridaDTO TipoDocumentacionRequeridaDTO { get; set; }
        public virtual TipoActividadDTO TipoActividadDTO { get; set; }
        public virtual RubrosDTO RubrosDTO { get; set; }

	}
}


