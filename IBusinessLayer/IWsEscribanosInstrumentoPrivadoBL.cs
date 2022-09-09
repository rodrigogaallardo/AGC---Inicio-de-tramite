using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosInstrumentoPrivadoBL<T>
	{
        T Single(int id_inspriv);
        IEnumerable<T> GetByFKIdActanotarial(int id_actanotarial);
    }
}


