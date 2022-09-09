using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TiposDeDocumentosSistemaRepository : BaseRepository<TiposDeDocumentosSistema>
    { 
        private readonly IUnitOfWork _unitOfWork;

        public TiposDeDocumentosSistemaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public TiposDeDocumentosSistema GetByCodigo(string cod_tipodocsis)
        {
            TiposDeDocumentosSistema domains = _unitOfWork.Db.TiposDeDocumentosSistema.
                Where(x => x.cod_tipodocsis == cod_tipodocsis).FirstOrDefault();

            return domains;
        }

    }
}
