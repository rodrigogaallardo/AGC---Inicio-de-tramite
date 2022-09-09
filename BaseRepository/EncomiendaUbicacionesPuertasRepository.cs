using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaUbicacionesPuertasRepository : BaseRepository<Encomienda_Ubicaciones_Puertas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaUbicacionesPuertasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Encomienda_Ubicaciones_Puertas> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
		{
			IEnumerable<Encomienda_Ubicaciones_Puertas> domains = _unitOfWork.Db.Encomienda_Ubicaciones_Puertas.Where(x => 													
														x.id_encomiendaubicacion == IdEncomiendaUbicacion											
														);
	
			return domains;
		}
	}
}

