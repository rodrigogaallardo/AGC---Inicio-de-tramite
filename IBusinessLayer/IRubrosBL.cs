using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IRubrosBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
        T Single(int IdRubro);
        int TipoDocumentoRequeridoMayor(int[] IdRubros);

        IEnumerable<RubrosIncendioDTO> getRubrosIncendioEncomienda(int IdEncomienda);
    }
}
