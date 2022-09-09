using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaFirmantesPersonasJuridicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdFirmantePj );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
		IEnumerable<T> GetByFKIdPersonaJuridica(int IdPersonaJuridica);
		IEnumerable<T> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal);
		IEnumerable<T> GetByFKIdTipoCaracter(int IdTipoCaracter);
        IEnumerable<T> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica);
    }
}


