using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesFirmantesPersonasFisicasDTO
	{
		public int IdFirmantePf { get; set; }
		public int IdSolicitud { get; set; }
		public int IdPersonaFisica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocPersonal { get; set; }
		public string NroDocumento { get; set; }
		public int IdTipoCaracter { get; set; }
		public string Email { get; set; }
        public string Cuit { get; set; }


        //public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
        //public virtual TiposDeCaracterLegalDTO TiposDeCaracterLegalDTO { get; set; }
        //public virtual SSITSolicitudesTitularesPersonasFisicasDTO SSITSolicitudesTitularesPersonasFisicasDTO { get; set; }
    }				
}


