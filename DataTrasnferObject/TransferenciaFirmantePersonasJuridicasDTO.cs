﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransferenciaFirmantePersonasJuridicasDTO
    {
        public int id_firmante_pj { get; set; }
        public int id_solicitud { get; set; }
        public int id_personajuridica { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int id_tipodoc_personal { get; set; }
        public string Nro_Documento { get; set; }
        public int id_tipocaracter { get; set; }
        public string cargo_firmante_pj { get; set; }

        public TiposDeCaracterLegalDTO TipoCaracterLegal { get; set; }
        public TipoDocumentoPersonalDTO TipoDocumentoPersonal { get; set; }
    }
}
