using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasRepository : BaseRepository<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
	}
}

