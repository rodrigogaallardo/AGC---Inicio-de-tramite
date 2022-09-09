using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConceptosBUIIndependientesDTO
    {
		public int id_concepto { get; set; }
        public string keycode { get; set; }
        public int cod_concepto_1 { get; set; }
        public int cod_concepto_2 { get; set; }
        public int cod_concepto_3 { get; set; }
        public string descripcion_concepto { get; set; }
        public bool admite_reglas { get; set; }
        public DateTime CreateDate { get; set; }
    }
}


