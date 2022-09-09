using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTitularPersonaJuridica );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal);
    }
}


