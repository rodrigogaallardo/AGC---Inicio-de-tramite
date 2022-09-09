using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class clsItemGrillaMails
    {
        public string Mail_ID { get; set; }
        public string Mail_Estado { get; set; }
        public string Mail_Proceso { get; set; }
        public string Mail_Asunto { get; set; }
        public string Mail_Email { get; set; }
        public DateTime? Mail_Fecha { get; set; }
        public string Mail_Html { get; set; }
        public DateTime? Mail_FechaAlta { get; set; }
        public DateTime? Mail_FechaEnvio { get; set; }
        public int? Mail_Intentos { get; set; }
        public int? Mail_Prioridad { get; set; }
    }
}