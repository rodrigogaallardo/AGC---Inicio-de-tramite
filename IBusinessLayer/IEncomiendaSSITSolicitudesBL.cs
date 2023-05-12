using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaSSITSolicitudesBL<T>
	{
        T Single(int id_encomiendaSolicitud);
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
        IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
        bool existe(int id_solicitud, int id_encomienda);
    }
}


