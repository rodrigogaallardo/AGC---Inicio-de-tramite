using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class wsEscribanosActaNotarialRepository : BaseRepository<wsEscribanos_ActaNotarial> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosActaNotarialRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanos_ActaNotarial> GetByFKListIdEncomienda(List<int> list)
        {
            IEnumerable<wsEscribanos_ActaNotarial> domains = (from ac in _unitOfWork.Db.wsEscribanos_ActaNotarial                                                                  
                                                                  where list.Contains(ac.id_encomienda)
                                                                  select ac);
            return domains;
        }

        public wsEscribanos_ActaNotarial GetByFKIdEncomienda(int id_encomienda)
        {
            var domains = _unitOfWork.Db.wsEscribanos_ActaNotarial.Where(x => x.id_encomienda== id_encomienda).FirstOrDefault();
            return domains;
        }

        public bool CopiarDesdeAPRA(int id_encomienda, int id_caa )
        {
            int ret = _unitOfWork.Db.wsEscribanos_CopiarDeAPRA(id_encomienda, id_caa);

            return (ret > 0);
        }


    }
}

