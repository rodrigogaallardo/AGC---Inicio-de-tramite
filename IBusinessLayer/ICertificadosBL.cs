using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ICertificadosBL<T>
	{
        T Single(int id_certificado);
        IEnumerable<T> GetByFKNroTipo(int NroTramite, int TipoTramite);
        IEnumerable<T> GetByFKListNroTipo(List<int> list, int TipoTramite);
    }
}


