using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaTitularesPersonasfisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaFisica );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
		IEnumerable<T> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal);
		IEnumerable<T> GetByFKIdTipoiibb(int IdTipoiibb);
		IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
        //IEnumerable<T> GetByIdEncomiendaCuitIdPersonaFisica(int id_encomienda, string Cuit, int IdPersonaFisica);
        //IEnumerable<T> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica);
        bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2);
    }
}


