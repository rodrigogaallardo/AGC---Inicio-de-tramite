using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronUbicacionesDTO
	{
		public int IdConsultaPadronUbicacion { get; set; }
		public int? IdConsultaPadron { get; set; }
		public int? IdUbicacion { get; set; }
		public int? IdSubTipoUbicacion { get; set; }
		public string LocalSubTipoUbicacion { get; set; }
		public string DeptoLocalConsultaPadronUbicacion { get; set; }
		public int IdZonaPlaneamiento { get; set; }
		public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public string Depto { get; set; }
        public string Local { get; set; }
        public string Torre { get; set; }

        public ICollection<ConsultaPadronUbicacionPropiedadHorizontalDTO> PropiedadesHorizontales { get; set; }
        public ICollection<ConsultaPadronUbicacionesPuertasDTO> Puertas { get; set; }

        public UbicacionesDTO Ubicacion { get; set; }
        public ZonasPlaneamientoDTO ZonaPlaneamiento { get; set; }
        public SubTipoUbicacionesDTO SubTipoUbicacion { get; set; }
	}				
}


