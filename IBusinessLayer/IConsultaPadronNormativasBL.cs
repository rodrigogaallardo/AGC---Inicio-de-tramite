using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronNormativasBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronTipoNormativa );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKIdTipoNormativa(int IdTipoNormativa);
		IEnumerable<T> GetByFKIdEntidadNormativa(int IdEntidadNormativa);
    }
}


