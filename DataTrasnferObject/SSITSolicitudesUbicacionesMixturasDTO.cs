using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesUbicacionesMixturasDTO
    {
        public int id_solicitudubicacionmixtura { get; set; }
        public int id_solicitudubicacion { get; set; }
        public int IdZonaMixtura { get; set; }
        public UbicacionesZonasMixturasDTO UbicacionesZonasMixturasDTO { get; set; }
    }
}
