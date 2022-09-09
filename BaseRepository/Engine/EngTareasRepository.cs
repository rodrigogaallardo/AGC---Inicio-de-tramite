using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;
using System.Data.Entity.Core.Objects;
using StaticClass;
using System.Configuration;
using DataAcess.EntityCustom;

namespace BaseRepository.Engine
{
    public class EngTareasRepository : BaseRepository<ENG_Tareas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EngTareasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }


        /// <summary>
        /// Controla si una tarea Ya esta asignada 
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <returns>False si la tarea NO esta asignada </returns>
        public bool ControldeTareaAsignada(int idTramiteTarea)
        {
            try
            {
                var tareaAsignada = from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                                    join prof in _unitOfWork.Db.SGI_Profiles on tt.UsuarioAsignado_tramitetarea equals prof.aspnet_Users.UserId
                                    select new
                                    {
                                        UsuarioAsignado = tt.UsuarioAsignado_tramitetarea,
                                        NombreApellido = prof.Nombres + " " + prof.Apellido
                                    };

                //si la tarea no tiene usuario es nula 
                if (tareaAsignada == null)
                    return false;
                else
                    return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Call to Store procedure ENG_Bandeja_Asignar and try to assign WORK ITEM
        /// </summary>
        /// <param name="id_tramitetarea">Numero de la tarea a signar</param>
        /// <param name="userIdTOAsig">Usuario A asignar</param>
        /// <param name="userIdAsignador">usuario que assigna</param>
        /// <returns></returns>
        public int AsignarTarea(int idTramiteTarea, Guid? userIdTOAsig, Guid? userIdAsignador)
        {
            try
            {
                var result = _unitOfWork.Db.ENG_Bandeja_Asignar(idTramiteTarea, userIdTOAsig, userIdAsignador);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Call to Store procedure SGI_ResignarTarea and try to Re-assign WORK ITEM
        /// </summary>
        /// <param name="id_tramitetarea">Numero de la tarea a signar</param>
        /// <param name="userIdTOAsig">Usuario A asignar</param>        
        /// <returns>int if cant assign</returns>
        public int ResignarTarea(int idTramiteTarea, Guid userIdTOAsig)
        {
            try
            {
                var result = _unitOfWork.Db.SGI_ResignarTarea(idTramiteTarea, userIdTOAsig).SingleOrDefault();
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// devuelve la lista de las tareas siguientes esto se desarrollo de esta forma dado el acoplamiento del ENGINE con la aplicacion. y el uso de MUCHA logica en los SP 
        /// 
        /// </summary>
        /// <param name="id_resultado"></param>
        /// <param name="id_tarea_actual"></param>
        /// <param name="id_tramitetarea"></param>
        /// <returns></returns>
        public IEnumerable<ENG_Tareas> SelectAllNExtTareas(int id_tarea_actual, int id_resultado, int id_tramitetarea)
        {

            try
            {
                /*Codigo anterior Copiado Tal cual*/
                List<ENG_Tareas> lstTareas = new List<ENG_Tareas>();
                ObjectResult<ENG_GetTransicionesxResultado_Result> objResult = _unitOfWork.Db.ENG_GetTransicionesxResultado(id_tarea_actual, id_resultado, id_tramitetarea);
                List<ENG_GetTransicionesxResultado_Result> lstResult = objResult.OrderBy(x => x.nombre_tarea).ToList();

                foreach (ENG_GetTransicionesxResultado_Result item in lstResult)
                {
                    lstTareas.Add(GetTareaByIdAndTramiteId(Convert.ToInt32(item.id_tarea), id_tramitetarea));
                }

                return lstTareas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la tarea en base al ID y al tramite id
        /// </summary>
        /// <param name="idTarea"></param>
        /// <param name="id_tramitetarea"></param>
        public ENG_Tareas GetTareaByIdAndTramiteId(int idTarea, int idTramiteTarea)
        {
            ENG_Tareas tareas = _unitOfWork.Db.ENG_Tareas.FirstOrDefault(x => x.id_tarea == idTarea);
            return tareas;
        }
        /// <summary>
        /// Crea una nueva Tarea y devuelve la tarea CReADA! 
        /// </summary>
        /// <param name="idTramite"></param>
        /// <param name="idTarea"></param>
        /// <param name="createUser"></param>
        /// <param name="idTramitetarea"></param>
        /// <returns></returns>
        public int CreateTarea(int? idTramite, int? idTarea, Guid? createUser)
        {

            try
            {
                ObjectParameter output = new ObjectParameter("id_tramitetarea", typeof(int));
                var tareaNueva = _unitOfWork.Db.ENG_Crear_Tarea(idTramite, idTarea, createUser, output);
                if (output != null && Convert.ToInt32(output.Value) > 0)
                    return Convert.ToInt32(output.Value);
                else
                    throw new Exception("No se asignaron Valores de retorno a la ejecucion de la creacion de tarea");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int GetIdCircuito(int id_tipotramite, int id_tipoexpediente, int id_subtipoexpediente, decimal version_circuito)
        {
            try
            {
                var circuito = (from cir in _unitOfWork.Db.ENG_Circuitos
                                join rel in _unitOfWork.Db.ENG_Rel_Circuitos_TiposDeTramite on cir.id_circuito equals rel.id_circuito
                                where rel.id_tipotramite == id_tipotramite && rel.id_tipoexpediente == id_tipoexpediente
                                && rel.id_subtipoexpediente == id_subtipoexpediente
                                && cir.version_circuito == version_circuito
                                orderby rel.id_circuito descending
                                select cir).FirstOrDefault();
                return circuito.id_circuito;
            }
            catch (Exception)
            {
                throw new Exception("No se encontro el ciruito correspondiente.");
            }
        }

        /// <summary>
        /// Obtiene un ID de tarea dado su codigo
        /// </summary>
        /// <param name="cod_tarea"></param>
        /// <returns></returns>
        public int GetIdTarea(int cod_tarea)
        {
            try
            {
                var tarea = _unitOfWork.Db.ENG_Tareas.Where(x => x.cod_tarea == cod_tarea).FirstOrDefault();
                return tarea != null ? tarea.id_tarea : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Finaliza TAREA! 
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <param name="idResultado"></param>
        /// <param name="idProximaTarea"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int FinalizarTarea(int idTramiteTarea, int idResultado, int idProximaTarea, Guid? userid)
        {
            try
            {
                ObjectParameter output = new ObjectParameter("id_tramitetarea_nuevo", typeof(int));
                var idTarea = _unitOfWork.Db.ENG_Finalizar_Tarea(idTramiteTarea, idResultado, idProximaTarea, userid, output);
                if (output.Value != DBNull.Value && Convert.ToInt32(output.Value) > 0)
                    return Convert.ToInt32(output.Value);

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public SGI_Tramites_Tareas GetUltimaTareaHabilitacionAbierta(int IdSolicitud)
        {
            return (
                 from sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas
                 join sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on sgi_tram_tarea.id_tramitetarea equals sgi_tareas.id_tramitetarea
                 join tareas in _unitOfWork.Db.ENG_Tareas on sgi_tram_tarea.id_tarea equals tareas.id_tarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 && !sgi_tram_tarea.FechaCierre_tramitetarea.HasValue
                 orderby sgi_tram_tarea.id_tramitetarea descending
                 select sgi_tram_tarea).FirstOrDefault();
        }
        public SGI_Tramites_Tareas GetUltimaTareaHabilitacion(int IdSolicitud)
        {
            return (
                 from sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas
                 join sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on sgi_tram_tarea.id_tramitetarea equals sgi_tareas.id_tramitetarea
                 join tareas in _unitOfWork.Db.ENG_Tareas on sgi_tram_tarea.id_tarea equals tareas.id_tarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 //&& !sgi_tram_tarea.FechaCierre_tramitetarea.HasValue
                 orderby sgi_tram_tarea.id_tramitetarea descending
                 select sgi_tram_tarea).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public SGI_Tramites_Tareas GetUltimaTareaConsultaPadron(int IdSolicitud)
        {
            return (
                 from sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas
                 join sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON on sgi_tram_tarea.id_tramitetarea equals sgi_tareas.id_tramitetarea
                 join tareas in _unitOfWork.Db.ENG_Tareas on sgi_tram_tarea.id_tarea equals tareas.id_tarea
                 where sgi_tareas.id_cpadron == IdSolicitud
                 && !sgi_tram_tarea.FechaCierre_tramitetarea.HasValue
                 orderby sgi_tram_tarea.id_tramitetarea descending
                 select sgi_tram_tarea).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public SGI_Tramites_Tareas GetUltimaTareaTransferencia(int IdSolicitud)
        {
            return (
                 from sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas
                 join sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on sgi_tram_tarea.id_tramitetarea equals sgi_tareas.id_tramitetarea
                 join tareas in _unitOfWork.Db.ENG_Tareas on sgi_tram_tarea.id_tarea equals tareas.id_tarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 && !sgi_tram_tarea.FechaCierre_tramitetarea.HasValue
                 orderby sgi_tram_tarea.id_tramitetarea descending
                 select sgi_tram_tarea).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdTarea"></param>
        /// <returns></returns>
        public IEnumerable<SGI_Tramites_Tareas> GetTareaTransferencia(int IdSolicitud, int IdTarea)
        {
            return (
                 from sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF
                 join sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas on sgi_tareas.id_tramitetarea equals sgi_tram_tarea.id_tramitetarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 && sgi_tram_tarea.id_tarea == IdTarea
                 select sgi_tram_tarea);
        }
        public IEnumerable<SGI_Tramites_Tareas> GetTareaHabilitacion(int IdSolicitud, int IdTarea)
        {
            return (
                 from sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                 join sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas on sgi_tareas.id_tramitetarea equals sgi_tram_tarea.id_tramitetarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 && sgi_tram_tarea.id_tarea == IdTarea
                 select sgi_tram_tarea);
        }
        public SGI_Tramites_Tareas GetTareaAnteriorHabilitacion(int IdSolicitud, int IdTramitetarea)
        {
            return (
                 from sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas
                 join sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on sgi_tram_tarea.id_tramitetarea equals sgi_tareas.id_tramitetarea
                 join tareas in _unitOfWork.Db.ENG_Tareas on sgi_tram_tarea.id_tarea equals tareas.id_tarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 && sgi_tram_tarea.id_tramitetarea < IdTramitetarea
                 orderby sgi_tram_tarea.id_tramitetarea descending
                 select sgi_tram_tarea).FirstOrDefault();
        }
        public IEnumerable<SGI_Tramites_Tareas> GetTareaHabilitacionCalificar(int IdSolicitud)
        {
            return (
                 from sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                 join sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas on sgi_tareas.id_tramitetarea equals sgi_tram_tarea.id_tramitetarea
                 join sgi_cal in _unitOfWork.Db.SGI_Tarea_Calificar on sgi_tareas.id_tramitetarea equals sgi_cal.id_tramitetarea
                 where sgi_tareas.id_solicitud == IdSolicitud
                 select sgi_tram_tarea);
        }
        public bool isCalificar(int IdTarea)
        {
            var tarea = _unitOfWork.Db.ENG_Tareas.FirstOrDefault(x => x.id_tarea == IdTarea);

            if (tarea != null)
            {
                string cod_tarea_solicitud = tarea.id_circuito.ToString() + StaticClass.Engine.Sufijo_Calificar_1;
                var cal1 = GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));

                cod_tarea_solicitud = tarea.id_circuito.ToString() + StaticClass.Engine.Sufijo_Calificar_2;
                var cal2 = GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));

                return IdTarea == cal1 || IdTarea == cal2;
            }
            else
                return false;
        }
        public IEnumerable<SGI_Tramites_Tareas> GetTareaConsultaPadron(int IdCPadron, int IdTarea)
        {
            return (
                 from sgi_tareas in _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON
                 join sgi_tram_tarea in _unitOfWork.Db.SGI_Tramites_Tareas on sgi_tareas.id_tramitetarea equals sgi_tram_tarea.id_tramitetarea
                 where sgi_tareas.id_cpadron == IdCPadron
                 && sgi_tram_tarea.id_tarea == IdTarea
                 select sgi_tram_tarea);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTarea"></param>
        /// <returns></returns>
        public IEnumerable<ENG_Rel_Resultados_Tareas> GetResultadoTarea(int IdTarea)
        {
            return (from rel_tarea in _unitOfWork.Db.ENG_Rel_Resultados_Tareas
                    where rel_tarea.id_tarea == IdTarea
                    orderby rel_tarea.id_resultadotarea
                    select rel_tarea);
        }

        public int GetIdCircuito(int id_tarea)
        {
            try
            {
                var tarea = (from tar in _unitOfWork.Db.ENG_Tareas
                             where tar.id_tarea == id_tarea
                             select tar).FirstOrDefault();
                return tarea.id_circuito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDescripcionCircuito(int id_solicitud)
        {
            var domains = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                           join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tth.id_tramitetarea
                           join tar in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tar.id_tarea
                           join cir in _unitOfWork.Db.ENG_Circuitos on tar.id_circuito equals cir.id_circuito
                           where tth.id_solicitud == id_solicitud
                           orderby tt.id_tramitetarea descending
                           select cir.descripcion).FirstOrDefault();

            return domains;
        }

        public IEnumerable<ENG_Circuitos> GetDescripcionCircuitos(List<int> lstIdCircuitoExcluir)
        {
            var nombre = (from cir in _unitOfWork.Db.ENG_Circuitos
                          where
                            cir.activo == true &&
                              (from rel_cir in _unitOfWork.Db.ENG_Rel_Circuitos_TiposDeTramite
                               where
                                    rel_cir.id_tipotramite == 1 && !lstIdCircuitoExcluir.Contains(rel_cir.id_circuito)
                               select new
                               {
                                   rel_cir.id_circuito
                               }).Contains(new { id_circuito = cir.id_circuito })
                          select cir).ToList();

            return nombre;
        }

        public ENG_Circuitos GetCircuitobyId(int idCircuito)
        {
            var nombre = (from cir in _unitOfWork.Db.ENG_Circuitos
                          where
                            cir.id_circuito == idCircuito
                          select cir).FirstOrDefault();

            return nombre;
        }

        public DispoEntity GetDatosDispoTransmision(int idSolicitud)
        {
            var dispo = (from tar in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF
                          join sade in _unitOfWork.Db.SGI_SADE_Procesos on tar.id_tramitetarea equals sade.id_tramitetarea
                          where tar.id_solicitud == idSolicitud && sade.id_proceso == (int)Constantes.SadeProcesos.RevFirma
                          select new DispoEntity
                          {
                              FechaDispo = sade.fecha_en_SADE.Value,
                              NroDispo = sade.resultado_ee
                          }).FirstOrDefault();
            return dispo;
        }

        public int GetIdCircuitoByIdEncomienda(int id_encomienda)
        {
            var parameter = new ObjectParameter("id_circuito", typeof(int));
            _unitOfWork.Db.SPGetIdCircuitoByEncomienda(id_encomienda, parameter);
            return (int)parameter.Value;
        }

        public ENG_Circuitos GetCircuito(int idCircuito)
        {
            var domains = (from cir in _unitOfWork.Db.ENG_Circuitos
                           where cir.id_circuito == idCircuito
                           select cir).FirstOrDefault();

            return domains;
        }

        public int GetIdCircuitoByIdSolicitud(int id_solicitud)
        {
            var parameter = new ObjectParameter("circuito", typeof(int));
            _unitOfWork.Db.SPGetIdCircuitoHAB(id_solicitud, parameter);
            return (int)parameter.Value;
        }
    }
}

