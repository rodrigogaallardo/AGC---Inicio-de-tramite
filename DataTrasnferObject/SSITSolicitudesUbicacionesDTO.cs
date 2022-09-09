using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesUbicacionesDTO : SSITSolicitudesUbicacionBaseDTO
	{
		public int IdSolicitudUbicacion { get; set; }
		public int? IdSolicitud { get; set; }
		public int? IdSubtipoUbicacion { get; set; }
		public string LocalSubtipoUbicacion { get; set; }
		public string DeptoLocalUbicacion { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
        public int IdZonaPlaneamiento { get; set; }
        public string Depto { get; set; }
        public string Local { get; set; }
        public string Torre { get; set; }

        public ICollection<UbicacionesPropiedadhorizontalDTO> PropiedadesHorizontales { get; set; }
        public ICollection<UbicacionesPuertasDTO> Puertas { get; set; }
        public virtual ICollection<SSITSolicitudesUbicacionesPropiedadHorizontalDTO> SSITSolicitudesUbicacionesPropiedadHorizontalDTO { get; set; }
        public virtual ICollection<SSITSolicitudesUbicacionesPuertasDTO> SSITSolicitudesUbicacionesPuertasDTO { get; set; }
        public virtual SubTipoUbicacionesDTO SubTipoUbicacionesDTO { get; set; }
        public virtual UbicacionesDTO UbicacionesDTO { get; set; }
        public virtual ZonasPlaneamientoDTO ZonasPlaneamientoDTO { get; set; }

        public virtual ICollection<SSITSolicitudesUbicacionesDistritoDTO> SSITSolicitudesUbicacionesDistritosDTO { get; set; }
        public virtual ICollection<SSITSolicitudesUbicacionesMixturasDTO> SSITSolicitudesUbicacionesMixturasDTO { get; set; }
    }				
}



