using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System.Globalization;
using System.Deployment.Application;
using System.Reflection;

namespace ConsejosProfesionales
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private string _UserIdProfesional = "_UserIdProfesional";

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
            bool esProf = EsProfesionales();

            Label lblUsername = (Label)LoginView1.FindControl("lblUsername");

            if (lblUsername != null)
            {
                string ret = string.Empty;
                if (Request.Cookies[Constantes.UserNameCookie] == null && Request.IsAuthenticated)
                {

                    MembershipUser usu = Membership.GetUser();
                    UsuarioBL usuBL = new UsuarioBL();

                    if (usu != null)
                    {
                        var usuDTO = usuBL.Single((Guid)usu.ProviderUserKey);
                        TextInfo txtInfo = new CultureInfo("es-AR", false).TextInfo;

                        string Apellido = txtInfo.ToTitleCase((usuDTO.Apellido != null) ? usuDTO.Apellido.ToLower() : string.Empty);
                        string Nombre = txtInfo.ToTitleCase((usuDTO.Nombre != null) ? usuDTO.Nombre.ToLower() : string.Empty);

                        ret = usu.UserName + " (" + Apellido + " " + Nombre + ")";

                        var userNameCookie = new HttpCookie(Constantes.UserNameCookie)
                        {
                            Value = ret
                        };

                        Response.Cookies.Set(userNameCookie);
                    }
                }
                else
                {
                    if (Request.IsAuthenticated )//&& Request.Cookies[Constantes.UserNameCookie].Expires > DateTime.Now)
                        ret = ((HttpCookie)Request.Cookies[Constantes.UserNameCookie]).Value;
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
        protected bool EsProfesionales()
        {
            bool EsProfUser = false;
            try
            {
                string queryURL = EsProfesionalesURL();

                if (queryURL == "Profesionales.aspx" || queryURL == "Profesionales")
                {
                    LinkButton LinkButton1 = (LinkButton)LoginView1.FindControl("LinkButton1");
                    LinkButton btnInicio = (LinkButton)LoginView1.FindControl("btnInicio");
                    LinkButton btnLogin = (LinkButton)LoginView1.FindControl("btnLogin");

                    if(LinkButton1 != null)
                        LinkButton1.PostBackUrl = "~/Profesionales.aspx";

                    if(btnInicio!=null)
                        btnInicio.PostBackUrl = "~/Profesionales.aspx";

                    if(btnLogin != null)
                        btnLogin.PostBackUrl = "~/Profesionales.aspx";

                    EsProfUser = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return EsProfUser;
        }
        public string EsProfesionalesURL()
        {

            String[] file = HttpContext.Current.Request.CurrentExecutionFilePath.Split('/');
            string queryURL = file[file.Length - 1];

            return queryURL;
        }
        public bool EsUserProfesionales()
        {
            bool esProf = false;

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (Session[_UserIdProfesional] == null)
                {
                    MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
                    MembershipUser usu = MemberProfesional.GetUser(HttpContext.Current.User.Identity.Name, true);
                    Session[_UserIdProfesional] = usu != null;
                }
                esProf = (bool)Session[_UserIdProfesional];
                      
            }
            return esProf;
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

            //Para evitar que la persona que se logea vea o no el entorno que le corresponde
            if (EsUserProfesionales() && !EsProfesionales() && HttpContext.Current.Request.IsAuthenticated)
            {
                if (EsProfesionalesURL().Contains("ManageProf"))
                {
                    //
                }
                else
                {
                    if (EsProfesionalesURL().Contains("Manage"))
                    {
                        Response.Redirect("~/Account/ManageProf.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Profesionales.aspx");
                        if (string.IsNullOrEmpty(Page.Title))
                            SiteTitle.Text = "Profesionales";
                        else
                            SiteTitle.Text = "Profesionales - " + Page.Title;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Page.Title))
                    SiteTitle.Text = "Consejos";
                else
                    SiteTitle.Text = "Consejos - " + Page.Title;
            }

            if (!EsUserProfesionales() && EsProfesionales() && HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("~/Default.aspx");

        }
        protected void LogOff(object sender, EventArgs e)
        {
            string queryURL = EsProfesionalesURL();
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            if (Request.Cookies[Constantes.UserNameCookie] != null)
            {
                var userNameCookie = new HttpCookie(Constantes.UserNameCookie);
                userNameCookie.Expires = DateTime.Now.AddDays(-1);

                Request.Cookies.Set(userNameCookie);
            }
            if (queryURL == "Profesionales.aspx" || queryURL == "Profesionales")
                Response.Redirect("~/Profesionales.aspx");
            else
                Response.Redirect("~/");
        }
    }
}