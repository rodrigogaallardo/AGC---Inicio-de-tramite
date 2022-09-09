using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasDatosLocalBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTransferenciaDatosLocal );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKCreateUser(Guid CreateUser);
		IEnumerable<T> GetByFKLastUpdateUser(Guid LastUpdateUser);
    }
}


