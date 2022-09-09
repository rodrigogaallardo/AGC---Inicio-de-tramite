using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSIT_Solicitudes_AvisoCaducidadDTO
    {
        public int id_aviso { get; set; }
        public int id_solicitud { get; set; }
        public int id_email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public DateTime? fechaNotificacionSSIT { get; set; }
    }
}
