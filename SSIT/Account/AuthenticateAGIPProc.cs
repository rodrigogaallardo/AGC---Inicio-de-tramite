using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace SSIT.Account
{
    public class AuthenticateAGIPProc
    {

        public bool ReadData()
        {


            bool ret = true;
            string token = "";
            string sign = "";
            HttpRequest Request = HttpContext.Current.Request;
            HttpResponse Response = HttpContext.Current.Response;

            string returnUrl = (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]) ? Request.QueryString["returnUrl"] : "");
            try
            {

                try
                {
                    token = Request.Form["token"];
                }
                catch (Exception)
                {
                    ret = false;
                    throw new Exception("El request no posee el token");
                }
                
                if (!string.IsNullOrWhiteSpace(token))
                {

                    if (validarToken(token, sign))
                    {


                        Datos datosToken = GetDatosTokenMiBA(token,ref sign);
                        if (datosToken == null)
                            throw new Exception("No se ha podido recuperar los datos del token de AGIP.");

                    

                        string username = datosToken.Autenticado.Cuit.ToString();


                        if (ExisteUsuario(username))
                        {
                            ActualizarDatosUsuario(datosToken, token, sign);
                        }
                        else
                        {
                            CrearUsuario(datosToken, token, sign);
                        }


                        GenerarTicketAutenticacion(username);


                        if (returnUrl.Length > 0)
                            Response.Redirect(returnUrl);
                    }
                    else
                    {
                        throw new Exception("El token es inválido con respecto a la firma.");
                    }
                }
                else
                {
                    string url = "";

                    if (string.IsNullOrWhiteSpace(Globals.username))
                        url = Functions.GetParametroChar("TAD.Url");
                    else
                    {
                        url = "~/" + RouteConfig.HOME;
                    }

                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                if (ex != null & !(ex is System.Threading.ThreadAbortException))
                {
                    string id = LogError.Log(ex);
                    ret = false;
                    Response.Redirect(string.Format("~/Error/{0}", id));
                }
            }

            return ret;

        }

        private Datos GetDatosTokenAGIP(string token)
        {
            Datos ret = null;
            byte[] data = Convert.FromBase64String(token);

            string decodedString = System.Text.UTF8Encoding.ASCII.GetString(data);

            using (var stream = new MemoryStream(data))
            {

                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Datos));
                ret = (Datos)serializer.Deserialize(stream);
            }
            if (ret.Autenticado.Email.Substring(ret.Autenticado.Email.Length - 1).Equals("."))
                ret.Autenticado.Email = ret.Autenticado.Email.Remove(ret.Autenticado.Email.Length - 1);
            return ret;

        }
        private Datos GetDatosTokenMiBA(string token, ref string sign  )
        {
          
            string[] tokenParts = token.Split('.');
            string Header = tokenParts[0];
            string payloadRecibido = tokenParts[1];
            string keyPublicaRecibida = tokenParts[2];

            string PayloadJsonString = Base64UrlEncoder.Decode(payloadRecibido);

            DatosMiBA datosMiBA = Newtonsoft.Json.JsonConvert.DeserializeObject<DatosMiBA>(PayloadJsonString);



            #region Fill Datos datosToken

            Datos datosToken = new Datos();

            Servicio servicio = new Servicio();
            servicio.Exp_time = DateTime.Now.ToLongDateString();  //ASOSA
            servicio.Nombre = datosMiBA.personaLogin.persona.terminosYCondiciones.nivelAcceso.nombre;//ASOSA
            datosToken.Servicio = servicio;

            Autenticado autenticado = new Autenticado();
            autenticado.Cuit = datosMiBA.personaLogin.persona.cuit;
            autenticado.Nombre = datosMiBA.personaLogin.persona.nombres + " " + datosMiBA.personaLogin.persona.apellidos;
            autenticado.Isib = "";
            autenticado.Codcalle = "";
            autenticado.Calle = datosMiBA.personaLogin.calle;
            autenticado.Puerta = datosMiBA.personaLogin.altura;
            autenticado.Codpostal = datosMiBA.personaLogin.codigoPostal;
            autenticado.Telefono = datosMiBA.personaLogin.persona.telefono;
            autenticado.Email = datosMiBA.personaLogin.persona.email;
            autenticado.Nivel = datosMiBA.personaLogin.persona.terminosYCondiciones.nivelAcceso.id.ToString();//ASOSA
            autenticado.TipoDocumento = datosMiBA.personaLogin.persona.tipoDocumento;
            autenticado.Documento = datosMiBA.personaLogin.persona.numeroDocumento;
            datosToken.Autenticado = autenticado;

            Representados representados = new Representados();
            Representado representado = new Representado();

            if (datosMiBA.apoderados != null)
            {
                representado.Cuit = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.cuit;
                representado.Nombre = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.nombres + " " + datosMiBA.apoderados.persona.apellidos;
                representado.Isib = "";
                representado.Codcalle = "";
                representado.Calle = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.calle;
                representado.Puerta = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.altura;
                representado.Codpostal = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.codigoPostal;
                representado.Telefono = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.telefono;
                representado.Email = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.email;
                representado.TipoRepresentacion = "";//ASOSA
                representado.TipoDocumento = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.tipoDocumento;
                representado.Documento = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.numeroDocumento;
                representado.Elegido = true.ToString();
            }
            else
            {
                representado.Cuit = datosMiBA.personaLogin.persona.cuit;
                representado.Nombre = datosMiBA.personaLogin.persona.nombres + " " + datosMiBA.personaLogin.persona.apellidos;
                representado.Isib = "";
                representado.Codcalle = "";
                representado.Calle = datosMiBA.personaLogin.calle;
                representado.Puerta = datosMiBA.personaLogin.altura;
                representado.Codpostal = datosMiBA.personaLogin.codigoPostal;
                representado.Telefono = datosMiBA.personaLogin.persona.telefono;
                representado.Email = datosMiBA.personaLogin.persona.email;
                representado.TipoRepresentacion = "";
                representado.TipoDocumento = datosMiBA.personaLogin.persona.tipoDocumento;
                representado.Documento = datosMiBA.personaLogin.persona.numeroDocumento;
                representado.Elegido = true.ToString();
            }
            representados.Representado = representado;
            datosToken.Representados = representados;
            #endregion

            sign = keyPublicaRecibida.ToString();   
            return datosToken;

        }

        private void CrearUsuario(Datos datosToken, string token, string signU)
        {
            var datosUsuario = datosToken.Autenticado;

            string newPassword = Membership.GeneratePassword(6, 0);
            newPassword = System.Text.RegularExpressions.Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "9");
            string email = (string.IsNullOrWhiteSpace(datosUsuario.Email) ? string.Empty : datosUsuario.Email);

            MembershipCreateStatus status;
            MembershipUser usu = Membership.CreateUser(datosUsuario.Cuit.ToString(), newPassword, email, null, null, true, out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new Exception(GetUserErrorMessage(status));
            }
            else
            {
                actualizarProfile((Guid)usu.ProviderUserKey, datosToken, token, signU);
            }

        }
        private bool ExisteUsuario(string username)
        {
            bool ret = false;

            MembershipUser usu = Membership.GetUser(username);

            ret = usu != null;

            return ret;
        }
        private void ActualizarDatosUsuario(Datos datosToken, string token, string signU)
        {
            string username = datosToken.Autenticado.Cuit.ToString();
            string email = (!string.IsNullOrWhiteSpace(datosToken.Autenticado.Email) ? datosToken.Autenticado.Email.Trim() : null);

            // actualiza datos del usuario 
            MembershipUser usu = Membership.GetUser(username);
            bool actualizarDatosUsuario = false;
            Guid userid = (Guid)usu.ProviderUserKey;

            if (usu.IsLockedOut)
                usu.UnlockUser();

            if (!usu.IsApproved)
            {
                usu.IsApproved = true;
                actualizarDatosUsuario = true;
            }

            if (!string.IsNullOrWhiteSpace(email) && email != usu.Email)
            {
                usu.Email = email;
                actualizarDatosUsuario = true;
            }
            if (actualizarDatosUsuario)
            {
                Membership.UpdateUser(usu);
            }

            actualizarProfile(userid, datosToken, token, signU);

        }

        private void actualizarProfile(Guid userid, Datos datosToken, string token, string signU)
        {
            UsuarioBL usuarioBl = new UsuarioBL();

            var usuario = usuarioBl.Single(userid);
            bool isAlta = false;
            if (usuario == null)
            {
                isAlta = true;
                usuario = new UsuarioDTO();
            }

            usuario.UserId = userid;
            usuario.CUIT = datosToken.Autenticado.Cuit.ToString();
            usuario.UserName = datosToken.Autenticado.Cuit.ToString();
            if (!string.IsNullOrWhiteSpace(datosToken.Autenticado.Email))
                usuario.Email = datosToken.Autenticado.Email.Trim();
            usuario.TipoPersona = 0;
            usuario.UserDni = Convert.ToInt32(datosToken.Autenticado.Documento);
            usuario.RazonSocial = "";
            var na = datosToken.Autenticado.Nombre;
            usuario.Apellido = na.Substring(0, na.IndexOf(" "));
            usuario.Nombre = na.Substring(na.IndexOf(" ") + 1).Trim();
            usuario.Calle = datosToken.Autenticado.Calle;
            usuario.NroPuerta = Convert.ToInt32(datosToken.Autenticado.Puerta);
            usuario.Piso = datosToken.Autenticado.Piso;
            usuario.Depto = datosToken.Autenticado.Dpto;
            usuario.CodigoPostal = datosToken.Autenticado.Codpostal;
            usuario.Telefono = datosToken.Autenticado.Telefono.ToString();
            usuario.token = token;
            usuario.sign = signU;

            if (isAlta)
                usuarioBl.Insert(usuario, null);
            else
                usuarioBl.Update(usuario);
        }
        private void GenerarTicketAutenticacion(string username)
        {
            // Genera el ticket de autenticación
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(20), true, "",
                                   FormsAuthentication.FormsCookiePath);

            string hashCookies = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //
            //FormsAuthentication.SetAuthCookie(username, true);
            Globals.username = username;
        }
        private bool validarToken(string token, string sign)
        {
            string keyPrivada = System.Configuration.ConfigurationManager.AppSettings["keyPrivada"];
            bool TokenValido= false;
            JWT jwt = new JWT(token);
            TokenValido = jwt.ValidateJwt();
            return TokenValido;
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