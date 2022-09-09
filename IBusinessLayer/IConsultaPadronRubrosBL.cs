using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronRubrosBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronRubro);  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		//IEnumerable<T> GetByFKIdTipoActividad(int IdTipoActividad);
		//IEnumerable<T> GetByFKIdTipoDocumentoReq(int IdTipoDocumentoReq);
		//IEnumerable<T> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental);
    }
}


