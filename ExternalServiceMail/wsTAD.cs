using ExternalService.Class;
using Newtonsoft.Json;
using RestSharp;
using StaticClass;
using System;
using System.Net;
using System.Text;

namespace ExternalService
{
    public class wsTAD
    {
        private class clsCrearTramiteTAD
        {
            public string tipoTramite { get; set; }
            public string cuit { get; set; }
            public string ubicacion { get; set; }
            public string sistemaExterno { get; set; }
            public string idSolicitud { get; set; }
        }
        private class clsActualizarTramiteTAD
        {
            public string idSolicitud { get; set; }
            public string numeroExpediente { get; set; }
            public string codTipoTramite { get; set; }
            public string ubicacion { get; set; }
        }

        private class Response
        {
            public string Resultado { get; set; }
            public PersonaTadEntity PersonaTAD { get; set; }
            public string Codigo { get; set; }
            public string Error { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
        }

        public static int crearTramiteTAD(string _urlESB, String cuit, string codTrata, string domicilio, string sistemaExterno, int idSolicitud)
        {
            int idTad = 0;
            string uriString = _urlESB + "/tiposTramite/" + codTrata + "/tramites";
            //ignorar validacion de https en ambiente de prueba
            if (Funciones.isDesarrollo())
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            RestClient clientrest = new RestClient();
            clientrest.BaseUrl = new Uri(uriString);
            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            var clsBody = new clsCrearTramiteTAD();
            clsBody.cuit = cuit;
            clsBody.ubicacion = "-";
            clsBody.sistemaExterno = sistemaExterno;
            clsBody.idSolicitud = idSolicitud.ToString();

            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);

            IRestResponse response = clientrest.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var objResult = JsonConvert.DeserializeObject<dynamic>(response.Content);
                idTad = Convert.ToInt32(objResult.idTramite);
            }
            else
            {
                clsError error = null;
                try
                {
                    error = JsonConvert.DeserializeObject<clsError>(response.Content);
                }
                catch (Exception)
                {
                    throw new Exception("500 - Error inesperado en el ESB.");
                }
                if (error.codigo == null && error.error == null)
                    throw new Exception("500 - Error inesperado en el ESB.");

                throw new Exception((error.codigo != null ? error.codigo.Value + " - " : "") + error.error);
            }

            return idTad;
        }

        public static void actualizarTramite(string _urlESB, int idTramite, int idSolicitud, string numeroExpediente, string tipoTramite, string ubicacion)
        {
            string uriString = _urlESB + "/tramites/" + idTramite;
            //ignorar validacion de https en ambiente de prueba
            if (Funciones.isDesarrollo())
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            RestClient clientrest = new RestClient();
            clientrest.BaseUrl = new Uri(uriString);
            RestRequest request = new RestRequest();
            request.Method = Method.PUT;

            var clsBody = new clsActualizarTramiteTAD();
            clsBody.codTipoTramite = tipoTramite;
            clsBody.ubicacion = ubicacion;
            clsBody.numeroExpediente = string.IsNullOrEmpty(numeroExpediente) ? string.Empty : numeroExpediente;
            clsBody.idSolicitud = idSolicitud.ToString();


            //request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);

            IRestResponse response = clientrest.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var objResult = JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            else
            {
                clsError error = null;
                LogError.Write(new Exception(response.StatusCode + "\n" + response.StatusDescription + "\n" +
                    response.Content + "\n" + response.Headers + "\n" + response.Request + "\n" +
                    response.ResponseUri + "\n" + response.ErrorException + "\n" + response.StatusDescription
                    + "\n" + response.ErrorMessage));
                try
                {
                    error = JsonConvert.DeserializeObject<clsError>(response.Content);
                }
                catch (Exception)
                {
                    throw new Exception("500 - Error inesperado en el ESB. Error al desserializar la respuesta.");
                }
                if (error.codigo == null && error.error == null)
                    throw new Exception("500 - Error inesperado en el ESB. Sin código de error.");

                throw new Exception((error.codigo != null ? error.codigo.Value + " - " : "") + error.error);
            }
        }

        public static PersonaTadEntity GetPersonaTAD(string url, string cuit)
        {
            RestClient clientrest = new RestClient
            {
                BaseUrl = new Uri(url)
            };

            clientrest.Encoding = Encoding.GetEncoding("ISO-8859-1");

            RestRequest request = new RestRequest
            {
                Method = Method.GET,
            };

            request.AddUrlSegment("cuit", cuit);

            IRestResponse restResponse = clientrest.Execute(request);

            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

            if (restResponse.RawBytes != null)
                restResponse.Content = encoding.GetString(restResponse.RawBytes);

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(restResponse.Content);

                if (response.Resultado != null && response.Resultado.Equals("200"))
                {
                    return response.PersonaTAD;
                }
                else if (response.Codigo != null)
                {
                    throw new ArgumentException(response.Error);
                }
                else
                {
                    throw new ArgumentException("Error inesperado en el servicio.");
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new Exception("Error inesperado en el sistema.");
            }

        }
    }
}