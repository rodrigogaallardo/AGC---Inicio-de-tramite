using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronTitularesSolicitudPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaFisica );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal);
		IEnumerable<T> GetByFKIdTipoiibb(int IdTipoiibb);
		IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
    }
}


