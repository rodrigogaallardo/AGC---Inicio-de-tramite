using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransferenciaUbicacionesMixturasDTO
    {
        public int id_transfubicacionmixtura { get; set; }
        public int id_transfubicacion { get; set; }
        public int IdZonaMixtura { get; set; }

        public UbicacionesZonasMixturasDTO UbicacionesZonasMixturasDTO { get; set; }
    }
}
