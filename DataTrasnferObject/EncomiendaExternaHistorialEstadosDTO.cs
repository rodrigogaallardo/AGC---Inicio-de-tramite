using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaHistorialEstadosDTO
    {
        public int id_enchistest { get; set; }
        public int id_encomienda { get; set; }
        public string cod_estado_ant { get; set; }
        public string cod_estado_nuevo { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public System.Guid usuario_modificacion { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string cod_estado_viejo { get; set; }
        public int id_estado_viejo { get; set; }
        public string nom_estado_viejo { get; set; }
        public string nom_estado_consejo_viejo { get; set; }
        public int id_estado_nuevo { get; set; }
        public string nom_estado_nuevo { get; set; }
        public string nom_estado_consejo_nuevo { get; set; }
    }
}
