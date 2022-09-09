using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaRectificatoriaBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id_encrec);
        bool Insert(T objectDto);        
		void Delete(T objectDto); 
		T GetByFKIdEncomienda(int IdEncomienda);
    }
}


