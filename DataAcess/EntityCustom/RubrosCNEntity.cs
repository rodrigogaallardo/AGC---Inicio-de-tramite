using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class RubrosCNEntity
    {
        public int IdRubro { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Busqueda { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoExpediente { get; set; }
        public string RestriccionZona { get; set; }
        public string RestriccionSup { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public bool EsAnterior { get; set; }
        public bool? Resultado { get; set; }

        public string TipoActividadNombre { get; set; }
        public bool TieneNormativa { get; set; }
        public decimal? Superficie { get; set; }
        public int ZonaMixtura { get; set; }
        public string CondicionZonaMixtura { get; set; }
        public string Mensaje { get; set; }
        public List<itemResultadoEvaluacionCondiciones> Resultadoscondiciones { get; set; }
    }

    public class itemResultadoEvaluacionCondiciones
    {
        public int IdCondicion {get;set;}
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
}
