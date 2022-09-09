using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITiposDePlanosBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int Id );  
    }
}


