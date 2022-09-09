using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class DatosLocal : System.Web.UI.UserControl
    {

        SSITSolicitudesDatosLocalBL datosLocalBL = new SSITSolicitudesDatosLocalBL();
        UbicacionesBL ubicBL = new UbicacionesBL();
        CallesBL callesBL = new CallesBL();

        public void CargarDatos(SSITSolicitudesDTO ssitDTO)
        {

            List<SSITSolicitudesUbicacionesDTO> lstUbic = ssitDTO.SSITSolicitudesUbicacionesDTO.ToList();

            foreach (var item in lstUbic)
            {
                var ubicacion = ubicBL.Single(item.IdUbicacion.Value);
                String dir = "";
                foreach (var p in item.SSITSolicitudesUbicacionesPuertasDTO)
                {
                    dir = callesBL.GetNombre(p.CodigoCalle, p.NroPuerta) + " " + p.NroPuerta;
                    break;
                }

                imgMapa1.ImageUrl = Funciones.GetUrlMapa(ubicacion.Seccion.Value, ubicacion.Manzana, ubicacion.Parcela, dir);
                imgMapa2.ImageUrl = Funciones.GetUrlCroquis(ubicacion.Seccion.Value, ubicacion.Manzana, ubicacion.Parcela, dir);

            }

            SSITSolicitudesDatosLocalDTO dl = datosLocalBL.Single(ssitDTO.IdSolicitud);
            if (dl != null)
            {
                txtSuperficieCubierta.Text = dl.superficie_cubierta_dl.ToString();
                txtSuperficieDescubierta.Text = dl.superficie_descubierta_dl.ToString();

                txtSuperficieTotal.Text = Convert.ToString(dl.superficie_cubierta_dl + dl.superficie_descubierta_dl);
            }


        }

    }
}