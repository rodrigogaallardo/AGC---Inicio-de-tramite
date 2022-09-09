using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class ItemPuertaEntity
    {
        public Nullable<int> seccion { get; set; }
        public string manzana { get; set; }
        public string parcela { get; set; }
        public Nullable<int> idUbicacion { get; set; }
        public int id_solicitud { get; set; }
        public string calle { get; set; }
        public string puerta { get; set; }
        public string torre { get; set; }
        public string local { get; set; }
        public string depto { get; set; }
        public string otros { get; set; }
    }
}
