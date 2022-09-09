using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TiposDeUbicacionDTO
	{
		public int IdTipoUbicacion { get; set; }
		public string DescripcionTipoUbicacion { get; set; }
        public bool? RequiereSMP { get; set; }

        public virtual ICollection<SubTipoUbicacionesDTO> SubTipoUbicacionesDTO { get; set; }
	}				
}


