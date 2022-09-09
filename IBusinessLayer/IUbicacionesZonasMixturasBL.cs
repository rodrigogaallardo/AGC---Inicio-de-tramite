using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IUbicacionesZonasMixturasBL<T>
	{
		IEnumerable<T> GetAll();        
        T Single(int IdZona);
        IEnumerable<T> GetZonasEncomienda(int IdEncomienda);
        IEnumerable<T> GetZonasUbicacion(int IdUbicacion);
    }
}


