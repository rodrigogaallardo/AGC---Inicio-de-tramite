using Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWork
{
    public class TransactionScopeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IsolationLevel _isolationLevel;

        public TransactionScopeUnitOfWorkFactory(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        public TransactionScopeUnitOfWorkFactory()
        {
            ;
        }
        
        public IUnitOfWork GetUnitOfWork(IsolationLevel isolationLevel)
        {
            return new TransactionScopeUnitOfWork(isolationLevel);
        }
        public IUnitOfWork GetUnitOfWork()
        {
            return new TransactionScopeUnitOfWork();
        }
    }
}
