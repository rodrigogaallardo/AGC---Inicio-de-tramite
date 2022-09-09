using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronNormativasDTO
	{
		public int IdConsultaPadronTipoNormativa { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdTipoNormativa { get; set; }
		public int IdEntidadNormativa { get; set; }
		public string NumeroNormativa { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }

        public EntidadNormativaDTO EntidadNormativa { get; set; }
        public TipoNormativaDTO TipoNormativa { get; set; }
	}
}


