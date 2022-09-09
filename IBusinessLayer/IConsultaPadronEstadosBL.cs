using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronEstadosBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int IdEstado );  
    }
}


