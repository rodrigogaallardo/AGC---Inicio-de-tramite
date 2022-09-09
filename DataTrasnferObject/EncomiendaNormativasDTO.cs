using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaNormativasDTO
	{
		public int IdEncomiendaNormativa { get; set; }
		public int IdEncomienda { get; set; }
		public int IdTipoNormativa { get; set; }
		public int IdEntidadNormativa { get; set; }
		public string NroNormativa { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }

        public virtual TipoNormativaDTO TipoNormativaDTO { get; set; }
        public virtual EntidadNormativaDTO EntidadNormativaDTO { get; set; }

	}				
}


