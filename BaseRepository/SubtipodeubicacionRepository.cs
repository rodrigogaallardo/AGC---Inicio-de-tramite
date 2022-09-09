using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;

namespace BaseRepository
{
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class SubTipoUbicacioneRepository : BaseRepository<SubTiposDeUbicacion>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubTipoUbicacioneRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<SubTiposDeUbicacion> Get(int IdTipoUbicacion)
        {
            var query = (from tu in _unitOfWork.Db.SubTiposDeUbicacion
                         where tu.id_tipoubicacion == IdTipoUbicacion

                         select tu);
            return query;
        }
    }
}
