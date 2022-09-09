using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ITramitesBL<T>
    {
        IEnumerable<T> GetTramites(Guid userid, int id_solicitud, int id_tipotramite, int id_estado, string nro_expediente, int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount);
        IEnumerable<T> GetTramitesNuevos(Guid userid, int id_solicitud, int id_tipotramite, int id_estado, string nro_expediente, int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount);
    }
}
