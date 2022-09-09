using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class SSIT_Solicitudes_AvisoCaducidadRepository : BaseRepository<SSIT_Solicitudes_AvisoCaducidad>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSIT_Solicitudes_AvisoCaducidadRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_AvisoCaducidad> GetNotificacionByUserId(Guid userid)
        {
            try
            {
                var ListaSol = (from avi in _unitOfWork.Db.SSIT_Solicitudes_AvisoCaducidad
                                where
                                  avi.SSIT_Solicitudes.CreateUser == userid && avi.fechaNotificacionSSIT == null
                                select avi);
                return ListaSol;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
