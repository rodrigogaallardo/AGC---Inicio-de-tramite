using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class SsitSolicitudesUbicacionesModel 
    {
        public int IdSolicitudUbicacion { get; set; }
        public int IdTipoUbicacion { get; set; }
        public bool? RequiereSMP { get; set; }
        public string DescripcionTipoUbicacion { get; set; }
        public string DescripcionSubtipoUbicacion { get; set; }
        public int? NroPartidaMatriz { get; set; }
        public int? Seccion { get; set; }
        public string Manzana { get; set; }
        public string Parcela { get; set; }
        public string deptoLocal_ubicacion { get; set; }
        public string Torre { get; set; }
        public string Depto { get; set; }
        public string Local { get; set; }
        public string CodZonaPla { get; set; }
        public string DescripcionZonaPla { get; set; }
        public string LocalSubTipoUbicacion { get; set; }

    }
}
