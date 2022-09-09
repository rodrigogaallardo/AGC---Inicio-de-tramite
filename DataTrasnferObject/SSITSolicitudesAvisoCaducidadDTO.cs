using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesAvisoCaducidadDTO
    {
        public int Id_notificacion { get; set; }
        public int Id_solicitud { get; set; }
        public int Id_email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public DateTime? FechaNotificacionSSIT { get; set; }
    }
}
