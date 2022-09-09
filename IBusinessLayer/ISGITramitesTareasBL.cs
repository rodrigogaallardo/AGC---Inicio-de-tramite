using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITramitesTareasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTramiteTarea );  
		IEnumerable<T> GetByFKIdTarea(int IdTarea);
		IEnumerable<T> GetByFKIdResultado(int IdResultado);
		IEnumerable<T> GetByFKUsuarioAsignadoTramiteTarea(Guid UsuarioAsignadoTramiteTarea);
		IEnumerable<T> GetByFKIdProximaTarea(int IdProximaTarea);
    }
}


