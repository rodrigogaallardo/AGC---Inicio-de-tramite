using RestSharp;
using RestSharp.Deserializers;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{
    public class ExternalServiceEncuesta
    {
        public bool enviar(ws_Encuesta enc)
        {
            string implemetado = ConfigurationManager.AppSettings["Encuesta_Implementado"];
            bool enviar = Boolean.Parse(implemetado);
            if (enviar)
                return envia(enc);
            return true;
        }
        public bool envia(ws_Encuesta enc)
        {
            try
            {
                string hostParametro = ConfigurationManager.AppSettings["Encuesta_NombreParamHost"];
                string user = ConfigurationManager.AppSettings["Encuesta_NombreParamUser"];
                string pass = ConfigurationManager.AppSettings["Encuesta_NombreParamPass"];
                string sToken = ConfigurationManager.AppSettings["Encuesta_NombreParamToken"];
                string serviceParametro = ConfigurationManager.AppSettings["Encuesta_NombreParamService"];

                string _host = "";
                if (hostParametro.IndexOf("http") < 0)
                    _host = "http://" + hostParametro + serviceParametro;
                else
                    _host = hostParametro + serviceParametro;

                var client = new RestClient(_host);

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded",
                    $"user=" + user
                    + "&pass=" + pass
                    + "&sToken=" + sToken
                    + "&tTramite=" + enc.tTramite
                    + "&email=" + enc.email
                    + "&nombre=" + enc.nombre
                    + "&apellido=" + enc.apellido
                    + "&solicitud=" + enc.solicitud
                    + "&domicilio=" + enc.domicilio
                    + "&tEstablecimiento=" + enc.tEstablecimiento
                    + "&f_ingreso=" + enc.f_ingreso.ToString("dd/MM/yyyy")
                    + "&f_liberado=" + (enc.f_liberado != null ? enc.f_liberado.Value.ToString("dd/MM/yyyy") : "")
                    + "&f_fin=" + (enc.f_fin != null ? enc.f_fin.Value.ToString("dd/MM/yyyy") : "")
                    , ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception("Encuenta - Mensaje: " + response.StatusCode + " " + response.Content);
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                //Se saco para que siga la solicitud
                //throw new Exception("Encuenta - Mensaje: " + ex.Message);
            }
            return true;
        }
   
    }
    public class ws_Encuesta
    {
        public string tTramite { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string solicitud { get; set; }
        public string domicilio { get; set; }
        public string tEstablecimiento { get; set; }
        public DateTime f_ingreso { get; set; }
        public DateTime? f_liberado { get; set; }
        public DateTime? f_fin { get; set; }
        public string un_transporte { get; set; }
    }
}
