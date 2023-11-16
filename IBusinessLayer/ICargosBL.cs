using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ICargosBL<T>
	{
		IEnumerable<T> GetAll();

        IEnumerable<T> GetCargos();
    }
}


