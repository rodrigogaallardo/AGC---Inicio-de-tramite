using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITramitesTareasTransferenciasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<T> GetByFKIdTramiteTarea(int IdTramiteTarea);
        IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
        T GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud);
    }
}


