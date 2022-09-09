using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasSolicitudesObservacionesDTO
	{
		public int Id { get; set; }
		public int IdSolicitud { get; set; }
		public string Observaciones { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public bool? Leido { get; set; }
        public string NombreCompleto { get;set;}
        public UsuarioDTO Usuario { get; set; }
        public UsuarioDTO UsuarioSGI { get; set; }
	}				
}


