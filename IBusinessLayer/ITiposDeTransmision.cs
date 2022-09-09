using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ITiposDeTransmisionBL <T>
    {
        IEnumerable<T> GetAll();
        T Single(int IdTipoCaracter);
    }
}
