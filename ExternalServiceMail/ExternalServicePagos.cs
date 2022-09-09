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

namespace ExternalService
{
    public class ExternalServicePagos
    {
        /// <summary>
        /// get token 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string GetToken(string user, string pass)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
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

        public void CancelarPago(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];

            string _token = GetToken(userParametro, passParametro);

            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + "/api/CancelarBoletaUnica");
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest("?IdPago=" + IdPago);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido cancelar el pago en el servicio.");
        }

        public BUBoletaUnica GetBoletaUnica(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetBoletaUnica";
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

            var request = new RestRequest("?IdPago=" + IdPago, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse<BUBoletaUnica> response = client.Execute<BUBoletaUnica>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener la boleta en el servicio.");

            BUBoletaUnica ret = Newtonsoft.Json.JsonConvert.DeserializeObject<BUBoletaUnica>(response.Content);

            return ret;
        }

        public List<BUConcepto> GetConcepto(int CodigoConcepto1, int CodigoConcepto2, int CodigoConcepto3)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetConcepto";
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

            var request = new RestRequest("?CodigoConcepto1=" + CodigoConcepto1 + "&CodigoConcepto2=" + CodigoConcepto2 + "&CodigoConcepto3=" + CodigoConcepto3, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse<List<BUConcepto>> response = client.Execute<List<BUConcepto>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el concepto en el servicio.");

            List<BUConcepto> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BUConcepto>>(response.Content);

            return ret;
        }

        public List<BUConcepto> GetConceptos()
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetConceptos";
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

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse<List<BUConcepto>> response = client.Execute<List<BUConcepto>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se han podido obtener los concepto en el servicio.");

            List<BUConcepto> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BUConcepto>>(response.Content);

            return ret;
        }

        public List<BUIConceptoConfig> GetConceptosConfig()
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetConceptosConfig";
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

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse<List<BUIConceptoConfig>> response = client.Execute<List<BUIConceptoConfig>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener la config del concepto en el servicio.");

            List<BUIConceptoConfig> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BUIConceptoConfig>>(response.Content);

            return ret;
        }

        public string GetEstadoPago(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetEstadoPago";
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

            var request = new RestRequest("?IdPago=" + IdPago, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el estado de la boleta en el servicio.");

            return response.Content.Replace("\"", "");
        }

        public string GetEstadoPosteriorAlPago(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetEstadoPosteriorAlPago";
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

            var request = new RestRequest("?IdPago=" + IdPago, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el estado de la boleta en el servicio.");

            return response.Content;
        }

        public byte[] GetPDFBoletaUnica(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetPDFBoletaUnica";
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

            var request = new RestRequest("?IdPago=" + IdPago, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el PDF de la boleta en el servicio.");

            return response.RawBytes;
        }

        public byte[] GetQR(int IdPago)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "GetQR";
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

            var request = new RestRequest("?IdPago=" + IdPago, Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido obtener el QR de la boleta en el servicio.");

            return response.RawBytes;
        }

        public List<BUBoletaUnica> ObtenerBoletas(List<int> IdPagos)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "PostObtenerBoletas";
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

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);
            request.AddJsonBody(IdPagos);

            IRestResponse<List<BUBoletaUnica>> response = client.Execute<List<BUBoletaUnica>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se han podido obtener las boleta en el servicio.");

            List<BUBoletaUnica> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BUBoletaUnica>>(response.Content);

            return ret;
        }

        private void ValidarDatosBoleta(BUDatosBoleta DatosBoleta)
        {
            int MaxLocalidadLength = 40;
            int MaxApellidoyNombreLength = 150;
            int MaxDireccionLength = 100;
            int MaxEmailLength = 70;
            int MaxPisoLength = 20;
            int MaxDepartamentoLength = 5;


            if (DatosBoleta.datosConstribuyente.Localidad.Length > MaxLocalidadLength)
                DatosBoleta.datosConstribuyente.Localidad = DatosBoleta.datosConstribuyente.Localidad.Substring(0, MaxLocalidadLength);

            if (DatosBoleta.datosConstribuyente.ApellidoyNombre.Length > MaxApellidoyNombreLength)
                DatosBoleta.datosConstribuyente.ApellidoyNombre = DatosBoleta.datosConstribuyente.ApellidoyNombre.Substring(0, MaxApellidoyNombreLength);

            if (DatosBoleta.datosConstribuyente.Direccion.Length > MaxDireccionLength)
                DatosBoleta.datosConstribuyente.Direccion = DatosBoleta.datosConstribuyente.Direccion.Substring(0, MaxDireccionLength);

            if (DatosBoleta.datosConstribuyente.Email.Length > MaxEmailLength)
                DatosBoleta.datosConstribuyente.Email = DatosBoleta.datosConstribuyente.Email.Substring(0, MaxEmailLength);

            if (DatosBoleta.datosConstribuyente.Piso.Length > MaxPisoLength)
                DatosBoleta.datosConstribuyente.Piso = DatosBoleta.datosConstribuyente.Piso.Substring(0, MaxPisoLength);

            if (DatosBoleta.datosConstribuyente.Departamento.Length > MaxDepartamentoLength)
                DatosBoleta.datosConstribuyente.Departamento = DatosBoleta.datosConstribuyente.Departamento.Substring(0, MaxDepartamentoLength);
        }

        public BUBoletaUnica GenerarBoleta(BUDatosBoleta DatosBoleta)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServicePagos"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];
            string restParametro = "PostGenerarBoletaUnica";
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

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Token", _token);

            ValidarDatosBoleta(DatosBoleta);

            var sz = JsonConvert.SerializeObject(DatosBoleta);
            request.AddJsonBody(DatosBoleta);

            IRestResponse<BUBoletaUnica> response = client.Execute<BUBoletaUnica>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new Exception(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido generar la boleta en el servicio.");

            BUBoletaUnica ret = Newtonsoft.Json.JsonConvert.DeserializeObject<BUBoletaUnica>(response.Content);

            return ret;
        }
    }
}
