using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class clsItemGrillaNotificaciones
    {
        public int id_notificacion { get; set; }
        public int id_solicitud { get; set; }
        public int id_email { get; set; }
        public DateTime createDate { get; set; }
        public DateTime? fechaNotificacionSSIT { get; set; }
    }
}
