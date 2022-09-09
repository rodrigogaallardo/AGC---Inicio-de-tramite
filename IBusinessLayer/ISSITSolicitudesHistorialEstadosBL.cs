using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesHistorialEstadosBL<T>
	{
        List<SSITSolicitudesHistorialEstadosGrillaDTO> GetByFKIdSolicitudGrilla(SSITSolicitudesDTO solicitud);
        void Delete(IEnumerable<T> historialesDTO);
    }
}


