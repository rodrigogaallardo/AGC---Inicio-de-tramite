using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Errores
{
    public partial class ErrorElmah : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarError();
            }
        }

        private void MostrarError()
        {
            if (Page.RouteData.Values["id"] != null)
            {

                string id = Page.RouteData.Values["id"].ToString();
                lblError.Text = "Error: " + LogError.Get(id);
            }
        }
    }
}