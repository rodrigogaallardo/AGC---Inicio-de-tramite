using System;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System.Web;
using System.Globalization;
using AnexoProfesionales.Common;

namespace AnexoProfesionales
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


        protected void LoginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Login LoginControl1 = (Login)sender;
            MembershipUser user = Membership.GetUser(LoginControl1.UserName);

            ProfesionalesBL profBl = new ProfesionalesBL();
            ProfesionalDTO prof = new ProfesionalDTO();
            if (user == null)
            {
                LoginControl1.FailureText = "El nombre de usuario no existe.";
            }
            else
            {
                prof = profBl.Get(new Guid(user.ProviderUserKey.ToString()));
                if (prof.BajaLogica == true)
                {
                    e.Authenticated = false;
                }
                else
                {
                    if (LoginControl1.Password == user.GetPassword())
                        e.Authenticated = true;
                    else
                        e.Authenticated = false;
                }
            }
                
            
        }


        protected void LoginControl_LoginError(object sender, EventArgs e)
        {

            Login LoginControl1 = (Login)sender;
            LoginControl1.FailureText = "";

            MembershipUser user = Membership.GetUser(LoginControl1.UserName);

            ProfesionalesBL profBl = new ProfesionalesBL();
            ProfesionalDTO prof = new ProfesionalDTO();

            try
            {
                if (user == null)
                {
                    LoginControl1.FailureText = "El nombre de usuario no existe.";
                }
                else
                {
                    prof = profBl.Get(new Guid(user.ProviderUserKey.ToString()));
                    if (prof.BajaLogica == true)
                    {
                        LoginControl1.FailureText = "El Profesional fue dado de baja. No puede ingresar al Sistema Anexo Técnico.";
                    }
                    else
                    {
                        if (!user.IsApproved)
                            LoginControl1.FailureText = "El Usuario no se encuentra habilitado. Por favor utilice el mail que se le ha enviado en la registración.";

                        if (user.IsLockedOut)
                            LoginControl1.FailureText = "El Usuario se encuentra bloqueado, comuníquese con su Consejo Profesional.";

                        if (LoginControl1.FailureText.Length == 0 && user.GetPassword() != LoginControl1.Password)
                            LoginControl1.FailureText = "La Contraseña ingresada es incorrecta.";
                    }

                }
            }
            catch (Exception ex)
            {
                LoginControl1.FailureText = ex.Message;
            }

        }

        protected void linkDescargaInstAt_Click(object sender, EventArgs e)
        {
            InstructivosBL instruc = new InstructivosBL();
            var regInstruc = instruc.getInstuctivo(Instructivos_tipos.DGHyP_Anexo);

            if (regInstruc != null)
                Response.Redirect(string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(regInstruc.id_file)));
        }
        protected void btnEncomienda_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/" + RouteConfig.INICIAR_ENCOMIENDA);
        }

        protected void LoginControl_LoggedIn(object sender, EventArgs e)
        {
            if (Request.Cookies[Constantes.UserNameCookie] != null)
            {

                MembershipUser usu = Membership.GetUser((sender as Login).UserName);
                UsuarioBL usuBL = new UsuarioBL();

                string ret = "";
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
        }

    }
}
