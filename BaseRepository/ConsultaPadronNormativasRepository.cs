using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronNormativasRepository : BaseRepository<CPadron_Normativas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronNormativasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Normativas> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Normativas> domains = _unitOfWork.Db.CPadron_Normativas.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoNormativa"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Normativas> GetByFKIdTipoNormativa(int IdTipoNormativa)
		{
			IEnumerable<CPadron_Normativas> domains = _unitOfWork.Db.CPadron_Normativas.Where(x => 													
														x.id_tiponormativa == IdTipoNormativa											
														);
	
			return domains;	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEntidadNormativa"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Normativas> GetByFKIdEntidadNormativa(int IdEntidadNormativa)
		{
			IEnumerable<CPadron_Normativas> domains = _unitOfWork.Db.CPadron_Normativas.Where(x => 													
														x.id_entidadnormativa == IdEntidadNormativa											
														);
	
			return domains;	
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Normativas> GetNormativas(int IdSolicitud)
        {
            var domains = (from query in _unitOfWork.Db.CPadron_Normativas
                           join tn in _unitOfWork.Db.TipoNormativa on query.id_tiponormativa equals tn.Id
                           join en in _unitOfWork.Db.EntidadNormativa on query.id_entidadnormativa equals en.Id
                           where query.id_cpadron == IdSolicitud
                           select query);

            return domains;
        }
	}
}

