using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class UbicacionesPuertasDTO
    {
      	public int IdUbicacionPuerta { get; set; }
		public int IdUbicacion { get; set; }
		public string TipoPuerta { get; set; }
		public int CodigoCalle { get; set; }
		public int NroPuertaUbic { get; set; }


        public string Nombre_calle { get; set; }
        public bool NuevaPuerta { get; set; }
    }
}
