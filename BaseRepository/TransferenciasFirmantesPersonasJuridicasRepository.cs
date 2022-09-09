using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasFirmantesPersonasJuridicasRepository : BaseRepository<Transf_Firmantes_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasFirmantesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Firmantes_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Firmantes_PersonasJuridicas> GetByFKIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica == IdPersonaJuridica);

            return domains;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasJuridicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<Transf_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasJuridicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<Transf_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasJuridicas> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			IEnumerable<Transf_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipocaracter == IdTipoCaracter											
														);
	
			return domains;	
		}      
	}
}

