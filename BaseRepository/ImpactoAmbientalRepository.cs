using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ImpactoAmbientalRepository : BaseRepository<ImpactoAmbiental>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImpactoAmbientalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public ImpactoAmbiental GetByFKIdEncomienda(int IdEncomienda)
        {
            var rubros = (from entity in _unitOfWork.Db.Encomienda_Rubros
                          where entity.id_encomienda == IdEncomienda
                          select entity);
            if (rubros.Count() == 0)
                return null;
            int id = rubros.Select(x => x.id_ImpactoAmbiental.Value).Max();
            return Single(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public ImpactoAmbiental GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            var rubros = (from entity in _unitOfWork.Db.CPadron_Rubros
                          where entity.id_cpadron == IdConsultaPadron
                          select entity);
            if (rubros.Count() == 0)
                return null;
            int id = rubros.Select(x => x.id_ImpactoAmbiental.Value).Max();
            return Single(id);
        }
    }
}

