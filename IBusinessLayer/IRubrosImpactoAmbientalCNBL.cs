using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IRubrosImpactoAmbientalCNBL<T>
    {
        //IEnumerable<T> GetAll();        
        T Single(int IdRubroImpactoAmbiental);
        IEnumerable<T> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental);
    }
}
