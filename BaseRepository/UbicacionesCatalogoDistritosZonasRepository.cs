using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UbicacionesCatalogoDistritosZonasRepository : BaseRepository<Ubicaciones_CatalogoDistritos_Zonas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionesCatalogoDistritosZonasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public Ubicaciones_CatalogoDistritos_Zonas GetZona(int idDistrito)
        {
            var query =  (from ubi in _unitOfWork.Db.Ubicaciones_CatalogoDistritos
                    join zona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas on ubi.IdDistrito equals zona.IdDistrito
                    join subzona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on zona.IdZona equals subzona.IdZona
                    where ubi.IdDistrito == idDistrito
                    select zona).FirstOrDefault();

            return query;
        }
    }
}
