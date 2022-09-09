using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasTitularesSolicitudPersonasFisicasRepository : BaseRepository<Transf_Titulares_Solicitud_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasTitularesSolicitudPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}

        public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
        {
            IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoDocumentoPersonal"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.Id_Localidad == IdLocalidad											
														);
	
			return domains;	
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> GetByIdSolicitudCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {

            IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas.Where(x =>
                                               x.Cuit.Equals(Cuit) 
                                               && x.id_solicitud == IdSolicitud
                                               && x.id_personafisica != IdPersonaFisica
                                               );

            return domains;	
        }
	}
}

