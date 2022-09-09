using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasPlantasDTO
	{
		public int IdTransferenciaTipoSector { get; set; }
		public int IdSolicitud { get; set; }
		public int IdTipoSector { get; set; }
		public string DetalleTransferenciaTipoSector { get; set; }

        public bool Seleccionado { get; set; }

        public TipoSectorDTO TipoSector { get; set; }
	}				
}


