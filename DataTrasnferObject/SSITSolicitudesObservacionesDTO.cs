using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesObservacionesDTO
	{
        public int id_solobs { get; set; }
        public int id_solicitud { get; set; }
		public string observaciones { get; set; }
        public bool leido { get; set; }
        public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
        public string userApeNom { get; set; }
        public string clase_leido
        {
            get
            {
                if (leido) return "imoon-ok";
                return "imoon-remove";
            }
        }
    }
}


