using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaFirmantesPersonasJuridicasDTO
	{
		public int IdFirmantePj { get; set; }
		public int IdEncomienda { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocPersonal { get; set; }
		public string NroDocumento { get; set; }
		public int IdTipoCaracter { get; set; }
		public string CargoFirmantePj { get; set; }
		public string Email { get; set; }

        public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
        public virtual TiposDeCaracterLegalDTO TiposDeCaracterLegalDTO { get; set; }
        public virtual EncomiendaTitularesPersonasJuridicasDTO EncomiendaTitularesPersonasJuridicasDTO { get; set; }
        
	}				
}


