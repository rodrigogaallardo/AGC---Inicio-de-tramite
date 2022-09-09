using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class wsEscribanosPersonasJuridicasRepresentantesDTO
    {
		public int id_wsRepresentantePJ { get; set; }
		public int id_wsPersonaJuridica { get; set; }
		public DateTime? fecha_designacion { get; set; }
		public DateTime? fecha_escritura_designacion { get; set; }
		public int? nro_escritura_designacion { get; set; }
        public int? nro_matricula_escribano_designacion { get; set; }
		public string registro_designacion { get; set; }
		public string jurisdiccion_designacion { get; set; }
        public int? id_firmante_pj { get; set; }
        public string apellido { get; set; }
        public string nombres { get; set; }
        public string TipoPersona { get; set; }
        public int id_firmante { get; set; }
        public string Titular { get; set; }
        public string DescTipoDocPersonal { get; set; }
        public string Nro_Documento { get; set; }
        public string nom_tipocaracter { get; set; }
    }
}


