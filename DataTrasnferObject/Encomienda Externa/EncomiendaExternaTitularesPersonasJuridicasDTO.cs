using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaTitularesPersonasJuridicasDTO
    {
        public int id_personajuridica { get; set; }
        public int id_encomienda { get; set; }
        public string Cuit { get; set; }
        public string Razon_Social { get; set; }
        public string Calle { get; set; }
        public Nullable<int> NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Email { get; set; }
        public string Torre { get; set; }
    }
}
