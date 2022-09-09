using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronTitularesSolicitudPersonasJuridicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaJuridica );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKIdTipoSociedad(int IdTipoSociedad);
		IEnumerable<T> GetByFKIdTipoiibb(int IdTipoiibb);
		IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
    }
}


