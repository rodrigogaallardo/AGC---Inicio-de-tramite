using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class CertificadosDTO
    {
		public int id_certificado { get; set; }
		public int TipoTramite { get; set; }
        public int NroTramite { get; set; }
        public byte[] Certificado { get; set; }
		public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public int? item { get; set; }
    }
}


