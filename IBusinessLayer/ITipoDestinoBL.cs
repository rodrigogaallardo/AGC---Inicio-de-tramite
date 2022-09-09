using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITipoDestinoBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id );  
    }
}


