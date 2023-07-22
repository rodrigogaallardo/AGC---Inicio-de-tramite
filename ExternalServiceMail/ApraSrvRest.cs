using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExternalService
{
    public class ApraSrvRest
    {
        static HttpClient client = new HttpClient();

        public async Task<string> LoginAsync()
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

        //Post
        public async Task<string> GenerarCAAAutomatico(int IdEncomienda, string codSeguridad) 
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                SolicitudEncomienda obj = new SolicitudEncomienda()
                {
                    IdEncomienda = IdEncomienda,
                    CodigoSeguridad = codSeguridad
                };
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GenerarCAAAutomatico";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    HttpResponseMessage response = await httpClient.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        return $"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Error generico al ejecutar  GenerarCAAAutomatico. Mensaje: {ex.Message}";
            }
        }
        public async Task<string> ValidarCodigoSeguridad(int IdSolicitud, string codSeguridad)
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                ValCodSeguridadCLS obj = new ValCodSeguridadCLS()
                {
                    id_solicitud = IdSolicitud,
                    CodigoSeguridad = codSeguridad
                };
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/ValidarCodigoSeguridad";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    HttpResponseMessage response = await httpClient.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        return $"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}";
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
                Task<string> accessToken = this.LoginAsync();
                AsocAnexoTecnicoCLS obj = new AsocAnexoTecnicoCLS()
                {
                    id_solicitud = IdSolicitud,
                    CodigoSeguridad = codSeguridad,
                    id_encomienda = IdEncomienda
                };
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/AsociarAnexoTecnico";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    HttpResponseMessage response = await httpClient.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        return $"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Error generico al ejecutar  GenerarCAAAutomatico. Mensaje: {ex.Message}";
            }
        }
        public async Task<string> GetIdPagosCAAsbyEncomiendas(int IdEncomienda)
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                GetPagosCLS obj = new GetPagosCLS()
                {
                    id_encomienda = IdEncomienda
                };
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GetIdPagosCAAsbyEncomiendas";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    HttpResponseMessage response = await httpClient.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        return $"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Error generico al ejecutar  GenerarCAAAutomatico. Mensaje: {ex.Message}";
            }
        }
        
        //Gets
        public async Task<string> GetCaa(int id_solicitud)
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                string apiUrl = $"https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GetCAA?{id_solicitud}";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                            return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().ToString());
                        else
                            return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                    }
                    catch (HttpRequestException ex)
                    {
                        return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                    }
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
                Task<string> accessToken = this.LoginAsync();
                string apiUrl = $"https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GetBUIsCAA?{id_solicitud}";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                            return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().ToString());
                        else
                            return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                    }
                    catch (HttpRequestException ex)
                    {
                        return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }
        }
        public async Task<string> GetBUIsPagos(int id_pago)
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                string apiUrl = $"https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GetBUIsPagos?{id_pago}";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                            return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().ToString());
                        else
                            return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                    }
                    catch (HttpRequestException ex)
                    {
                        return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }
        }
        public async Task<string> GetCAAsByEncomiendas(int IdEncomienda)
        {
            try
            {
                Task<string> accessToken = this.LoginAsync();
                string apiUrl = $"https://clientes.grupomost.com/ws.rest.apra.agc/api/CAA/GetCAAsByEncomiendas?{IdEncomienda}";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                            return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().ToString());
                        else
                            return ($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                    }
                    catch (HttpRequestException ex)
                    {
                        return ($"Error al realizar la solicitud HTTP: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                return ($"Error generico al ejecutar  GetCaa. Mensaje: {ex.Message}");
            }
        }
    }

    //Se crean clases para cada post por eventuales cambios en los servicios, en nombres de propiedades o lo que sea
    public class SolicitudEncomienda
    {
        public int IdEncomienda { get; set; }
        public string CodigoSeguridad { get; set; }
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
}