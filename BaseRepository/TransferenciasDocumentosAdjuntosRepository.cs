using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRepository
{
    public class TransferenciasDocumentosAdjuntosRepository : BaseRepository<Transf_DocumentosAdjuntos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasDocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Transf_DocumentosAdjuntos> GetByFKIdSolicitud(int IdSolicitud)
		{
            IEnumerable<Transf_DocumentosAdjuntos> domains = (from tdoc in _unitOfWork.Db.Transf_DocumentosAdjuntos
                                                            where tdoc.id_solicitud == IdSolicitud
                                                            select  tdoc);
	
			return domains;	
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="id_tipodocsis"></param>
        /// <returns></returns>
        public IEnumerable<Transf_DocumentosAdjuntos> GetByFKIdSolicitudTipoDocSis(int id_solicitud, int id_tipodocsis)
        {
            return _unitOfWork.Db.Transf_DocumentosAdjuntos
                         .Where(x => x.id_solicitud == id_solicitud
                           && x.id_tipodocsis == id_tipodocsis);
        }
    }
}

