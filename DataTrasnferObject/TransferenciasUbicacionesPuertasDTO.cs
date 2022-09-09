using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasUbicacionesPuertasDTO
    {
		public int IdTranferenciaPuerta { get; set; }
		public int IdTranferenciaUbicacion { get; set; }
		public int CodigoCalle { get; set; }
		public string NombreCalle { get; set; }
		public int NumeroPuerta { get; set; }
	}
}


