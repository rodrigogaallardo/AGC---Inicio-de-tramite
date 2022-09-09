using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasTitularesPersonasJuridicasDTO
	{
		public int IdPersonaJuridica { get; set; }
		public int IdSolicitud { get; set; }
		public int IdTipoSociedad { get; set; }
		public string RazonSocial { get; set; }
		public string Cuit { get; set; }
		public int IdTipoiibb { get; set; }
		public string NumeroIibb { get; set; }
		public string Calle { get; set; }
		public int? NumeroPuerta { get; set; }
		public string Piso { get; set; }
		public string Depto { get; set; }
		public int IdLocalidad { get; set; }
		public string CodigoPostal { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
        public string Torre { get; set; }

        public List<FirmantesSHDTO> firmantesSH {get;set;}
        public List<TitularesSHDTO> titularesSH {get;set;}
        public List<TransferenciasFirmantesPersonasJuridicasDTO> encFirDTO  {get;set;}
        public ICollection<TransferenciaFirmantePersonasJuridicasDTO> Firmantes { get; set; } 
	}				
}


