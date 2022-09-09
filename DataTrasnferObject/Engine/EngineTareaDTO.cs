using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Engine
{
    /// <summary>
    /// Soprte para tarea del engine
    /// </summary>
    public class EngineTareaDTO
    {
        public int id_tarea { get; set; }
        public int id_circuito { get; set; }
        public string nombre_circuito { get; set; }
        public int cod_tarea { get; set; }
        public string nombre_tarea { get; set; }
        public List<ResultadoDTO> Resultados { get; set; }
    }
}
