using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IRubrosDepositosCNBL<T>
    {
        IEnumerable<T> GetAll();
        RubrosDepositosCNDTO Single(int idDeposito);
        void Insert(RubrosDepositosCNDTO rubrosDepositosCN, Guid userid);
    }
}
