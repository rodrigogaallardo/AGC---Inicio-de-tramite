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
using ExternalService.ws_interface_AGC;
using StaticClass;

namespace ExternalService
{
    public class ExternalServiceAGIP_REST
    {
        /// <summary>
        /// get token 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string GetToken(string user, string pass)
        {
            try
            {
                string resultado;
                string hostParametro = ConfigurationManager.AppSettings["AGIP.Rest.ClaveCiudad.URL"];

                string _host;
                if (hostParametro.IndexOf("http") < 0)
                    _host = "http://" + hostParametro;
                else
                    _host = hostParametro;

                var client = new RestClient(_host);
                client.ClearHandlers();
                client.AddHandler("application/json", new JsonDeserializer());
                var request = new RestRequest(Method.GET);
                request.AddParameter("user", user);
                request.AddParameter("psw", pass);
                request.AddHeader("Content-Type", "application/json charset=UTF-8");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                LogError.Write(new Exception("Client: " + Funciones.GetDataFromClient(client)));
                LogError.Write(new Exception("Request: " + Funciones.GetDataFromRequest(request)));
                LogError.Write(new Exception("Response: " + Funciones.GetDataFromResponse(response)));
                TokenResponseAGIPRest respuesta = JsonConvert.DeserializeObject<TokenResponseAGIPRest>(response.Content);
                if (respuesta == null)
                    throw new Exception("Error al Autenticar con AGIP: No se obtuvo respuesta");
                else
                {
                    if (!respuesta.success)
                        throw new Exception("Error al Autenticar con AGIP: " + respuesta.code.ToString() + ": " + respuesta.message);
                    else
                        resultado = respuesta.token;
                }
                return resultado;
            }
            catch (Exception excep)
            {
                LogError.Write(excep);
                throw excep;
            }
        }

        public CuitsRelacionadosPOSTRest CuitsRelacionadosRest(CuitsRelacionadosDTO_REST cuitsRel)
        {
            try
            {
                string hostParametro = ConfigurationManager.AppSettings["AGIP.Rest.ClaveCiudad.URL"];
                string serviceParametro = ConfigurationManager.AppSettings["AGIP.Rest.ClaveCiudad.Metodo"];
                string userParametro = ConfigurationManager.AppSettings["AGIP.Rest.ClaveCiudad.User"];
                string passParametro = ConfigurationManager.AppSettings["AGIP.Rest.ClaveCiudad.Pass"];

                string _token = GetToken(userParametro, passParametro);

                string _host;
                if (hostParametro.IndexOf("http") < 0)
                    _host = "http://" + hostParametro;
                else
                    _host = hostParametro;

                var client = new RestClient(_host);
                client.ClearHandlers();
                client.AddHandler("application/json", new JsonDeserializer());
                var request = new RestRequest(Method.POST);
                request.AddParameter("method", serviceParametro);
                request.AddParameter("cuitRepresentado", cuitsRel.cuitRepresentado);
                request.AddParameter("cuitAValidar", cuitsRel.cuitAValidar);
                request.AddHeader("Authorization", "Bearer " + _token.ToString());
                IRestResponse response = client.Execute(request);
                LogError.Write(new Exception("Client: " + Funciones.GetDataFromClient(client)));
                LogError.Write(new Exception("Request: " + Funciones.GetDataFromRequest(request)));
                LogError.Write(new Exception("Response: " + Funciones.GetDataFromResponse(response)));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(string.Format("No se ha podido verificar la existencia de representación de servicios: {0} - {1}", response.StatusCode, response.Content));

                CuitsRelacionadosPOSTRest ret = JsonConvert.DeserializeObject<CuitsRelacionadosPOSTRest>(response.Content);

                if (ret.statusCode != 200)
                    throw new Exception("Error al Validar CUITs con AGIP: " + ret.statusCode + ": " + ret.message);

                return ret;
            }
            catch (Exception excep)
            {
                LogError.Write(excep);
                throw excep;
            }
}
    }
}
