using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronSolicitudesObservacionesBL<T>
	{

        T Single(int IdConsultaPadronObservacion );  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
        IEnumerable<T> Get(int IdConsultaPadron);
        void Leer(int IdConsultaPadronObservacion);
    }
}


