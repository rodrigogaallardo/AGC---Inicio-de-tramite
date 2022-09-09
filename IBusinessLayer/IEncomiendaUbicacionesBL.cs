using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaUbicacionesBL<T>
	{
        bool Insert(T objectDto);        
		void Delete(T objectDto); 
        T Single(int IdEncomiendaUbicacion );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);      
		IEnumerable<T> GetByFKIdUbicacion(int IdUbicacion);      
        bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2);
    }
}


