using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class SGITramitesTareasEntity
    {
        public int id_tramitetarea { get; set; }
        public int id_tarea { get; set; }
        public int id_resultado { get; set; }
        public System.DateTime FechaInicio_tramitetarea { get; set; }
        public Nullable<System.DateTime> FechaCierre_tramitetarea { get; set; }
        public Nullable<System.Guid> UsuarioAsignado_tramitetarea { get; set; }
        public Nullable<System.DateTime> FechaAsignacion_tramtietarea { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<int> id_proxima_tarea { get; set; }

        public Guid? UsuarioAsignado {get;set;}
        public string NomApe { get; set; }
    }
}
