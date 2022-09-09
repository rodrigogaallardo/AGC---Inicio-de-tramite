using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TipoDocumentacionRequeridaDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Documentos { get; set; }
        public string Otros { get; set; }
        public string TipoTramite { get; set; }
        public string Nomenclatura { get; set; }

        //public virtual ICollection<CPadron_Rubros> CPadron_Rubros { get; set; }
        //public virtual ICollection<Encomienda_Rubros> Encomienda_Rubros { get; set; }
        //public virtual ICollection<Rubros_Historial_Cambios> Rubros_Historial_Cambios { get; set; }

    }
}
