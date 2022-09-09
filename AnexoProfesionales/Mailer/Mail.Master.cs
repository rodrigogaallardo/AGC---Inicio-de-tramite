using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Mailer
{
    public partial class Mailer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public string Titulo
        {
            set
            {
                lblTitulo.Text = value;
            }
            get
            {
                return lblTitulo.Text;
            }
        }

    }
}