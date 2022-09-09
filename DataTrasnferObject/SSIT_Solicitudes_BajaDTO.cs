using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class SSIT_Solicitudes_BajaDTO
    {
        public int id_baja { get; set; }
        public int id_solicitud { get; set; }
        public int id_tipo_motivo_baja { get; set; }
        public DateTime fecha_baja { get; set; }
        public string observaciones { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
    }

}


