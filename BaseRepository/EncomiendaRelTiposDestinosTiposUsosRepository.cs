using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaRelTiposDestinosTiposUsosRepository : BaseRepository<Encomienda_Rel_TiposDestinos_TiposUsos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaRelTiposDestinosTiposUsosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        public IEnumerable<Encomienda_Rel_TiposDestinos_TiposUsos> GetByFKIdTipoDestinoTipoTipo(int id_tipo_uso, int id_tipo_destino)
        {
            var domains = _unitOfWork.Db.Encomienda_Rel_TiposDestinos_TiposUsos.Where(x => x.id_tipo_destino == id_tipo_destino && x.id_tipo_uso == id_tipo_uso);
            return domains;
        }

    }
}

