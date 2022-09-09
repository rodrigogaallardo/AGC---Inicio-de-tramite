using System;

namespace DataTransferObject
{
	public class SSITDocumentosAdjuntosEntityDTO
    {
        public int id_estado { get; set; }
        public int id_docadjunto { get; set; }
        public int id_master { get; set; }
        public int id_file { get; set; }
        public string detalle { get; set; }
        public DateTime CreateDate { get; set; }
        public string nombre_archivo { get; set; }
        public string usuario { get; set; }
        public string url { get; set; }
        public int id_tdocreq { get; set; }
    }
}


