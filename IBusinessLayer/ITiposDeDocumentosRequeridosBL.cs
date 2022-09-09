using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface ITiposDeDocumentosRequeridosBL<T>
    {
        IEnumerable<T> GetAll();
        T Single(int id_tipdocsis);
        IEnumerable<T> GetVisibleSSIT();
        IEnumerable<T> GetVisibleSSITXTipoTramite(int IdTipoTramite);
    }
}
