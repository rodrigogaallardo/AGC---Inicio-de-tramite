using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesLayer
{
    public interface IBaseBusinesLayer<T>
    {
        /// <summary>
        /// gets all data 
        /// </summary>
        /// <returns>DTO of data</returns>
        IEnumerable<T> GetAll();
        T Single(object primaryKey);

    }
}
