using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository.Engine
{
    public class SGITramitesTareasepository : BaseRepository<SGI_Tramites_Tareas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SGITramitesTareasepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public Guid GetUltimoUsuarioAsignado(int id_solicitud, int id_tarea)
        {

            Guid? userid = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                            join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                            where tt_hab.id_solicitud == id_solicitud && tt.id_tarea == id_tarea && tt.FechaCierre_tramitetarea.HasValue
                            orderby tt.FechaCierre_tramitetarea descending
                            select tt.UsuarioAsignado_tramitetarea).FirstOrDefault();

            Guid ret = Guid.Empty;
            if (userid.HasValue)
                ret = (Guid)userid;

            return ret;

        }

        /// <summary>
        /// devuelve una coleccion de perfiles permitidopor tarea
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IEnumerable<string> PerfilesPermitidosPorTarea(int idTramiteTarea)
        {

            var perfilesPermitidos = (
                from perfiles_tarea in _unitOfWork.Db.ENG_Rel_Perfiles_Tareas
                join perfiles in _unitOfWork.Db.SGI_Perfiles on perfiles_tarea.id_perfil equals perfiles.id_perfil
                join tareas in _unitOfWork.Db.SGI_Tramites_Tareas on perfiles_tarea.id_tarea equals tareas.id_tarea
                //where perfiles_tarea.id_tarea == id_tramitetarea
                where tareas.id_tramitetarea == idTramiteTarea
                select perfiles.nombre_perfil);

            return perfilesPermitidos;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <returns></returns>
        public IEnumerable<SGITramitesTareasEntity> GetProfile(int idTramiteTarea)
        {
            var result =
                (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                 join prof in _unitOfWork.Db.SGI_Profiles on tt.UsuarioAsignado_tramitetarea equals prof.userid
                 into w
                 from j in w
                 where tt.id_tramitetarea == idTramiteTarea
                 select new SGITramitesTareasEntity()
                 {
                     UsuarioAsignado = tt.UsuarioAsignado_tramitetarea,
                     NomApe = j.Nombres + " " + j.Apellido,
                     CreateUser = tt.CreateUser,
                     FechaAsignacion_tramtietarea = tt.FechaAsignacion_tramtietarea,
                     FechaCierre_tramitetarea = tt.FechaCierre_tramitetarea,
                     FechaInicio_tramitetarea = tt.FechaInicio_tramitetarea,
                     id_proxima_tarea = tt.id_proxima_tarea,
                     id_resultado = tt.id_resultado,
                     id_tarea = tt.id_tarea,
                     id_tramitetarea = tt.id_tramitetarea,
                     UsuarioAsignado_tramitetarea = tt.UsuarioAsignado_tramitetarea
                 });

            return result;
        }
    }

}

