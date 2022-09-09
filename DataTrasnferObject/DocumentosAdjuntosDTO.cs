using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class DocumentosAdjuntosDTO
    {
		public int id_docadjunto { get; set; }
		public int id_tdocreq { get; set; }
		public int id_encomienda { get; set; }
		public string tdocreq_detalle { get; set; }
        public byte[] documento { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public bool puede_eliminar { get; set; }
        public string origen { get; set; }
        public int? id_solicitud { get; set; }
        public int? id_file { get; set; }
        public int? id_agrupamiento { get; set; }
        public string nombre_tdocreq { get; set; }
    }				
}


