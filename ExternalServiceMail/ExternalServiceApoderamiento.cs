using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using RestSharp.Authenticators;
using RestSharp;
using RestSharp.Deserializers;
using ExternalService.Class;
using System.Net;
using System.Web;
using System.Web.Security;

namespace ExternalService
{
    public class ExternalServiceApoderamiento
    {
        public string GetToken(string user, string pass)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceApoderamiento"];
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
                throw new Exception(string.Format("No se ha podido loguear en el servicio de Token con el usuario  {0}.", user));
            return response.Headers.Where(p => p.Name == "Token").First().Value.ToString();
        }

        public ApoderamientoEntity ValidarApoderamientoonados(string cuitTitular, string cuitApoderado)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings[""];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "TAD";
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

            //var request = new RestRequest("Apoderamiento?cuitTitular=" + cuitTitular+"&cuitApoderado="+cuitApoderado, Method.GET);
            var request = new RestRequest("Apoderamiento", Method.GET);
            request.AddParameter("cuitTitular", cuitTitular);
            request.AddParameter("cuitApoderado", cuitApoderado);

            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                throw new Exception(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(string.Format("No se ha podido verificar la existencia de representación de servicios: {0} - {1}", response.StatusCode, response.Content));
            }
            ApoderamientoEntity ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ApoderamientoEntity>(response.Content);            

            return ret;
        }
    }
}
