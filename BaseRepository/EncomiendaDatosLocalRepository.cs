using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaDatosLocalRepository : BaseRepository<Encomienda_DatosLocal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaDatosLocalRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public Encomienda_DatosLocal GetByFKIdEncomienda(int IdEncomienda)
        {
            Encomienda_DatosLocal domains = _unitOfWork.Db.Encomienda_DatosLocal.
                FirstOrDefault(x => x.id_encomienda == IdEncomienda);

            return domains;
        }
    }
}

