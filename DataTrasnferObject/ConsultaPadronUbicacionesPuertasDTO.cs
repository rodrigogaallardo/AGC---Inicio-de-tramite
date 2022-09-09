using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronUbicacionesPuertasDTO
	{
		public int IdConsultaPadronPuerta { get; set; }
		public int IdConsultaPadronUbicacion { get; set; }
		public int CodigoCalle { get; set; }
		public string NombreCalle { get; set; }
		public int NumeroPuerta { get; set; }
	}
}


