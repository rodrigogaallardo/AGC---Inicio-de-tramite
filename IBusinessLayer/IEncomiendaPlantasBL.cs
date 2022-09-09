using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaPlantasBL<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<EncomiendaPlantasDTO> GetByFKIdEncomienda(int IdEncomienda);

        T GetByFKIdEncomiendaIdEncomiendaTiposector(int IdEncomienda, int IdEncomiendaTiposector);
    }
}
