using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class Encomienda_RubrosCN_DepositoDTO
    {
        public int id_encomienda { get; set; }
        public int IdRubro { get; set; }
        public int IdDeposito { get; set; }

        public virtual EncomiendaDTO Encomienda { get; set; }
        public virtual RubrosDepositosCNDTO RubrosDepositosCNDTO { get; set; }
        public virtual RubrosCNDTO RubrosCNDTO { get; set; }
    }
}
 

