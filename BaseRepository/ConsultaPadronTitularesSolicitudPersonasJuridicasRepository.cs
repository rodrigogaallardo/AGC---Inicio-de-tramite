using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesSolicitudPersonasJuridicasRepository : BaseRepository<CPadron_Titulares_Solicitud_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSociedad"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> GetByFKIdTipoSociedad(int IdTipoSociedad)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.Id_TipoSociedad == IdTipoSociedad											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
														x.id_tipoiibb == IdTipoiibb											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas.Where(x => 													
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
        public IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> GetByIdConsultaPadronCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {

            IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas.Where(x =>
                                                      x.id_cpadron == IdSolicitud
                                                      && x.id_personajuridica != IdPersonaJuridica
                                                      && x.CUIT.Equals(Cuit) 
                                                      );

            return domains;
        }
	}
}

