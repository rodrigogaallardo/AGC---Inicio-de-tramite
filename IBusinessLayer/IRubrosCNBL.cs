using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IRubrosCNBL<T>
    {
        IEnumerable<T> GetAll();      
        T Single(int IdsSubRubro);
        
    }
}
