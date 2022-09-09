using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosImpactoAmbientalCNDTO
    {
        public int IdRubroIA { get; set; }
        public int IdRubro { get; set; }
        public int id_tipocertificado { get; set; }
        public decimal DesdeM2 { get; set; }
        public decimal HastaM2 { get; set; }
        public bool AntenaEmisora { get; set; }
        public string LetraAnexo { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
    }
}
