using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasTitularesPersonasFisicasRepository : BaseRepository<Transf_Titulares_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasTitularesPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Titulares_PersonasFisicas> GetByIdTransferenciasCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
                                                        && x.Cuit == Cuit
                                                        && x.id_personafisica != IdPersonaFisica
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
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
        public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud
                                                        && x.id_personafisica == IdPersonaFisica
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipodocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKIdTipodocPersonal(int IdTipodocPersonal)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipodocPersonal											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.id_Localidad == IdLocalidad											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKCreateUser(Guid CreateUser)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.CreateUser == CreateUser											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasFisicas> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			IEnumerable<Transf_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasFisicas.Where(x => 													
														x.LastUpdateUser == LastUpdateUser											
														);
	
			return domains;	
		}
	}
}

