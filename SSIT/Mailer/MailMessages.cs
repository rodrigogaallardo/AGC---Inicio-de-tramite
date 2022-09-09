using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SSIT.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using ExternalService;



namespace SSIT.Mailer
{
    public class MailMessages
    {
        public static void MailWelcome(Guid userid)
        {
            UsuarioBL usuBL = new UsuarioBL();
            var user = usuBL.Single(userid);

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
            SendMail("Activación de usuario - SSIT", emailHtml, user.Email, (int)ExternalService.TiposDeMail.CreacionUsuario, 1, null);


            return;

        }

        public static void MailPassRecovery(Guid userid)
        {
            UsuarioBL usuBL = new UsuarioBL();
            var user = usuBL.Single(userid);

            Control ctl = new Control();
            string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailPassRecovery.aspx");
            surl = BasePage.IPtoDomain(surl);

            WebRequest request = WebRequest.Create(string.Format("{0}?userid={1}", surl, userid));
            WebResponse response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

            string emailHtml = reader.ReadToEnd();

            reader.Dispose();
            response.Dispose();
            SendMail("Recupero de contraseña - SSIT", emailHtml, user.Email, (int)ExternalService.TiposDeMail.RecuperoContrasena, 1, null);


            return;

        }

        public static void MailAnulacionAnexoTecnico(int IdEncomienda, int IdSolicitud, int IdProfesional)
        {
            MailProfesionalAnulacionAnexoTecnico(IdEncomienda, IdSolicitud, IdProfesional);
            MailTitularAnulacionAnexoTecnico(IdEncomienda, IdSolicitud, IdProfesional);
            return;
        }

        public static void MailProfesionalAnulacionAnexoTecnico(int IdEncomienda, int IdSolicitud, int IdProfesional)
        {

            Control ctl = new Control();
            string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailProfesional.aspx");
            surl = BasePage.IPtoDomain(surl);

            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            ProfesionalesBL profBL = new ProfesionalesBL();
            List<int> lstIdSolicitudes = new List<int>();

            lstIdSolicitudes.Add(IdSolicitud);

            var profDTO = profBL.Single(IdProfesional);

            var lstDireccionesSSIT = solBL.GetDireccionesSSIT(lstIdSolicitudes).ToList();

            WebRequest request = WebRequest.Create(string.Format("{0}?IdEncomienda={1}", surl, IdEncomienda));
            WebResponse response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

            string emailHtml = reader.ReadToEnd();

            reader.Dispose();
            response.Dispose();

            string asunto = "";
            asunto = asunto + "Solicitud de habilitación N: " + IdSolicitud.ToString();
            asunto = asunto + " - Anexo Técnico N: " + IdEncomienda.ToString();
            asunto = asunto + " - Ubicación: " + lstDireccionesSSIT[0].direccion;

            TitularesBL titBL = new TitularesBL();

            SendMail(asunto, emailHtml, profDTO.Email, (int)ExternalService.TiposDeMail.AnulacionAnexoTecnico, 1, null);

            return;


        }
        public static void MailTitularAnulacionAnexoTecnico(int IdEncomienda, int IdSolicitud, int IdProfesional)
        {
            Control ctl = new Control();
            string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailTitulares.aspx");
            surl = BasePage.IPtoDomain(surl);

            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            ProfesionalesBL profBL = new ProfesionalesBL();
            List<int> lstIdSolicitudes = new List<int>();

            lstIdSolicitudes.Add(IdSolicitud);

            var NombreProf = profBL.Single(IdProfesional);

            var lstDireccionesSSIT = solBL.GetDireccionesSSIT(lstIdSolicitudes).ToList();

            WebRequest request = WebRequest.Create(string.Format("{0}?Profesional={1}", surl, NombreProf.Apellido + " " + NombreProf.Nombre));
            WebResponse response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

            string emailHtml = reader.ReadToEnd();

            reader.Dispose();
            response.Dispose();

            string asunto = "";
            asunto = asunto + "Solicitud de habilitación N: " + IdSolicitud.ToString();
            asunto = asunto + " - Anexo Técnico N: " + IdEncomienda.ToString();
            asunto = asunto + " - Ubicación: " + lstDireccionesSSIT[0].direccion;

            TitularesBL titBL = new TitularesBL();
            var lstTitulares = titBL.GetTitularesSolicitud(IdSolicitud);

            string mailTitulares = "";
            foreach (var mailItem in lstTitulares)
                mailTitulares = mailTitulares + mailItem.Email + "; ";

            SendMail(asunto, emailHtml, mailTitulares, (int)ExternalService.TiposDeMail.AnulacionAnexoTecnico, 1, null);

            return;

        }

        public static void MailSolicitudNuevaPuerta(Guid userid, int id_ubicacion, string Calle, int NroPuerta)
        {
            ParametrosBL pBL = new ParametrosBL();
            UsuarioBL usuBL = new UsuarioBL();
            var user = usuBL.Single(userid);

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
            string mailUbicacionesAGC = pBL.GetParametroChar("ENC.Mail.Alta.Calle.Parcela");

            SendMail("Solicitud de nueva calle en parcela", emailHtml, mailUbicacionesAGC, (int)ExternalService.TiposDeMail.Generico, 1, user.Email);

            return;

        }

        public static void MailSolicitudNueva(int id_solicitud)
        {
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            UsuarioBL blUser = new UsuarioBL();
            var sol = blSol.Single(id_solicitud);
            var user = blUser.Single(sol.CreateUser);
            List<int> lisSol = new List<int>();
            lisSol.Add(id_solicitud);
            string Direccion = blSol.GetDireccionesSSIT(lisSol).First().direccion;
            string asunto = "Solicitud de habilitación N°: " + id_solicitud + " - " + Direccion;


            Control ctl = new Control();

            string url = string.Format("~/Mailer/MailSolicitudNueva.aspx?id_solicitud={0}&codigo_seguridad={1}", id_solicitud, sol.CodigoSeguridad);
            string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl(url);
            surl = BasePage.IPtoDomain(surl);

            WebRequest request = WebRequest.Create(surl);
            WebResponse response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

            string emailHtml = reader.ReadToEnd();

            reader.Dispose();
            response.Dispose();

            TitularesBL titBL = new TitularesBL();
            var lstTitulares = titBL.GetTitularesSolicitud(id_solicitud);

            string mailTitulares = "";
            foreach (var mailItem in lstTitulares)
                mailTitulares = mailTitulares + mailItem.Email + "; ";
            FirmantesBL firBL = new FirmantesBL();
            var lstFirmantes = firBL.GetFirmantes(id_solicitud);

            foreach (var mailItem in lstFirmantes)
                mailTitulares = mailTitulares + mailItem.Email + "; ";

            mailTitulares = mailTitulares + user.Email + "; ";

            SendMail(asunto, emailHtml, mailTitulares, (int)ExternalService.TiposDeMail.Generico, 1, null);

            return;

        }

        public static void MailDisponibilzarQR(int id_solicitud)
        {
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            UsuarioBL blUser = new UsuarioBL();
            var sol = blSol.Single(id_solicitud);
            var user = blUser.Single(sol.CreateUser);
            List<int> lisSol = new List<int>();
            lisSol.Add(id_solicitud);
            string Direccion = blSol.GetDireccionesSSIT(lisSol).First().direccion;
            string asunto = "Solicitud de habilitación N°: " + id_solicitud + " - " + Direccion;

            Control ctl = new Control();
            string surl = "http://" + HttpContext.Current.Request.Url.Authority + ctl.ResolveUrl("~/Mailer/MailQrDisponible.aspx");
            surl = BasePage.IPtoDomain(surl);

            WebRequest request = WebRequest.Create(surl);
            WebResponse response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);

            string emailHtml = reader.ReadToEnd();
            reader.Dispose();
            response.Dispose();

            SendMail(asunto, emailHtml, user.Email, (int)ExternalService.TiposDeMail.WebSGI_AprobacionDG, 1, null);
        }

        public static void SendMail(string Subject, string bodyHtml, string EmailAdress, int IdTipoEmail, int prioridad, string EmailCC)
        {
            try
            {

                if (isDesarrollo())
                {
                    EmailAdress = Mail_Pruebas;
                }

                ExternalServiceMail esMail = new ExternalServiceMail();
                EmailEntity email = new EmailEntity();
                email.Email = EmailAdress;
                email.Cc = EmailCC;
                email.Html = bodyHtml;
                email.Asunto = Subject;
                email.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                email.IdTipoEmail = IdTipoEmail;
                email.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                email.CantIntentos = 3;
                email.CantMaxIntentos = 3;
                email.FechaAlta = DateTime.Now;
                email.Prioridad = prioridad;

                esMail.SendMail(email);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
            }
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