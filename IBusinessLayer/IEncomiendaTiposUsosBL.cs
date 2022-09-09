using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaTiposUsosBL<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<EncomiendaTiposUsosDTO> GetByFKIdTipoDestinoGrupo(int id_tipo_destino, int nroGrupo);
    }
}
