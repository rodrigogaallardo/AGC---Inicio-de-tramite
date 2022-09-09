using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesFirmantesPersonasJuridicasRepository : BaseRepository<SSIT_Solicitudes_Firmantes_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesFirmantesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;
		}
        public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
		{
            IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica == IdPersonaJuridica
                                                        );
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetCargoFirmantesPersonasJuridicas()
        {
            var query = (from cfpj in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas
                         where cfpj.cargo_firmante_pj.Trim().Length > 0
                         select cfpj.cargo_firmante_pj).Distinct().OrderBy(x => x).ToArray();
            return query;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocPersonal											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipocaracter == IdTipoCaracter											
														);
	
			return domains;
		}
	}
}

