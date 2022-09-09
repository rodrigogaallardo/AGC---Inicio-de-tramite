using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TipoSociedadDTO
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int TitularesMinimo { get; set; }
		public int TitularesMaximo { get; set; }
		public bool RequiereNombreFantasia { get; set; }
		public bool RequiereRazonSocial { get; set; }
		public bool RequiereCuitPropio { get; set; }
	}				
}

