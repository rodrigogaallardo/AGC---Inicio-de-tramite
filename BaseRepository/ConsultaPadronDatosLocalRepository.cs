using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronDatosLocalRepository : BaseRepository<CPadron_DatosLocal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronDatosLocalRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_DatosLocal> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_DatosLocal> domains = _unitOfWork.Db.CPadron_DatosLocal.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_DatosLocal> GetByFKCreateUser(Guid CreateUser)
		{
			IEnumerable<CPadron_DatosLocal> domains = _unitOfWork.Db.CPadron_DatosLocal.Where(x => 													
														x.CreateUser == CreateUser											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_DatosLocal> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			IEnumerable<CPadron_DatosLocal> domains = _unitOfWork.Db.CPadron_DatosLocal.Where(x => 													
														x.LastUpdateUser == LastUpdateUser											
														);
	
			return domains;	
		}
	}
}

