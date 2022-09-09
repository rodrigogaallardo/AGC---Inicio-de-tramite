using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class wsEscribanosInstrumentoJudicialRepository : BaseRepository<wsEscribanos_InstrumentoJudicial> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosInstrumentoJudicialRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanos_InstrumentoJudicial> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains =(from ist in _unitOfWork.Db.wsEscribanos_InstrumentoJudicial
                          join der in _unitOfWork.Db.wsEscribanos_DerechoOcupacion on ist.id_derecho_ocupacion equals der.id_derecho_ocupacion
                          where der.id_actanotarial==id_actanotarial
                          select ist);
            return domains;
        }
    }
}

