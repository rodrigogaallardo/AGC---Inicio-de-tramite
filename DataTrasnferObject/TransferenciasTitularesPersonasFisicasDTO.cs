using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasTitularesPersonasFisicasDTO
	{
		public int IdPersonaFisica { get; set; }
		public int IdSolicitud { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public int IdTipodocPersonal { get; set; }
		public string NumeroDocumento { get; set; }
		public string Cuit { get; set; }
		public int IdTipoiibb { get; set; }
		public string IngresosBrutos { get; set; }
		public string Calle { get; set; }
		public int NumeroPuerta { get; set; }
		public string Piso { get; set; }
		public string Depto { get; set; }
		public int IdLocalidad { get; set; }
		public string CodigoPostal { get; set; }
		public string Telefono { get; set; }
		public string Celular { get; set; }
		public string Email { get; set; }
		public bool MismoFirmante { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
        public string Torre { get; set; }

        public TransferenciasFirmantesPersonasFisicasDTO DtoFirmantes { get; set; }
        public ICollection<TransferenciaFirmantePersonasFisicasDTO> Firmantes { get; set; }

	}				
}


