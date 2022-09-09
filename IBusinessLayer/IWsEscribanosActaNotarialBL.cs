using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosActaNotarialBL<T>
	{
        T Single(int id_caa );
        IEnumerable<T> GetByFKListIdEncomienda(List<int> lstEncomiendas);
        T GetByFKIdEncomienda(int IdEncomienda);
    }
}


