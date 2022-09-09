using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoDocumentoPersonalRepository : BaseRepository<TipoDocumentoPersonal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoDocumentoPersonalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        public IEnumerable<TipoDocumentoPersonal> GetDniPasaporte()
        {
           // return dbSet.AsEnumerable();
            var lstTipoDocPersonal = (from td in _unitOfWork.Db.TipoDocumentoPersonal where (td.Descripcion == "Documento Nacional de Identidad") || (td.Descripcion == "Pasaporte") select td);
            return lstTipoDocPersonal;
        }
	}
}

