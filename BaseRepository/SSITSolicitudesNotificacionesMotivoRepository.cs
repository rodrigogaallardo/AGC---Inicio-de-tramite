using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRepository;

namespace BaseRepository
{
    public class SSITSolicitudesNotificacionesMotivoRepository : BaseRepository<SSIT_Solicitudes_Notificaciones_motivos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesNotificacionesMotivoRepository(IUnitOfWork unit)
                : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_Notificaciones_motivos> getAllMotivos()
        {
            return (from mot in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones_motivos select mot);
        }
    }
}
