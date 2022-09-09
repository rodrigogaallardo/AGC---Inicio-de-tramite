using BusinesLayer.Implementation;
using DataAcess.EntityCustom;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class AvisosSolicitud : System.Web.UI.UserControl
    {
        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_solicitud"].ToString(), out ret);
                return ret;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNotificacionesDeSolicitud();
            }
        }

        private void CargarNotificacionesDeSolicitud()
        {
            MailsBL MailBL = new MailsBL();

            var listAvisos = MailBL.GetNotificacionesByIdSolicitud(id_solicitud);

            grdMails.DataSource = listAvisos;
            grdMails.DataBind();

            updPnlNotificaciones.Update();
        }


        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            hfMailID.Value = (sender as LinkButton).CommandArgument;
            int id_mail;
            int.TryParse(hfMailID.Value, out id_mail);
            MailsBL mailBL = new MailsBL();
            var mailDTO = mailBL.Single(id_mail);

            TableCell IDCorreo = (TableCell)FindControl("IDCorreo");
            TableCell Email = (TableCell)FindControl("Email");
            TableCell Asunto = (TableCell)FindControl("Asunto");
            //TableCell Proceso = (TableCell)FindControl("Proceso");
            TableCell FecAlta = (TableCell)FindControl("FecAlta");
            TableCell FecEnvio = (TableCell)FindControl("FecEnvio");
            TableCell CantInt = (TableCell)FindControl("CantInt");
            TableCell Prioridad = (TableCell)FindControl("Prioridad");
            TableCell CuerpoHTML = (TableCell)FindControl("CuerpoHTML");

            //CuerpoHTML.Text = mailDTO.Mail_Html;

            //Message.Attributes["src"] = "http://localhost:56469/Handlers/Mail_Handler.ashx?HtmlID=387989";
            //ltlHTMLBody.Text = mailDTO.Mail_Html;
            //var htmlBody = HttpContext.Current.Server.HtmlDecode(mailDTO.Mail_Html.Replace("'","\"")).Replace(System.Environment.NewLine, "");
            /*
             myString = myString.Replace(System.Environment.NewLine, "replacement text")
             */

            IDCorreo.Text = mailDTO.Mail_ID.ToString();
            Email.Text = mailDTO.Mail_Email.ToString();
            Asunto.Text = mailDTO.Mail_Asunto.ToString();
            FecAlta.Text = mailDTO.Mail_FechaAlta.ToString();
            CantInt.Text = mailDTO.Mail_Intentos.ToString();
            Prioridad.Text = mailDTO.Mail_Prioridad.ToString();
            FecEnvio.Text = mailDTO.Mail_FechaEnvio.ToString();

            Message.Attributes["src"] = "~/Handlers/Mail_Handler.ashx?HtmlID=" + id_mail;



            ScriptManager.RegisterStartupScript(updPnlNotificaciones, updPnlNotificaciones.GetType(), "showfrmAvisoNotificacion", "showfrmAvisoNotificacion('');", true);
        }


    }
}