using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Titulo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(int id_solicitud, string titulo)
        {
            lblTitulo.Text = titulo;
            lblSolicitud.Text = id_solicitud.ToString();
        }
    }
}