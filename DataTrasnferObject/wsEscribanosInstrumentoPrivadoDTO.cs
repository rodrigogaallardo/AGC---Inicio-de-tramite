using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosInstrumentoPrivadoDTO
    {
        public int id_inspriv { get; set; }
        public int id_derecho_ocupacion { get; set; }
        public DateTime? fecha_certificacion_inspriv { get; set; }
        public int? nro_acta_inspriv { get; set; }
        public int? nro_matricula_escribano_inspriv { get; set; }
        public string registro_inspriv { get; set; }
        public string jurisdiccion_inspriv { get; set; }
        public DateTime? fecha_pago_impsellos_inspriv { get; set; }
        public string banco_pago_inspriv { get; set; }
        public decimal? monto_pago_inspriv { get; set; }
    }
}


