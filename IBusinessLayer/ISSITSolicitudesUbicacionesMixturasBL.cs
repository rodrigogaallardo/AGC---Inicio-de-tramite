using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesUbicacionesMixturasBL<T>
    {
        IEnumerable<T> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion);
    }
}
