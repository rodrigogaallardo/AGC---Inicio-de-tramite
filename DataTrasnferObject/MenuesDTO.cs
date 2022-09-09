using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class MenuesDTO
    {
        public int id_menu { get; set; }
        public string descripcion_menu { get; set; }
        public string aclaracion_menu { get; set; }
        public string pagina_menu { get; set; }
        public string iconCssClass_menu { get; set; }
        public Nullable<int> id_menu_padre { get; set; }
        public int nroOrden { get; set; }
        public bool visible { get; set; }

        /*Auditoria*/
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }        

        public IEnumerable<PerfilesDTO> PerfilesDtoCollection{ get; set; }
 
    }
}
