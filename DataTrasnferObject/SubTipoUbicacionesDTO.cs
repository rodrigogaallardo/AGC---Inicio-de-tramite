using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SubTipoUbicacionesDTO
    {
        public int id_subtipoubicacion { get; set; }
        public string descripcion_subtipoubicacion { get; set; }
        public int id_tipoubicacion { get; set; }

        public virtual TiposDeUbicacionDTO TiposDeUbicacionDTO { get; set; }
    }
}
