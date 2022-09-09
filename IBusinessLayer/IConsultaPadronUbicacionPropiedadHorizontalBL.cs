using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronUbicacionPropiedadHorizontalBL<T>
	{
        IEnumerable<T> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadron);
    }
}


