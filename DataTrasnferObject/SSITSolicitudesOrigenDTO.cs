using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesOrigenDTO
    {

        public int id_solicitud { get; set; }
        public int? id_solicitud_origen { get; set; }
        public int? id_transf_origen { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
