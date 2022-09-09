using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesSolicitudPersonasFisicasRepository : BaseRepository<CPadron_Titulares_Solicitud_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronTitularesSolicitudPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocumentoPersonal											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas.Where(x => 													
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
        public IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> GetByIdConsultaPadronCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {

            IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas.Where(x =>
                                               x.Cuit.Equals(Cuit) 
                                               && x.id_cpadron == IdSolicitud
                                               && x.id_personafisica != IdPersonaFisica
                                               );

            return domains;	
        }
	}
}

