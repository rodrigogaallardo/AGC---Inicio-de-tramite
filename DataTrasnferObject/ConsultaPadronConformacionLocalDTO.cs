using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronConformacionLocalDTO
	{
		public int IdConsultaPadronConformacionLocal { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdDestino { get; set; }
		public decimal? Largo { get; set; }
		public decimal? Ancho { get; set; }
		public decimal? Alto { get; set; }
		public string Paredes { get; set; }
		public string Techos { get; set; }
		public string Pisos { get; set; }
		public string Frisos { get; set; }
		public string Observaciones { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? UpdateDate { get; set; }
		public Guid? UpdateUser { get; set; }
		public string Detalle { get; set; }
		public int? IdConsultaPadronTipoSector { get; set; }
		public int? IdVentilacion { get; set; }
		public int? IdIluminacion { get; set; }
		public decimal? Superficie { get; set; }
		public int IdTiposuperficie { get; set; }

	}				
}


