using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosActaNotarialDTO
    {
		public int id_actanotarial { get; set; }
		public int id_encomienda { get; set; }
		public int id_tipo_escritura { get; set; }
		public int nro_matricula_escribano_acta { get; set; }
		public int? id_actuacion_notarial_acta { get; set; }
		public int? nro_escritura_acta { get; set; }
		public DateTime fecha_escritura_acta { get; set; }
		public string registro_acta { get; set; }
		public bool local_afectado_ley13512 { get; set; }
		public bool reglamento_admite_actividad_ley13512 { get; set; }
		public DateTime? fecha_asamblea_ley13512 { get; set; }
		public DateTime? fecha_reglamento_ley13512 { get; set; }
		public int? nro_escritura_ley13512 { get; set; }
		public int? nro_matricula_escribano_ley13512 { get; set; }
		public string registro_ley13512 { get; set; }
        public string jurisdiccion_ley13512 { get; set; }
        public DateTime? fecha_inscrip_reglamento_ley13512 { get; set; }
        public int? nro_matricula_regprop_ley13512 { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public bool anulada { get; set; }
        public string url { get; set; }
        //public string ApyNom { get; set; }
        public EscribanoDTO Escribano { get; set; }
        public int id_file { get; set; }
    }
}


