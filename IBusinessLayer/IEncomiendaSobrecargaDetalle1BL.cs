using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaSobrecargaDetalle1BL<T>
    {
        IEnumerable<T> GetByFKIdSobrecarga(int id_sobrecarga);

        int Insert(EncomiendaSobrecargaDetalle1DTO objectDto);

        void Delete(EncomiendaSobrecargaDetalle1DTO objectDto);
    }
}
