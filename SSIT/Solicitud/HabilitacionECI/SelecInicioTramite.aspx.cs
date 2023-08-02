using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Web.Security;
using System.Web.UI;
using static StaticClass.Constantes;

namespace SSIT.Solicitud.HabilitacionECI
{
    public partial class SelecInicioTramite : SecurePage
    {

        public string HabilitacionECI { get; set; }

        AmpliacionesBL blSol = new AmpliacionesBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HabilitacionECI = TipoTramiteDescripcion.HabilitacionECI;
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
            }

        }

        protected void lnkNuevaECI_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjecutarScript(this, "showfrmConfirmarNuevaAmpliacion();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(this, "showfrmError();");
            }
        }

        protected void lnkCrearECI_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjecutarScript(this, "showfrmExpSeg()();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(this, "showfrmError();");
            }
        }
        protected void btnNuevaAmpliacion_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string url_destino = "";
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            int id_solicitud = 0;
            try
            {

                sol.Servidumbre_paso = false;

                sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;

                sol.IdTipoTramite = (int)Constantes.TipoTramite.HabilitacionECIHabilitacion;
                sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.NoDefinido;
                sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                sol.CreateDate = DateTime.Now;
                sol.CreateUser = userid;
                sol.SSITSolicitudesOrigenDTO = null;
                sol.EsECI = true;

                id_solicitud = blSol.Insert(sol);

                url_destino = "~/" + RouteConfig.VISOR_SOLICITUD_ECI + id_solicitud.ToString();

                string cuit = Membership.GetUser().UserName;
                ParametrosBL parametrosBL = new ParametrosBL();
                string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
                bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                if (!tad)
                {
                    int idTAD = 0;
                    idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, id_solicitud);
                    sol = sSITSolicitudesBL.Single(id_solicitud);
                    sol.idTAD = idTAD;
                    sSITSolicitudesBL.Update(sol);
                }
            }
            catch (Exception ex)
            {
                if (id_solicitud != 0)
                    sSITSolicitudesBL.Delete(sol);
                url_destino = "";

                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(this, "showfrmError();");
            }

            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }
    }

}