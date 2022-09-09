using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SGITareaDocumentosAdjuntosDTO
	{
		public int id_doc_adj { get; set; }
		public int id_tramitetarea { get; set; }
		public string tdoc_adj_detalle { get; set; }
		public int id_file { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public string nombre_archivo { get; set; }
		public int? id_tdocreq { get; set; }
	}				
}


