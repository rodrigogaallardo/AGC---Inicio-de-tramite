using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWork
{
    public class TransactionScopeUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly EncomiendadigitalEntityes _db;

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
            _db = new EncomiendadigitalEntityes();
        }

        /// <summary>
        /// Unit OF Work Without transaction SCOPE 
        /// </summary>
        public TransactionScopeUnitOfWork()
        {
            _db = new EncomiendadigitalEntityes();
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
        public DataAcess.EncomiendadigitalEntityes Db
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

        public void DeAttach<T>(T entity) where T : class
        {
            var objContext = ((IObjectContextAdapter)_db).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
            //context.Set<T>().Attach(entity);
        }
    }
}
