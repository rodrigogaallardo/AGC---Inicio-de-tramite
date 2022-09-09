using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBaseRepository;
using Dal.UnitOfWork;
using System.Data.Entity;
using DataAcess;


namespace BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T>
       where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        /// <summary>
        /// Returns the object with the primary key specifies or throws
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T Single(object primaryKey)
        {

            var dbResult = dbSet.Find(primaryKey);
            return dbResult;

        }
        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T SingleOrDefault(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            return dbResult;
        }

        //public virtual IEnumerable<T> GetAllWithDeleted()
        //{
        //    var dbresult = _unitOfWork.Db.Fetch<T>("");

        //    return dbresult;
        //}

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) == null ? false : true;
        }

        public virtual int Insert(T entity)
        {
            dynamic obj = dbSet.Add(entity);
           // this._unitOfWork.Db.SaveChanges();
            return 1;

        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }
        public int Delete(T entity)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dynamic obj = dbSet.Remove(entity);
            this._unitOfWork.Db.SaveChanges();
            //I'm returning 1 because this entity doesen't has property named ID
            return 1;
        }
        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        internal DGHP_SolicitudesEntities Database { get { return _unitOfWork.Db; } }
        public Dictionary<string, string> GetAuditNames(dynamic dynamicObject)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }
    }
}

