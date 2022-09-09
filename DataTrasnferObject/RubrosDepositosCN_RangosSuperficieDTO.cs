using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosDepositosCN_RangosSuperficieDTO
    {
        public int idRepositoRangoSup { get; set; }
        public int IdDeposito { get; set; }
        public int id_tipocircuito { get; set; }
        public string LetraAnexo { get; set; }
        public decimal DesdeM2 { get; set; }
        public decimal HastaM2 { get; set; } 
    }
}
