using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TipoNormativaRepository : BaseRepository<TipoNormativa> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TipoNormativaRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoRequerido"></param>
		/// <returns></returns>	
		public IEnumerable<TipoNormativa> GetByFKIdTipoDocumentoRequerido(int IdTipoDocumentoRequerido)
		{
			IEnumerable<TipoNormativa> domains = (from tm in _unitOfWork.Db.TipoNormativa
                                                  join rel in _unitOfWork.Db.Rel_TiposDeDocumentosRequeridos_TipoNormativa on tm.Id equals rel.id_tnor
                                                  where rel.id_tdocreq==IdTipoDocumentoRequerido
                                                  select tm);
			return domains;
		}

	}
}

