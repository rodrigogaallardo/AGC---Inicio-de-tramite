using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class UbicacionesGruposDistritosRepository : BaseRepository<Ubicaciones_GruposDistritos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionesGruposDistritosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public bool PoseeDistritosU(int IdEncomienda)
        {
            return (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                    join encubicDist in _unitOfWork.Db.Encomienda_Ubicaciones_Distritos on encubic.id_encomiendaubicacion equals encubicDist.id_encomiendaubicacion
                    join cat in _unitOfWork.Db.Ubicaciones_CatalogoDistritos on encubicDist.IdDistrito equals cat.IdDistrito
                    join gd in _unitOfWork.Db.Ubicaciones_GruposDistritos on cat.IdGrupoDistrito equals gd.IdGrupoDistrito
                    where encubic.id_encomienda == IdEncomienda && gd.Codigo == "U"
                    select gd.Codigo).Count() > 0;

        }

    }
}
