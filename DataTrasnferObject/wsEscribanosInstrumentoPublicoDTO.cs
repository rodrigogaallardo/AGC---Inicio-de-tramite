using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosInstrumentoPublicoDTO
    {
		public int id_inspub { get; set; }
		public int id_derecho_ocupacion { get; set; }
		public DateTime? fecha_escritura_inspub { get; set; }
		public int? nro_escritura_inspub { get; set; }
		public int nro_matricula_escribano_inspub { get; set; }
		public string jurisdiccion_inspub { get; set; }
        public string matricula_registro_prop_inspub { get; set; }
    }
}


