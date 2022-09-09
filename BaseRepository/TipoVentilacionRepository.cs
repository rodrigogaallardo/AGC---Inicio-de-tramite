using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoVentilacionRepository : BaseRepository<tipo_ventilacion> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoVentilacionRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
	}
}

