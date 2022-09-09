using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITPermisosDatosAdicionalesRepository : BaseRepository<SSIT_Permisos_DatosAdicionales>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITPermisosDatosAdicionalesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Permisos_DatosAdicionales> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Permisos_DatosAdicionales> domains = _unitOfWork.Db.SSIT_Permisos_DatosAdicionales.Where(x =>
                                                        x.IdSolicitud == IdSolicitud
                                                        );

            return domains;
        }

    }
}
