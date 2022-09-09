using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EntidadNormativaRepository : BaseRepository<EntidadNormativa>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntidadNormativaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
    }
}
