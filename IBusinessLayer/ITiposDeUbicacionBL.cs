using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITiposDeUbicacionBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);
        void Update(T objectDto);
        void Delete(T objectDto);
        T Single(int IdTipoUbicacion);
        IEnumerable<T> GetTiposDeUbicacionExcluir(int IdTipoUbicacion);
    }
}


