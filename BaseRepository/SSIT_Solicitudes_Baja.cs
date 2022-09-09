using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class SSIT_Solicitudes_BajaRepository : BaseRepository<SSIT_Solicitudes_Baja>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSIT_Solicitudes_BajaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

    }
}
