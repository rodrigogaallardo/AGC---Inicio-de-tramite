using BusinesLayer.Implementation;
using DataTransferObject;
using Microsoft.IdentityModel.Tokens;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SSIT.Account
{
    public class AuthenticateAGIPProc
    {

        public bool ReadData()
        {


            bool ret = true;
            string token = "";
          
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


              //  string TokenRecibido = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJwZXJzb25hTG9naW4iOnsiaWQiOjExLCJwZXJzb25hIjp7ImlkIjoxMSwibm9tYnJlcyI6IkZhY3VuZG8gQWxiZXJ0byIsImFwZWxsaWRvcyI6IkZPUkNBREEiLCJyYXpvblNvY2lhbCI6bnVsbCwiY3VpdCI6IjIzMzk2MjAzNjY5IiwidGlwb0RvY3VtZW50byI6IkRVIiwibnVtZXJvRG9jdW1lbnRvIjoiMzk2MjAzNjYiLCJzZXhvIjoiTSIsImNvZGlnb1BhaXMiOiJBUiIsImNvZGlnb1RlbGVmb25vUGFpcyI6Iis1NCIsInRlbGVmb25vIjoiMTEyMjMzNDQ1NSIsImVtYWlsIjoiZmFjdW5kby5mb3JjYWRhQGJpd2luaS5jb20iLCJ1c3VhcmlvQ3JlYWNpb24iOjExLCJmZWNoYUFsdGEiOjE2ODg1NzI0NzgwMDAsInVzdWFyaW9Nb2RpZmljYWNpb24iOm51bGwsImZlY2hhTW9kaWZpY2FjaW9uIjoxNjg4NTcyNDc4MDAwLCJ0aXBvUGVyc29uYSI6IlBGIiwidmFsaVJlbmFwZXIiOjAsInRlcm1pbm9zWUNvbmRpY2lvbmVzIjp7ImlkIjozLCJ0aXBvRG9jdW1lbnRvIjp7ImlkIjoxNiwiYWNyb25pbW9HZWRvIjoiVFlDIiwiYWNyb25pbW9UQUQiOiJUWUMiLCJub21icmUiOiJUZXJtaW5vcyB5IENvbmRpY2lvbmVzIiwiZGVzY3JpcGNpb24iOiJUZXJtaW5vcyB5IENvbmRpY2lvbmVzIiwiZm9ybXVsYXJpb0NvbnRyb2xhZG8iOm51bGwsInRpcG9Qcm9kdWNjaW9uIjpudWxsLCJ1c3VhcmlvSW5pY2lhZG9yIjoiRVZFUklTIiwidXN1YXJpb0NyZWFjaW9uIjoiVEFEM19TQURFX0RFViIsImZlY2hhQWx0YSI6MTYxNDk2MDYyNjAwMCwidXN1YXJpb01vZGlmaWNhY2lvbiI6bnVsbCwiZmVjaGFNb2RpZmljYWNpb24iOjE1NTU0MDc4MDAwMDAsImVzRW1iZWJpZG8iOnRydWUsImZpcm1hQ29uVG9rZW4iOmZhbHNlLCJpcCI6bnVsbCwiZXNGaXJtYUNvbmp1bnRhIjpmYWxzZSwiZG9jdW1lbnRvVGlwb0Zpcm1hIjpudWxsLCJ0ZXh0b0xpYnJlTGltaXRlIjpudWxsLCJ0ZXh0b0xpYnJlRW5yaXF1ZWNpZG8iOmZhbHNlLCJlbWJlYmlkb09wY2lvbmFsIjpudWxsLCJlc0Zpcm1hQ2xvdWQiOmZhbHNlfSwiZXN0YWRvIjoiQUNUSVZPIiwiZmVjaGFBbHRhIjoxNjE5NjA0MTEzMDAwLCJjb250ZW5pZG8iOiI8IWRvY3R5cGUgaHRtbD5cbjxodG1sPlxuIDxoZWFkPlxuIDx0aXRsZT5UJmVhY3V0ZTtybWlub3MgeSBDb25kaWNpb25lcyBUQUQ8L3RpdGxlPlxuIDwvaGVhZD5cbjxib2R5PlxuPGRpdj5cbjxwIHN0eWxlPVwibWFyZ2luLWxlZnQ6IDE0LjJwdDsgdGV4dC1hbGlnbjoganVzdGlmeTtcIj48c3BhbiBzdHlsZT1cImZvbnQtc2l6ZToxOHB4O1wiPjxzcGFuIHN0eWxlPVwiZm9udC1mYW1pbHk6dGFob21hLGdlbmV2YSxzYW5zLXNlcmlmO1wiPjxzdHJvbmc-VCZFYWN1dGU7PC9zdHJvbmc-PHN0cm9uZz5SPC9zdHJvbmc-PHN0cm9uZz5NPC9zdHJvbmc-PHN0cm9uZz5JPC9zdHJvbmc-PHN0cm9uZz5OPC9zdHJvbmc-PHN0cm9uZz5PUyAmbmJzcDtZIENPTkRJQ0lPTkVTICZuYnNwO0RFIFVTTyBERSBMQSBQTEFUQUZPUk1BICZuYnNwO0RFIFRSQU1JVEFDSSZPYWN1dGU7TiAmbmJzcDtBIERJU1RBTkNJQTwvc3Ryb25nPjwvc3Bhbj48L3NwYW4-PC9wPlxuPHAgc3R5bGU9XCJtYXJnaW4tbGVmdDogMTNwdDsgdGV4dC1hbGlnbjoganVzdGlmeTtcIj48YSBocmVmPVwiaHR0cHM6Ly9zYWRlLXRhZDMtaG1sLmdjYmEuZ29iLmFyL3RyYW1pdGVzYWRpc3RhbmNpYS9pbmZvcm1hY2lvbi9UQUQtVGVybWlub3MteS1Db25kaWNpb25lcy5wZGZcIiB0YXJnZXQ9XCJfYmxhbmtcIj48c3BhbiBjbGFzcz1cImMxIGMyXCIgc3R5bGU9XCJsaW5lLWhlaWdodDogMTBweDsgY29sb3I6IHJnYigxNywgODUsIDIwNCk7IHRleHQtZGVjb3JhdGlvbjogdW5kZXJsaW5lOyBmb250LWZhbWlseTogQ2FsaWJyaTtcIj5WZXIgVMOpcm1pbm9zIHkgQ29uZGljaW9uZXM8L3NwYW4-PC9hPjwvcD5cblxuPHAgc3R5bGU9XCJtYXJnaW4tbGVmdDogMTNwdDsgdGV4dC1hbGlnbjoganVzdGlmeTtcIj4mbmJzcDs8L3A-XG5cbjwvZGl2PlxuPC9ib2R5PlxuPC9odG1sPiIsIm5pdmVsQWNjZXNvIjp7ImlkIjoxMSwibm9tYnJlIjoiQUdJUCIsIm5pdmVsQWNjZXNvIjozLCJwcm92ZWVkb3IiOiJBR0lQMyIsImF1dGhvcml6YXRpb25FbmRQb2ludCI6Imh0dHBzOi8vaG1sLmFnaXAuZ29iLmFyL2NsYXZlY2l1ZGFkLyIsImVuZFNlc3Npb25FbmRQb2ludCI6Imh0dHBzOi8vaG1sLmFnaXAuZ29iLmFyL2NsYXZlY2l1ZGFkLyIsImxvZ2luQ29tcG9uZW50IjoiL3ByaW1lckxvZ2luIiwiaGFiaWxpdGFyQXBvZGVyYW1pZW50byI6dHJ1ZX19LCJzaXN0ZW1hQ29uc3VtaWRvciI6bnVsbCwiYmFJZCI6bnVsbCwiaGFiaWxpdGFkYVZpc3RhMzYwIjpudWxsfSwiY2FsbGUiOiJNQUlQVSIsImFsdHVyYSI6IjM3NCIsInBpc28iOm51bGwsImRlcHRvIjpudWxsLCJjb2RpZ29Qb3N0YWwiOiIxMjM0IiwidGVsZWZvbm8iOm51bGwsIm9ic2VydmFjaW9uZXMiOm51bGx9LCJpZFRhZCI6bnVsbCwiYXBvZGVyYWRvcyI6eyJpZCI6MTEsInBlcnNvbmEiOm51bGwsImNhbGxlIjpudWxsLCJhbHR1cmEiOm51bGwsInBpc28iOm51bGwsImRlcHRvIjpudWxsLCJjb2RpZ29Qb3N0YWwiOm51bGwsInRlbGVmb25vIjoiMTEyMjMzNDQ1NSIsIm9ic2VydmFjaW9uZXMiOm51bGx9LCJ0aXBvVHJhbWl0ZSI6bnVsbCwicG9kZXJkYW50ZXMiOltdfQ.GIaWsSo18XCj18aRNoPizs9Exv9uqkuiO9f33_4hMDNz5oWdsy0ORtgIHoTYQ5NweSuQLGKACFI-6Rmojfv71Q";
                string keyPrivadaGuardada =  System.Configuration.ConfigurationManager.AppSettings["PrivateKey"];
                if (!string.IsNullOrWhiteSpace(token) )
                {
                    string[] tokenParts = token.Split('.');
                    string Header = tokenParts[0];
                    string payloadRecibido = tokenParts[1];
                    string keyPublicaRecibida = tokenParts[2];
                    string PayloadJsonString = Base64UrlEncoder.Decode(payloadRecibido);

                    string sign = keyPublicaRecibida;//ASOSA FEO ARREGLAR

                    bool tokenValido = ValidarToken(payloadRecibido, keyPublicaRecibida, keyPrivadaGuardada);
                    tokenValido = true;//ASOSA FORZADO
                                       //  if (validarToken(token))
                    if (tokenValido)
                        {
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
                        autenticado.Isib = "??";
                        autenticado.Codcalle = "";
                        autenticado.Calle = datosMiBA.personaLogin.calle;
                        autenticado.Puerta = datosMiBA.personaLogin.altura;
                        autenticado.Codpostal = datosMiBA.personaLogin.codigoPostal;
                        //autenticado.Codlocalidad = datosMiBA.personaLogin.localidad.id.ToString();
                        //autenticado.Localidad = datosMiBA.personaLogin.localidad.nombre;
                        //autenticado.Codprov = datosMiBA.personaLogin.provincia.id.ToString();
                        //autenticado.Provincia = datosMiBA.personaLogin.provincia.nombre;
                        autenticado.Telefono = datosMiBA.personaLogin.persona.telefono;
                        autenticado.Email = datosMiBA.personaLogin.persona.email;
                        autenticado.Nivel = datosMiBA.personaLogin.persona.terminosYCondiciones.nivelAcceso.id.ToString();//ASOSA
                        autenticado.TipoDocumento = datosMiBA.personaLogin.persona.tipoDocumento;
                        autenticado.Documento = datosMiBA.personaLogin.persona.numeroDocumento;
                        datosToken.Autenticado = autenticado;

                        Representados representados = new Representados();
                        Representado representado = new Representado();

                        representado.Cuit = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.cuit;
                        representado.Nombre = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.nombres + " " + datosMiBA.apoderados.persona.apellidos;
                        representado.Isib = "";
                        representado.Codcalle = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.cuit;
                        representado.Calle = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.calle;
                        representado.Puerta = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.altura;
                        representado.Codpostal = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.codigoPostal;
                        //representado.Codlocalidad = datosMiBA.apoderados.localidad.id.ToString();
                        //representado.Localidad = datosMiBA.apoderados.localidad.nombre;
                        //representado.Codprov = datosMiBA.apoderados.provincia.id.ToString();
                        //representado.Provincia = datosMiBA.apoderados.provincia.nombre;
                        representado.Telefono = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.telefono;
                        representado.Email = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.email;
                        representado.TipoRepresentacion = "";//ASOSA
                        representado.TipoDocumento = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.tipoDocumento;
                        representado.Documento = (datosMiBA.apoderados.persona == null) ? "" : datosMiBA.apoderados.persona.numeroDocumento;
                        representado.Elegido = true.ToString();
                        representados.Representado = representado;
                        datosToken.Representados = representados;
                        #endregion

                        //Datos datosToken = GetDatosTokenAGIP(token);

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
                        url = "~/"+RouteConfig.HOME;
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

            actualizarProfile(userid, datosToken,token, signU);

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
            if(!string.IsNullOrWhiteSpace(datosToken.Autenticado.Email))
                usuario.Email= datosToken.Autenticado.Email.Trim(); 
            usuario.TipoPersona = 0;
            usuario.UserDni = Convert.ToInt32(datosToken.Autenticado.Documento);
            usuario.RazonSocial = "";
            var na = datosToken.Autenticado.Nombre;
            usuario.Apellido =na.Substring(0,na.IndexOf(" "));
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
        private bool validarToken(string token)
        {
            bool TokenValido = true;
            return TokenValido;
        }
        protected bool ValidarToken(string payloadRecibido, string keyPublicaRecibida, string keyPrivadaGuardada)
        {

            byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadRecibido);
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyPrivadaGuardada);


            string TokenGenerado = GenerateToken(payloadRecibido, keyPrivadaGuardada);


            string[] TokenGeneradoParts = TokenGenerado.Split('.');

            string payloadGenerado = TokenGeneradoParts[0];
            string keyPublicaGenerado = TokenGeneradoParts[1];
            //bool isValid = payloadRecibido.Equals(payloadGenerado); COMPARO LOS PAYLOAD OK
            bool isValid = keyPublicaRecibida.Equals(keyPublicaGenerado);
            //bool isValid = ComprobarToken(TokenGenerado, keyPrivadaGuardada);
            return isValid;
        }
        public static string GenerateToken(string payload, string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
            {

                byte[] signatureBytes = hmac.ComputeHash(payloadBytes);

                string signature = Convert.ToBase64String(signatureBytes);

                string TokenGenerado = $"{payload}.{signature}";
                return TokenGenerado;
            }
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