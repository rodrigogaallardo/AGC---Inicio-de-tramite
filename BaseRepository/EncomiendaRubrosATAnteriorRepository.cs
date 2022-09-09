using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class EncomiendaRubrosATAnteriorRepository : BaseRepository<Encomienda_Rubros_AT_Anterior>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EncomiendaRubrosATAnteriorRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
    }
}
