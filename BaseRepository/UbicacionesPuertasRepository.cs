using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UbicacionesPuertasRepository : BaseRepository<Ubicaciones_Puertas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionesPuertasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<Ubicaciones_Puertas> GetByFKIdUbicacion(int IdUbicacion)
        {
            IEnumerable<Ubicaciones_Puertas> domains = _unitOfWork.Db.Ubicaciones_Puertas.Where(x =>
                                                        x.id_ubicacion == IdUbicacion
                                                        );

            return domains;
        }
    }
}
