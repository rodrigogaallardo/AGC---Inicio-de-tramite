using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaPlanosBL<T>
	{
        T Single(int id_encomienda_plano);
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
		IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
        IEnumerable<T> GetByFKIdEncomiendaTipoPlano(int IdEncomienda, int TipoPlano);
        bool existe(int id_tipo_plano, string nombre, int id_encomienda);
    }
}


