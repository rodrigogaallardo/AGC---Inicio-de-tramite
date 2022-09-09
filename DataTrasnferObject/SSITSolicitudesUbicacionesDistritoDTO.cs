using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesUbicacionesDistritoDTO
    {
        public int id_solicitudubicaciondistrito { get; set; }
        public int id_solicitudubicacion { get; set; }
        public int IdDistrito { get; set; }
        public int? IdZona { get; set; }
        public int? IdSubZona { get; set; }

        public UbicacionesCatalogoDistritosDTO UbicacionesCatalogoDistritosDTO { get; set; }
    }
}
