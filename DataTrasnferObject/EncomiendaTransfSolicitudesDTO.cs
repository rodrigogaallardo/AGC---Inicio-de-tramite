﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaTransfSolicitudesDTO
    {
        public int id_encomiendaSolicitud { get; set; }
        public int id_encomienda { get; set; }
        public int id_solicitud { get; set; }

        public virtual EncomiendaDTO EncomiendaDTO { get; set; }
        public virtual TransferenciasSolicitudesDTO TransferenciasSolicitudesDTO { get; set; }
    }
}
