
namespace IBusinessLayer
{
    public interface IEncomiendaDatosLocalBL<T>
	{
        int Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomiendaDatosLocal );  
		T GetByFKIdEncomienda(int IdEncomienda);      
    }
}


