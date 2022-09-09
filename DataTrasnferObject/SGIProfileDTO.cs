using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SGIProfileDTO
    {
        public System.Guid userid { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string UserName_SADE { get; set; }
        public string UserName_SIPSA { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string Reparticion_SADE { get; set; }
    }
}
