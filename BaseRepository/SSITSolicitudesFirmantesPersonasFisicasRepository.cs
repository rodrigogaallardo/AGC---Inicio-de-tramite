using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesFirmantesPersonasFisicasRepository : BaseRepository<SSIT_Solicitudes_Firmantes_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesFirmantesPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> GetByIdSolicitudIdPersonaFisica(int id_solicitud, int IdPersonaFisica)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == id_solicitud
											            && x.id_personafisica == IdPersonaFisica
														);
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> GetByFKIdPersonaFisica(int IdPersonaFisica)
        {
            IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;
		}		
	}
}

