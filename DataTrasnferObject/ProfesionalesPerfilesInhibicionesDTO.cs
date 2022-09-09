using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ProfesionalesPerfilesInhibicionesDTO
    {
        public int id { get; set; }
        public int id_Prof { get; set; }
        public System.Guid RoleId { get; set; }
        public string motivo { get; set; }
        public DateTime fecha_inhibicion { get; set; }
        public Nullable<DateTime> fecha_vencimiento { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<Guid> LastUpdateUser { get; set; }
        public Nullable<DateTime> LastUpdateDate { get; set; }
    }
}
