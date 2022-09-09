using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface ITitularesBL
    {
        IEnumerable<TitularesDTO> GetTitularesEncomienda(int id_encomienda);
        IEnumerable<TitularesSHDTO> GetTitularesEncomiendaSH(int IdPersonaJuridica);
    }
}
