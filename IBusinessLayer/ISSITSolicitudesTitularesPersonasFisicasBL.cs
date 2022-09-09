using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesTitularesPersonasFisicasBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaFisica );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
    }
}


