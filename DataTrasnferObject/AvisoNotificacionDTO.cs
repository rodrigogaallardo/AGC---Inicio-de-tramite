using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class AvisoNotificacionDTO
    {
        public int IdNotificacion { get; set; }
        public int IdTramite { get; set; }
        public string AsuntoMail { get; set; }
        public string FechaAviso { get; set; }
        public string Domicilio { get; set; }
        public int IdMail { get; set; }
        public string Url { get; set; }
        public string NotificacionMotivo { get; set; }
        public string FechaNotificacion { get; set; }
    }
}
