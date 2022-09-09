using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ILocalidadBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<T> GetByFKIdProvincia(int IdProvincia);
        IEnumerable<T> GetByFKIdProvinciaExcluir(int IdProvincia, bool Excluir);
    }
}


