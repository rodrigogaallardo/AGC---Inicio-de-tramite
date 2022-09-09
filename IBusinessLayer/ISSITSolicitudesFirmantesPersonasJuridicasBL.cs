using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesFirmantesPersonasJuridicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdFirmantePj );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal);
		IEnumerable<T> GetByFKIdTipoCaracter(int IdTipoCaracter);
        IEnumerable<T> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica);
        string[] GetCargoFirmantesPersonasJuridicas();
    }
}


