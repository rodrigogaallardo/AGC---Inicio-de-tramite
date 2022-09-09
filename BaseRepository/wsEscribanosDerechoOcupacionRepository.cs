using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class wsEscribanosDerechoOcupacionRepository : BaseRepository<wsEscribanos_DerechoOcupacion> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosDerechoOcupacionRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanos_DerechoOcupacion> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains = _unitOfWork.Db.wsEscribanos_DerechoOcupacion.Where(x=>x.id_actanotarial==id_actanotarial);
            return domains;
        }
    }
}

