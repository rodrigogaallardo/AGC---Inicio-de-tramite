using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class EncomiendaPlanosDTO
    {
		public int id_encomienda_plano { get; set; }
		public int id_encomienda { get; set; }
		public int id_file { get; set; }
		public int id_tipo_plano { get; set; }
        public string detalle { get; set; }
        public string nombre_archivo { get; set; }
        public string CreateUser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
        public string url { get; set; }
        public DateTime? fechaPresentado { get; set; }
        public virtual TiposDePlanosDTO TiposDePlanosDTO { get; set; }

        public string DetalleExtra
        {
            get { return TiposDePlanosDTO.requiere_detalle == true && !string.IsNullOrEmpty(detalle) ? detalle : TiposDePlanosDTO.nombre; }
        }

    }				
}


