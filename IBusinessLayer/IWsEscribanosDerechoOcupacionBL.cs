using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosDerechoOcupacionBL<T>
	{
        T Single(int id_wsPersonaJuridica);
        IEnumerable<T> GetByFKIdActanotarial(int id_actanotarial);
    }
}


