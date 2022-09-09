using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IRelRubrosCNSolicitudesNuevasBL<T>
    { 

        int Insert(T objectDto);
    IEnumerable<T> GetAll();
        IEnumerable<T> GetRByIdSolicitud(int IdSolicitud);
        // IEnumerable<RubrosIncendioDTO> getRubrosIncendioEncomienda(int IdEncomienda);
    }
}
