using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTitularPj );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal);
		IEnumerable<T> GetByFKIdFirmantePj(int IdFirmantePj);
        IEnumerable<T> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica);
    }
}


