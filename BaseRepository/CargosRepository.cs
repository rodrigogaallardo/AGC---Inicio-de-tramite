using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class CargosRepository : BaseRepository<Provincia> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public CargosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<Cargos> GetCargos()
        {
            return _unitOfWork.Db.Cargos.Where(x => x.id_cargo > 0);
        }

	}
}

