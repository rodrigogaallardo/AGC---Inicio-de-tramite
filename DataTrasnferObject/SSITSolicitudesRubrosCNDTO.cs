using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SSITSolicitudesRubrosCNDTO
    {
        public int IdSolicitudRubro { get; set; }
        public int IdSolicitud { get; set; }
        public int IdRubro { get; set; }
        public string CodigoRubro { get; set; }
        public string DescripcionRubro { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoExpediente { get; set; }
        public decimal SuperficieHabilitar { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<Guid> CreateUser { get; set; }
        public bool? EsAnterior { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public string TipoActividadNombre { get; set; }
        public int idImpactoAmbiental { get; set; }

        public virtual TipoActividadDTO TipoActividadDTO { get; set; }
        public virtual RubrosCNDTO RubrosDTO { get; set; }
        public virtual ImpactoAmbientalDTO ImpactoAmbientalDTO { get; set; }
        public virtual ICollection<SolicitudRubrosCNSubrubrosDTO> solicitudRubrosCNSubrubrosDTO { get; set; }
    }
    public class SolicitudRubrosCNSubrubrosDTO
    {
        public int Id_SolRubCNSubrubros { get; set; }
        public int IdSolicitudRubro { get; set; }
        public int Id_rubrosubrubro { get; set; }

        //public virtual Encomienda_RubrosCN Encomienda_RubrosCN { get; set; }
        public virtual RubrosCNSubRubrosDTO rubrosCNSubRubrosDTO { get; set; }
    }

}
