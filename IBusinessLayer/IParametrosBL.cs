using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IParametrosBL<T>
    {
        IEnumerable<T> GetAll();
        string GetParametroChar(string CodParam);
    }
}
