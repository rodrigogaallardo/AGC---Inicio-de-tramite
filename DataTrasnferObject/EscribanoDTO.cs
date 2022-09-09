using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EscribanoDTO
	{
		public int Matricula { get; set; }
		public int? Registro { get; set; }
        public string ApyNom { get; set; }
        public int? Cargo { get; set; }
        public string Calle { get; set; }
        public string NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public int? CodPostal { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Inhibido { get; set; }
        public DateTime CreateDate { get; set; }
    }
}


