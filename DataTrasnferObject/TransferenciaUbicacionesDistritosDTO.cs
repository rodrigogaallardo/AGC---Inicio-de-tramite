using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TransferenciaUbicacionesDistritosDTO
    {
        public int id_transfubicaciondistrito { get; set; }
        public int id_transfubicacion { get; set; }
        public int IdDistrito { get; set; }
        public int? IdZona { get; set; }
        public int? IdSubZona { get; set; }
        public UbicacionesCatalogoDistritosDTO UbicacionesCatalogoDistritosDTO { get; set; }
    }
}
