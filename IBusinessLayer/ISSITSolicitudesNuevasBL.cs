using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesNuevasBL<T>
	{
        int Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitud );  		
        bool anularSolicitud(int id_solicitud, Guid userid);
        bool confirmarSolicitud(int id_solicitud, Guid userid);
       
    }
}


