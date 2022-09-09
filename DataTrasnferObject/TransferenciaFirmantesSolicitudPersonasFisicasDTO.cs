using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransferenciaFirmantesSolicitudPersonasFisicasDTO
    {
        public int id_firmante_pf { get; set; }
        public int id_solicitud { get; set; }
        public int TitularesHAB { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int id_tipodoc_personal { get; set; }
        public string Nro_Documento { get; set; }
        public int id_tipocaracter { get; set; }
        public string Email { get; set; }
        public string Cuit { get; set; }

        public TiposDeCaracterLegalDTO TipoCaracterLegal { get; set; }
        public TipoDocumentoPersonalDTO TipoDocumentoPersonal { get; set; }
    }
}
