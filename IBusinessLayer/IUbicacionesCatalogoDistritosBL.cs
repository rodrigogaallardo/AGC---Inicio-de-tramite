using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IUbicacionesCatalogoDistritosBL<T>
    {
        IEnumerable<T> GetAll();
        T Single(int IdDistrito);
        IEnumerable<T> GetDistritosEncomienda(int IdEncomienda);
        IEnumerable<T> GetDistritosUbicacion(int IdUbicacion);
    }
}
