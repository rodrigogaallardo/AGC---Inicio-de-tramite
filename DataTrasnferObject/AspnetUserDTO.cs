using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class AspnetUserDTO
    {
        public System.Guid ApplicationId { get; set; }
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public System.DateTime LastActivityDate { get; set; }

        public ICollection<RolesDTO> AspNetRoles { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public SGIProfileDTO SGIProfile { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
