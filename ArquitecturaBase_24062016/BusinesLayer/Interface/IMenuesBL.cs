using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTrasnferObject;

namespace BusinesLayer.Interface
{
    public interface IMenuesBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);

    }
}
