using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class EncomiendaRubrosCNEntity
    {
        public int IdEncomiendaRubro { get; set; }
        public int IdEncomienda { get; set; }
        public string CodigoRubro { get; set; }
        public string DescripcionRubro { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoExpediente { get; set; }
        public decimal SuperficieHabilitar { get; set; }
        public DateTime CreateDate { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public bool EsAnterior { get; set; }

        public string TipoActividadNombre { get; set; }

    }
}
