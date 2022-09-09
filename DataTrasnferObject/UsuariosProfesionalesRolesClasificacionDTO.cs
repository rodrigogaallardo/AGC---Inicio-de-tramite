using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class UsuariosProfesionalesRolesClasificacionDTO
	{
		public int Id { get; set; }
		public Guid UserID { get; set; }
		public Guid RoleID { get; set; }
		public int IdClasificacion { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
	}				
}


