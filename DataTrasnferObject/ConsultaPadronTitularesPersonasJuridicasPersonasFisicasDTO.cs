using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO
	{
		public int IdTitularPersonaJuridica { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocumentoPersonal { get; set; }
		public string Email { get; set; }
		public bool FirmanteMismaPersona { get; set; }
		public string NumeroDocumento { get; set; }
	}				
}


