using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class SSITSolicitudesOrigenRepository : BaseRepository<SSIT_Solicitudes_Origen>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesOrigenRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }


    }
}
