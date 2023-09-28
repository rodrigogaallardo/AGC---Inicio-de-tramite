using AuthenticationAGIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AuthenticationAGIP.Entity;
using System.Web.Security;
using AuthenticationAGIP.App_Components;
using System.Net;
using System.Collections.Specialized;

namespace AuthenticationAGIP
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ReadData();
            }
        }

        private void ReadData()
        {

            string token = "";
            string sign = "";
            bool error = false;
            try
            {

            
                try
                {
                    token = Request.Form["token"];
                }
                catch (Exception)
                {
                    throw new Exception("El request no posee el token");
                }
                try
                {
                    sign = Request.Form["sign"];
                }
                catch (Exception)
                {

                    throw new Exception("El request no posee el sign");
                }

                //token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/PjxkYXRvcz48c2VydmljaW8gbm9tYnJlPSJhcHJhU0lQU0EiIGV4cF90aW1lPSIxNTI4MzQxNTk5Ii8+PGF1dGVudGljYWRvIGN1aXQ9IjIwMjk0MTA5NDEzIiBub21icmU9IlBFUkVaIEpPU0UiIGlzaWI9IjIwMjk0MTA5NDEzIiBjYXQ9IjQwIiBjb2RjYWxsZT0iNDMwNTUiIGNhbGxlPSJDT0NIQUJBTUJBIiBwdWVydGE9IjE2NzIiIHBpc289IiIgZHB0bz0iIiBjb2Rwb3N0YWw9IkMxMTQ4QUJGIiBjb2Rsb2NhbGlkYWQ9IjEyMDAiIGxvY2FsaWRhZD0iQ0lVREFEIEFVVE9OT01BIERFIEJTIEFTIiBjb2Rwcm92PSIyIiBwcm92aW5jaWE9IkNBUElUQUwgRkVERVJBTCIgdGVsZWZvbm89IjQzMjM4NjAwIiBlbWFpbD0iYXJpZWxkYWdvc3RvQGhvdG1haWwuY29tIiBuaXZlbD0iMiIgdGlwb0RvY3VtZW50bz0iMzAwMyIgZG9jdW1lbnRvPSIyOTQxMDk0MSI+PC9hdXRlbnRpY2Fkbz48cmVwcmVzZW50YWRvcz48cmVwcmVzZW50YWRvIGN1aXQ9IjIwMjk0MTA5NDEzIiBub21icmU9IlBFUkVaIEpPU0UiIGlzaWI9IjIwMjk0MTA5NDEzIiBjYXQ9IjQwIiBjb2RjYWxsZT0iNDMwNTUiIGNhbGxlPSJDT0NIQUJBTUJBIiBwdWVydGE9IjE2NzIiIHBpc289IiIgZHB0bz0iIiBjb2Rwb3N0YWw9IkMxMTQ4QUJGIiBjb2Rsb2NhbGlkYWQ9IjEyMDAiIGxvY2FsaWRhZD0iQ0lVREFEIEFVVE9OT01BIERFIEJTIEFTIiBjb2Rwcm92PSIyIiBwcm92aW5jaWE9IkNBUElUQUwgRkVERVJBTCIgdGVsZWZvbm89IjQzMjM4NjAwIiBlbWFpbD0iYXJpZWxkYWdvc3RvQGhvdG1haWwuY29tIiB0aXBvUmVwcmVzZW50YWNpb249Ijk5OTkiIHRpcG9Eb2N1bWVudG89IjMwMDMiIGRvY3VtZW50bz0iMjk0MTA5NDEiIGVsZWdpZG89InRydWUiPjwvcmVwcmVzZW50YWRvPjwvcmVwcmVzZW50YWRvcz48L2RhdG9zPg==";
                
                datos datosToken = GetDatosTokenAGIP(token);
                if (datosToken == null)
                    throw new Exception("No se ha podido recuperar los datos del token de AGIP.");


                string username = datosToken.autenticado.cuit.ToString();
                string email = (!string.IsNullOrWhiteSpace(datosToken.autenticado.email) ? datosToken.autenticado.email.Trim() : null);

                if (ExisteUsuario(username))
                {
                    ActualizarDatosUsuario(username, email);
                }
                else
                {
                    CrearUsuario(datosToken);
                }

                GenerarTicketAutenticacion(username);
                
            }
            catch (Exception ex)
            {
                string id = Errors.Log(ex);
                error = true;
                Server.Transfer(string.Format("~/Error/{0}", id));
                
            }

            if (!error)
                RedireccionarAlSistema(token);

        }

        private datos GetDatosTokenAGIP(string token)
        {
            datos ret = null;
            byte[] data = Convert.FromBase64String(token);

            string decodedString = System.Text.UTF8Encoding.ASCII.GetString(data);

            using (var stream = new MemoryStream(data))
            {

                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(datos));
                ret = (datos)serializer.Deserialize(stream);
            }

            return ret;

        }

        private void CrearUsuario(datos TokenAGIP)
        {
            var datosUsuario = TokenAGIP.autenticado;

            string newPassword = Membership.GeneratePassword(6, 0);
            newPassword = System.Text.RegularExpressions.Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "9");

            MembershipCreateStatus status;
            Membership.CreateUser(datosUsuario.cuit.ToString(), newPassword, datosUsuario.email,null,null,true,out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new Exception(GetUserErrorMessage(status));
            }
            
        }
        private bool ExisteUsuario(string username)
        {
            bool ret = false;
            MembershipUser user = Membership.GetUser(username);
            ret = user != null;
            return ret;
        }
        private void ActualizarDatosUsuario(string username, string email)
        {
            // actualiza datos del usuario 
            MembershipUser usu = Membership.GetUser(username);
            bool actualizarDatosUsuario = false;

            if (usu.IsLockedOut)
                usu.UnlockUser();

            if (!usu.IsApproved)
            {
                usu.IsApproved = true;
                actualizarDatosUsuario = true;
            }

            if (email.Length > 0 && email != usu.Email)
            {
                usu.Email = email;
                actualizarDatosUsuario = true;
            }
            if (actualizarDatosUsuario)
                Membership.UpdateUser(usu);

        }
        private void GenerarTicketAutenticacion(string username)
        {
            // Genera el ticket de autenticación
            FormsAuthentication.SetAuthCookie(username, true);
        }

        private void RedireccionarAlSistema(string token)
        {

            string url = Functions.GetAppSetting("UrlSistemaPublico");

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();

            StringBuilder s = new StringBuilder();
            s.Append("<html>");
            s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
            s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
            s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", "token", token);
            s.Append("</form></body></html>");
            response.Write(s.ToString());
            response.End();
        }

        private string GetUserErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe en la base de datos de la aplicación.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "La dirección de correo electrónico ya existe en la base de datos de la aplicación.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña no tiene el formato correcto.";

                case MembershipCreateStatus.InvalidEmail:
                    return "La dirección de correo electrónico no tiene el formato correcto.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta de la contraseña no tiene el formato correcto.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunta de la contraseña no tiene el formato correcto.";

                case MembershipCreateStatus.InvalidUserName:
                    return "No se encontró el nombre de usuario en la base de datos.";

                case MembershipCreateStatus.ProviderError:
                    return "El proveedor devolvió un error no descrito por otros valores de la enumeración MembershipCreateStatus.";

                case MembershipCreateStatus.UserRejected:
                    return "El usuario no se ha creado, por un motivo definido por el proveedor.";

                default:
                    return "Se ha producido un error desconocido. Por favor, compruebe los datos e inténtelo de nuevo. Si el problema persiste, póngase en contacto con el administrador del sistema.";
            }
        }

    }
}