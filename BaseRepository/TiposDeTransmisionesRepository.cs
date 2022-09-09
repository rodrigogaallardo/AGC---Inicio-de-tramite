using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;
using StaticClass;


namespace BaseRepository 
{
    public class TiposDeTransmisionesRepository : BaseRepository<TiposdeTransmision>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TiposDeTransmisionesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

    }
}
