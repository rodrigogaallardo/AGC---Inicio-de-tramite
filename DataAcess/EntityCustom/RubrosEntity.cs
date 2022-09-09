using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class RubrosEntity
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
    }
}
