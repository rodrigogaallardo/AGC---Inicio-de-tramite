using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITipoSuperficieBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int TipoDocumentoPersonalId );  
    }
}


