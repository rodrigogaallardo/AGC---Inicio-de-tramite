using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaConformacionLocalBL<T>
    {
        IEnumerable<T> GetByFKIdEncomienda(int IdEncomienda);
        T Single(int id_encomiendaconflocal);
        int Insert(T objectDto);
        bool Update(T objectDto);
        void Delete(T objectDto);
    }
}
