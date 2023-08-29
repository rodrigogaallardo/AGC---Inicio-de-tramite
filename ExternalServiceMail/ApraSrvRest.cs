using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Caching;
using System.Web.UI;
using System.Reflection;
using System.IO;
using System.Security.Policy;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections;
using ExternalService.Class.Express;
using ExternalService.Class;

namespace ExternalService
{
    public class ApraSrvRest : Page
    {
        public string Usuario;
        public string Password;
        public string UrlApraAgc;

        public ApraSrvRest()
        {
            Usuario = ConfigurationManager.AppSettings["UsuarioApraAgc"];
            Password = ConfigurationManager.AppSettings["PasswordApraAgc"];
            UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];

        }


        static HttpClient client = new HttpClient();

        public async Task<string> LoginAsync2()
        {
            var query = new Dictionary<string, string>()
            {
                ["usuario"] = "ws-ssit",
                ["password"] = "prueba123"
            };

            var json = JsonConvert.SerializeObject(query);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://clientes.grupomost.com/ws.rest.apra.agc/api/login";
            var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
        private async Task<TokenResponse> LoginAsync()
        {
            TokenResponse tokenResponse;
            var tokenResponseApplication = System.Web.HttpContext.Current.Application["TokenResponse"];
            if (tokenResponseApplication != null)
            {
                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseApplication.ToString());
                if (tokenResponse.expires.AddMinutes(-10) > DateTime.Now)
                {
                    return tokenResponse;
                }
            }

            string usuario = this.Usuario;
            string password = this.Password;
            string UrlApraAgc = this.UrlApraAgc;

            var query = new Dictionary<string, string>()
            {
                ["usuario"] = usuario,
                ["password"] = password
            };

            var json = JsonConvert.SerializeObject(query);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = UrlApraAgc + "api/Login";
            var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                System.Web.HttpContext.Current.Application["TokenResponse"] = result;
                var borrar = System.Web.HttpContext.Current.Application["TokenResponse"];
            }
            catch (Exception ex)
            {
                return null;
            }
            tokenResponseApplication = System.Web.HttpContext.Current.Application["TokenResponse"];
            tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseApplication.ToString());

            return tokenResponse;
        }
        //Post
        public async Task<string> GenerarCAAAutomatico(int IdEncomienda, string codSeguridad)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GenerarCAAAutomatico";

                SolicitudEncomienda obj = new SolicitudEncomienda()
                {
                    idEncomienda = IdEncomienda,
                    codigoSeguridad = codSeguridad
                };

                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);

                var data = JsonConvert.SerializeObject(obj);

                request.AddParameter("application/json", data, ParameterType.RequestBody);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        GenerarCAAAutomaticoResponse generarCAAAutomaticoResponse = new GenerarCAAAutomaticoResponse();
                        generarCAAAutomaticoResponse = JsonConvert.DeserializeObject<GenerarCAAAutomaticoResponse>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }

        }
      
        //Gets
        public async Task<string> GetCaa(int id_solicitud)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GetCAA?id_solicitud={id_solicitud}";


                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);
                // request.AddBody(data);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        GetCAAResponse getCAAResponse = new GetCAAResponse();
                        getCAAResponse = JsonConvert.DeserializeObject<GetCAAResponse>(content);

                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }
        }

        public async Task<string> GetBUIsCAA(int id_solicitud)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GetBUIsCAA?id_solicitud={id_solicitud}";


                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);
                // request.AddBody(data);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        List<GetBUIsCAAResponse> getBUIsCAAResponseList = new List<GetBUIsCAAResponse>();
                        getBUIsCAAResponseList = JsonConvert.DeserializeObject<List<GetBUIsCAAResponse>>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }




        }
        public async Task<string> GetBUIsPagos(List<int> id_pagoList)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GetBUIsPagos";


                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);

                var data = JsonConvert.SerializeObject(id_pagoList);

                request.AddParameter("application/json", data, ParameterType.RequestBody);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        List<GetBUIsPagosResponse> getBUIsPagosResponseList = new List<GetBUIsPagosResponse>();
                        getBUIsPagosResponseList = JsonConvert.DeserializeObject<List<GetBUIsPagosResponse>>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }

        }

        public async Task<string> GetIdPagosCAAsbyEncomiendas(List<int> IdEncomiendaList)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GetIdPagosCAAbyEncomiendas";



                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);

                var data = JsonConvert.SerializeObject(IdEncomiendaList);

                request.AddParameter("application/json", data, ParameterType.RequestBody);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        List<int> getCAAByEncomiendasResponseList = new List<int>();
                        getCAAByEncomiendasResponseList = JsonConvert.DeserializeObject<List<int>>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetIdPagosCAAsbyEncomiendas. Mensaje: {ex.Message}");
            }

        }
        public async Task<string> GetCAAsByEncomiendas(List<int> IdEncomiendaList)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/GetCAAsbyEncomiendas";



                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);

                var data = JsonConvert.SerializeObject(IdEncomiendaList);

                request.AddParameter("application/json", data, ParameterType.RequestBody);
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        List<GetCAAsByEncomiendasResponse> getCAAsByEncomiendasResponseList = new List<GetCAAsByEncomiendasResponse>();
                        getCAAsByEncomiendasResponseList = JsonConvert.DeserializeObject<List<GetCAAsByEncomiendasResponse>>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }
           
        }


        public async Task<string> ValidarCodigoSeguridad(int IdSolicitud, string codSeguridad)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/ValidarCodigoSeguridad?id_solicitud={IdSolicitud}&CodigoSeguridad={codSeguridad}";

               
                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);
              
                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        bool validarCodigoSeguridadResponse = new bool();
                        validarCodigoSeguridadResponse = JsonConvert.DeserializeObject<bool>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return $"Error generico al ejecutar  GenerarCAAAutomatico. Mensaje: {ex.Message}";
            }
        }
        public async Task<string> AsociarAnexoTecnico(int IdSolicitud, string codSeguridad, int IdEncomienda)
        {
            try
            {
                TokenResponse tokenResponse = await this.LoginAsync();
                string UrlApraAgc = ConfigurationManager.AppSettings["UrlApraAgc"];
                string apiUrl = $"{UrlApraAgc}api/CAA/AsociarAnexoTecnico?id_solicitud={IdSolicitud}&CodigoSeguridad={codSeguridad}&id_encomienda={IdEncomienda}";


                var client = new RestClient(apiUrl);
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("content-type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer " + tokenResponse.token);

                try
                {
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content;
                        bool asociarAnexoTecnicoResponse = new bool();
                        asociarAnexoTecnicoResponse = JsonConvert.DeserializeObject<bool>(content);
                        return JsonConvert.SerializeObject(content);
                    }
                    else
                        return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return $"Error generico al ejecutar  GenerarCAAAutomatico. Mensaje: {ex.Message}";
            }
            
        }



    }

    //Se crean clases para cada post por eventuales cambios en los servicios, en nombres de propiedades o lo que sea
    public class SolicitudEncomienda
    {
        public int idEncomienda { get; set; }
        public string codigoSeguridad { get; set; }
    }
    public class ValCodSeguridadCLS
    {
        public int id_solicitud { get; set; }
        public string CodigoSeguridad { get; set; }
    }
    public class AsocAnexoTecnicoCLS
    {
        public int id_solicitud { get; set; }
        public string CodigoSeguridad { get; set; }
        public int id_encomienda { get; set; }
    }
    public class GetPagosCLS
    {
        public int id_encomienda { get; set; }
    }


    public class TokenResponse
    {
        public string token { get; set; }
        public bool success { get; set; }
        public DateTime expires { get; set; }
        public string[] errors { get; set; }
    }




}