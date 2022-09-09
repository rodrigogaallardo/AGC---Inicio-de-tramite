using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITipoVentilacionBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id );  
    }
}


