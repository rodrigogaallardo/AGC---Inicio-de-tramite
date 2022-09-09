using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosDerechoOcupacionDTO
    {
		public int id_derecho_ocupacion { get; set; }
		public int id_actanotarial { get; set; }
		public int id_tipo_derecho_ocupacion { get; set; }
		public string tipo_derecho_ocupacion { get; set; }
		public decimal? porcentaje_titularidad { get; set; }
		public bool acredita_istrumento_publico { get; set; }
        public bool acredita_instrumento_privado { get; set; }
        public bool acredita_instrumento_judicial { get; set; }
    }
}


