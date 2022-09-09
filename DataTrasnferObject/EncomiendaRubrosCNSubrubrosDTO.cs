using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class EncomiendaRubrosCNSubrubrosDTO
    {
        public int Id_EncRubCNSubrubros { get; set; }
        public int Id_EncRubro { get; set; }
        public int Id_rubrosubrubro { get; set; }

        //public virtual Encomienda_RubrosCN Encomienda_RubrosCN { get; set; }
        public virtual RubrosCNSubRubrosDTO rubrosCNSubRubrosDTO { get; set; }
    }
}
