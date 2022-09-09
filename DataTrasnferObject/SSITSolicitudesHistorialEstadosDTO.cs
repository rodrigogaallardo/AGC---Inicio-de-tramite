using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SSITSolicitudesHistorialEstadosDTO
    {
        public int id_solhistest { get; set; }
        public int id_solicitud { get; set; }
		public string cod_estado_ant { get; set; }
        public string cod_estado_nuevo { get; set; }
        public string username { get; set; }
		public DateTime fecha_modificacion { get; set; }
        public Guid usuario_modificacion { get; set; }
    }

    public class SSITSolicitudesHistorialEstadosGrillaDTO
    {
        public int id_solhistest { get; set; }
        public DateTime fecha { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
    }
}


