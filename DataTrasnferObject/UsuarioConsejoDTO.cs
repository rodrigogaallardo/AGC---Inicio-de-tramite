using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class UsuarioConsejoDTO
    {
        public int Id { get;set; }
        public Guid UserId { get; set; }
        public int IdConsejo { get; set; }
    }
}
