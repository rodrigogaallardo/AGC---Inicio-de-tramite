using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class Encomienda_Ubicaciones_MixturasDTO
    {
        public int id_encomiendaubicacionmixtura { get; set; }
        public int id_encomiendaubicacion { get; set; }
        public int IdZonaMixtura { get; set; }
        public UbicacionesZonasMixturasDTO UbicacionesZonasMixturasDTO { get; set; }
    }
}
