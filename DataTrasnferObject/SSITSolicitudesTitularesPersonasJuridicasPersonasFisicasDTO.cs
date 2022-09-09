using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO
	{
		public int IdTitularPj { get; set; }
		public int IdSolicitud { get; set; }
		public int IdPersonaJuridica { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipoDocPersonal { get; set; }
		public string NroDocumento { get; set; }
		public string Email { get; set; }
		public int IdFirmantePj { get; set; }
		public bool FirmanteMismaPersona { get; set; }
        public string Cuit { get; set; }

        //public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
        //public virtual SSITSolicitudesFirmantesPersonasJuridicasDTO SSITSolicitudesFirmantesPersonasJuridicasDTO { get; set; }
        //public virtual SSITSolicitudesTitularesPersonasJuridicasDTO SSITSolicitudesTitularesPersonasJuridicasDTO { get; set; }
    }				
}


