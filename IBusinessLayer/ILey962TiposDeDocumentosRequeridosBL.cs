using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ILey962TiposDeDocumentosRequeridosBL<T>
    {
        IEnumerable<T> GetAll();
        T Single(int id);  
    }
}
