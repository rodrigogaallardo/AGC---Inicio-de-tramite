using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IDocumentosAdjuntosBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id_docadjunto);
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
		IEnumerable<T> GetByFKIdSolicitudIdAgrupamiento(int id_solicitud, int id_agrupamiento);
    }
}


