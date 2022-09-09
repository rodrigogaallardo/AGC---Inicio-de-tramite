using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class wsEscribanosInstrumentoPrivadoRepository : BaseRepository<wsEscribanos_InstrumentoPrivado> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosInstrumentoPrivadoRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanos_InstrumentoPrivado> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains = (from ist in _unitOfWork.Db.wsEscribanos_InstrumentoPrivado
                           join der in _unitOfWork.Db.wsEscribanos_DerechoOcupacion on ist.id_derecho_ocupacion equals der.id_derecho_ocupacion
                           where der.id_actanotarial == id_actanotarial
                           select ist);
            return domains;
        }
    }
}

