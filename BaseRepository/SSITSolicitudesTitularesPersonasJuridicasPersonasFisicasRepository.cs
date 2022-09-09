using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository : BaseRepository<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;
		}
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x =>
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
		public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocPersonal											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdFirmantePj"></param>
		/// <returns></returns>	
		public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdFirmantePj(int IdFirmantePj)
		{
			IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_firmante_pj == IdFirmantePj											
														);
	
			return domains;
		}
	}
}

