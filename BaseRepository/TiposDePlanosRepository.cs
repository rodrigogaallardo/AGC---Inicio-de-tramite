using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TiposDePlanosRepository : BaseRepository<TiposDePlanos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TiposDePlanosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
	}
}

