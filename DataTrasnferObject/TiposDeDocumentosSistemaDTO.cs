using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TiposDeDocumentosSistemaDTO
    {
        public int id_tipdocsis { get; set; }
        public string cod_tipodocsis { get; set; }
        public string nombre_tipodocsis { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
