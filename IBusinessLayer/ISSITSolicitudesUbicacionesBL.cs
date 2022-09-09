using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesUbicacionesBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitudUbicacion );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);      
		IEnumerable<T> GetByFKIdUbicacion(int IdUbicacion);      
		IEnumerable<T> GetByFKIdSubtipoUbicacion(int IdSubtipoUbicacion);
        IEnumerable<T> GetByFKIdZonaPlaneamiento(int IdZonaPlaneamiento);
        //IEnumerable<SSITSolicitudesUbicacionModelDTO> GetUbicacion(int IdSSITSolicitudesUbicacion);
        bool validarUbicacionClausuras(int id_solicitud);
        bool validarUbicacionInhibiciones(int id_solicitud);
    }
}


