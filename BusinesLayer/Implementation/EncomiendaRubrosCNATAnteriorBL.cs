using System;

using BaseRepository;
using Dal.UnitOfWork;
using DataTransferObject;
using IBusinessLayer;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaRubrosCNATAnteriorBL : IEncomiendaRubrosCNATAnteriorBL<EncomiendaRubrosCNATAnteriorDTO>
    {
        private EncomiendaRubrosCNATAnteriorRepository repo = null;
        private IUnitOfWorkFactory uowF = null;

        public bool Delete(int IdEncomiendaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosCNATAnteriorRepository(unitOfWork);
                    var entity = repo.Single(IdEncomiendaRubro);
                    repo.Delete(entity);
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
