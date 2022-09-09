using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasSolicitudesHistorialEstadosBL<T>
    {
        List<TransferenciasSolicitudesHistorialEstadosGrillaDTO> GetByFKIdSolicitudGrilla(TransferenciasSolicitudesDTO solicitud);
    }
}
