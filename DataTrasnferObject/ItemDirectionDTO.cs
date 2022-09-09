using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    
    public class ItemDirectionDTO
    {  
        public Nullable<int> Seccion { get; set; }
        public string Manzana { get; set; }
        public string Parcela { get; set; }
        public Nullable<int> idUbicacion { get; set; }
        public int id_solicitud { get; set; }
        public string direccion { get; set; }
        public string Numero { get; set; }
    }
}
