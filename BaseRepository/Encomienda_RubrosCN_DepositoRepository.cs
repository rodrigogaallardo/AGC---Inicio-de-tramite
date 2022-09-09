using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    
    public class Encomienda_RubrosCN_DepositoRepository : BaseRepository<Encomienda_RubrosCN_Deposito>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Encomienda_RubrosCN_DepositoRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_RubrosCN_Deposito> GetRubrosDepositosByEncomienda(int id_encomienda)
        {
            var domains = (from erd in _unitOfWork.Db.Encomienda_RubrosCN_Deposito where erd.id_encomienda == id_encomienda select erd);
            return domains;
        }

        public IEnumerable<Encomienda_RubrosCN_Deposito> GetRubrosDepositosByEncomiendaRubro(int id_encomienda,int idRubro)
        {
            var domains = (from erd in _unitOfWork.Db.Encomienda_RubrosCN_Deposito where erd.id_encomienda == id_encomienda && erd.IdRubro == idRubro select erd);
            return domains;
        }
    }
}
