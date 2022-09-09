using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class SSITSolicitudesObservacionesEntity
    {
        public int id_solobs { get; set; }
        public int id_solicitud { get; set; }
        public string observaciones { get; set; }
        public bool leido { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public string userApeNom { get; set; }
    }
}
