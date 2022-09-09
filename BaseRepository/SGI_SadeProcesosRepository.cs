using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;


namespace BaseRepository
{
    public class SGI_SadeProcesosRepository : BaseRepository<SGI_SADE_Procesos> 
    {
        private readonly IUnitOfWork _unitOfWork;

        public SGI_SadeProcesosRepository(IUnitOfWork unit) : base(unit)
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
        public string GetPlanoVisadoByFKIdTramiteTarea(int IdTarea, int IdSol)
        {
            int id_tramite = _unitOfWork.Db.SGI_Tramites_Tareas_HAB.Where(x => x.id_solicitud == IdSol && x.SGI_Tramites_Tareas.id_tarea == IdTarea).Select(y => y.id_tramitetarea).FirstOrDefault();
            string domains = (from plan in _unitOfWork.Db.Solicitud_planoVisado
                           join doc in _unitOfWork.Db.SGI_Tarea_Documentos_Adjuntos on plan.id_tramiteTarea equals doc.id_tramitetarea
                           join proc in _unitOfWork.Db.SGI_SADE_Procesos on doc.id_file equals proc.id_file    
                           where plan.id_solicitud == IdSol &&
                           proc.id_tramitetarea == id_tramite
                           orderby plan.id_solPlanoVisado descending
                           select proc.resultado_ee).FirstOrDefault();

            

            return domains;
        }
    }
}
