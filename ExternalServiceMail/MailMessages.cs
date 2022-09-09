using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Configuration;


namespace ExternalService
{
    public class MailMessages
    {
        private string GetActivate(string resourceName)
        {
            string result = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="UrlActivacion"></param>
        /// <returns></returns>
        public string MailWelcome(string UserName, string Password, string UrlActivacion, string NombreApellido)
        {
            var asd = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string htmlMessage = GetActivate("ExternalService.Recursos.MailWelcome.html");
            htmlMessage = htmlMessage.Replace("[NombreApellido]", NombreApellido);
            htmlMessage = htmlMessage.Replace("[UserName]", UserName);
            htmlMessage = htmlMessage.Replace("[Password]", Password);
            htmlMessage = htmlMessage.Replace("[UrlActivacion]", UrlActivacion);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);

            return htmlMessage;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="UrlActivacion"></param>
        /// <returns></returns>
        public string MailWelcomeProfesional(string UserName, string Password, string UrlActivacion, string NombreApellido)
        {
            var asd = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string htmlMessage = GetActivate("ExternalService.Recursos.MailWelcomeProfesional.html");
            htmlMessage = htmlMessage.Replace("[NombreApellido]", NombreApellido);
            htmlMessage = htmlMessage.Replace("[UserName]", UserName);
            htmlMessage = htmlMessage.Replace("[Password]", Password);
            htmlMessage = htmlMessage.Replace("[UrlActivacion]", UrlActivacion);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);

            return htmlMessage;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="UrlLogin"></param>
        /// <returns></returns>
        public string MailPassRecovery(string UserName, string Password, string UrlLogin)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailPassRecovery.html");
            htmlMessage = htmlMessage.Replace("[UserName]", UserName);
            htmlMessage = htmlMessage.Replace("[Password]", Password);
            htmlMessage = htmlMessage.Replace("[UrlLogin]", UrlLogin);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;

        }


        public string MailPassRecoveryProfesional(string UserName, string Password)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailPassRecoveryProfesional.html");
            htmlMessage = htmlMessage.Replace("[UserName]", UserName);
            htmlMessage = htmlMessage.Replace("[Password]", Password);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Apellido"></param>
        /// <param name="Nombre"></param>
        /// <param name="Email"></param>
        /// <param name="NroPartidaMatriz"></param>
        /// <param name="Seccion"></param>
        /// <param name="Manzana"></param>
        /// <param name="Parcela"></param>
        /// <param name="Calle"></param>
        /// <param name="NroPuerta"></param>
        /// <param name="urlFoto"></param>
        /// <param name="UrlMapa"></param>
        /// <returns></returns>
        public string MailSolicitudNuevaPuerta(string Username, string Apellido, string Nombre
                                            , string Email, string NroPartidaMatriz, string Seccion
                                            , string Manzana, string Parcela, string Calle
                                            , string NroPuerta, string urlFoto, string UrlMapa)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailSolicitudNuevaPuerta.html");
            htmlMessage = htmlMessage.Replace("[Username]", Username);
            htmlMessage = htmlMessage.Replace("[Apellido]", Apellido);
            htmlMessage = htmlMessage.Replace("[Nombre]", Nombre);
            htmlMessage = htmlMessage.Replace("[Email]", Email);
            htmlMessage = htmlMessage.Replace("[NroPartidaMatriz]", NroPartidaMatriz);
            htmlMessage = htmlMessage.Replace("[Seccion]", Seccion);
            htmlMessage = htmlMessage.Replace("[Manzana]", Manzana);
            htmlMessage = htmlMessage.Replace("[Parcela]", Parcela);
            htmlMessage = htmlMessage.Replace("[Calle]", Calle);
            htmlMessage = htmlMessage.Replace("[NroPuerta]", NroPuerta);
            htmlMessage = htmlMessage.Replace("[urlFoto]", urlFoto);
            htmlMessage = htmlMessage.Replace("[UrlMapa]", UrlMapa);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public string MailProfesionalAnulacionAnexoTecnico(int IdEncomienda)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailProfesional.html");
            htmlMessage = htmlMessage.Replace("[IdEncomienda]", IdEncomienda.ToString());

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        /// <param name="ApellidoProfesional"></param>
        /// <param name="NombreProfesional"></param>
        /// <returns></returns>
        public string MailTitularAnulacionAnexoTecnico(int IdProfesional, string ApellidoProfesional, string NombreProfesional)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailTitulares.html");
            htmlMessage = htmlMessage.Replace("[IdEncomienda]", IdProfesional.ToString());
            htmlMessage = htmlMessage.Replace("[Apellido]", ApellidoProfesional);
            htmlMessage = htmlMessage.Replace("[Nombre]", NombreProfesional);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="codigo_seguridad"></param>
        /// <returns></returns>
        public string MailSolicitudNueva(int id_solicitud, string codigo_seguridad,string descripcion_tipo_tramite)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailSolicitudNueva.html");
            htmlMessage = htmlMessage.Replace("[id_solicitud]", id_solicitud.ToString());
            htmlMessage = htmlMessage.Replace("[codigo_seguridad]", codigo_seguridad);
            htmlMessage = htmlMessage.Replace("[descripcion_tipo_tramite]", descripcion_tipo_tramite);

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);
            return htmlMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public string MailDisponibilzarQR(int id_solicitud)
        {
            string htmlMessage = GetActivate("ExternalService.Recursos.MailQrDisponible.html");

            string HeaderMailImg = ConfigurationManager.AppSettings["HeaderMailImg"];
            string FooterMailImg = ConfigurationManager.AppSettings["FooterMailImg"];
            htmlMessage = htmlMessage.Replace("[HeaderMailImg]", HeaderMailImg);
            htmlMessage = htmlMessage.Replace("[FooterMailImg]", FooterMailImg);

            return htmlMessage;
        }

        public string GetRecoveryEncomiendaAprobado()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<p>");
            s.Append("Sr Contribuyente:<br />");
            s.Append("Le informamos que el Anexo T&eacute;cnico de su tr&aacute;mite de habilitaci&oacute;n se encuentra disponible en el SSIT.<br />");
            s.Append("A partir de ahora, usted puede obtener el Certificado de Aptitud Ambiental.<br />");
            s.Append("<br />");

            return s.ToString();
        }
    }
}