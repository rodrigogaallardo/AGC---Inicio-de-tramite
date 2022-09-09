using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSIT_Listado_ProfesionalesDTO
    {
        public string Apellido { get; set; }
        public string nombre { get; set; }
        public string Consejo { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> Aprobadas { get; set; }
        public Nullable<decimal> porcentaje_aprob { get; set; }
        public Nullable<int> Rechazadas { get; set; }
        public Nullable<decimal> porcentaje_recha { get; set; }
        public Nullable<int> Vencidas { get; set; }
        public Nullable<decimal> porcentaje_venci { get; set; }
    }
}
