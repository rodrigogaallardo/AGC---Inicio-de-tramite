using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinesLayer.Implementation;
using SSIT.App_Components;
using StaticClass;
using System.Collections.Generic;
using System.Globalization;
using DataTransferObject;

namespace SSIT.Solicitud.Consulta_Padron.Controls
{
    public partial class ucDatosSolicitud : System.Web.UI.UserControl
    {
        public int IdCPadron
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }

        public class ucActualizarEstadoVisorEventsArgs : EventArgs
        {
            public string NroExpediente { get; set; }
        }

        public delegate void EventHandlerActualizarEstadoVisor(object sender, ucActualizarEstadoVisorEventsArgs e);
        public event EventHandlerActualizarEstadoVisor EventActualizarEstadoVisor;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                ConsultaPadronSolicitudesBL cpadron = new ConsultaPadronSolicitudesBL();
                nro_solicitud.Text = cpadron.Single(IdCPadron).IdConsultaPadron.ToString();
            }
        }

        public void CargarDatos(ConsultaPadronSolicitudesDTO cpadron)
        {
            nro_solicitud.Text = cpadron.IdConsultaPadron.ToString();

            txtObservaciones.Text = cpadron.Observaciones;
            txtNroExpediente.Text = cpadron.NroExpedienteAnterior;
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarDatos();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                //mensaje a pantalla
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updContinuar, updContinuar.GetType(), "mostrarPopup('pnlInformacion',200);", "mostrarPopup('pnlInformacion',200);", true);
            }

            if (string.IsNullOrEmpty(lblError.Text))
            {
                Response.Redirect("~/" + RouteConfig.AGREGAR_UBICACION_CPADRON + IdCPadron);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarDatos();
            updDatosSolicitud.Update();
            if (!String.IsNullOrWhiteSpace(txtObservaciones.Text) && !String.IsNullOrWhiteSpace(txtNroExpediente.Text))
            {
                pnlContenido.Visible = false;
                updDatosSolicitud.Update();
                ucActualizarEstadoVisorEventsArgs es = new ucActualizarEstadoVisorEventsArgs();
                es.NroExpediente = txtNroExpediente.Text;
                this.EventActualizarEstadoVisor(this, es);
            }
        }

        public void botonesDeVisor()
        {
            pnlBotonesGuardar.Visible = false;
        }

        private void ActualizarDatos()
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
            var cpadron = consultaPadronSolicitudesBL.Single(IdCPadron);

            cpadron.NroExpedienteAnterior = txtNroExpediente.Text;
            cpadron.LastUpdateUser = userid;
            cpadron.Observaciones = txtObservaciones.Text;

            consultaPadronSolicitudesBL.Update(cpadron);
        }
    }
}
