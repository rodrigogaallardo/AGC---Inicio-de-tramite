using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class TransferenciasSolicitudesObservacionesRepository : BaseRepository<Transf_Solicitudes_Observaciones> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasSolicitudesObservacionesRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Solicitudes_Observaciones> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Solicitudes_Observaciones> domains = _unitOfWork.Db.Transf_Solicitudes_Observaciones.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}

        public IEnumerable<TransferenciasSolicitudesObservacionesEntity> GetByFKIdSolicitud2(int IdSolicitud)
        {
            List<int> lstIdTareas = new List<int>();

            lstIdTareas = (from t in _unitOfWork.Db.ENG_Tareas
                           where t.cod_tarea.ToString().Substring(t.cod_tarea.ToString().Length - 2, 2) == Constantes.ENG_Tipos_Tareas.Correccion_Solicitud
                           select t.id_tarea).ToList();

            DateTime maxFecha = new DateTime();
            var list = (from h in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF
                        join t in _unitOfWork.Db.SGI_Tramites_Tareas on h.id_tramitetarea equals t.id_tramitetarea
                        where h.id_solicitud == IdSolicitud && lstIdTareas.Contains(t.id_tarea)
                        select t.FechaInicio_tramitetarea);
            if (list.Count() > 0)
                maxFecha = list.Max();
            var domains = (from obs in _unitOfWork.Db.Transf_Solicitudes_Observaciones
                           join user in _unitOfWork.Db.SGI_Profiles on obs.CreateUser equals user.userid
                           where obs.id_solicitud == IdSolicitud && obs.CreateDate <= maxFecha
                           select new TransferenciasSolicitudesObservacionesEntity
                           {
                               id_solobs = obs.id_solobs,
                               id_solicitud = obs.id_solicitud,
                               leido = obs.leido != null ? obs.leido.Value : false,
                               CreateDate = obs.CreateDate,
                               CreateUser = obs.CreateUser,
                               observaciones = obs.observaciones,
                               userApeNom = user.Apellido + ", " + user.Nombres
                           });

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        //public IEnumerable<TransferenciasSolicitudesObservacionesEntity> Get(int IdSolicitud)
        //{
        //    var query = (from obs in _unitOfWork.Db.Transf_Solicitudes_Observaciones
        //                 join user in _unitOfWork.Db.Usuario on obs.CreateUser equals user.UserId
        //                 into uservar
        //                 from j in uservar.DefaultIfEmpty()
        //                 join sgi_p in _unitOfWork.Db.SGI_Profiles on obs.CreateUser equals sgi_p.userid
        //                 into sgi_var
        //                 from p in sgi_var.DefaultIfEmpty()
        //                 where obs.id_solicitud == IdSolicitud
        //                 select new TransferenciasSolicitudesObservacionesEntity()
        //                 {
        //                     Id = obs.id_solobs ,
        //                     CreateDate = obs.CreateDate,
        //                     CreateUser = obs.CreateUser,
        //                     IdSolicitud = obs.id_solicitud,
        //                     Leido =  obs.leido,
        //                     Observaciones = obs.observaciones,
        //                     NombreCompleto = (string.IsNullOrEmpty(j.Apellido) ? p.Apellido : j.Apellido) + " , " + (string.IsNullOrEmpty(j.Nombre) ? p.Nombres : j.Nombre)
        //                 });
        //    return query;
        //}
    }
}

