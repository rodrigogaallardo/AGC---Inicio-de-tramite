using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SGITramitesTareasRepository : BaseRepository<SGI_Tramites_Tareas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGITramitesTareasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tramites_Tareas> GetByFKIdTarea(int IdTarea)
		{
			IEnumerable<SGI_Tramites_Tareas> domains = _unitOfWork.Db.SGI_Tramites_Tareas.Where(x => 													
														x.id_tarea == IdTarea											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdResultado"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tramites_Tareas> GetByFKIdResultado(int IdResultado)
		{
			IEnumerable<SGI_Tramites_Tareas> domains = _unitOfWork.Db.SGI_Tramites_Tareas.Where(x => 													
														x.id_resultado == IdResultado											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="UsuarioAsignadoTramiteTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tramites_Tareas> GetByFKUsuarioAsignadoTramiteTarea(Guid UsuarioAsignadoTramiteTarea)
		{
			IEnumerable<SGI_Tramites_Tareas> domains = _unitOfWork.Db.SGI_Tramites_Tareas.Where(x => 													
														x.UsuarioAsignado_tramitetarea == UsuarioAsignadoTramiteTarea											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdProximaTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tramites_Tareas> GetByFKIdProximaTarea(int IdProximaTarea)
		{
			IEnumerable<SGI_Tramites_Tareas> domains = _unitOfWork.Db.SGI_Tramites_Tareas.Where(x => 													
														x.id_proxima_tarea == IdProximaTarea											
														);
	
			return domains;	
		}
        
		public void DeleteTareaByIdTramiteTarea(int IdTramiteTarea, int IdTipoTramite)
        {
            _unitOfWork.Db.SGI_Tarea_EliminarTarea(IdTramiteTarea, IdTipoTramite);
        }

        public string Buscar_ObservacionPlanchetaDispoFirmada(int id_solicitud, DateTime? fechaDispoFirmada)
        {
            string observ = "";

            var q = (
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join dghp in _unitOfWork.Db.SGI_Tarea_Revision_DGHP on tt.id_tramitetarea equals dghp.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud && tt.FechaCierre_tramitetarea <= fechaDispoFirmada
                select new
                {
                    tt.id_tramitetarea,
                    dghp.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join gerente in _unitOfWork.Db.SGI_Tarea_Revision_Gerente on tt.id_tramitetarea equals gerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud && tt.FechaCierre_tramitetarea <= fechaDispoFirmada
                select new
                {
                    tt.id_tramitetarea,
                    gerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join subgerente in _unitOfWork.Db.SGI_Tarea_Revision_SubGerente on tt.id_tramitetarea equals subgerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud && tt.FechaCierre_tramitetarea <= fechaDispoFirmada
                select new
                {
                    tt.id_tramitetarea,
                    subgerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join cal in _unitOfWork.Db.SGI_Tarea_Calificar on tt.id_tramitetarea equals cal.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud && tt.FechaCierre_tramitetarea <= fechaDispoFirmada
                select new
                {
                    tt.id_tramitetarea,
                    observacion_plancheta = cal.Observaciones
                }
                );
            var lista = q.ToList().OrderByDescending(x => x.id_tramitetarea).ToList();
            if (lista != null && lista.Count > 0)
            {
                observ = !string.IsNullOrEmpty(lista[0].observacion_plancheta) ? lista[0].observacion_plancheta : "";
            }
            _unitOfWork.Db.Dispose();
            return observ; 
        }

        public string Buscar_ObservacionPlancheta(int id_solicitud)
        {
            string observ = "";

            var q = (
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join dghp in _unitOfWork.Db.SGI_Tarea_Revision_DGHP on tt.id_tramitetarea equals dghp.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    dghp.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join gerente in _unitOfWork.Db.SGI_Tarea_Revision_Gerente on tt.id_tramitetarea equals gerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    gerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join subgerente in _unitOfWork.Db.SGI_Tarea_Revision_SubGerente on tt.id_tramitetarea equals subgerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    subgerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join cal in _unitOfWork.Db.SGI_Tarea_Calificar on tt.id_tramitetarea equals cal.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    observacion_plancheta = cal.Observaciones
                }
                );
            var lista = q.ToList().OrderByDescending(x => x.id_tramitetarea).ToList();
            if (lista != null && lista.Count > 0)
            {
                observ = !string.IsNullOrEmpty(lista[0].observacion_plancheta) ? lista[0].observacion_plancheta : "";
            }
            _unitOfWork.Db.Dispose();
            return observ;
        }

        public string Buscar_ObservacionPlanchetaTransmision(int id_solicitud)
        {
            string observ = "";

            var q = (
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join dghp in _unitOfWork.Db.SGI_Tarea_Revision_DGHP on tt.id_tramitetarea equals dghp.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    dghp.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join gerente in _unitOfWork.Db.SGI_Tarea_Revision_Gerente on tt.id_tramitetarea equals gerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    gerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join subgerente in _unitOfWork.Db.SGI_Tarea_Revision_SubGerente on tt.id_tramitetarea equals subgerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    subgerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join cal in _unitOfWork.Db.SGI_Tarea_Calificar on tt.id_tramitetarea equals cal.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    observacion_plancheta = cal.Observaciones
                }
                );
            var lista = q.ToList().OrderByDescending(x => x.id_tramitetarea).ToList();
            if (lista != null && lista.Count > 0)
            {
                observ = !string.IsNullOrEmpty(lista[0].observacion_plancheta) ? lista[0].observacion_plancheta : "";
            }
            _unitOfWork.Db.Dispose();
            return observ;
        }

        public string GetObservacionPlanchetaTransmision(int id_solicitud)
        {
            string observ = "";

            var q = (
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join dghp in _unitOfWork.Db.SGI_Tarea_Revision_DGHP on tt.id_tramitetarea equals dghp.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    dghp.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join gerente in _unitOfWork.Db.SGI_Tarea_Revision_Gerente on tt.id_tramitetarea equals gerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    gerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join subgerente in _unitOfWork.Db.SGI_Tarea_Revision_SubGerente on tt.id_tramitetarea equals subgerente.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    subgerente.observacion_plancheta
                }
                ).Union(
                from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea
                join tarea in _unitOfWork.Db.ENG_Tareas on tt.id_tarea equals tarea.id_tarea
                join cal in _unitOfWork.Db.SGI_Tarea_Calificar on tt.id_tramitetarea equals cal.id_tramitetarea
                where tt_hab.id_solicitud == id_solicitud
                select new
                {
                    tt.id_tramitetarea,
                    observacion_plancheta = cal.Observaciones
                }
                );
            var lista = q.ToList().OrderByDescending(x => x.id_tramitetarea).ToList();
            if (lista != null && lista.Count > 0)
            {
                observ = lista.Select(x => x.observacion_plancheta)
                                .Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault();
            }

            return observ;
        }
    }
}

