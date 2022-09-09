using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ENG_CircuitosDTO
    {
        public int id_circuito { get; set; }
        public string nombre_circuito { get; set; }
        public string cod_circuito { get; set; }
        public Nullable<decimal> version_circuito { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> prioridad { get; set; }
        public bool activo { get; set; }
        public string nombre_grupo { get; set; }
    }
}
