using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class TareasEntity
    {
        public string Descripcion { get; set; }
        public Nullable<DateTime> FechaCreacion { get; set; }
        public Nullable<DateTime> FechaAsignacion { get; set; }
        public Nullable<DateTime> FechaFinalizacion { get; set; }
        public Nullable<Guid> UsuarioAsignado { get; set; }
        public string UserName { get; set; }
        public string ApenomUsuario { get; set; }
        public int id_tramitetarea { get; set; }
    }
}
