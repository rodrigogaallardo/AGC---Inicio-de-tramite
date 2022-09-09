using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class UbicacionesInhibicionesDTO
	{
		public int IdUbicacionInhibicion { get; set; }
		public int IdUbicacion { get; set; }
		public string Motivo { get; set; }
		public DateTime FechaInhibicion { get; set; }
		public DateTime? FechaVencimiento { get; set; }
		public string Resultado { get; set; }
		public string Observaciones { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
	}				
}


