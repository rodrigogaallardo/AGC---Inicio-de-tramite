using System;

using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class RubrosCNATAnteriorRepository : BaseRepository<Encomienda_RubrosCN_AT_Anterior>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RubrosCNATAnteriorRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
    }
}
