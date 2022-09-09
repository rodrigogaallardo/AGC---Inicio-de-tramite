using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosPersonasJuridicasDTO
    {
		public int id_wsPersonaJuridica { get; set; }
		public int id_actanotarial { get; set; }
		public int? id_personajuridica { get; set; }
		public DateTime? fecha_ultimo_pago_IIBB { get; set; }
		public decimal? porcentaje_titularidad { get; set; }
		public string direccion_sede_social { get; set; }
		public DateTime? fecha_contrato_social { get; set; }
		public int? instrumento_constitucion { get; set; }
        public int? nro_escritura_constitucion { get; set; }
        public int? nro_matricula_escribano_constitucion { get; set; }
		public string registro_constitucion { get; set; }
		public string jurisdiccion_constitucion { get; set; }
		public string nom_organismo_inscripcion { get; set; }
		public DateTime? fecha_incripcion { get; set; }
		public string datos_incripcion { get; set; }
		public string nom_tipo_IVA { get; set; }
        public string TipoPersona { get; set; }
        public string TipoPersonaDesc { get; set; }
        public int? id_persona { get; set; }
        public string ApellidoNomRazon { get; set; }
        public string cuit { get; set; }
        public string Domicilio { get; set; }
    }
}


