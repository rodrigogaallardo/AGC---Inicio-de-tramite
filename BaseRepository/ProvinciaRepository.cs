using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ProvinciaRepository : BaseRepository<Provincia> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ProvinciaRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<Provincia> GetProvincias()
        {
            return _unitOfWork.Db.Provincia.Where(x => x.Id > 0);
            
        }

	}
}

