using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class TipoSectorDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> Ocultar { get; set; }
        public Nullable<bool> MuestraCampoAdicional { get; set; }
        public Nullable<int> TamanoCampoAdicional { get; set; }

        //public virtual ICollection<CPadron_Plantas> CPadron_Plantas { get; set; }
        //public virtual ICollection<Encomienda_Plantas> Encomienda_Plantas { get; set; }
        //public virtual ICollection<SolicitudPartida> SolicitudPartida { get; set; }
        //public virtual ICollection<SolicitudPuerta> SolicitudPuerta { get; set; }

    }
}
