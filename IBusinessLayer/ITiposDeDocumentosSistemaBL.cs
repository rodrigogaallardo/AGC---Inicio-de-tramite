using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface ITiposDeDocumentosSistemaBL<T>
    {
        IEnumerable<T> GetAll();

        T GetByCodigo(string cod_tipodocsis);

        T Single(int id_tipdocsis);
    }
}
