using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaNormativasBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaNormativa );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
    }
}


