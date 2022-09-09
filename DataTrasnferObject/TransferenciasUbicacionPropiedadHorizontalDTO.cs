using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasUbicacionPropiedadHorizontalDTO
	{
		public int IdTranferenciaPropiedadHorizontal { get; set; }
		public int? IdTranferenciaUbicacion { get; set; }
		public int? IdPropiedadHorizontal { get; set; }
        public UbicacionesPropiedadhorizontalDTO UbicacionPropiedadaHorizontal { get; set; }
	}

    public class TransferenciasUbicacionesPropiedadHorizontalModelDTO
    {
        public int IdTranferenciaUbicacionPropiedadHorizontal { get; set; }
        public int? IdTranferenciaUbicacion { get; set; }
        public int? NroPartidaHorizontal { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
    }
}


