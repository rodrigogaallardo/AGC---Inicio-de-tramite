using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronConformacionLocalRepository : BaseRepository<CPadron_ConformacionLocal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronConformacionLocalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_ConformacionLocal> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_ConformacionLocal> domains = _unitOfWork.Db.CPadron_ConformacionLocal.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;
		}
	}
}

