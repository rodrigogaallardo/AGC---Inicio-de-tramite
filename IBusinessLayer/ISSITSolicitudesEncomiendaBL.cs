using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesEncomiendaBL<T>
	{
        bool Insert(T objectDto);

        IEnumerable<T> GetByFKIdSolicitud(int id_solicitud);
    }
}


