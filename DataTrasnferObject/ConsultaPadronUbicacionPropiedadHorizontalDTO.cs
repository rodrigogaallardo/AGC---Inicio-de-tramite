using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronUbicacionPropiedadHorizontalDTO
	{
		public int IdConsultaPadronPropiedadHorizontal { get; set; }
		public int? IdConsultaPadronUbicacion { get; set; }
		public int? IdPropiedadHorizontal { get; set; }
        public UbicacionesPropiedadhorizontalDTO UbicacionPropiedadaHorizontal { get; set; }
	}

    public class ConsultaPadronUbicacionesPropiedadHorizontalModelDTO
    {
        public int IdConsultaPadronUbicacionPropiedadHorizontal { get; set; }
        public int? IdConsultaPadronUbicacion { get; set; }
        public int? NroPartidaHorizontal { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
    }
}


