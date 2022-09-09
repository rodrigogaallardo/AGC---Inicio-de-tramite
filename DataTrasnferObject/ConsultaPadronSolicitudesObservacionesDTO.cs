using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronSolicitudesObservacionesDTO
	{
		public int IdConsultaPadronObservacion { get; set; }
		public int IdConsultaPadron { get; set; }
		public string Observaciones { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public bool? Leido { get; set; }

        public string userApeNom { get {
            return ((User.Usuario == null) ? User.SGIProfile.Apellido : User.Usuario.Apellido) + ", " + ((User.Usuario == null) ? User.SGIProfile.Nombres : User.Usuario.Nombre);
        } }

        public string clase_leido { get {
            return Leido.Value ? "icon-ok" : "icon-remove";
        } }

        public AspnetUserDTO User { get; set; }
        
	}				
}


