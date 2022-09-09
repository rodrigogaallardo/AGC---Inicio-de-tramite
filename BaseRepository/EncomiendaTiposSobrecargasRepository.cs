using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTiposSobrecargasRepository : BaseRepository<Encomienda_Tipos_Sobrecargas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaTiposSobrecargasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
    }
}

