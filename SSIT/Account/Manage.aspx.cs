using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Security;
using SSIT.App_Components;

namespace SSIT.Account
{
    public partial class Manage : SecurePage
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
                    // Seccionar la cadena de consulta desde la acción
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Se cambió la contraseña."
                        : message == "SetPwdSuccess" ? "Se estableció la contraseña."
                        : message == "RemoveLoginSuccess" ? "El inicio de sesión externo se ha quitado."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }

        }

        protected void setPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {

                Membership.GetUser().ChangePassword(changepwd.CurrentPassword, changepwd.NewPassword);
                Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
            }

        }

        protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
        {
            // Puede cambiar este método para convertir la hora y fecha UTC con el formato y el desfase
            // deseados. En este caso, se convertirá a la zona horaria del servidor y se asignará el formato
            // de cadena de hora larga y fecha corta mediante la cultura de subproceso actual.
            return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[nunca]";
        }
    }
}