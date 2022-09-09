using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosInstrumentoJudicialBL<T>
	{
        T Single(int id_insjud);
        IEnumerable<T> GetByFKIdActanotarial(int id_actanotarial);
    }
}


