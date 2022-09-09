using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronDatosLocalBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronDatosLocal );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKCreateUser(Guid CreateUser);
		IEnumerable<T> GetByFKLastUpdateUser(Guid LastUpdateUser);
    }
}


