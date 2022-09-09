using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IMenuesBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);
        IEnumerable<MenuesDTO> GetMenuByUserId(Guid userId);

    }
}
