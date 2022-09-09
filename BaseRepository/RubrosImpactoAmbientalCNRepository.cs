using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class RubrosImpactoAmbientalCNRepository : BaseRepository<RubrosImpactoAmbientalCN>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RubrosImpactoAmbientalCNRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<RubrosImpactoAmbientalCN> GetByFKIdRubro(int IdRubro)
        {
            IEnumerable<RubrosImpactoAmbientalCN> domains = _unitOfWork.Db.RubrosImpactoAmbientalCN.Where(x =>
                                                        x.IdRubro == IdRubro
                                                        );

            return domains;
        }

        public IEnumerable<RubrosImpactoAmbientalCN> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
        {
            IEnumerable<RubrosImpactoAmbientalCN> domains = _unitOfWork.Db.RubrosImpactoAmbientalCN.Where(x =>
                                                        x.id_tipocertificado == IdImpactoAmbiental
                                                        );

            return domains;
        }
    }
}
