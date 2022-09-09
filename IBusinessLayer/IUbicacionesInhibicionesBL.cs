using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IUbicacionesInhibicionesBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdUbicacionInhibicion );  
		IEnumerable<T> GetByFKIdUbicacion(int IdUbicacion);
    }
}


