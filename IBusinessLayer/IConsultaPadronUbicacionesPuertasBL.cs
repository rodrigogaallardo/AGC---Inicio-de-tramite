using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronUbicacionesPuertasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronPuerta );  
		IEnumerable<T> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadronUbicacion);
    }
}


