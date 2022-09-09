using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SGITareaDocumentosAdjuntosRepository : BaseRepository<SGI_Tarea_Documentos_Adjuntos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGITareaDocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id_tramitetarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tarea_Documentos_Adjuntos> GetByFKid_tramitetarea(int id_tramitetarea)
		{
			IEnumerable<SGI_Tarea_Documentos_Adjuntos> domains = _unitOfWork.Db.SGI_Tarea_Documentos_Adjuntos.Where(x => 													
														x.id_tramitetarea == id_tramitetarea											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id_tdocreq"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Tarea_Documentos_Adjuntos> GetByFKid_tdocreq(int id_tdocreq)
		{
			IEnumerable<SGI_Tarea_Documentos_Adjuntos> domains = _unitOfWork.Db.SGI_Tarea_Documentos_Adjuntos.Where(x => 													
														x.id_tdocreq == id_tdocreq											
														);
	
			return domains;	
		}
		/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Max()
        {
            return (from entity in _unitOfWork.Db.SGI_Tarea_Documentos_Adjuntos
                select entity.id_doc_adj).Max();            
        }
	}
}

