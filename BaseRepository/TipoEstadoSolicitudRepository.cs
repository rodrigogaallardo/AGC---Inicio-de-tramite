using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoEstadoSolicitudRepository : BaseRepository<TipoEstadoSolicitud> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoEstadoSolicitudRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
	}
}

