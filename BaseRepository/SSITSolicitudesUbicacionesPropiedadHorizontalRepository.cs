using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesUbicacionesPropiedadHorizontalRepository : BaseRepository<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesUbicacionesPropiedadHorizontalRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> GetByFKIdSolicitudUbicacion(int id_solicitudubicacion)
		{
			IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_solicitudubicacion == id_solicitudubicacion
                                                        );
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPropiedadHorizontal"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal)
		{
			IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_propiedadhorizontal == IdPropiedadHorizontal											
														);
	
			return domains;
		}
	}
}

