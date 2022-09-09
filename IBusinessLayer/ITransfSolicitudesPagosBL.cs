using DataTransferObject;
using StaticClass;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransfSolicitudesPagosBL<T>
    {
        bool Insert(T objectDto);

        IEnumerable<T> GetByFKIdSolicitud(int id_solicitud);

        IEnumerable<clsItemGrillaPagos> GetGrillaByFKIdSolicitud(int id_solicitud);
    }
}
