using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoSociedadRepository : BaseRepository<TipoSociedad> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoSociedadRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
	}
}

