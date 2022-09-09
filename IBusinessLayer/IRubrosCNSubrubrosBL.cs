using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IRubrosCNSubrubrosBL<T>
    {
        IEnumerable<T> GetSubRubros(int IdRubro);
        T Single(int IdSubRubro);
    }
}
