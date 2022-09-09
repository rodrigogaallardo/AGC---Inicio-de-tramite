using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciaUbicacionesMixturasRepository : BaseRepository<Transf_Ubicaciones_Mixturas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferenciaUbicacionesMixturasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdSSITSolicitudesUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Ubicaciones_Mixturas> GetByFKIdSolicitudUbicacion(int id_solicitudubicacion)
        {
            IEnumerable<Transf_Ubicaciones_Mixturas> domains = _unitOfWork.Db.Transf_Ubicaciones_Mixturas.Where(x =>
                                                        x.id_transfubicacion == id_solicitudubicacion
                                                        );

            return domains;
        }
    }
}
