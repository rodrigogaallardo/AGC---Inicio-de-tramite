using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaRubrosBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaRubro );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
        //IEnumerable<T> GetByFKIdTipoActividad(int IdTipoActividad);
        //IEnumerable<T> GetByFKIdTipoDocumentoRequerido(int IdTipoDocumentoRequerido);
        //IEnumerable<T> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental);
        IEnumerable<T> GetRubros(int IdEncomienda);
        bool Encomienda_ValidarCargaProfesional_porRubro(int id_encomienda);

        void ActualizarSubTipoExpediente(int IdEncomienda);
    }
}


