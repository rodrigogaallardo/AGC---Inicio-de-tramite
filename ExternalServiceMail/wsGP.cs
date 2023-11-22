using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{
    public class wsGP
    {

        public static List<PerfilDTO> perfilesPorTrata(string _urlESB, string p_trata)
        {
            //ignorar validacion de https en ambiente de prueba
            if (Funciones.isDesarrollo())
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string uriString = _urlESB + "/tiposTramite/" + p_trata + "/perfiles";
            var client = new RestClient(uriString);
            //ignorar validacion de https en ambiente de prueba
            if(Funciones.isDesarrollo())
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json charset=UTF-8");
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
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

            List<PerfilDTO> list = Newtonsoft.Json.JsonConvert.DeserializeObject< List<PerfilDTO>>(response.Content);
            return list;
        }

        public static List<clsParticipantes> GetParticipantesxTramite(string _urlESB, int id_tad)
        {
            List<clsParticipantes> result = new List<clsParticipantes>();
            //ignorar validacion de https en ambiente de prueba
            if (Funciones.isDesarrollo())
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string uriString = _urlESB + "/tramites/" + id_tad.ToString() + "/participaciones";
            var clientrest = new RestClient(uriString);

            RestRequest request = new RestRequest();
            request.Method = Method.GET;

            IRestResponse response = clientrest.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<clsParticipantes>>(response.Content);
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

                if (error.codigo != 6)
                    throw new Exception((error.codigo != null ? error.codigo.Value + " - " : "") + error.error);
            }
            return result;
        }

        public static void nuevoTramiteParticipante(string _urlESB, string p_codigoTrata, int p_idTad, string p_nroExp, string p_Cuit, int p_idPerfil,
            bool p_IntervinienteTAD, string p_Sistema, string p_nombreParticipante, string p_apellidoParticipante,
            string p_razonSocialParticipante)
        {
            string uriString = _urlESB + "/tramites/" + p_idTad + "/participaciones?sinGPPrevio=false";
            var client = new RestClient(uriString);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());


            var request = new RestRequest(Method.POST);
            var clsBody = new clsNuevoTramiteParticipante();
            clsBody.cuitParticipante = p_Cuit;
            clsBody.idPerfil = p_idPerfil;
            clsBody.codTipoTramite = p_codigoTrata;
            clsBody.idSistema = p_Sistema;
            clsBody.nroExpediente = p_nroExp;
            clsBody.intervinienteTad = p_IntervinienteTAD;
            clsBody.nombre = p_nombreParticipante;
            clsBody.apellido = p_apellidoParticipante;
            clsBody.razonSocial = p_razonSocialParticipante;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
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
        }

        public static void DesvincularParticipante(string _urlESB, int p_idTad, string p_cuitOperador, int p_idPerfilOperador, string p_Sistema, 
            string p_cuitParticipante, int p_idPerfilParticipante)
        {
            string uriString = _urlESB + "/tramites/" + p_idTad + "/participaciones?cuitParticipante=" + p_cuitParticipante +
                "&idPerfilParticipante=" + p_idPerfilParticipante + "&funcionario=true";
            var client = new RestClient(uriString);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            LogError.Write(new Exception("CLIENT: " + Funciones.GetDataFromClient(client)));


            var request = new RestRequest(Method.DELETE);
            var clsBody = new clsDesvincularParticipante();
            clsBody.sistema = p_Sistema;
            clsBody.operador = new clsOperador();
            clsBody.operador.cuit = p_cuitOperador;
            clsBody.operador.idPerfil = p_idPerfilOperador;
            //request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);
            LogError.Write(new Exception("REQUEST: " + Funciones.GetDataFromRequest(request)));

            IRestResponse response = client.Execute(request);
            LogError.Write(new Exception("RESPONSE: " + Funciones.GetDataFromResponse(response)));
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
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
        }

        public static void VincularParticipante(string _urlESB, int p_idTad, string p_cuitOperador, int p_idPerfilOperador, string p_Sistema,
            string p_cuitParticipante, int p_idPerfilParticipante, string p_nombreParticipante, string p_apellidoParticipante,
            string p_razonSocialParticipante)
        {
            string uriString = _urlESB + "/tramites/" + p_idTad + "/participaciones?sinGPPrevio=false&funcionario=true";
            var client = new RestClient(uriString);
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());


            var request = new RestRequest(Method.POST);
            var clsBody = new clsVincularParticipante();
            clsBody.cuitParticipante = p_cuitParticipante;
            clsBody.idPerfil = p_idPerfilParticipante;
            clsBody.idSistema = p_Sistema;
            clsBody.nombre = p_nombreParticipante;
            clsBody.apellido = p_apellidoParticipante;
            clsBody.razonSocial = p_razonSocialParticipante;
            clsBody.operador = new clsOperador();
            clsBody.operador.cuit = p_cuitOperador;
            clsBody.operador.idPerfil = p_idPerfilOperador;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(clsBody), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
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
        }
    }

    public class PerfilDTO
    {
        public int idPerfil { get; set; }
        public string nombrePerfil { get; set; }
        public bool perfilObligatorio { get; set; }
    }
    public class clsParticipantes
    {
        public int idTAD { get; set; }
        public string cuit { get; set; }
        public int idPerfil { get; set; }
        public bool? vigenciaParticipante { get; set; }
        public bool? accesoGP { get; set; }
        public bool? vistaDetallada { get; set; }
    }
    public class clsNuevoTramiteParticipante
    {
        public string cuitParticipante { get; set; }
        public int idPerfil { get; set; }
        public string codTipoTramite { get; set; }
        public string idSistema { get; set; }
        public string nroExpediente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string razonSocial { get; set; }
        public bool intervinienteTad { get; set; }
    }
    public class clsDesvincularParticipante
    {
        public string sistema { get; set; }
        public clsOperador operador { get; set; }
    }
    public class clsOperador
    {
        public string cuit { get; set; }
        public int idPerfil { get; set; }
    }
    public class clsVincularParticipante
    {
        public string cuitParticipante { get; set; }
        public int idPerfil { get; set; }
        public string idSistema { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string razonSocial { get; set; }
        public clsOperador operador { get; set; }
    }
}
