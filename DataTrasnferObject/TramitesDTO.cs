using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TramitesDTO
    {
        public int IdTramite { get; set; }
        public string CodigoSeguridad { get; set; }
        public DateTime CreateDate { get; set; }
        public int IdTipoTramite { get; set; }
        public string TipoTramiteDescripcion { get; set; }
        public int TipoExpediente { get; set; }
        public int SubTipoExpediente { get; set; }
        public string SubTipoExpedienteDescripcion { get; set; }
        public string TipoExpedienteDescripcion { get; set; }
        public int IdEstado { get; set; }
        public string EstadoDescripcion { get; set; }
        public string Domicilio { get; set; }
        public string Url { get; set; }
        public int id_encomienda { get; set; }
    }
}
