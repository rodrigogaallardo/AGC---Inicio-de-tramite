using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SIPSA.Entity;

namespace AnexoProfesionales.Controls
{
    public partial class CargaPlanos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(EncomiendaDTO encomienda)
        {

            var elements = encomienda.EncomiendaPlanosDTO;
            foreach (var elem in elements)
                elem.url = string.Format("~/" + RouteConfig.DESCARGA_PLANO + "{0}", Functions.ConvertToBase64String(elem.id_encomienda_plano));

            grdPlanos.DataSource = elements;
            grdPlanos.DataBind();
        }
    }
}