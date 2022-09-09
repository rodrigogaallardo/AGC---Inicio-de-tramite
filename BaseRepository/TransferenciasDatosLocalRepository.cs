using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasDatosLocalRepository : BaseRepository<Transf_DatosLocal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasDatosLocalRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_DatosLocal> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_DatosLocal> domains = _unitOfWork.Db.Transf_DatosLocal.Where(x => 													
														x.id_solicitud == IdSolicitud
                                                        );
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_DatosLocal> GetByFKCreateUser(Guid CreateUser)
		{
			IEnumerable<Transf_DatosLocal> domains = _unitOfWork.Db.Transf_DatosLocal.Where(x => 													
														x.CreateUser == CreateUser											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_DatosLocal> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			IEnumerable<Transf_DatosLocal> domains = _unitOfWork.Db.Transf_DatosLocal.Where(x => 													
														x.LastUpdateUser == LastUpdateUser											
														);
	
			return domains;	
		}
	}
}

