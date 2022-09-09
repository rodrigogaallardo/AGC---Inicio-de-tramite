using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesPagosDTO
	{
        public int id_sol_pago { get; set; }
        public int id_solicitud { get; set; }
		public int id_pago { get; set; }
		public decimal monto_pago { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}


