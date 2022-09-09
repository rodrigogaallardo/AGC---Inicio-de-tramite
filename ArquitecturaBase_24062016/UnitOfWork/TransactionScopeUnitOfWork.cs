using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWork
{
    public class TransactionScopeUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly DGHP_SolicitudesEntities _db;

        private readonly TransactionScope transactionScope;

        public TransactionScopeUnitOfWork(IsolationLevel isolationLevel)
        {
            this.transactionScope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions
                    {
                        IsolationLevel = isolationLevel,
                        Timeout = TransactionManager.MaximumTimeout
                    });
            _db = new DGHP_SolicitudesEntities();
        }

        /// <summary>
        /// Unit OF Work Without transaction SCOPE 
        /// </summary>
        public TransactionScopeUnitOfWork()
        {            
            _db = new DGHP_SolicitudesEntities();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.transactionScope.Dispose();
                }

                disposed = true;
            }
        }
        public void Commit()
        {
            _db.SaveChanges();            
            this.transactionScope.Complete();
        }
        public DataAcess.DGHP_SolicitudesEntities Db
        {
            get { return _db; }
        }
        public void StartTransaction()
        {
            throw new NotImplementedException();
        }
        public void RollBack()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
