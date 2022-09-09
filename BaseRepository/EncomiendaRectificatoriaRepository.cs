using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaRectificatoriaRepository : BaseRepository<Encomienda_Rectificatoria> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaRectificatoriaRepository(IUnitOfWork unit) : base(unit)
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
		public Encomienda_Rectificatoria GetByFKIdEncomienda(int IdEncomienda)
		{
            var domains = _unitOfWork.Db.Encomienda_Rectificatoria.FirstOrDefault(x => x.id_encomienda_nueva == IdEncomienda);
	
			return domains;
		}
    }
}

