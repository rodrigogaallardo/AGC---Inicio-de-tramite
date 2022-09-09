using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesUbicacionesPropiedadHorizontalBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitudPropiedadHorizontal );  
		IEnumerable<T> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion);
		IEnumerable<T> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal);
    }
}


