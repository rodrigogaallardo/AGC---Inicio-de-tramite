using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
        public class InstructivosDTO
        {
            public int id_instructivo { get; set; }
            public string cod_instructivo { get; set; }
            public int id_file { get; set; }
            public System.DateTime CreateDate { get; set; }
            public System.Guid CreateUser { get; set; }
            public Nullable<System.DateTime> LastUpdateDate { get; set; }
            public Nullable<System.Guid> LastUpdateUser { get; set; }

         
        }
    }

