using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosTiposDeDocumentosRequeridosDTO
    {
        public int id_rubtdocreq { get; set; }
        public int id_rubro { get; set; }
        public int id_tdocreq { get; set; }
        public bool obligatorio_rubtdocreq { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual TiposDeDocumentosRequeridosDTO TiposDeDocumentosRequeridosDTO { get; set; }
    }
}
