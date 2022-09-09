using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IEncomiendaRubrosSubCNBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto, Guid userlogued);
        void Update(T objectDto);
        void Delete(T objectDto);
        T Single(int IdEncomiendaRubro);
        IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
    }
}
