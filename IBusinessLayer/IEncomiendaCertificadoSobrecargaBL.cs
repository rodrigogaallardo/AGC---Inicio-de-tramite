using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaCertificadoSobrecargaBL<T>
    {
        IEnumerable<T> GetAll();

        EncomiendaCertificadoSobrecargaDTO GetByFKIdEncomiendaDatosLocal(int IdEncomiendaDatosLocal);

        int Insert(EncomiendaCertificadoSobrecargaDTO objectDto);

        bool Update(EncomiendaCertificadoSobrecargaDTO objectDto);

        void Delete(EncomiendaCertificadoSobrecargaDTO objectDto);
    }
}
