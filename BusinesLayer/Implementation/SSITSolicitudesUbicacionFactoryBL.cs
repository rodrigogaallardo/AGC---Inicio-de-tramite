using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesManagerBL
    {
        public SSITSolicitudesUbicacionBaseDTO GetUbicacionBySolicitud(SSITSolicitudesOrigenDTO ssitSolicitudesOrigenDTO)
        {
            SSITSolicitudesUbicacionBaseDTO ssitSolicitudesUbicacionBaseDTO;

            if (ssitSolicitudesOrigenDTO.id_solicitud != null)
            {
                SSITSolicitudesUbicacionesDTO ssitSolicitudesUbicacionesDTO;
                SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                ssitSolicitudesUbicacionesDTO = ssitBL.Single(Convert.ToInt32(ssitSolicitudesOrigenDTO.id_solicitud)).SSITSolicitudesUbicacionesDTO.FirstOrDefault();
                ssitSolicitudesUbicacionBaseDTO = ssitSolicitudesUbicacionesDTO;
            }
            else
            {
                TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
                TransferenciaUbicacionesDTO transferenciaUbicacionesDTO;
                transferenciaUbicacionesDTO = transferenciasSolicitudesBL.Single(Convert.ToInt32(ssitSolicitudesOrigenDTO.id_transf_origen)).Ubicaciones.FirstOrDefault();
                ssitSolicitudesUbicacionBaseDTO = transferenciaUbicacionesDTO;
            }

            return ssitSolicitudesUbicacionBaseDTO;
        }
    }
}
