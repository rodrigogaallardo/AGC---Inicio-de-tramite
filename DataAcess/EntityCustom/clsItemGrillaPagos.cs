using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class clsItemGrillaPagosEntity
    {
        public int id_sol_pago { get; set; }
        public int id_solicitud { get; set; }
        public int id_pago { get; set; }
        public int id_medio_pago { get; set; }
        public decimal monto_pago { get; set; }
        public DateTime CreateDate { get; set; }
        public string desc_medio_pago { get; set; }
        public string desc_estado_pago { get; set; }
        public int id_estado_pago { get; set; }
    }
}
