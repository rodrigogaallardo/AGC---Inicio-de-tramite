using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaUbicacionesDTO
    {
        public int id_encomiendaubicacion { get; set; }
        public int id_encomienda { get; set; }
        public int id_ubicacion { get; set; }
        public Nullable<int> id_subtipoubicacion { get; set; }
        public string local_subtipoubicacion { get; set; }
        public string deptoLocal_encomiendaubicacion { get; set; }
        public int id_zonaplaneamiento { get; set; }

        public UbicacionesDTO Ubicacion { get; set; }

        public ICollection<EncomiendaExternaUbicacionesPropiedadHorizontalDTO> EncomiendaExternaUbicacionesPropiedadHorizontal { get; set; }
    }
}
