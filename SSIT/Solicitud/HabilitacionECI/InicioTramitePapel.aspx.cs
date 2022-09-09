using System;
using SSIT.App_Components;
using BusinesLayer.Implementation;
using System.Web.UI;
using static StaticClass.Constantes;
using StaticClass;
using System.Web.Security;
using DataTransferObject;
using ExternalService;

namespace SSIT.Solicitud.HabilitacionECI
{
    public partial class InicioTramitePapel : SecurePage
    {
        public string HabilitacionECI { get; set; }

        AmpliacionesBL blSol = new AmpliacionesBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HabilitacionECI = TipoTramiteDescripcion.AdecuacionECI;
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updDatos, updDatos.GetType(), "init_Js_updDatos", "init_Js_updDatos();", true);
            }

            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
            }
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            int? anio_expediente = null;
            int? nro_expediente = null;

            try
            {
                anio_expediente = int.Parse(txtExpediente_Anio.Text);
                nro_expediente = int.Parse(txtExpediente_Nro.Text);

                this.EjecutarScript(updDatos, "showfrmConfirmarNuevaAmpliacion();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevaAmpliacion, "showfrmError();");
                return;
            }
        }

        protected void btnNuevaAmpliacion_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string url_destino = "";
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            int id_solicitud = 0;
            string nroExSadeRel = "EX-" + txtExpediente_Anio.Text.Trim() + "-" + txtExpediente_Nro.Text.Trim().ToUpper() + "-   -MGYEA-DGHP";
            try
            {
                sol.Servidumbre_paso = false;

                sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;

                sol.IdTipoTramite = (int)Constantes.TipoTramite.HabilitacionECIAdecuacion;
                sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.NoDefinido;
                sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                sol.CreateDate = DateTime.Now;
                sol.CreateUser = userid;
                sol.SSITSolicitudesOrigenDTO = null;
                sol.NroExpedienteSadeRelacionado = nroExSadeRel;
                sol.EsECI = true;

                id_solicitud = blSol.Insert(sol);

                url_destino = "~/" + RouteConfig.CARGA_PLANCHETA_ECI + id_solicitud.ToString();

                string cuit = Membership.GetUser().UserName;
                ParametrosBL parametrosBL = new ParametrosBL();
                string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
                bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                if (tad)
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