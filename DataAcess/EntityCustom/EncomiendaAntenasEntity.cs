using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class EncomiendaAntenasEntity
    {
        public string cod_estado {get;set;}
		public string nom_estado {get;set;}
		public int id_encomienda {get;set;}
		public DateTime FechaEncomienda {get;set;}
		public string descripcion_tipotramite {get;set;}
		public int id_estado {get;set;}
        public string Matricula { get; set; }
        public string  Apellido {get;set;}
        public string Cuit {get;set;}
    }
}
