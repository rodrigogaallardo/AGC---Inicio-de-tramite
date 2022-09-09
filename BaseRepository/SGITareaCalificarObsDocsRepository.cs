using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SGITareaCalificarObsDocsRepository : BaseRepository<SGI_Tarea_Calificar_ObsDocs> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGITareaCalificarObsDocsRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTramiteTarea"></param>
        /// <returns></returns>	
        public IEnumerable<SGITareaCalificarObsGrillaEntity> GetByFKIdObsGrupo(int id_ObsGrupo)
        {
            var domains = (from a in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs
                           where a.id_ObsGrupo == id_ObsGrupo
                           select new SGITareaCalificarObsGrillaEntity
                           {
                               id_ObsDocs = a.id_ObsDocs,
                               nombre_tdocreq = a.TiposDeDocumentosRequeridos.nombre_tdocreq,
                               Observacion_ObsDocs = a.Observacion_ObsDocs,
                               Respaldo_ObsDocs = a.Respaldo_ObsDocs,
                               Actual = a.Actual.Value,
                               Decido_no_subir = a.Decido_no_subir.Value,
                               cod_tipodocsis = a.TiposDeDocumentosRequeridos.TiposDeDocumentosSistema.cod_tipodocsis,
                               filename = a.id_file != null ? _unitOfWork.Db.Files.Where(x => x.id_file == a.id_file.Value).Select(x => x.FileName).FirstOrDefault():"",
                               id_file = a.id_file != null ? a.id_file.Value : 0,
                               id_certificado = a.id_certificado != null ? a.id_certificado.Value : 0
                           });
            return domains;
        }

        public bool ExistenObservacionesdetalleSinProcesar(int id_solicitud)
        {
            var domains = (from a in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs
                           join b in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo on a.id_ObsGrupo equals b.id_ObsGrupo
                           join th in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on b.id_tramitetarea equals th.id_tramitetarea
                           where th.id_solicitud == id_solicitud
                            && a.id_file == null
                            && a.id_certificado == null
                            && a.Decido_no_subir == false
                           select a.id_ObsDocs);

            return domains.Count() > 0;
        }

        public bool ExistenObservacionesdetalleSinProcesarTR(int id_solicitud)
        {
            var domains = (from a in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs
                           join b in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo on a.id_ObsGrupo equals b.id_ObsGrupo
                           join th in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on b.id_tramitetarea equals th.id_tramitetarea
                           where th.id_solicitud == id_solicitud
                            && a.id_file == null
                            && a.id_certificado == null
                            && a.Decido_no_subir == false
                           select a.id_ObsDocs);

            return domains.Count() > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_ObsGrupo"></param>
        /// <returns></returns>
        public IEnumerable<SGI_Tarea_Calificar_ObsDocs> GetByFKIdObs(int id_ObsGrupo)
        {
            var domains = _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs.Where(x=>x.id_ObsGrupo == id_ObsGrupo);
            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_Solicitud"></param>
        /// <returns></returns>
        public IEnumerable<SGITareaCalificarObsGrillaEntity> GetByFKIdSolicitud(int id_Solicitud)
        {
            var domains = (from a in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs
                           join b in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo on a.id_ObsGrupo equals b.id_ObsGrupo
                           join th in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on b.id_tramitetarea equals th.id_tramitetarea
                           where th.id_solicitud == id_Solicitud
                           select new SGITareaCalificarObsGrillaEntity
                           {
                               id_ObsDocs = a.id_ObsDocs,
                               id_master = id_Solicitud,
                               nombre_tdocreq = a.TiposDeDocumentosRequeridos.nombre_tdocreq,
                               Observacion_ObsDocs = a.Observacion_ObsDocs,
                               Respaldo_ObsDocs = a.Respaldo_ObsDocs,
                               Actual = a.Actual.Value,
                               Decido_no_subir = a.Decido_no_subir.Value,
                               cod_tipodocsis = a.TiposDeDocumentosRequeridos.TiposDeDocumentosSistema.cod_tipodocsis,
                               filename = a.id_file != null ? _unitOfWork.Db.Files.Where(x => x.id_file == a.id_file.Value).Select(x => x.FileName).FirstOrDefault() : "",
                               id_file = a.id_file != null ? a.id_file.Value : 0,
                               id_certificado = a.id_certificado != null ? a.id_certificado.Value : 0,
                               CreateDate = a.CreateDate,
                               Url = ""
                           });
            return domains;
        }
    }
}

