using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronDocumentosAdjuntosBL<T>
	{
		//IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int Id );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		//IEnumerable<T> GetByFKIdTipodocumentoRequerido(int IdTipodocumentoRequerido);
		//IEnumerable<T> GetByFKIdTipoDocumentoSistema(int IdTipoDocumentoSistema);
    }
}


