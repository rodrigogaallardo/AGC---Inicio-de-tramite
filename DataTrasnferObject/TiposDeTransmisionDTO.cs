using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TiposDeTransmisionDTO
    {
        public int id_tipoTransmision { get; set; }
        public string nom_tipotransmision { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
