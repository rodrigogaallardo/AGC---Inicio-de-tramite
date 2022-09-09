using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITareaCalificarObsGrupoBL<T>
	{
        T Single(int Id );  
		IEnumerable<SGITareaCalificarObsGrupoGrillaDTO> GetByFKIdSolicitud(int IdSolicitud);
    }
}


