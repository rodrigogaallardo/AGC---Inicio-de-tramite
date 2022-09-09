using System.Collections.Generic;
using System;

namespace IBusinessLayer
{
    public interface IEncomiendaRubrosCNBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto, Guid userlogued);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaRubro );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
        IEnumerable<T> GetRubros(int IdEncomienda);
        //bool Encomienda_ValidarCargaProfesional_porRubro(int id_encomienda);

        void ActualizarSubTipoExpediente(int IdEncomienda);
    }
}


