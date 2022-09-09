using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGISolicitudesPagosBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitudPago );  
		IEnumerable<T> GetByFKIdTramiteTarea(int IdTramiteTarea);
    }
}


