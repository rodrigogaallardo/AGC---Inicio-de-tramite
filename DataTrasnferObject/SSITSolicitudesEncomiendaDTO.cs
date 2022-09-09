using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesEncomiendaDTO
	{
        public int id_sol_enc { get; set; }
        public int id_solicitud { get; set; }
		public int id_encomienda { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
	}				
}


