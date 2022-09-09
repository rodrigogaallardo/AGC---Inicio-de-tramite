using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class EncomiendaNormativasRepository : BaseRepository<Encomienda_Normativas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaNormativasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Normativas> GetByFKIdEncomienda(int IdEncomienda)
		{
			IEnumerable<Encomienda_Normativas> domains = _unitOfWork.Db.Encomienda_Normativas.Where(x => 													
														x.id_encomienda == IdEncomienda											
														);
	
			return domains;
		}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdTipoNormativa"></param>
        ///// <returns></returns>	
        //public IEnumerable<Encomienda_Normativas> GetByFKIdTipoNormativa(int IdTipoNormativa)
        //{
        //    IEnumerable<Encomienda_Normativas> domains = _unitOfWork.Db.Encomienda_Normativas.Where(x => 													
        //                                                x.id_tiponormativa == IdTipoNormativa											
        //                                                );
	
        //    return domains;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdEntidadNormativa"></param>
        ///// <returns></returns>	
        //public IEnumerable<Encomienda_Normativas> GetByFKIdEntidadNormativa(int IdEntidadNormativa)
        //{
        //    IEnumerable<Encomienda_Normativas> domains = _unitOfWork.Db.Encomienda_Normativas.Where(x => 													
        //                                                x.id_entidadnormativa == IdEntidadNormativa											
        //                                                );
	
        //    return domains;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEntidadNormativa"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_Normativas> GetNormativas(int IdEncomienda)
        {            
            var domains = (from query in _unitOfWork.Db.Encomienda_Normativas
                           join tn in _unitOfWork.Db.TipoNormativa on query.id_tiponormativa equals tn.Id
                           join en in _unitOfWork.Db.EntidadNormativa on query.id_entidadnormativa equals en.Id
                           where query.id_encomienda == IdEncomienda
                           select query
							);
	
			return domains;
        }
	}
}

