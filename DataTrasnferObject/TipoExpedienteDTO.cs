using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TipoExpedienteDTO
    {
        public int IdTipoExpediente { get; set; }
        public string CodTipoExpediente { get; set; }
        public string DescripcionTipoExpediente { get; set; }
        public string CodTipoExpedienteWs { get; set; }
    }
}
