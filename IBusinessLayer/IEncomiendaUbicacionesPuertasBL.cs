using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaUbicacionesPuertasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaPuerta );  
		IEnumerable<T> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion);
    }
}


