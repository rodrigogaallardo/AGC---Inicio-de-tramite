using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IEncomienda_RubrosCN_DepositoBL<T>
    {
        List<T> GetByEncomienda(int IdEncomienda);
    }
}
