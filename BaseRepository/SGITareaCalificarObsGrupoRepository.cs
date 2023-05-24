using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SGITareaCalificarObsGrupoRepository : BaseRepository<SGI_Tarea_Calificar_ObsGrupo> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGITareaCalificarObsGrupoRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<SGITareaCalificarObsGrupoGrillaEntity> GetByFKIdSolicitud(int id_solicitud)
        {
            /*
            var intermediateDomains = (from obs in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo
                                       join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on obs.id_tramitetarea equals tth.id_tramitetarea
                                       join asp in _unitOfWork.Db.aspnet_Users on obs.CreateUser equals asp.UserId
                                       join usr_sgi in _unitOfWork.Db.SGI_Profiles on asp.UserId equals usr_sgi.userid
                                       join obsDoc in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs on obs.id_ObsGrupo equals obsDoc.id_ObsGrupo
                                       where tth.id_solicitud == id_solicitud
                                       && tth.id_tramitetarea <= (from ha in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                                                                  join ta in _unitOfWork.Db.SGI_Tramites_Tareas on ha.id_tramitetarea equals ta.id_tramitetarea
                                                                  where ha.id_solicitud == id_solicitud
                                                                  && ta.ENG_Tareas.cod_tarea.ToString().Contains("25")
                                                                  select ha.id_tramitetarea).Max()
                                        select new SGITareaCalificarObsGrupoGrillaEntity
                                        {
                                            id_tramitetarea = tth.id_tramitetarea,
                                            id_ObsGrupo = obs.id_ObsGrupo,
                                            CreateDate = obs.CreateDate,
                                            LastUpdateDate = tth.SGI_Tramites_Tareas.FechaInicio_tramitetarea,
                                            userApeNom = usr_sgi.Apellido + ", " + usr_sgi.Nombres
                                        }
                                       );
        

            var sig_correccionSol = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                                     join hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals hab.id_tramitetarea
                                     join tskr in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tskr.id_tarea
                                     join users in _unitOfWork.Db.aspnet_Users on tt.UsuarioAsignado_tramitetarea equals users.UserId
                                     where hab.id_solicitud == id_solicitud && hab.id_tramitetarea >= intermediateDomains.FirstOrDefault().id_tramitetarea && tskr.cod_tarea.ToString().Contains("25")
                                     select tt.FechaInicio_tramitetarea).Min();
            */
            var domains = (from obs in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo
                           join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on obs.id_tramitetarea equals tth.id_tramitetarea
                           join asp in _unitOfWork.Db.aspnet_Users on obs.CreateUser equals asp.UserId 
                           join usr_sgi in _unitOfWork.Db.SGI_Profiles on asp.UserId equals usr_sgi.userid
                           join obsDoc in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs on obs.id_ObsGrupo equals obsDoc.id_ObsGrupo
                           where tth.id_solicitud == id_solicitud
                           && tth.id_tramitetarea <= (from ha in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                                                      join ta in _unitOfWork.Db.SGI_Tramites_Tareas on ha.id_tramitetarea equals ta.id_tramitetarea
                                                      where ha.id_solicitud == id_solicitud
                                                      && ta.ENG_Tareas.cod_tarea.ToString().Contains("25")
                                                      select ha.id_tramitetarea).Max()
                           select new SGITareaCalificarObsGrupoGrillaEntity
                           {
                               id_tramitetarea = tth.id_tramitetarea,
                               id_ObsGrupo = obs.id_ObsGrupo,
                               CreateDate = obs.CreateDate,
                               LastUpdateDate = tth.SGI_Tramites_Tareas.FechaInicio_tramitetarea,
                               userApeNom = usr_sgi.Apellido + ", " + usr_sgi.Nombres,
                               Observaciones =                                                
                                        new SGITareaCalificarObsGrillaEntity
                                        {
                                           id_ObsGrupo = obsDoc.id_ObsGrupo,
                                           id_ObsDocs = obsDoc.id_ObsDocs,
                                           nombre_tdocreq = obsDoc.TiposDeDocumentosRequeridos.nombre_tdocreq,
                                           Observacion_ObsDocs = obsDoc.Observacion_ObsDocs,
                                           Respaldo_ObsDocs = obsDoc.Respaldo_ObsDocs,
                                           Actual = obsDoc.Actual.Value,
                                           Decido_no_subir = obsDoc.Decido_no_subir.Value,
                                           cod_tipodocsis = obsDoc.TiposDeDocumentosRequeridos.TiposDeDocumentosSistema.cod_tipodocsis,
                                           filename = obsDoc.id_file != null ? _unitOfWork.Db.Files.Where(x => x.id_file == obsDoc.id_file.Value).Select(x => x.FileName).FirstOrDefault():"",
                                           id_file = obsDoc.id_file != null ? obsDoc.id_file.Value : 0,
                                           id_certificado = obsDoc.id_certificado != null ? obsDoc.id_certificado.Value : 0,
                                           LastUpdateDate = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                                                             join hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals hab.id_tramitetarea
                                                             join tskr in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tskr.id_tarea
                                                             join users in _unitOfWork.Db.aspnet_Users on tt.UsuarioAsignado_tramitetarea equals users.UserId
                                                             where hab.id_solicitud == id_solicitud && hab.id_tramitetarea > tth.id_tramitetarea
                                                             select tt.FechaInicio_tramitetarea).Min()
                                        }
                           });
            
            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>	
        public IEnumerable<SGITareaCalificarObsGrupoGrillaEntity> GetByFKIdSolicitudtr(int id_solicitud)
        {
            var domains = (from obs in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo
                           join tth in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on obs.id_tramitetarea equals tth.id_tramitetarea
                           join asp in _unitOfWork.Db.aspnet_Users on obs.CreateUser equals asp.UserId
                           join usr_sgi in _unitOfWork.Db.SGI_Profiles on asp.UserId equals usr_sgi.userid
                           join obsDoc in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs on obs.id_ObsGrupo equals obsDoc.id_ObsGrupo
                           where tth.id_solicitud == id_solicitud
                           && tth.id_tramitetarea <= (from ha in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF
                                                      join ta in _unitOfWork.Db.SGI_Tramites_Tareas on ha.id_tramitetarea equals ta.id_tramitetarea
                                                      where ha.id_solicitud == id_solicitud
                                                      && ta.ENG_Tareas.cod_tarea.ToString().Contains("25")
                                                      select ha.id_tramitetarea).Max()
                           select new SGITareaCalificarObsGrupoGrillaEntity
                           {
                               id_ObsGrupo = obs.id_ObsGrupo,
                               CreateDate = obs.CreateDate,
                               userApeNom = usr_sgi.Apellido + ", " + usr_sgi.Nombres,
                               Observaciones =
                                        new SGITareaCalificarObsGrillaEntity
                                        {
                                            id_ObsGrupo = obsDoc.id_ObsGrupo,
                                            id_ObsDocs = obsDoc.id_ObsDocs,
                                            nombre_tdocreq = obsDoc.TiposDeDocumentosRequeridos.nombre_tdocreq,
                                            Observacion_ObsDocs = obsDoc.Observacion_ObsDocs,
                                            Respaldo_ObsDocs = obsDoc.Respaldo_ObsDocs,
                                            Actual = obsDoc.Actual.Value,
                                            Decido_no_subir = obsDoc.Decido_no_subir.Value,
                                            cod_tipodocsis = obsDoc.TiposDeDocumentosRequeridos.TiposDeDocumentosSistema.cod_tipodocsis,
                                            filename = obsDoc.id_file != null ? _unitOfWork.Db.Files.Where(x => x.id_file == obsDoc.id_file.Value).Select(x => x.FileName).FirstOrDefault() : "",
                                            id_file = obsDoc.id_file != null ? obsDoc.id_file.Value : 0,
                                            id_certificado = obsDoc.id_certificado != null ? obsDoc.id_certificado.Value : 0
                                        }
                           });

            return domains;
        }

    }
}

