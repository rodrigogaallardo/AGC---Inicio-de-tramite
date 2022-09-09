using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasTitularesSolicitudPersonasJuridicasRepository : BaseRepository<Transf_Titulares_Solicitud_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasTitularesSolicitudPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.id_solicitud == IdSolicitud										
														);
	
			return domains;	
		}

        public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica == IdPersonaJuridica
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoSociedad"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByFKIdTipoSociedad(int IdTipoSociedad)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.Id_TipoSociedad == IdTipoSociedad											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.id_localidad == IdLocalidad											
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
        public IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> GetByIdSolicitudCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {

            IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas.Where(x =>
                                                      x.id_solicitud == IdSolicitud
                                                      && x.id_personajuridica != IdPersonaJuridica
                                                      && x.CUIT.Equals(Cuit) 
                                                      );

            return domains;
        }
	}
}

