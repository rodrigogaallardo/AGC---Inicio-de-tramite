using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasFirmantesPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdFirmantePersonaFisica );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdPersonaFisica(int IdPersonaFisica);
		IEnumerable<T> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal);
		IEnumerable<T> GetByFKIdTipoCaracter(int IdTipoCaracter);
    }
}


