using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SIPSA.Entity;

namespace SSIT.Controls
{
    public partial class Titulares : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(int id_solicitud)
        {
            TitularesBL titularesBL = new TitularesBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var lstTitulares = titularesBL.GetTitularesSolicitud(id_solicitud);
            var lstFirmantes = firmantesBL.GetFirmantesSolicitud(id_solicitud);

            grdTitularesHab.DataSource = lstTitulares;
            grdTitularesHab.DataBind();

            grdTitularesTra.DataSource = lstFirmantes;
            grdTitularesTra.DataBind();
        }
    }
}