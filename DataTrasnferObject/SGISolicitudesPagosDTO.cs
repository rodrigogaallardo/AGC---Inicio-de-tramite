using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SGISolicitudesPagosDTO
	{
		public int IdSolicitudPago { get; set; }
		public int IdTramiteTarea { get; set; }
		public int IdPago { get; set; }
		public int IdMedioPago { get; set; }
		public decimal MontoPago { get; set; }
		public string CodigoBarras { get; set; }
		public string NumeroBoletaUnica { get; set; }
		public int? NumeroDependencia { get; set; }
		public string CodigoVerificador { get; set; }
		public string UrlPago { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? UpdateUser { get; set; }
		public DateTime? UpdateDate { get; set; }
	}				
}


