using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaTitularesPersonasJuridicasPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTitularPj );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal);
		IEnumerable<T> GetByFKIdFirmantePj(int IdFirmantePj);
        IEnumerable<T> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica);
    }
}


