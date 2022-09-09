using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Mailer
{
    public partial class MailSolicitudNueva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public StaticClass.MailSolicitudNueva GetData()
        {

            object pid_solicitud = Request.QueryString["id_solicitud"];
            object pcodigo_seguridad = Request.QueryString["codigo_seguridad"];

            if (pid_solicitud != null && pcodigo_seguridad != null)
            {
                int id_solicitud = Convert.ToInt32(pid_solicitud);
                string codigo_seguridad = pcodigo_seguridad.ToString();

                StaticClass.MailSolicitudNueva result = new StaticClass.MailSolicitudNueva();
                result.id_solicitud = id_solicitud.ToString();
                result.codigo_seguridad = codigo_seguridad;
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}