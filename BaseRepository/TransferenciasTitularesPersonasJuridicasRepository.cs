using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasTitularesPersonasJuridicasRepository : BaseRepository<Transf_Titulares_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasTitularesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x => 													
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
        public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica == IdPersonaJuridica
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByIdTransferenciasCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica != IdPersonaJuridica
                                                        && x.CUIT.Equals(Cuit) 
                                                        );

            return domains;
        }
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x => 													
														x.id_localidad == IdLocalidad											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByFKCreateUser(Guid CreateUser)
		{
			IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x => 													
														x.CreateUser == CreateUser											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_PersonasJuridicas> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			IEnumerable<Transf_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_PersonasJuridicas.Where(x => 													
														x.LastUpdateUser == LastUpdateUser											
														);
	
			return domains;	
		}
	}
}

