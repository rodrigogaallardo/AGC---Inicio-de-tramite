using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class Ley962TiposDeDocumentosRequeridosDTO
    {
        public int id { get; set; }
        public int id_tdocreq { get; set; }

        public virtual TiposDeDocumentosRequeridosDTO TiposDeDocumentosRequeridosDTO { get; set; }
    }
}
