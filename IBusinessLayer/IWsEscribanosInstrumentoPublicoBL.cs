using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosInstrumentoPublicoBL<T>
	{
        T Single(int id_inspub);
        IEnumerable<T> GetByFKIdActanotarial(int id_actanotarial);
    }
}


