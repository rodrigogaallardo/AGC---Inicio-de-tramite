using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TiposDePlanosDTO
    {
		public int id_tipo_plano { get; set; }
		public string codigo { get; set; }
        public string nombre { get; set; }
        public bool requiere_detalle { get; set; }
        public string extension { get; set; }
        public int tamanio_max_mb { get; set; }
        public String acronimo_SADE { get; set; }
    }
}


