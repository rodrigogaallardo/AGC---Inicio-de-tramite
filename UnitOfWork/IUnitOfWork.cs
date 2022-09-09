using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;


namespace Dal.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();       
        
        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        EncomiendadigitalEntityes Db { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();

        void RollBack();

    }
}
