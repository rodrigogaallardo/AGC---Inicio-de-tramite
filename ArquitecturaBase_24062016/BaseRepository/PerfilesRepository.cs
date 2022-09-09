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
    public class PerfilesRepository :BaseRepository<BAFYCO_Perfiles>  
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public PerfilesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
                       
    }
}
