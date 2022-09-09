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
    public class Ley962TiposDeDocumentosRequeridosRepository: BaseRepository<Ley962_TiposDeDocumentosRequeridos>
    {
         private readonly IUnitOfWork _unitOfWork;

         public Ley962TiposDeDocumentosRequeridosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
    }
}
