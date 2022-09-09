using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class AzraelBuscadorFileEntity
    {
        public int IdTramite { get; set; }
        public int IdFile { get; set; }
        public string TipoTramite1 { get; set; }
        public string TipoTramite2 { get; set; }
        public string Estado { get; set; }
        public string TipoDocReq { get; set; }
        public string TipoDocSis { get; set; }
        public string FileName { get; set; }
        public int idTipoTramite { get; set; }
        public int id_docadjunto { get; set; }
    }
}
