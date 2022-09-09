using ExternalService.Class;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Configuration;
using System.Linq;
using System.Net;

namespace ExternalService
{
    public class ExternalServiceMail
    {
        protected string _hostUri;
        protected string _autenticateNameHost;
        protected string _serviceNameHost;
        protected string _userName;
        protected string _password;

        public ExternalServiceMail(ParametrosService param)
        {
            _hostUri = param.hostUri;
            _autenticateNameHost = param.autenticateNameHost;
            _serviceNameHost = param.serviceNameHost;
            _userName = param.userName;
            _password = param.password;
        }

        /// <summary>
        /// get token 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public Guid GetToken(string user, string pass)
        {
            string _host = "";
            if (_hostUri.IndexOf("http") < 0)
                _host = "http://" + _hostUri;
            else
                _host = _hostUri;
            var client = new RestClient(_hostUri + _serviceNameHost + _autenticateNameHost);

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            client.Authenticator = new HttpBasicAuthenticator(user, pass);
            var request = new RestRequest(Method.POST);
            request.AddParameter("redirect", "false");
            request.AddParameter("redirectUrl", "");
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Guid token;
            if (response.StatusCode == HttpStatusCode.OK)
                token = Guid.Parse(Convert.ToString(response.Headers.Where(p => p.Name == "Token").First().Value));
            else
                throw new Exception(string.Format("Error al adquirir el Token con el usuario  {0}.", user));

            return token;
        }

        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="mailEntity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int SendMail2(EmailEntity mailEntity)
        {
            string hostParametroMail = "/Emails";
            var _token = GetToken(_userName, _password);

            int idMail = 0;
            RestClient client = new RestClient();

            client.BaseUrl = new Uri(_hostUri + _serviceNameHost + hostParametroMail);

            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Token", _token.ToString());
            request.AddJsonBody(mailEntity);
            var sz = JsonConvert.SerializeObject(mailEntity);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                int.TryParse(response.Content, out idMail);

            return idMail;
        }


        public int SendMail(EmailServicePOST datosEmail)
        {
            string hostParametroMail = "/api/Email";
            var _token = GetToken(_userName, _password);

            int idMail = 0;
            RestClient client = new RestClient();

            client.BaseUrl = new Uri(_hostUri + _serviceNameHost + hostParametroMail);

            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Token", _token.ToString());
            request.AddJsonBody(datosEmail);
            var sz = JsonConvert.SerializeObject(datosEmail);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                int.TryParse(response.Content, out idMail);

            return idMail;
        }
    }
}
