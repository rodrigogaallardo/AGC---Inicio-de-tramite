using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class GrupoConsejosRolesClasificacionDTO
    {
        public int IdClasificacion { get; set; }
        public Guid RoleId { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
