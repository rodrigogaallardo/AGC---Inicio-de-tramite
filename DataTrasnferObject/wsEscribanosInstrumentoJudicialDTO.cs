using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosInstrumentoJudicialDTO
    {
		public int id_insjud { get; set; }
		public int id_derecho_ocupacion { get; set; }
		public DateTime? fecha_testimonio_insjud { get; set; }
		public string juzgado_insjud { get; set; }
		public string secretaria_insjud { get; set; }
		public string jurisdiccion_insjud { get; set; }
        public string autos_insjud { get; set; }
        public DateTime? fecha_pago_impsellos_insjud { get; set; }
        public string Banco_pago_insjud { get; set; }
        public decimal? monto_pago_insjud { get; set; }
    }
}


