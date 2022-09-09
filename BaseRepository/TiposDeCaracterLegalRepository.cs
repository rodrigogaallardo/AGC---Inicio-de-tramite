using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TiposDeCaracterLegalRepository : BaseRepository<TiposDeCaracterLegal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TiposDeCaracterLegalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<TiposDeCaracterLegal> GetByDisponibilidad(int[] disponibilidad)
        {
            var query = (from tcl in _unitOfWork.Db.TiposDeCaracterLegal
                         where disponibilidad.Contains(tcl.disponibilidad_tipocaracter)
                         select tcl);
            return query;
        }
	}
}

