using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using StaticClass;
using BusinesLayer.Implementation;
using System.Web;
using SSIT.App_Components;
using ExternalService;
using System.Web.UI.HtmlControls;

namespace SSIT.Account
{
    public partial class ForgotPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var DivcontendorGral = (HtmlControl)Master.FindControl("contendorGral");
            
            DivcontendorGral.Attributes.Add("class", "container-fluid");
        }

        protected void PasswordRecovery_VerifyingUser(object sender, EventArgs e)
        {
            SuccessEmail.Visible = false;
            ErrorUsuario.Visible = false;
            try
            {
                MembershipUser usuario = Membership.GetUser(UserName.Text);
                MailMessages mailer = new MailMessages();

                lblError.Text = "";

                if (usuario != null)
                {
                    string Usuario = usuario.UserName;
                    string Password = usuario.GetPassword();
                    string url;
                    string htmlBody;
                    string Asunto;
                    int IdTipoEmail;
                    Guid userid = (Guid)usuario.ProviderUserKey;

                    if (usuario.IsLockedOut)
                        usuario.UnlockUser();

                    if (!usuario.IsApproved)
                    {
                        url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/ActivateUser") + string.Format("?userid={0}", userid);
                        url = IPtoDomain(url);

                        UsuarioBL userBl = new UsuarioBL();
                        var userDatos = userBl.Single(userid);

                        htmlBody = mailer.MailWelcome(Usuario, Password, url, userDatos.Nombre + " " + userDatos.Apellido);
                        Asunto = "Activación de usuario - SSIT";
                        IdTipoEmail = (int)ExternalService.TiposDeMail.CreacionUsuario;

                        lblError.Text = "El usuario no se encuentra aprobado, se ha enviado el mail de activación al correo declarado " + usuario.Email + ", es necesario que el mismo sea activado.";
                    }
                    else
                    {
                        url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/");
                        url = IPtoDomain(url);

                        htmlBody = mailer.MailPassRecovery(Usuario, Password, url);
                        Asunto = "Recupero de contraseña - SSIT";
                        IdTipoEmail = (int)ExternalService.TiposDeMail.RecuperoContrasena;

                        lblEmail.Text = usuario.Email;
                        SuccessEmail.Visible = true;
                    }

                    EmailServiceBL mailService = new EmailServiceBL();
                    EmailEntity emailEntity = new EmailEntity();
                    emailEntity.Email = usuario.Email;
                    emailEntity.Html = htmlBody;
                    emailEntity.Asunto = Asunto;
                    emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                    emailEntity.IdTipoEmail = IdTipoEmail;
                    emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                    emailEntity.CantIntentos = 3;
                    emailEntity.CantMaxIntentos = 3;
                    emailEntity.FechaAlta = DateTime.Now;
                    emailEntity.Prioridad = 1;

                    mailService.SendMail(emailEntity);

                }
                else
                    ErrorUsuario.Visible = true;
            }
            catch (Exception ex)
            {
                //PasswordRecovery.UserNameFailureText = "No fue posible tener acceso a su información. Inténtelo nuevamente.";
            }
        }

    }
}