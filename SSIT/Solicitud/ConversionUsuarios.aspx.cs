using BusinesLayer.Implementation;
using Reporting.CommonClass;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud
{
    public partial class ConversionUsuarios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool validarUsuario(string user, string pass)
        {
            bool ret = false;
            pnlError.Visible = false;
            pnlSuccess.Visible = false;

            try
            {
                if (txtUsuAnterior.Text.Trim().Length == 0)
                    throw new Exception("Debe ingresar el nombre de usuario.");

                if (txtPassAnterior.Text.Trim().Length == 0)
                    throw new Exception("Debe ingresar la contraseña.");

                Guid userActual = (Guid)Membership.GetUser().ProviderUserKey;
                var usuarioAnterior = Membership.GetUser(txtUsuAnterior.Text.Trim());

                if (usuarioAnterior == null || usuarioAnterior.GetPassword() != txtPassAnterior.Text.Trim())
                {
                    throw new Exception("Los datos de usuario y/o contraseña no corresponden a un usuario válido.");
                }
                ret = true;

            }
            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                pnlError.Visible = true;
            }
            return ret;
        }

        protected void btnConfirmacion_SI_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
            pnlSuccess.Visible = false;

            try
            {
                if (validarUsuario(txtUsuAnterior.Text.Trim(), txtPassAnterior.Text.Trim()))
                {
                    Guid userActual = (Guid)Membership.GetUser().ProviderUserKey;
                    var usuarioAnterior = Membership.GetUser(txtUsuAnterior.Text.Trim());
                    Guid useridAnterior = (Guid)Membership.GetUser(txtUsuAnterior.Text.Trim()).ProviderUserKey;
                    UsuarioBL usuarioBL = new UsuarioBL();
                    usuarioBL.TransferirUsuario(userActual, useridAnterior);
                    lblSucess.Text = string.Format("Se han transferido los trámites del usuario anterior y el mismo ha sido eliminado.");
                    pnlSuccess.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                pnlError.Visible = true;
            }
            finally
            {
                this.EjecutarScript(updPagina, "hidefrmConfirmarAsociarUsuario();");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (validarUsuario(txtUsuAnterior.Text.Trim(), txtPassAnterior.Text.Trim()))
            {
                this.EjecutarScript(updPagina, "showfrmConfirmarAsociarUsuario();");
            }
        }
    }
}