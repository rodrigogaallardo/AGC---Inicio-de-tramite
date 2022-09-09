using BusinesLayer;
using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.Common;
using StaticClass;
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SSIT
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var DivcontendorGral = (HtmlControl)Master.FindControl("contendorGral");

            DivcontendorGral.Attributes.Add("class", "container-fluid");
            MembershipUser usu = Membership.GetUser();
            
            if (usu == null)
            {
                var navBarMaster = (HtmlControl)Master.FindControl("navBarMaster");
                navBarMaster.Visible = false;
                Session["totalNot"] = null;
            }
            else
            {
                var sectionPlaceHolder = (HtmlControl)Master.FindControl("sectionPlaceHolder");
                sectionPlaceHolder.Attributes.Add("class", "content-wrapper clear-fix mtop20");
            }

            if (!IsPostBack)
            {
                LinkButton lnkCrearAmpliacion = (LinkButton)LoginView1.FindControl("lnkCrearAmpliacion");
                LinkButton lnkCrearECI = (LinkButton)LoginView1.FindControl("lnkCrearECI");
                LinkButton lnkCrearPermisoMC = (LinkButton)LoginView1.FindControl("lnkCrearPermisoMC");

                LinkButton linkDescargaInstAmpliaciones = (LinkButton)LoginView1.FindControl("linkDescargaInstAmpliaciones");
                LinkButton linkDescargaInstECI = (LinkButton)LoginView1.FindControl("linkDescargaInstECI");
                LinkButton linkDescargaInstPermisoMC = (LinkButton)LoginView1.FindControl("linkDescargaInstPermisoMC");
                

                LinkButton lnkCrearRedistribucionUso = (LinkButton)LoginView1.FindControl("lnkCrearRedistribucionUso");
                LinkButton linkDescargaInstRedistUso = (LinkButton)LoginView1.FindControl("linkDescargaInstRedistUso");
                LinkButton lnkAsistenteOnline = (LinkButton)LoginView1.FindControl("lnkAsistenteOnline");

                if (lnkAsistenteOnline != null)
                {
                    lnkAsistenteOnline.PostBackUrl = string.Format(Functions.GetParametroChar("AsistenteOnline.Url"));
                }


                if (lnkCrearAmpliacion != null)
                {
                    lnkCrearAmpliacion.Visible = Funciones.is_Ampliaciones_Implementado();
                    linkDescargaInstAmpliaciones.Visible = Funciones.is_Ampliaciones_Implementado();
                }

                if (lnkCrearECI != null)
                {
                    lnkCrearECI.Visible = Funciones.is_ECI_Implementado();
                    linkDescargaInstECI.Visible = Funciones.is_ECI_Implementado();
                }

                if (lnkCrearPermisoMC != null)
                {
                    lnkCrearPermisoMC.Visible = Funciones.is_MC_Implementado();
                    linkDescargaInstPermisoMC.Visible = Funciones.is_MC_Implementado();
                }
                if (lnkCrearRedistribucionUso != null)
                {
                    lnkCrearRedistribucionUso.Visible = Funciones.is_RedistribucionUso_Implementado();
                    linkDescargaInstRedistUso.Visible = Funciones.is_RedistribucionUso_Implementado();
                }
                Label labelAgip = (Label)LoginView1.FindControl("labelAgip");
                if (labelAgip != null)
                {
                    ParametrosBL parametrosBL = new ParametrosBL();
                    string value = parametrosBL.GetParametroChar("TAD.Url");
                    labelAgip.Text = "<h4>Para ingresar al portal deberas hacerlo desde el siguiente Link <b><a href='" + value + "'>TAD</a></b>" +
                        "<br />Recuerda que debes tener generada tu clave ciudad.</h4>";
                }
                #region BandejaNotificaciones                         

                if (usu != null)
                {
                    var userid = Functions.GetUserid();

                    SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();

                    int totalNot = 0;
                    totalNot = notifBL.GetCantidadNotificacionesByUser(userid);

                    Label btnBandejaNoti = (Label)FindControlRecursive(this, "lbtBandejaNotificaciones");
                    btnBandejaNoti.Text = totalNot > 0 ? $" ({totalNot})" : "";

                    if (totalNot > 0)
                    {
                        Session["totalNot"] = totalNot.ToString();
                        if (totalNot == 1)
                            lblModalNotificaciones.InnerText = "Usted posee actualmente una notificación";
                        else
                            lblModalNotificaciones.InnerText = $"Usted tiene {totalNot} notificaciones sin leer";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalAvisoNotificacion", "showModalAvisoNotificacion('');", true);
                    }
                    #endregion
                }
            }
        }

        protected void LoginControl_LoginError(object sender, EventArgs e)
        {
            Login LoginControl = (Login)sender;
            LoginControl.FailureText = "";

            MembershipUser user = Membership.GetUser(LoginControl.UserName);
            try
            {
                if (user == null)
                {
                    LoginControl.FailureText = "El nombre de usuario no existe.";
                }
                else
                {
                    if (!user.IsApproved)
                        LoginControl.FailureText = "El usuario no se encuentra habilitado. Por favor utilice el mail que se le ha enviado en la registración.";

                    if (user.IsLockedOut)
                        LoginControl.FailureText = "El usuario se encuentra bloqueado. Por favor, revise a su casilla de e-mail declarada para activarlo desbloquearlo.";

                    if (LoginControl.FailureText.Length == 0 && user.GetPassword() != LoginControl.Password)
                        LoginControl.FailureText = "La Contraseña ingresada es incorrecta.";

                }
            }
            catch (Exception ex)
            {
                LoginControl.FailureText = ex.Message;
            }

        }
        //protected void LoginControl_Authenticate(object sender, EventArgs e) {

        //}
        public string formatUrl()
        {
            return Functions.GetParametroChar("AsistenteOnline.Url");
        }
        protected void linkDescargaInstTrans_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_Transferencias);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void linkDescargaInstHab_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_Habilitaciones);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void linkDescargaConsPadr_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_Consulta_Padron);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void crearSolicitud(Guid userid)
        {
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            int id_solicitud = 0;
            try
            {
                try
                {
                    sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                    sol.IdTipoTramite = 1;
                    sol.IdTipoExpediente = 0;
                    sol.IdSubTipoExpediente = 0;
                    sol.CreateDate = DateTime.Now;
                    sol.CreateUser = userid;
                    sol.Servidumbre_paso = false;
                    id_solicitud = blSol.Insert(sol);

                    string cuit = Membership.GetUser().UserName;
                    ParametrosBL parametrosBL = new ParametrosBL();
                    string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
                    bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                    if (tad)
                    {
                        int idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, id_solicitud);
                        sol = blSol.Single(id_solicitud);
                        sol.idTAD = idTAD;
                        blSol.Update(sol);
                    }
                }
                catch (Exception e)
                {
                    if (id_solicitud != 0)
                        blSol.Delete(sol);
                    throw e;
                }
                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", id_solicitud), false);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updConfirmarNuevaSolicitud, updConfirmarNuevaSolicitud.GetType(), "hideModalAvisoNotificacion", "hideModalAvisoNotificacion();", true);
                ScriptManager.RegisterStartupScript(updConfirmarNuevaSolicitud, updConfirmarNuevaSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            crearSolicitud((Guid)Membership.GetUser().ProviderUserKey);
        }

        protected void lnkCrearTransferencia_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/" + RouteConfig.INICIAR_TRANSICIONES));
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ConsultaPadronSolicitudesBL bl = new ConsultaPadronSolicitudesBL();
            ConsultaPadronSolicitudesDTO dto = new ConsultaPadronSolicitudesDTO();

            dto.IdTipoTramite = (int)Constantes.TipoDeTramite.ConsultaPadron;
            dto.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
            dto.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;
            dto.CreateDate = DateTime.Now;
            dto.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
            bl.Insert(dto);
            try
            {
                string cuit = Membership.GetUser().UserName;
                ParametrosBL parametrosBL = new ParametrosBL();
                string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                string trata = parametrosBL.GetParametroChar("Trata.Consulta.Padron");
                bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                if (tad)
                {
                    int idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, dto.IdConsultaPadron);
                    dto.idTAD = idTAD;
                    bl.Update(dto);
                }
                Response.Redirect(string.Format("~/" + RouteConfig.DATOS_CPADRON + "{0}", dto.IdConsultaPadron), false);
            }
            catch (Exception ex)
            {
                if (dto.IdConsultaPadron != 0)
                {
                    var sol = bl.Single(dto.IdConsultaPadron);
                    bl.Delete(sol);
                }
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updConfirmarNuevaSolicitudCPadron, updConfirmarNuevaSolicitudCPadron.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void linkDescargaInstAmpliaciones_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_Ampliaciones);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void linkDescargaInstRedistUso_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_RedistribucionesUso);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void linkDescargaInstECI_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_HabilitacionECI);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }

        protected void linkDescargaInstPermisoMC_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_PermisoMC);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }
        public static Control FindControlRecursive(Control Root, string Id)

        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }
    }
}