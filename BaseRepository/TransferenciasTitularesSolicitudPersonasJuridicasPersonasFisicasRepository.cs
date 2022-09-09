using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository : BaseRepository<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}

        public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x =>
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
        public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
	}
}

