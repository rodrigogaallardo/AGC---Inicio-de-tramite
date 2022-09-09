using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaFirmantesPersonasFisicasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdFirmantePf );  
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
		IEnumerable<T> GetByFKIdPersonaFisica(int IdPersonaFisica);
		IEnumerable<T> GetByFKIdTipodocPersonal(int IdTipodocPersonal);
		IEnumerable<T> GetByFKIdTipoCaracter(int IdTipoCaracter);
        IEnumerable<T> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica);
    }
}


