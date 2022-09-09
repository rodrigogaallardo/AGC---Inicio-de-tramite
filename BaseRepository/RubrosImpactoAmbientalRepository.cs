using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class RubrosImpactoAmbientalRepository : BaseRepository<Rel_Rubros_ImpactoAmbiental> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public RubrosImpactoAmbientalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdImpactoAmbiental"></param>
		/// <returns></returns>	
		public IEnumerable<Rel_Rubros_ImpactoAmbiental> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
		{
			IEnumerable<Rel_Rubros_ImpactoAmbiental> domains = _unitOfWork.Db.Rel_Rubros_ImpactoAmbiental.Where(x => 													
														x.id_ImpactoAmbiental == IdImpactoAmbiental											
														);
	
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public IEnumerable<Rel_Rubros_ImpactoAmbiental> GetByFKIdRubro(int IdRubro)
        {
			IEnumerable<Rel_Rubros_ImpactoAmbiental> domains = _unitOfWork.Db.Rel_Rubros_ImpactoAmbiental.Where(x => 													
														x.id_rubro == IdRubro
														);
	
			return domains;
		}
	}
}

