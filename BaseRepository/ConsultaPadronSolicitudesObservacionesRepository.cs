using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class ConsultaPadronSolicitudesObservacionesRepository : BaseRepository<CPadron_Solicitudes_Observaciones> 
    {
        /// <summary>
        /// 
        /// </summary>
		private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
		public ConsultaPadronSolicitudesObservacionesRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Solicitudes_Observaciones> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Solicitudes_Observaciones> domains = _unitOfWork.Db.CPadron_Solicitudes_Observaciones.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Solicitudes_Observaciones> Get(int IdConsultaPadron)
        {
            return (from obs in _unitOfWork.Db.CPadron_Solicitudes_Observaciones
                    join user in _unitOfWork.Db.Usuario on obs.CreateUser equals user.UserId
                    into u
                    from user in u.DefaultIfEmpty()
                    join usr in _unitOfWork.Db.aspnet_Users on obs.CreateUser equals usr.UserId
                    into usr_d
                    from usr in usr_d.DefaultIfEmpty()
                    join usr_sgi in _unitOfWork.Db.SGI_Profiles on usr.UserId equals usr_sgi.userid
                    into sgi
                    from usr_sgi in sgi.DefaultIfEmpty()
                    where obs.id_cpadron == IdConsultaPadron
                    && obs.CreateDate <=
                    (
                       from ha in _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON
                       join ta in _unitOfWork.Db.SGI_Tramites_Tareas on ha.id_tramitetarea equals ta.id_tramitetarea
                       where
                       ha.id_cpadron == IdConsultaPadron
                       && ta.id_tarea == (int)Constantes.ENG_Tareas.CP_Correccion_Solicitud
                       select ta.FechaInicio_tramitetarea
                    ).Max()
                    select obs);
        }
	}
}

