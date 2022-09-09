using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaRubrosCNDTO
    {
		public int IdEncomiendaRubro { get; set; }
		public int IdEncomienda { get; set; }
        public int IdRubro { get; set; }
        public string CodigoRubro { get; set; }
		public string DescripcionRubro { get; set; }
		public int IdTipoActividad { get; set; }
		public int IdTipoExpediente { get; set; }
		public decimal SuperficieHabilitar { get; set; }		
		public DateTime CreateDate { get; set; }
        public Nullable<Guid> CreateUser { get; set; }
        public bool? EsAnterior { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public string TipoActividadNombre { get; set; }
        public int idImpactoAmbiental { get; set; }

        public virtual TipoActividadDTO TipoActividadDTO { get; set; }
        public virtual RubrosCNDTO RubrosDTO { get; set; }
        public virtual ImpactoAmbientalDTO ImpactoAmbientalDTO { get; set; }
        public virtual ICollection<EncomiendaRubrosCNSubrubrosDTO> encomiendaRubrosCNSubrubrosDTO { get; set; }
    }
}


