using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;
using RestSharp.Authenticators;
using ExternalService.Class;

namespace ExternalService
{
    public class ExternalServiceReporting
    {
        public string GetToken(string user, string pass)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceReporting"];
            string autenticateHostParametro = ConfigurationManager.AppSettings["NombreParamHostAutorizacion"];
            //string host = "http://www.dghpsh.agcontrol.gob.ar/test/ws.rest.files";
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + autenticateHostParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            client.Authenticator = new HttpBasicAuthenticator(user, pass);
            var request = new RestRequest(Method.POST);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(string.Format("No se ha podido loguear en el servicio ws.rest.reporting con el usuario  {0}, Status Description {1}.", user, response.StatusDescription));
            return response.Headers.Where(p => p.Name == "Token").First().Value.ToString();
        }


        private ReportingEntity GetPDFReporte(string restParametro, int id_tramite, bool guardar)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceReporting"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            restParametro = "/api/" + restParametro;
            string _token = GetToken(userParametro, passParametro);
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + restParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest(string.Format("?id_tramite={0}&guardar={1}", id_tramite, guardar), Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse<ReportingEntity> response = client.Execute<ReportingEntity>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                throw new Exception(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(string.Format("No se ha podido obtener el Reporte en PDF.Status: {0} - {1}", response.StatusCode, response.Content));

            ReportingEntity ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportingEntity>(response.Content);

            return ret;

        }


        private ReportingEntity GetPDFInforme(string restParametro, bool guardar)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceReporting"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            restParametro = "/api/" + restParametro;

            string _token = GetToken(userParametro, passParametro);

            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + restParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest(string.Format("?guardar={0}", guardar), Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);


            IRestResponse<ReportingEntity> response = client.Execute<ReportingEntity>(request);


            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el Reporte en PDF.");

            ReportingEntity ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportingEntity>(response.Content);

            return ret;

        }
        public ReportingEntity GetPDFEscribanos(bool guardar)
        {
            return GetPDFInforme("Escribanos", guardar);
        }
        public ReportingEntity GetPDFProfesionales(bool guardar)
        {
            return GetPDFInforme("Profesionales", guardar);
        }

        public ReportingEntity GetPDFEncomienda(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("Encomienda", id_encomienda, guardar);
        }

        public ReportingEntity GetPDFEncomiendaDDRRASP(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("EncomiendaDDRRASP", id_encomienda, guardar);
        }

        public ReportingEntity GetPDFEncomiendaDDRRPL(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("EncomiendaDDRRPL", id_encomienda, guardar);
        }
        public ReportingEntity GetPDFEncomiendaTransmision(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("EncomiendaTransmision", id_encomienda, guardar);
        }
        public ReportingEntity GetPDFOblea(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("SolicitudOblea", id_solicitud, guardar);
        }
        public ReportingEntity GetPDFObleaTransmision(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("TransmisionOblea", id_solicitud, guardar);
        }
        public ReportingEntity GetPDFSolicitud(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("Solicitud", id_solicitud, guardar);
        }

        public ReportingEntity GetPDFSolicitudActEconomica(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("SolicitudActEconomica", id_solicitud, guardar);
        }
        public ReportingEntity GetPDFTransmision(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("ManifiestoTransmision", id_solicitud, guardar);
        }
        public ReportingEntity GetPDFSolicitudNueva(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("SolicitudNueva", id_solicitud, guardar);
        }
        public ReportingEntity GetPDFCertificadoConsejoEncomienda(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("CertificadoConsejoEncomienda", id_encomienda, guardar);
        }

        public ReportingEntity GetPDFCertificadoExtConsejoEncomienda(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("CertificadoExtConsejoEncomienda", id_encomienda, guardar);
        }

        public ReportingEntity GetPDFCertificadoExtConsejoEncomiendaDeEx(int id_encomienda, bool guardar)
        {
            return GetPDFReporte("CertificadoExtConsejoEncomiendaDeEx", id_encomienda, guardar);
        }

        public ReportingEntity GetPDFCPadron(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("CPadron", id_solicitud, guardar);
        }

        public ReportingEntity GetPDFTransferencia(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("Transferencia", id_solicitud, guardar);
        }

        public ReportingEntity GetPDFReportePlancheta(string restParametro, int id_tramite, int id_tipo_informe, bool guardar)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"]; //"http://www.dghpsh.agcontrol.gob.ar/test"; 
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceReporting"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            restParametro = "/api/" + restParametro;

            string _token = GetToken(userParametro, passParametro);

            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + restParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest(string.Format("?id_tramite={0}&id_tipoinforme={1}&guardar={2}", id_tramite, id_tipo_informe, guardar), Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);


            IRestResponse<ReportingEntity> response = client.Execute<ReportingEntity>(request);


            if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                throw new Exception(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el Reporte en PDF.");

            ReportingEntity ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportingEntity>(response.Content);

            return ret;

        }

        public ReportingEntity GetPDFManifiestoTransmision(int idSolicitud, bool guardar)
        {
            return GetPDFReporte("ManifiestoTransmision", idSolicitud, guardar);
        }

        public ReportingEntity GetPDFPermisoMC(int id_solicitud, bool guardar)
        {
            return GetPDFReporte("PermisoMC", id_solicitud, guardar);
        }

        public ReportingEntity GetPDFCertificadoHabilitacion(string restParametro, int id_tramite, string nroExpediente, bool impresionDePrueba)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"]; //"http://www.dghpsh.agcontrol.gob.ar/test"; 
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceReporting"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            restParametro = "/api/" + restParametro;

            string _token = GetToken(userParametro, passParametro);

            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + restParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            if (nroExpediente == null)
                nroExpediente = "' '";

            var request = new RestRequest(string.Format("?id_tramite={0}&nro_Expediente={1}&impresionDePrueba={2}&guardar={3}", id_tramite, nroExpediente, impresionDePrueba, true), Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);


            IRestResponse<ReportingEntity> response = client.Execute<ReportingEntity>(request);


            if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                throw new Exception(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el Reporte en PDF.");

            ReportingEntity ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportingEntity>(response.Content);

            return ret;

        }
    }
}
