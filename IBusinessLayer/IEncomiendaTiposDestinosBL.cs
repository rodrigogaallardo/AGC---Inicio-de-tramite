using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaTiposDestinosBL<T>
    {
        IEnumerable<T> GetAll();

        T Single(int id_tipo_destino);
        IEnumerable<EncomiendaTiposDestinosDTO> GetByFKIdTipoSobrecarga(int id_tipo_sobrecarga);
    }
}
