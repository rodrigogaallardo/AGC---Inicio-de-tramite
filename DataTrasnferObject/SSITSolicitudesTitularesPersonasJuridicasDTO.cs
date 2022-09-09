using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesTitularesPersonasJuridicasDTO
	{
		public int IdPersonaJuridica { get; set; }
		public int IdSolicitud { get; set; }
		public int IdTipoSociedad { get; set; }
		public string RazonSocial { get; set; }
		public string CUIT { get; set; }
		public int IdTipoiibb { get; set; }
		public string NroIibb { get; set; }
		public string Calle { get; set; }
		public int? NroPuerta { get; set; }
		public string Piso { get; set; }
		public string Depto { get; set; }
		public int IdLocalidad { get; set; }
		public string CodigoPostal { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Torre { get; set; }

        public ICollection<FirmantesSHDTO> firmantesSH { get; set; }
        public ICollection<TitularesSHDTO> titularesSH { get; set; }
        public ICollection<SSITSolicitudesFirmantesPersonasJuridicasDTO> solFirDTO { get; set; }

        //public virtual LocalidadDTO LocalidadDTO { get; set; }
        //public virtual ICollection<SSITSolicitudesFirmantesPersonasJuridicasDTO> SSITSolicitudesFirmantesPersonasJuridicasDTO { get; set; }
        //public virtual ICollection<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO { get; set; }
        //public virtual TiposDeIngresosBrutosDTO TiposDeIngresosBrutosDTO { get; set; }
        //public virtual TipoSociedadDTO TipoSociedadDTO { get; set; }

	}				
}


