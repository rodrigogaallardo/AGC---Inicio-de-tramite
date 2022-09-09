using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaSobrecargaDetalle2BL<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<EncomiendaSobrecargaDetalle2DTO> GetByFKIdSobrecargaDetalle1(int id_sobrecarga_detalle1);

        bool Insert(EncomiendaSobrecargaDetalle2DTO objectDto);

        void Delete(EncomiendaSobrecargaDetalle2DTO objectDto);
    }
}
