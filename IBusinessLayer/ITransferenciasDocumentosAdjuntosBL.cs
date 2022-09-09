using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasDocumentosAdjuntosBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
    }
}


