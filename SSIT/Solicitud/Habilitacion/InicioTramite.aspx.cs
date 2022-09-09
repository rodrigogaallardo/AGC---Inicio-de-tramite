using SSIT.App_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion
{
    public partial class InicioTramite : SecurePage
    {

        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComprobarSolicitud();
            }
        }

        private void ComprobarSolicitud()
        {
            if (!string.IsNullOrEmpty(Page.RouteData.Values["id_solicitud"].ToString()))
            {
                this.id_solicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());
                lbl_ID.Text = this.id_solicitud.ToString();
                updMsj.Update();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_TITULAR_SOLICITUD + "{0}", id_solicitud));
        }
    }
}