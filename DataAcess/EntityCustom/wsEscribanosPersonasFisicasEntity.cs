using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class wsEscribanosPersonasFisicasEntity
    {
        public int id_wsPersonasFisicas { get; set; }
        public int id_actanotarial { get; set; }
        public int id_personafisica { get; set; }
        public DateTime? fecha_ultimo_pago_IIBB { get; set; }
        public decimal? porcentaje_titularidad { get; set; }
        public string apellido { get; set; }
        public string nombres { get; set; }
        public string DescTipoDocPersonal { get; set; }
        public string Nro_Documento { get; set; }
        public string TipoPersona { get; set; }
        public string TipoPersonaDesc { get; set; }
        public int? id_persona { get; set; }
        public string ApellidoNomRazon { get; set; }
        public string cuit { get; set; }
        public string Domicilio { get; set; }
    }
}
