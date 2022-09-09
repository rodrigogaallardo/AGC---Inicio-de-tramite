using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesDatosLocalRepository : BaseRepository<SSIT_Solicitudes_DatosLocal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesDatosLocalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_DatosLocal> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_DatosLocal> domains = _unitOfWork.Db.SSIT_Solicitudes_DatosLocal.Where(x =>
                                                        x.IdSolicitud == IdSolicitud
                                                        );

            return domains;
        }


    }
}
