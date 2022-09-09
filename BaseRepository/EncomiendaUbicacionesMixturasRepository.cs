using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaUbicacionesMixturasRepository : BaseRepository<Encomienda_Ubicaciones_Mixturas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaUbicacionesMixturasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Ubicaciones_Mixturas> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
        {
            IEnumerable<Encomienda_Ubicaciones_Mixturas> domains = _unitOfWork.Db.Encomienda_Ubicaciones_Mixturas.Where(x =>
                                                        x.id_encomiendaubicacion == IdEncomiendaUbicacion
                                                        );

            return domains;
        }
    }
}
