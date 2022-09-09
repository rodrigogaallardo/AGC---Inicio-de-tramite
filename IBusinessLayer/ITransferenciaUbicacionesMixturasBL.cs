using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ITransferenciaUbicacionesMixturasBL<T>
    {
        IEnumerable<T> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion);
    }
}
