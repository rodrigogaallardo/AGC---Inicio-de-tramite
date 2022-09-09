using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransferenciasSolicitudesHistorialEstadosDTO
    {
        public int id_solhistest { get; set; }
        public int id_solicitud { get; set; }
        public string cod_estado_ant { get; set; }
        public string cod_estado_nuevo { get; set; }
        public string username { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public System.Guid usuario_modificacion { get; set; }
    }

    public class TransferenciasSolicitudesHistorialEstadosGrillaDTO
    {
        public int id_solhistest { get; set; }
        public DateTime fecha { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
    }
}
