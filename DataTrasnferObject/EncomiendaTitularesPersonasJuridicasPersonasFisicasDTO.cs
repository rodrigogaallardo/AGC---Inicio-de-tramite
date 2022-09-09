using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO
	{
		public int IdTitularPj { get; set; }
		public int IdEncomienda { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocPersonal { get; set; }
		public string NroDocumento { get; set; }
		public string Email { get; set; }
		public int IdFirmantePj { get; set; }
		public bool FirmanteMismaPersona { get; set; }

        public virtual EncomiendaFirmantesPersonasJuridicasDTO EncomiendaFirmantesPersonasJuridicasDTO { get; set; }
        public virtual EncomiendaTitularesPersonasJuridicasDTO EncomiendaTitularesPersonasJuridicasDTO { get; set; }
        public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
	}				
}


