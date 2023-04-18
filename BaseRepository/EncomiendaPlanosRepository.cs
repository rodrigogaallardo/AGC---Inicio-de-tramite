using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class EncomiendaPlanosRepository : BaseRepository<Encomienda_Planos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaPlanosRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Encomienda_Planos> GetByFKIdEncomienda(int IdEncomienda)
		{
            IEnumerable<Encomienda_Planos> domains = (from ep in _unitOfWork.Db.Encomienda_Planos
                                                      where ep.id_encomienda == IdEncomienda
                                                      select ep
                                                    );
	
			return domains;
		}
        public IEnumerable<Encomienda_Planos> GetByFKIdEncomiendaTipoPlano(int IdEncomienda, int TipoPlano)
        {
            IEnumerable<Encomienda_Planos> domains = (from ep in _unitOfWork.Db.Encomienda_Planos
                                                      where ep.id_encomienda == IdEncomienda
                                                        && ep.id_tipo_plano == TipoPlano
                                                      select ep
                                                    );

            return domains;
        }
        
        public bool existe(int id_tipo_plano, string nombre, int id_encomienda)
        {
            IEnumerable<Encomienda_Planos> domains = _unitOfWork.Db.Encomienda_Planos.Where(x =>
                                                        x.id_encomienda == id_encomienda && x.id_tipo_plano==id_tipo_plano
                                                        && x.nombre_archivo==nombre
                                                        );

            return domains.Count() > 0;
        }

    }
}

