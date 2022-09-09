using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransfSolicitudesPagosDTO
    {
        public int id_sol_pago { get; set; }
        public int id_solicitud { get; set; }
        public int id_pago { get; set; }
        public decimal monto_pago { get; set; }
        public System.Guid CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
