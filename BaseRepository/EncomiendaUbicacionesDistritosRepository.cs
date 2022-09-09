using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;
using System.Data.Entity.Core.Objects;
using StaticClass;

namespace BaseRepository
{
    public class EncomiendaUbicacionesDistritosRepository : BaseRepository<Encomienda_Ubicaciones_Distritos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaUbicacionesDistritosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_Ubicaciones_Distritos> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
        {
            IEnumerable<Encomienda_Ubicaciones_Distritos> domains = _unitOfWork.Db.Encomienda_Ubicaciones_Distritos
                .Where(x => x.id_encomiendaubicacion == IdEncomiendaUbicacion);
            return domains;
        }
    }
}
