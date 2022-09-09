using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoTramiteRepository : BaseRepository<TipoTramite> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoTramiteRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<TipoTramite> getExcluirIdTipoTramite(List<int> lstIdTipoTramite)
        {
            IEnumerable<TipoTramite> domains = _unitOfWork.Db.TipoTramite.Where(x => !lstIdTipoTramite.Contains(x.id_tipotramite));
            return domains;
        }
	}
}

