using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RolesDTO
    {
        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }

        public ICollection<GrupoConsejosRolesClasificacionDTO> RolesGruposClasificacion {get;set;}
        public UsuariosProfesionalesRolesClasificacionDTO GruposUsuariosClasificacion { get; set; }
        public ICollection<EncomiendaURLxROLDTO> EncomiendaURLDTO { get; set; }
    }
}
