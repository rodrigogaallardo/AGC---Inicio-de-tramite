using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronPlantasBL<T>
	{
		IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadronTipoSector );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
		IEnumerable<T> GetByFKIdTipoSector(int IdTipoSector);
        void ActualizarPlantas(List<T> plantas);
    }
}


