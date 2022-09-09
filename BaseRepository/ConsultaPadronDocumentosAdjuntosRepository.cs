using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronDocumentosAdjuntosRepository : BaseRepository<CPadron_DocumentosAdjuntos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronDocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
        public IEnumerable<CPadron_DocumentosAdjuntos> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{

            return (from query in _unitOfWork.Db.CPadron_DocumentosAdjuntos
                    join tipo in _unitOfWork.Db.TiposDeDocumentosSistema on query.id_tipodocsis equals tipo.id_tipdocsis
                    where query.id_cpadron == IdConsultaPadron
                    select query); 
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>	
        public IEnumerable<CPadron_DocumentosAdjuntos> GetByFKIdConsultaPadronTipoSis(int IdConsultaPadron, int idTipo)
        {
            IEnumerable<CPadron_DocumentosAdjuntos> domains = (from cp in _unitOfWork.Db.CPadron_DocumentosAdjuntos
                                                                  where cp.id_cpadron == IdConsultaPadron && cp.id_tipodocsis == idTipo
                                                                  select cp);

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipodocumentoRequerido"></param>
        /// <returns></returns>	
        public IEnumerable<CPadron_DocumentosAdjuntos> GetByFKIdTipodocumentoRequerido(int IdTipodocumentoRequerido)
		{
			IEnumerable<CPadron_DocumentosAdjuntos> domains = _unitOfWork.Db.CPadron_DocumentosAdjuntos.Where(x => 													
														x.id_tdocreq == IdTipodocumentoRequerido											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoSistema"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_DocumentosAdjuntos> GetByFKIdTipoDocumentoSistema(int IdTipoDocumentoSistema)
		{
			IEnumerable<CPadron_DocumentosAdjuntos> domains = _unitOfWork.Db.CPadron_DocumentosAdjuntos.Where(x => 													
														x.id_tipodocsis == IdTipoDocumentoSistema											
														);
	
			return domains;	
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_DocumentosAdjuntos> Get(int IdConsultaPadron)
        {

            return (from tc in _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON
                               join tt in _unitOfWork.Db.SGI_Tramites_Tareas on tc.id_tramitetarea equals tt.id_tramitetarea
                               join sol in _unitOfWork.Db.CPadron_Solicitudes on tc.id_cpadron equals sol.id_cpadron
                               join doc in _unitOfWork.Db.CPadron_DocumentosAdjuntos on sol.id_cpadron equals doc.id_cpadron
                               where tc.id_cpadron == IdConsultaPadron
                               && tt.id_tarea == (int)Constantes.ENG_Tareas.CP_Fin_Tramite
                               && doc.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.INFORMES_CPADRON
                               select doc);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Max()
        {
            return (from da in _unitOfWork.Db.CPadron_DocumentosAdjuntos
             select da.id_docadjunto).Max();

        }
	}
}