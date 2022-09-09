using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IUsuariosProfesionalesRolesClasificacionBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<T> GetByFKRoleID(Guid RoleID);
		IEnumerable<T> GetByFKIdClasificacion(int IdClasificacion);
		IEnumerable<T> GetByFKCreateUser(Guid CreateUser);
    }
}


