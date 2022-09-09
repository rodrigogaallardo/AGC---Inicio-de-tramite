using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEncomiendaEstadosBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int IdEstado );  
    }
}


