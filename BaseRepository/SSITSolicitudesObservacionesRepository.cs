using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITSolicitudesObservacionesRepository : BaseRepository<SSIT_Solicitudes_Observaciones>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesObservacionesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesObservacionesEntity> GetByFKIdSolicitud(int id_solicitud)
        {
            List<int> lstIdTareas = new List<int>();

            lstIdTareas = (from t in _unitOfWork.Db.ENG_Tareas
                      where t.cod_tarea.ToString().Substring(t.cod_tarea.ToString().Length - 2, 2) == Constantes.ENG_Tipos_Tareas.Correccion_Solicitud
                      select t.id_tarea).ToList();

            DateTime maxFecha= new DateTime();
            var list = (from h in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                            join t in _unitOfWork.Db.SGI_Tramites_Tareas on h.id_tramitetarea equals t.id_tramitetarea
                            where h.id_solicitud == id_solicitud && lstIdTareas.Contains(t.id_tarea)
                            select t.FechaInicio_tramitetarea);
            if (list.Count() > 0)
                maxFecha = list.Max();
            var domains = (from obs in _unitOfWork.Db.SSIT_Solicitudes_Observaciones
                           join user in _unitOfWork.Db.SGI_Profiles on obs.CreateUser equals user.userid
                           where obs.id_solicitud == id_solicitud && obs.CreateDate <= maxFecha
                           select new SSITSolicitudesObservacionesEntity
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
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public bool ExistenObservacionesdetalleSinProcesar(int id_solicitud)
        {
            var domains = (from obs in _unitOfWork.Db.SSIT_Solicitudes_Observaciones
                           where obs.id_solicitud == id_solicitud && obs.leido == false
                           select obs.id_solobs);

            return domains.Count() > 0;
        }
    }
}

