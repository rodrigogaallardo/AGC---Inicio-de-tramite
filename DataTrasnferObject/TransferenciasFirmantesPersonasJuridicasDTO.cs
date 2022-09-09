using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasFirmantesPersonasJuridicasDTO
	{
		public int IdFirmantePersonaJuridica { get; set; }
		public int IdSolicitud { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocumentoPersonal { get; set; }
		public string NumeroDocumento { get; set; }
		public string Email { get; set; }
		public int IdTipoCaracter { get; set; }
		public string CargoFirmantePersonaJuridica { get; set; }
	}				
}


