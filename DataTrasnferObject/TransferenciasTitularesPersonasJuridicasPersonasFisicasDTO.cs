using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO
	{
		public int IdTitularPersonaJuridica { get; set; }
		public int IdSolicitud { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocumentoPersonal { get; set; }
		public string NumeroDocumento { get; set; }
		public string Email { get; set; }
		public int IdFirmantePersonaJuridica { get; set; }
		public bool FirmanteMismaPersona { get; set; }
	}				
}


