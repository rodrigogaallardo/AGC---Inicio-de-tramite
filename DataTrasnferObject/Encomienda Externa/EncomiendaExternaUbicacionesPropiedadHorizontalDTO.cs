using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaUbicacionesPropiedadHorizontalDTO
    {
        public int id_encomiendaprophorizontal { get; set; }
        public int id_encomiendaubicacion { get; set; }
        public int id_propiedadhorizontal { get; set; }

        public UbicacionesPropiedadhorizontalDTO UbicacionesPropiedadHorizontal { get; set; }
    }
}
