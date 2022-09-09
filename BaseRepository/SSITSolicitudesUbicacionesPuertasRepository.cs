using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesUbicacionesPuertasRepository : BaseRepository<SSIT_Solicitudes_Ubicaciones_Puertas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesUbicacionesPuertasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSSITSolicitudesUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Ubicaciones_Puertas> GetByFKIdSolicitudUbicacion(int id_solicitudubicacion)
		{
			IEnumerable<SSIT_Solicitudes_Ubicaciones_Puertas> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Puertas.Where(x => 													
														x.id_solicitudubicacion == id_solicitudubicacion
                                                        );
	
			return domains;
		}
	}
}

