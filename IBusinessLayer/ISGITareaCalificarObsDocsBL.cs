using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITareaCalificarObsDocsBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<SGITareaCalificarObsDocsGrillaDTO> GetByFKIdObsGrupo(int id_ObsGrupo);
        bool ExistenObservacionesdetalleSinProcesar(int id_solicitud);
    }
}


