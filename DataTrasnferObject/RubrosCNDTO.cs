using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosCNDTO
    {
        public int IdRubro { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Busqueda { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoExpediente { get; set; }
        public int IdGrupoCircuito { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public bool EsAnterior { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public bool TieneNormativa { get; set; }

        public string TipoActividadNombre { get; set; }
       
        public decimal? Superficie { get; set; }

        public string Mensaje { get; set; }
        public bool Asistentes350 { get; set; }
        public bool TieneRubroDeposito { get; set; }
        public int idCondicionIncendio { get; set; }
        public bool SinBanioPCD { get; set; }
        public bool CondicionExpress { get; set; }
        public virtual ICollection<RubrosCNSubRubrosDTO> RubrosCN_SubrubrosDTO { get; set; }
        public virtual CondicionesIncendioDTO CondicionesIncendio { get; set; }

        // public virtual ICollection<RubrosTiposDeDocumentosRequeridosDTO> RubrosTiposDeDocumentosRequeridosDTO { get; set; }
        //  public virtual ICollection<RubrosCircuitoAtomaticoZonasDTO> RubrosCircuitoAtomaticoZonasDTO { get; set; }
        // public virtual ICollection<RubrosTiposDeDocumentosRequeridosZonasDTO> RubrosTiposDeDocumentosRequeridosZonasDTO { get; set; }
    }
    
}
