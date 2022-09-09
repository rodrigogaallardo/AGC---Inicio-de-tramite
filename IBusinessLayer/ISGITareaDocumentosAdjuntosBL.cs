using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITareaDocumentosAdjuntosBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int id_doc_adj );  
		IEnumerable<T> GetByFKid_tramitetarea(int id_tramitetarea);
		IEnumerable<T> GetByFKid_tdocreq(int id_tdocreq);
    }
}


