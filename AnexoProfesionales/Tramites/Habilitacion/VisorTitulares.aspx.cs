using BusinesLayer.Implementation;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Tramites.Habilitacion
{
    public partial class VisorTitulares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ComprobarSolicitud();
        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                var id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
              
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var encDTO = encomiendaBL.Single(id_encomienda);
                if (encDTO != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != encDTO.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                        ucVisorTitulares.CargarDatos(encDTO);
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }
    }
}