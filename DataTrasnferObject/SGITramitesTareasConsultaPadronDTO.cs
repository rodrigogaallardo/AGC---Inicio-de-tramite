using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SGITramitesTareasConsultaPadronDTO : ICloneable
    {
        public int id_rel_tt_CPADRON { get; set; }
        public int id_tramitetarea { get; set; }
        public int id_cpadron { get; set; }

        public object Clone()
        {
            return new SGITramitesTareasConsultaPadronDTO()
            {
                id_rel_tt_CPADRON = this.id_rel_tt_CPADRON,
                id_tramitetarea = this.id_tramitetarea,
                id_cpadron = this.id_cpadron
            };
        }
    }
}
