using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesNormativasBL<T>
    {
        bool Insert(T objectDto);
        void Update(T objectDto);
        void Delete(T objectDto);
        T Single(int IdEncomiendaNormativa);
        IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
    }
}
