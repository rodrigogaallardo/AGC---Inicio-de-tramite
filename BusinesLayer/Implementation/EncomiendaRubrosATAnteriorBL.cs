using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBusinessLayer;
using BaseRepository;
using UnitOfWork;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaRubrosATAnteriorBL : IEncomiendaRubrosATAnteriorBL<EncomiendaRubrosCNATAnteriorDTO>
    {
        private EncomiendaRubrosATAnteriorRepository repo = null;
        private IUnitOfWorkFactory uowF = null;

        public bool Delete(int IdEncomiendaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosATAnteriorRepository(unitOfWork);
                    var entity = repo.Single(IdEncomiendaRubro);
                    repo.Delete(entity);
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
