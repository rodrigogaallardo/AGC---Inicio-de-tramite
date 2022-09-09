using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConceptosBUIIndependientesBL<T>
	{
		IEnumerable<T> GetAll();
        T Single(int id_concepto);
        IEnumerable<T> GetList(string[] arrCodConceptosCobrar);
    }
}


