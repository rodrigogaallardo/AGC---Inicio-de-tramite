using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaRectificatoriaDTO
    {
		public int id_encrec { get; set; }
		public int id_encomienda_anterior { get; set; }
		public int id_encomienda_nueva { get; set; }
    }				
}


