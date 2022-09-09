using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class wsEscribanosInstrumentoPublicoRepository : BaseRepository<wsEscribanos_InstrumentoPublico> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosInstrumentoPublicoRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanos_InstrumentoPublico> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains = (from ist in _unitOfWork.Db.wsEscribanos_InstrumentoPublico
                           join der in _unitOfWork.Db.wsEscribanos_DerechoOcupacion on ist.id_derecho_ocupacion equals der.id_derecho_ocupacion
                           where der.id_actanotarial == id_actanotarial
                           select ist);
            return domains;
        }
    }
}

