using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IEncomiendaUbicacionesDistritoBL<T>
    {
        IEnumerable<T> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion);
    }
}
