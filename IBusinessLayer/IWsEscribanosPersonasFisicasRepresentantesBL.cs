using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosPersonasFisicasRepresentantesBL<T>
	{
        T Single(int id_wsPersonaFisica);
        IEnumerable<T> GetByFKIdWsPersonasFisica(int id_wsPersonaFisica);
    }
}


