using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UbicacionesPropiedadhorizontalRepository : BaseRepository<Ubicaciones_PropiedadHorizontal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public UbicacionesPropiedadhorizontalRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Ubicaciones_PropiedadHorizontal> GetByFKIdUbicacion(int IdUbicacion)
		{
			IEnumerable<Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_ubicacion == IdUbicacion && x.baja_logica == false											
														);
	
			return domains;
		}
	}
}

