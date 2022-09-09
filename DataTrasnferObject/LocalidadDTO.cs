using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class LocalidadDTO
	{
		public int Id { get; set; }
		public int? IdProvincia { get; set; }
		public int? IdDepto { get; set; }
		public string CodDepto { get; set; }
		public string Depto { get; set; }
		public string Cabecera { get; set; }
		public double? Area { get; set; }
		public double? Perimetro { get; set; }
		public double? Clave { get; set; }
		public bool? Excluir { get; set; }


        public virtual ProvinciaDTO ProvinciaDTO { get; set; }
	}				
}


