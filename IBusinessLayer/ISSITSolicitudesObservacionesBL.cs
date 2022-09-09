using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesObservacionesBL<T>
	{
		void Update(T objectDto); 
        T Single(int id_solobs);  
		IEnumerable<T> GetByFKIdSolicitud(int id_solicitud);
    }
}


