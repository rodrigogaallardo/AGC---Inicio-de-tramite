using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTitularPersonaJuridica );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal);
    }
}


