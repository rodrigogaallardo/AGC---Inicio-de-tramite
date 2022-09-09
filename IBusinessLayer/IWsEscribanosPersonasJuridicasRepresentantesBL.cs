using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IWsEscribanosPersonasJuridicasRepresentantesBL<T>
	{
        T Single(int id_wsPersonaJuridica);
        IEnumerable<T> GetByFKIdWsPersonasJuridica(int id_wsPersonaJuridica);
    }
}


