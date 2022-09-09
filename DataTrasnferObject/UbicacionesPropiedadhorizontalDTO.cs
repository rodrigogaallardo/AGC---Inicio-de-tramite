using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class UbicacionesPropiedadhorizontalDTO
	{
		public int IdPropiedadHorizontal { get; set; }
		public int IdUbicacion { get; set; }
		public int? NroPartidaHorizontal { get; set; }
		public string Piso { get; set; }
		public string Depto { get; set; }
		public string UnidadFuncional { get; set; }
		public string Observaciones { get; set; }
		public DateTime VigenciaDesde { get; set; }
		public DateTime? VigenciaHasta { get; set; }
		public bool EsEntidadGubernamental { get; set; }

        public string DescripcionCompleta { get { 
        
        return (NroPartidaHorizontal.HasValue ?  this.NroPartidaHorizontal.Value.ToString() : "N/A") +
                                                                (Piso.Length > 0 ? " - Piso: " + Piso : "") +
                                                                (Depto.Length > 0 ? " - U.F.: " + Depto  : ""); 
        } }
	}				
}


