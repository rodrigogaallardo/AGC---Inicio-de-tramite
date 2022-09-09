using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesUbicacionesPropiedadHorizontalDTO
	{
		public int IdSolicitudPropiedadHorizontal { get; set; }
		public int? IdSolicitudUbicacion { get; set; }
		public int? IdPropiedadHorizontal { get; set; }


        public virtual UbicacionesPropiedadhorizontalDTO UbicacionesPropiedadhorizontalDTO { get; set; }
	}				
}


