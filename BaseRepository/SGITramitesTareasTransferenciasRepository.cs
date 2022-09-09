using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SGITramitesTareasTransferenciasRepository : BaseRepository<SGI_Tramites_Tareas_TRANSF> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGITramitesTareasTransferenciasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<SGI_Tramites_Tareas_TRANSF> GetByFKIdTramiteTarea(int IdTramiteTarea)
		{
			IEnumerable<SGI_Tramites_Tareas_TRANSF> domains = _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF.Where(x => 													
														x.id_tramitetarea == IdTramiteTarea											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tramites_Tareas_TRANSF> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<SGI_Tramites_Tareas_TRANSF> domains = _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tramitetarea"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public IEnumerable<SGI_Tramites_Tareas_TRANSF> GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud)
        {
            var domains = _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF.Where(x => x.id_tramitetarea == id_tramitetarea && x.id_solicitud == id_solicitud);
            return domains;
        }
	}
}

