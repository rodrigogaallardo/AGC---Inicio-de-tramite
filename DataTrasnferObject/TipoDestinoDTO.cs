using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TipoDestinoDTO
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Nombre { get; set; }
        public bool RequiereObservaciones { get; set; }
        public bool RequiereDetalle { get; set; }
    }				
}


