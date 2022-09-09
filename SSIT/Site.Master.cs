using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            try
            {
                Label lblUsername = (Label)LoginView1.FindControl("lblUsername");

                if (lblUsername != null && lblUsername.Text.Length == 0)
                {
                    string ret = "";
                    MembershipUser usu = Membership.GetUser();
                    UsuarioBL usuBL = new UsuarioBL();

                    if (usu != null)
                    {
                        var usuDTO = usuBL.Single((Guid)usu.ProviderUserKey);
                        TextInfo txtInfo = new CultureInfo("es-AR", false).TextInfo;

                        if (usuDTO != null)
                        {
                            string Apellido = txtInfo.ToTitleCase((usuDTO.Apellido != null) ? usuDTO.Apellido.ToLower() : string.Empty);
                            string Nombre = txtInfo.ToTitleCase((usuDTO.Nombre != null) ? usuDTO.Nombre.ToLower() : string.Empty);

                            ret = usu.UserName + " (" + Apellido + " " + Nombre + ")";
                        }
                        else
                            ret = usu.UserName;


                    }

                    lblUsername.Text = ret;
                }

                if (!IsPostBack)
                {
                    // Set Anti-XSRF token
                    ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                    ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
                }
                else
                {
                    // Validate the Anti-XSRF token
                    if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                        || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                    {
                        throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                    }
                }
            }
            catch
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/");
            }
        }

        public static Version ApplicationVersion
        {
            get
            {
                if ((ApplicationDeployment.IsNetworkDeployed))
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion;
                else
                    return Assembly.GetExecutingAssembly().GetName().Version;
            }
            set { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "[Version " + ApplicationVersion.Major.ToString() + " Revision " + ApplicationVersion.Revision.ToString() + "]";

            if (string.IsNullOrEmpty(Page.Title))
                SiteTitle.Text = "SSIT";
            else
                SiteTitle.Text = "SSIT - " + Page.Title;
            string value = System.Configuration.ConfigurationManager.AppSettings["mantenimiento"];
            if (value != null && Convert.ToBoolean(value))
            {
                int cargo = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["cargo"]), out cargo);
                if (cargo == 0)
                    Response.Redirect(string.Format("~/" + RouteConfig.MANTENIMIENTO + "{0}", 1));
            }
            if (Session["totalNot"] != null)
            {
                #region BandejaNotificaciones                        
                SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();
                var userid = Functions.GetUserid();
                int totalNot = 0;

                if (userid != null)
                {
                    totalNot = notifBL.GetCantidadNotificacionesByUser(userid); 
                }
                Label lblBandejaNoti = (Label)FindControlRecursive(this, "lbtBandejaNotificaciones");
                lblBandejaNoti.Text = totalNot > 0 ? $" ({totalNot})" : "";
                #endregion
            }
        }

        protected void LogOff(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }

        public void Titulo(string value)
        {
            //lblTitulo.Text = value;
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