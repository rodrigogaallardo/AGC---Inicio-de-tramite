using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesNormativasDTO
    {
        public int IdSolicitud { get; set; }
        public int id_tiponormativa { get; set; }
        public int id_entidadnormativa { get; set; }
        public string nro_normativa { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual TipoNormativaDTO TipoNormativaDTO { get; set; }
        public virtual EntidadNormativaDTO EntidadNormativaDTO { get; set; }

    }
}
