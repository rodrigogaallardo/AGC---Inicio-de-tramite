using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface ISobrecargasEntityBL<T>
    {
        IEnumerable<T> getSobrecargaDetallado(int id_sobrecarga);
    }
}
