using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class wsEscribanosPersonasFisicasRepresentantesEntity
    {
        public int id_wsRepresentantePF { get; set; }
        public int id_wsPersonaFisica { get; set; }
        public DateTime? fecha_poder { get; set; }
        public int? nro_escritura_poder { get; set; }
        public int? nro_matricula_escribano_poder { get; set; }
        public string registro_poder { get; set; }
        public string jurisdiccion_poder { get; set; }
        public int? id_firmante_pf { get; set; }
        public string TipoPersona { get; set; }
        public int id_firmante { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Titular { get; set; }
        public string DescTipoDocPersonal { get; set; }
        public string Nro_Documento { get; set; }
        public string nom_tipocaracter { get; set; }
    }
}
