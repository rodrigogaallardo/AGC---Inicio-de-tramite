using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UbicacionesCatalogoDistritosSubzonasRepository : BaseRepository<Ubicaciones_CatalogoDistritos_Subzonas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionesCatalogoDistritosSubzonasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public Ubicaciones_CatalogoDistritos_Subzonas GetSubZona(int idDistrito)
        {
            return (from ubi in _unitOfWork.Db.Ubicaciones_CatalogoDistritos
                    join zona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas on ubi.IdDistrito equals zona.IdDistrito
                    join subzona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on zona.IdZona equals subzona.IdZona
                    where ubi.IdDistrito == idDistrito
                    select subzona).FirstOrDefault();
        }
        public IEnumerable<Ubicaciones_CatalogoDistritos_Subzonas> GetSubZonas(int idZona)
        {
            return (from zona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas 
                    join subzona in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on zona.IdZona equals subzona.IdZona
                    where subzona.IdZona == idZona
                    select subzona).ToList();
        }
    }
}
