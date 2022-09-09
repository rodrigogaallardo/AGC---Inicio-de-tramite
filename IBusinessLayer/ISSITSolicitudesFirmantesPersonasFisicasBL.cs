using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesFirmantesPersonasFisicasBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdFirmantePf );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
        IEnumerable<T> GetByIdSolicitudIdPersonaFisica(int id_solicitud, int IdPersonaFisica);
    }
}


