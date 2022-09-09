using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class Encomienda_Ubicaciones_DistritosDTO
    {
        public int id_encomiendaubicaciondistrito { get; set; }
        public int id_encomiendaubicacion { get; set; }
        public int IdDistrito { get; set; }
        public int? IdZona { get; set; }
        public int? IdSubZona { get; set; }        
        public UbicacionesCatalogoDistritosDTO UbicacionesCatalogoDistritosDTO { get; set; }
    }
}
