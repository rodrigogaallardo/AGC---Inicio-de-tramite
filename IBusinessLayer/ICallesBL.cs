using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ICallesBL<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetCalles();
        IEnumerable<T> Get(int Codigo);
    }
}
