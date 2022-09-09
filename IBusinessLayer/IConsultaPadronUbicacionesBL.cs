using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronUbicacionesBL<T>
	{		
        bool Insert(T objectDto);        
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronUbicacion );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
    }
}


