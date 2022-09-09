using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ItemCertificadoDTO
    {
        public int IdCertificado { get; set; }
        public DateTime CreateDate { get; set; }
        public string Url { get; set; }
        public Guid UserId { get; set; }
        public string NombreDocumentoAdjunto { get; set; }
        public int TipoTramite { get; set; }
        public int NumeroTramite { get; set; }
    }
}
