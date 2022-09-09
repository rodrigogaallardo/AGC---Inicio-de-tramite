using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesUbicacionesDistritosRepository : BaseRepository<SSIT_Solicitudes_Ubicaciones_Distritos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesUbicacionesDistritosRepository(IUnitOfWork unit)
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
		public IEnumerable<SSIT_Solicitudes_Ubicaciones_Distritos> GetByFKIdSolicitudUbicacion(int id_solicitudubicacion)
        {
            IEnumerable<SSIT_Solicitudes_Ubicaciones_Distritos> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Distritos.Where(x =>
                                                        x.id_solicitudubicacion == id_solicitudubicacion
                                                        );

            return domains;
        }
    }
}
