using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITPermisosDatosAdicionalesDTO
    {
        public int IdSolicitud { get; set; }
        public int id_solicitud_caa { get; set; }
        public int id_caa { get; set; }
        public int id_rac { get; set; }
        public int id_form_rac { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }

    }


}
