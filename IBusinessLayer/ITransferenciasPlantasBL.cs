using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasPlantasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdTransferenciaTipoSector );  
		IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
		IEnumerable<T> GetByFKIdTipoSector(int IdTipoSector);
        void ActualizarPlantas(List<T> plantas);
    }
}


