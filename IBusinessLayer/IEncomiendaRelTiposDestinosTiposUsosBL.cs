using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaRelTiposDestinosTiposUsosBL<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetByFKIdTipoDestinoTipoTipo(int id_tipo_uso, int id_tipo_destino);
    }
}
