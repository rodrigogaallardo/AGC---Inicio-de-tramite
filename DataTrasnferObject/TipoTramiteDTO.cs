using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TipoTramiteDTO
	{
		public int IdTipoTramite { get; set; }
		public string CodTipoTramite { get; set; }
		public string DescripcionTipoTramite { get; set; }
		public string CodTipoTramiteWs { get; set; }
        public bool Habilitado_SSIT { get; set; }
        public int Orden { get; set; }
	}				
}


