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
using System.IO;
using System.Web.Hosting;
using StaticClass;

namespace ExternalService
{
    public class ExternalServiceFiles
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
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceFile"];
            string autenticateHostParametro = ConfigurationManager.AppSettings["NombreParamHostAutorizacion"];

            //string host = "http://www.dghpsh.agcontrol.gob.ar/test/ws.rest.files";
            string _host ="";
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
                throw new Exception(string.Format("Error al adquirir el Token con el usuario  {0}.", user));
           return response.Headers.Where(p => p.Name == "Token").First().Value.ToString();
        }

        public int addFile(string name, byte[] file)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceFile"];
            string fileParametro = ConfigurationManager.AppSettings["NombreParamHostFile"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];

            string _token = GetToken(userParametro, passParametro);

            
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;
            
            var client = new RestClient(_host + fileParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest("?fileName=" + name, Method.POST);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);
            request.AddFile("name", file, name);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido agregar el file en el servicio ");
            return Convert.ToInt32(response.Content);
        }

        public string deleteFile(int id_file)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceFile"];
            string fileParametro = "/api/Files";
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];

            string _token = GetToken(userParametro, passParametro);

            //string host = "http://www.dghpsh.agcontrol.gob.ar/test/ws.rest.files";
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + fileParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest("?IdFile=" + id_file);
            if (Funciones.isDesarrollo())
                request.Method = Method.POST;
            else
                request.Method = Method.DELETE;
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido eliminar el file en el servicio ");
            return response.Content;
        }

        public byte[] downloadFile(int id_file, out string fileExtension)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceFile"];
            string fileParametro = ConfigurationManager.AppSettings["NombreParamHostFile"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUser"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];

            string _token = GetToken(userParametro, passParametro);

            //string host = "http://www.dghpsh.agcontrol.gob.ar/test/ws.rest.files";
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + fileParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest("?IdFile=" + id_file, Method.GET);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);


            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido descargar el file en el servicio ");

            fileExtension = response.Headers.First(p => p.Name.Equals("Content-Disposition")).Value.ToString().Replace("attachment; filename=", "");
            //fileExtension = Path.GetExtension(fileExtension);

            return response.RawBytes;
        }

        public byte[] downloadFileByidCertificado(int id_certificado)
        {
            string hostParametro = ConfigurationManager.AppSettings["NombreParamHost"];
            string serviceParametro = ConfigurationManager.AppSettings["NombreParamServiceFile"];
            string fileParametro = ConfigurationManager.AppSettings["NombreParamHostFileCE"];
            string userParametro = ConfigurationManager.AppSettings["NombreParamUserCE"];
            string passParametro = ConfigurationManager.AppSettings["NombreParamPass"];

            

            string _token = GetToken(userParametro, passParametro);
            
            string _host = "";
            if (hostParametro.IndexOf("http") < 0)
                _host = "http://" + hostParametro + serviceParametro;
            else
                _host = hostParametro + serviceParametro;

            var client = new RestClient(_host + fileParametro);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest("?IdFile=" + id_certificado, Method.GET);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Token", _token);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("No se ha podido descargar el archivo en el servicio ");
            return response.RawBytes;
        }
    }
}
