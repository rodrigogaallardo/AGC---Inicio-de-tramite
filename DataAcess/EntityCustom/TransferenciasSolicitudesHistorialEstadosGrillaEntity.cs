using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class TransferenciasSolicitudesHistorialEstadosGrillaEntity
    {
        public int id_solhistest { get; set; }
        public DateTime fecha { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
    }
}
