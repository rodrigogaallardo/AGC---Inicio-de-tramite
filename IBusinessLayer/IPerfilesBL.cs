using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IPerfilesBL<T>
    {
         T MyProperty { get; set; }
         IEnumerable<T> GetAll();
         T Single(int idPerfil);

    }
}
