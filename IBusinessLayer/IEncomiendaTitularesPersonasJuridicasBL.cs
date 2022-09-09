using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaTitularesPersonasJuridicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaJuridica );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
		IEnumerable<T> GetByFKIdTipoSociedad(int IdTipoSociedad);
		IEnumerable<T> GetByFKIdTipoIb(int IdTipoIb);
        IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
        IEnumerable<T> GetByIdEncomiendaCuitIdPersonaJuridica(int id_solicitud, string Cuit, int IdPersonaJuridica);
        IEnumerable<T> GetByIdEncomiendaIdPersonaJuridica(int id_solicitud, int IdPersonaJuridica);
        bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2);
    }
}


