using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IEscribanoBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int Matricula);  
    }
}


