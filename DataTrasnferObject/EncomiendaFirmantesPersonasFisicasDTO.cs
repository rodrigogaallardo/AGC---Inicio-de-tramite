using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaFirmantesPersonasFisicasDTO
	{
		public int IdFirmantePf { get; set; }
		public int IdEncomienda { get; set; }
		public int IdPersonaFisica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipodocPersonal { get; set; }
		public string NroDocumento { get; set; }
		public int IdTipoCaracter { get; set; }
		public string Email { get; set; }

        public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
        public virtual TiposDeCaracterLegalDTO TiposDeCaracterLegalDTO { get; set; }
        public virtual EncomiendaTitularesPersonasFisicasDTO EncomiendaTitularesPersonasFisicasDTO { get; set; }
	}				
}


