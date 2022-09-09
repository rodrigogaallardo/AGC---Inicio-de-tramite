using AnexoProfesionales.App_Components;
using ExternalService;
using StaticClass;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;



namespace AnexoProfesionales.Mailer
{
        public class MailMessages
        {


            public static void MailWelcome(Guid userid)
            {

                MembershipUser user = Membership.GetUser(userid);

                Control ctl = new Control();
                string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailWelcome.aspx");
                surl = BasePage.IPtoDomain(surl);

                WebRequest request = WebRequest.Create(string.Format("{0}?userid={1}", surl, userid));
                WebResponse response = request.GetResponse();
                
                Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
                StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

                string emailHtml = reader.ReadToEnd();

                reader.Dispose();
                response.Dispose();
                SendMail("Activación de usuario - Anexo", emailHtml, user.Email, (int)Constantes.TiposDeMail.CreacionUsuario);
                

                return;

            }

            public static void MailPassRecovery(Guid userid)
            {

                MembershipUser user = Membership.GetUser(userid);

                Control ctl = new Control();
                string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailPassRecovery.aspx");
                surl = BasePage.IPtoDomain(surl);

                WebRequest request = WebRequest.Create(string.Format("{0}?userid={1}", surl, userid));
                WebResponse response = request.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
                StreamReader reader = new StreamReader(response.GetResponseStream(),enc);

                string emailHtml = reader.ReadToEnd();

                reader.Dispose();
                response.Dispose();
                SendMail("Recupero de contraseña - Anexo", emailHtml, user.Email, (int)Constantes.TiposDeMail.RecuperoContrasena);


                return;

            }


            public static void MailSolicitudNuevaPuerta(Guid userid, int id_ubicacion, string Calle, int NroPuerta)
            {

                MembershipUser user = Membership.GetUser(userid);

                Control ctl = new Control();
                string url = string.Format("~/Mailer/MailSolicitudNuevaPuerta.aspx?userid={0}&id_ubicacion={1}", userid, id_ubicacion);
                string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl(url);
                surl = BasePage.IPtoDomain(surl);

                WebRequest request = WebRequest.Create(surl);
                WebResponse response = request.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
                StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

                string emailHtml = reader.ReadToEnd();

                emailHtml = emailHtml.Replace(":Calle:", Calle);
                emailHtml = emailHtml.Replace(":NroPuerta:", NroPuerta.ToString());

                reader.Dispose();
                response.Dispose();
                SendMail("SIPSA - Solicitud de nueva calle en parcela", emailHtml, user.Email, (int)Constantes.TiposDeMail.Generico);


                return;

            }

            public static void SendMail(string Subject, string bodyHtml, string EmailAdress, int idTipoEmail)
            {

                if (isDesarrollo())
                {
                    EmailAdress = Mail_Pruebas;
                }

                ExternalServiceMail esMail = new ExternalServiceMail();
                EmailEntity email = new EmailEntity();
                email.Email = EmailAdress;
                email.Html = bodyHtml;
                email.Asunto = Subject;
                email.IdEstado = (int)Constantes.TiposDeEstadosEmail.PendienteDeEnvio;
                email.IdTipoEmail = idTipoEmail;
                email.IdOrigen = (int)Constantes.MailOrigenes.AGC;
                email.CantIntentos = 3;
                email.CantMaxIntentos = 3;
                email.FechaAlta = DateTime.Now;
                email.Prioridad = 1;

                esMail.SendMail(email);

            }

            public static bool isDesarrollo()
            {
                bool ret = false;
                string value = System.Configuration.ConfigurationManager.AppSettings["isDesarrollo"];
                if (!string.IsNullOrEmpty(value))
                {
                    ret = Convert.ToBoolean(value);
                }

                return ret;
            }

            public static string Mail_Pruebas
            {
                get
                {
                    string ret = "";
                    string value = System.Configuration.ConfigurationManager.AppSettings["Mail.Pruebas"];
                    if (!string.IsNullOrEmpty(value))
                    {
                        ret = value.ToString();
                    }

                    return ret;
                }

            }
            
        }
        

}