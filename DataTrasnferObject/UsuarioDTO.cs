using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class UsuarioDTO
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public string Calle { get; set; }
		public int? NroPuerta { get; set; }
		public string Piso { get; set; }
		public string Depto { get; set; }
		public string CodigoPostal { get; set; }
		public int? IdLocalidad { get; set; }
		public int? IdProvincia { get; set; }
		public string Movil { get; set; }
		public string TelefonoArea { get; set; }
		public string TelefonoPrefijo { get; set; }
		public string TelefonoSufijo { get; set; }
		public string Sms { get; set; }
		public int? UserDni { get; set; }
		public string UserDetalleCaracter { get; set; }
		public int TipoPersona { get; set; }
		public string RazonSocial { get; set; }
		public string CUIT { get; set; }
		public string Telefono { get; set; }
		public bool AceptarTerminos { get; set; }
        public string Password { get; set; }
        public string token { get; set; }
        public string sign { get; set; }

        public List<RolesDTO> Roles { get; set; }
        public int? IdProfesional { get; set; }
        public bool Bloqueado { get; set; }

        public Guid CreateUser { get; set; }
        public ICollection<UsuarioConsejoDTO> Consejos { get; set; } 
	}				
}


