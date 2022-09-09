using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UbicacionesInhibicionesRepository : BaseRepository<Ubicaciones_Inhibiciones> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public UbicacionesInhibicionesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Ubicaciones_Inhibiciones> GetByFKIdUbicacion(int IdUbicacion)
		{
			IEnumerable<Ubicaciones_Inhibiciones> domains = _unitOfWork.Db.Ubicaciones_Inhibiciones.Where(x => 													
														x.id_ubicacion == IdUbicacion											
														);
	
			return domains;
		}
	}
}

