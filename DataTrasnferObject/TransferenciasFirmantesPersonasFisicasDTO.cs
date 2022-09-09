using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasFirmantesPersonasFisicasDTO
	{
		public int IdFirmantePersonaFisica { get; set; }
		public int IdSolicitud { get; set; }
		public int IdPersonaFisica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocumentoPersonal { get; set; }
		public string NumeroDocumento { get; set; }
		public int IdTipoCaracter { get; set; }
		public string Email { get; set; }       
    }				
}


