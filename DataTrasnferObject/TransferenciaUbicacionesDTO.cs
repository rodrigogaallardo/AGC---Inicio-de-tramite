using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciaUbicacionesDTO : SSITSolicitudesUbicacionBaseDTO
    {
		public int IdTransferenciaUbicacion { get; set; }
        public int? IdSolicitud { get; set; }
		public int? IdSubTipoUbicacion { get; set; }
		public string LocalSubTipoUbicacion { get; set; }
		public string DeptoLocalTransferenciaUbicacion { get; set; }
		public int IdZonaPlaneamiento { get; set; }
		public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public string Depto { get; set; }
        public string Local { get; set; }
        public string Torre { get; set; }

        public ICollection<TransferenciasUbicacionPropiedadHorizontalDTO> PropiedadesHorizontales { get; set; }
        public ICollection<TransferenciasUbicacionesPuertasDTO> Puertas { get; set; }

        public UbicacionesDTO Ubicacion { get; set; }
        public ZonasPlaneamientoDTO ZonaPlaneamiento { get; set; }
        public SubTipoUbicacionesDTO SubTipoUbicacion { get; set; }

        public virtual ICollection<TransferenciaUbicacionesDistritosDTO> TransferenciaUbicacionesDistritosDTO { get; set; }
        public virtual ICollection<TransferenciaUbicacionesMixturasDTO> TransferenciaUbicacionesMixturasDTO { get; set; }
    }				
}


