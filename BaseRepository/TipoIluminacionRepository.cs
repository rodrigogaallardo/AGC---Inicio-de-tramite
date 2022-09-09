using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoIluminacionRepository : BaseRepository<tipo_iluminacion> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoIluminacionRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
	}
}

