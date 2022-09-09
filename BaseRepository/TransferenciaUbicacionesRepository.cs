using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciaUbicacionesRepository : BaseRepository<Transf_Ubicaciones> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciaUbicacionesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Ubicaciones> GetByFKIdSolicitud(int IdSolicitud)
        {
            var query = (from tu in _unitOfWork.Db.Transf_Ubicaciones
                         where tu.id_solicitud == IdSolicitud
                         select tu);
                        
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Ubicaciones> GetByFKIdUbicacion(int IdUbicacion)
        {
            IEnumerable<Transf_Ubicaciones> domains = _unitOfWork.Db.Transf_Ubicaciones.Where(x =>
                                                        x.id_ubicacion == IdUbicacion
                                                        );

            return domains;
        }
	}
}

