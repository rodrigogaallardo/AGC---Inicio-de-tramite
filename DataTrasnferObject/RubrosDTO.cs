using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosDTO
    {
        public int IdRubro { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Busqueda { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoDocumentorRequerido { get; set; }
        public bool EsAnterior { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public bool PregAntenaEmisora { get; set; }
        public bool SoloAPRA { get; set; }
        public string Tooltip { get; set; }
        public double? LocalVenta { get; set; }
        public bool? Ley105 { get; set; }

        public string TipoActividadNombre { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public decimal? Superficie { get; set; }
        public bool TieneNormativa { get; set; }
        public int IdTipoTramite { get; set; }
        public bool TieneDeposito { get; set; }
        public Nullable<decimal> SupMinCargaDescarga { get; set; }
        public Nullable<decimal> SupMinCargaDescargaRefII { get; set; }
        public Nullable<decimal> SupMinCargaDescargaRefV { get; set; }
        public bool OficinaComercial { get; set; }
        public bool EsRubroIndividual { get; set; }
        public bool EsProTeatro { get; set; }
        public bool EsEstadio { get; set; }
        public bool EsCentroCultural { get; set; }
        public bool ValidaCargaDescarga { get; set; }
        public int? IdCircuito { get; set; }

        public virtual ICollection<RubrosTiposDeDocumentosRequeridosDTO> RubrosTiposDeDocumentosRequeridosDTO { get; set; }
        public virtual ICollection<RubrosCircuitoAtomaticoZonasDTO> RubrosCircuitoAtomaticoZonasDTO { get; set; }
        public virtual ICollection<RubrosTiposDeDocumentosRequeridosZonasDTO> RubrosTiposDeDocumentosRequeridosZonasDTO { get; set; }
    }

    public class RubrosIncendioDTO
    {
        public int id_rubro_incendio { get; set; }
        public int id_rubro { get; set; }
        public int riesgo { get; set; }
        public decimal DesdeM2 { get; set; }
        public decimal HastaM2 { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
    }
}
