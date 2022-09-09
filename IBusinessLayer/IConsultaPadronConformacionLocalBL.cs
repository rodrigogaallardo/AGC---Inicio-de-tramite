using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronConformacionLocalBL<T>
	{
		
        //bool Insert(T objectDto);        
		//void Update(T objectDto); 
		//void Delete(T objectDto); 
        T Single(int IdConsultaPadronConformacionLocal );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
	
    }
}


