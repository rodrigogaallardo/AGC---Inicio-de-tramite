using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosPersonasFisicasBL<T>
	{
        T Single(int id_wsPersonaFisica);
        IEnumerable<T> GetByFKIdActanotarial(int id_actanotarial);
    }
}


