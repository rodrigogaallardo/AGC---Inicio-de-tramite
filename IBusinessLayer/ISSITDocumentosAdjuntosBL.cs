using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITDocumentosAdjuntosBL<T>
	{
        bool Insert(T objectDto, bool ForzarGenerar);
        void Update(T objectDto);
        void Delete(T objectDto);
        IEnumerable<T> GetByFKIdSolicitud(int id_solicitud);
    }
}


