using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class RelRubrosSolicitudesNuevasDTO
    {
		public int idRelRubSol { get; set; }
		public string codigo { get; set; }
		public int IdSolicitud { get; set; }
		public decimal Superficie { get; set; }
        public string Descripcion { get; set; }

    }				
}


