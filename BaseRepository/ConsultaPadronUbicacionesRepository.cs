using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronUbicacionesRepository : BaseRepository<CPadron_Ubicaciones> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronUbicacionesRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<CPadron_Ubicaciones> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            var query = (from cu in _unitOfWork.Db.CPadron_Ubicaciones
                         where cu.id_cpadron == IdConsultaPadron
                         select cu);
                        
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<CPadron_Ubicaciones> GetByFKIdUbicacion(int IdUbicacion)
        {
            IEnumerable<CPadron_Ubicaciones> domains = _unitOfWork.Db.CPadron_Ubicaciones.Where(x =>
                                                        x.id_ubicacion == IdUbicacion
                                                        );

            return domains;
        }
	}
}

