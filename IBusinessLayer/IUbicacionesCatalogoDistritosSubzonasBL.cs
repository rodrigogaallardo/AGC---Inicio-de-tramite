using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IUbicacionesCatalogoDistritosSubzonasBL<T>
    {
        T GetSubZona(int IdDistrito);
    }
}
