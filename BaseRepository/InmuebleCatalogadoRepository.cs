using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class InmuebleCatalogadoRepository : BaseRepository<Encomienda_Ubicaciones>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InmuebleCatalogadoRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public bool EsInmuebleCatalogado(int IdEncomienda)
        {
            return _unitOfWork.Db.Encomienda_Ubicaciones.Any(encubic => encubic.id_encomienda == IdEncomienda && encubic.InmuebleCatalogado == true);
        }


    }
}
