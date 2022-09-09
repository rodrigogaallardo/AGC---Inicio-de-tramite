using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITipoIluminacionBL<T>
	{
		IEnumerable<T> GetAll();
 
        T Single(int id );  
    }
}


