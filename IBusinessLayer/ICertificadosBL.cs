using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ICertificadosBL<T>
	{
        T Single(int id_certificado);
        IEnumerable<T> GetByFKNroTipo(string NroTramite, int TipoTramite);
        IEnumerable<T> GetByFKListNroTipo(List<string> list, int TipoTramite);
    }
}


