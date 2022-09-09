using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronPlantasDTO
	{
		public int IdConsultaPadronTipoSector { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdTipoSector { get; set; }
		public string DetalleConsultaPadronTipoSector { get; set; }

        public bool Seleccionado { get; set; }

        public TipoSectorDTO TipoSector { get; set; }
	}				
}


