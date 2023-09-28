using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.Class
{
    public class AgipEntity
    {
    }

    public class CuitsRelacionadosDTO
    {
        public long cuitAValidar { get; set; }
        public bool cuitAValidarSpecified { get; set; }
        public long cuitRepresentado { get; set; }
        public bool cuitRepresentadoSpecified { get; set; }
        public string token { get; set; }
        public string sign { get; set; }
        public string servicioNombre { get; set; }
    }

    public class Result
    {
        public bool msg { get; set; }
    }
    public class CuitsRelacionadosPOST
    {
        public int statusCode { get; set; }
        public Result result { get; set; }
        public long time { get; set; }
        public string status { get; set; }
        public string url { get; set; }
    }

    public class TokenResponseAGIPRest
    {
        public int code { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }

    public class CuitsRelacionadosPOSTRest
    {
        public int statusCode { get; set; }
        public bool? success { get; set; }
        public string message { get; set; }
    }

    public class CuitsRelacionadosDTO_REST
    {
        public long cuitAValidar { get; set; }
        public long cuitRepresentado { get; set; }
    }

    public class CuitsRepresentadosPOSTRest
    {
        public int statusCode { get; set; }
        public bool? success { get; set; }
        public List<Representado> representados { get; set; }
        public string message { get; set; }
    }

    public class Representado
    {
        public long cuit { get; set; }
        public string nombre { get; set; }
        public int? isib { get; set; }
        public int? cat { get; set; }
        public string calle { get; set; }
        public int? puerta { get; set; }
        public string piso { get; set; }
        public string dpto { get; set; }
        public string codpostal { get; set; }
        public string localidad { get; set; }
        public string provincia { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string tipo_representacion { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }


    }
}
