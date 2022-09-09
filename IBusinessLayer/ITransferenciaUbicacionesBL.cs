using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciaUbicacionesBL<T>
	{		
        bool Insert(T objectDto);        
		void Delete(T objectDto); 
        T Single(int IdTransferenciaUbicacion);  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
    }
}


