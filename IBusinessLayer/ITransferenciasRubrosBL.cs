using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasRubrosBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		//void Update(T objectDto); 
		//void Delete(T objectDto); 
        T Single(int IdTransferenciaRubro);  
		//IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		//IEnumerable<T> GetByFKIdTipoActividad(int IdTipoActividad);
		//IEnumerable<T> GetByFKIdTipoDocumentoReq(int IdTipoDocumentoReq);
		//IEnumerable<T> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental);
    }
}


