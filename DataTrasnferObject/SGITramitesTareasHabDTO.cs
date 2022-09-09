using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SGITramitesTareasHabDTO : ICloneable
    {
        public int id_rel_tt_HAB { get; set; }
        public int id_tramitetarea { get; set; }
        public int id_solicitud { get; set; }

        public object Clone()
        {
            return new SGITramitesTareasHabDTO()
            {
                id_rel_tt_HAB = this.id_rel_tt_HAB,
                id_tramitetarea = this.id_tramitetarea,
                id_solicitud = this.id_solicitud
            };
        }
    }
}
