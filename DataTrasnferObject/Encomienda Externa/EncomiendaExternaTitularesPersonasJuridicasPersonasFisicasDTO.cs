using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaTitularesPersonasJuridicasPersonasFisicasDTO
    {
        public int id_titular_pj { get; set; }
        public int id_encomienda { get; set; }
        public int id_personajuridica { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Cuit { get; set; }
        public int id_tipodoc_personal { get; set; }
        public string Nro_Documento { get; set; }
        public string Email { get; set; }
        public Nullable<int> id_firmante_pj { get; set; }
        public bool firmante_misma_persona { get; set; }

        public string ApellidoNombres {
            get {
                return Apellido + " " + Nombres;
            }
        }
    }
}
