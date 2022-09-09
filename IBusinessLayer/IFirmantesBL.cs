using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IFirmantesBL
    {
        IEnumerable<FirmantesDTO> GetFirmantesPJEncomienda(int id_encomienda);
        IEnumerable<FirmantesDTO> GetFirmantesEncomienda(int id_encomienda);
        IEnumerable<FirmantesPJDTO> GetFirmantesPJPF(int id_firmante_pj);
        IEnumerable<FirmantesPJDTO> GetFirmantesPJ(int id_firmante_pj);
    }
}
