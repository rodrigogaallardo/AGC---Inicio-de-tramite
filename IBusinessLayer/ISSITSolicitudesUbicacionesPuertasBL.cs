using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesUbicacionesPuertasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitudPuerta );  
		IEnumerable<T> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion);
    }
}


