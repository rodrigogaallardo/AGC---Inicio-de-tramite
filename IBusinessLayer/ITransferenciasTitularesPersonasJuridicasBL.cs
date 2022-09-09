using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasTitularesPersonasJuridicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdPersonaJuridica );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdTipoiibb(int IdTipoiibb);
		IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
		IEnumerable<T> GetByFKCreateUser(Guid CreateUser);
		IEnumerable<T> GetByFKLastUpdateUser(Guid LastUpdateUser);
    }
}


