using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaUbicacionesDTO
	{
		public int IdEncomiendaUbicacion { get; set; }
		public int? IdEncomienda { get; set; }
		public int? IdUbicacion { get; set; }
		public int? IdSubtipoUbicacion { get; set; }
		public string LocalSubtipoUbicacion { get; set; }
		public string DeptoLocalEncomiendaUbicacion { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
        public int IdZonaPlaneamiento { get; set; }
        public string Depto { get; set; }
        public string Local { get; set; }
        public string Torre { get; set; }
        public decimal AnchoCalle { get; set; }
        public bool InmuebleCatalogado { get; set; }

        public ICollection<UbicacionesPropiedadhorizontalDTO> PropiedadesHorizontales { get; set; }
        public ICollection<UbicacionesPuertasDTO> Puertas { get; set; }


        public virtual ICollection<EncomiendaUbicacionesPropiedadHorizontalDTO> EncomiendaUbicacionesPropiedadHorizontalDTO { get; set; }
        public virtual ICollection<EncomiendaUbicacionesPuertasDTO> EncomiendaUbicacionesPuertasDTO { get; set; }
        public virtual SubTipoUbicacionesDTO SubTipoUbicacionesDTO { get; set; }
        public virtual ZonasPlaneamientoDTO ZonasPlaneamientoDTO { get; set; }
        public UbicacionesDTO Ubicacion { get; set; }
        public virtual ICollection<Encomienda_Ubicaciones_DistritosDTO> EncomiendaUbicacionesDistritosDTO { get; set; }
        public virtual ICollection<Encomienda_Ubicaciones_MixturasDTO> EncomiendaUbicacionesMixturasDTO { get; set; }
    }				
}



