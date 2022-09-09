using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaDocumentosAdjuntosBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id_docadjunto);
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
		IEnumerable<T> GetByFKIdEncomiendaTipoSis(int IdEncomienda, int idTipo);

        IEnumerable<T> GetByFKListIdEncomiendaTipoSis(List<int> IdsEncomienda, int idTipo);
    }
}


