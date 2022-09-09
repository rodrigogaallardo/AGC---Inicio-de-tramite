using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaAntenasGrillaDTO
    {
        public string cod_estado { get; set; }
        public string nom_estado { get; set; }
        public int id_encomienda { get; set; }
        public DateTime FechaEncomienda { get; set; }
        public string descripcion_tipotramite { get; set; }
        public int id_estado { get; set; }
        public ItemDirectionDTO Direccion { get; set; }
    }
}
