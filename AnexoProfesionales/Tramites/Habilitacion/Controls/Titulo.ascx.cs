using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Tramites.Habilitacion.Controls
{
    public partial class Titulo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(int id_encomienda, string titulo)
        {
            lblTitulo.Text = titulo;
            lblEncomienda.Text = id_encomienda.ToString();
        }
    }
}