using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ZonasPlaneamientoDTO
	{
		public int IdZonaPlaneamiento { get; set; }
		public string CodZonaPla { get; set; }
		public string DescripcionZonaPla { get; set; }
		public string CreateUser { get; set; }
		public DateTime CreateDate { get; set; }

        public string DescripcionCompleta
        {
            get
            {
                return CodZonaPla + " - (Zona Parcela)";
            }
        }
	}				
}


