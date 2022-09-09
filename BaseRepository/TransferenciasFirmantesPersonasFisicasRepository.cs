using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasFirmantesPersonasFisicasRepository : BaseRepository<Transf_Firmantes_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasFirmantesPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Firmantes_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Firmantes_PersonasFisicas> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
        {
            IEnumerable<Transf_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }      
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaFisica"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasFisicas> GetByFKIdPersonaFisica(int IdPersonaFisica)
		{
			IEnumerable<Transf_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasFisicas.Where(x => 													
														x.id_personafisica == IdPersonaFisica											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasFisicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<Transf_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Firmantes_PersonasFisicas> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			IEnumerable<Transf_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_PersonasFisicas.Where(x => 													
														x.id_tipocaracter == IdTipoCaracter											
														);
	
			return domains;	
		}
	}
}

