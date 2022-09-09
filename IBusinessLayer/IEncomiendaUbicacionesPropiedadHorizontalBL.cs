using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaUbicacionesPropiedadHorizontalBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaPropiedadHorizontal );  
		IEnumerable<T> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion);
		IEnumerable<T> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal);
    }
}


