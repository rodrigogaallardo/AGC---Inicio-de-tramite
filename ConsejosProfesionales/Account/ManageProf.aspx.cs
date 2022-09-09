using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ConsejosProfesionales.Account
{
    public partial class ManageProf : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                changePassword.Visible = true;

                // Presentar mensaje de operación correcta
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/ManageProf");


                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Se cambió la contraseña."
                        : message == "SetPwdSuccess" ? "Se estableció la contraseña."
                        : message == "RemoveLoginSuccess" ? "El inicio de sesión externo se ha quitado."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);


                    if (message == "ChangePwdSuccess" || message == "SetPwdSuccess")
                    {
                        changePassword.Visible = false;
                    }

                }
            }

        }

        protected void setPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
                //MembershipUser usu = MemberProfesional.GetUser(HttpContext.Current.User.Identity.Name, true);
                //MemberProfesional.GetUser(HttpContext.Current.User.Identity.Name, true).ChangePassword(changepwd.CurrentPassword, changepwd.NewPassword);
                //MemberProfesional.GetUser((Guid)usu.ProviderUserKey, true).ChangePassword(changepwd.CurrentPassword, changepwd.NewPassword);

                Membership.GetUser().ChangePassword(changepwd.CurrentPassword, changepwd.NewPassword);
                
                Response.Redirect("~/Account/ManageProf?m=SetPwdSuccess");
            }
        }

        protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
        {
            // You can change this method to convert the UTC date time into the desired display
            // offset and format. Here we're converting it to the server timezone and formatting
            // as a short date and a long time string, using the current thread culture.
            return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[never]";
        }
    }
}