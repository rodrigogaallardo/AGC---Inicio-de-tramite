using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITDocumentosAdjuntosEntityBL<T>
	{
        T Single(int Id);
        IEnumerable<T> GetByFKIdSolicitud(int id_solicitud);
        IEnumerable<T> GetByFKIdSolicitudGenerados(int id_solicitud);
        IEnumerable<T> GetByFKListIdEncomienda(List<int> lstIdEncomienda);
    }
}


