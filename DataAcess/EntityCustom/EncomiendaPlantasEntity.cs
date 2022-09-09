using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class EncomiendaPlantasEntity
    {
        public int IdTipoSector { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public bool MuestraCampoAdicional { get; set; }
        public string Detalle { get; set; }
        public int TamanoCampoAdicional { get; set; }
        public bool? Ocultar { get; set; }

        public int id_encomiendatiposector { get; set; }
        public int id_encomienda { get; set; }
        public virtual TipoSector TipoSector { get; set; }
    }        
}
