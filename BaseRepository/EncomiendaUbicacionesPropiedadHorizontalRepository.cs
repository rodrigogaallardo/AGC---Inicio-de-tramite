using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaUbicacionesPropiedadHorizontalRepository : BaseRepository<Encomienda_Ubicaciones_PropiedadHorizontal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaUbicacionesPropiedadHorizontalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
		{
			IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.Encomienda_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_encomiendaubicacion == IdEncomiendaUbicacion											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPropiedadHorizontal"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal)
		{
			IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.Encomienda_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_propiedadhorizontal == IdPropiedadHorizontal											
														);
	
			return domains;
		}
	}
}

