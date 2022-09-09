using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronTitularesSolicitudPersonasJuridicasDTO
	{
		public int IdPersonaJuridica { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdTipoSociedad { get; set; }
		public string RazonSocial { get; set; }
		public string CUIT { get; set; }
		public int IdTipoiibb { get; set; }
		public string Numeroiibb { get; set; }
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

        public List<TitularesSHDTO> titularesSH { get; set; }
	}				
}


