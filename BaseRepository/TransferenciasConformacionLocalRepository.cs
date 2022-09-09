using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasConformacionLocalRepository : BaseRepository<Transf_ConformacionLocal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasConformacionLocalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_ConformacionLocal> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_ConformacionLocal> domains = _unitOfWork.Db.Transf_ConformacionLocal.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;
		}
	}
}

