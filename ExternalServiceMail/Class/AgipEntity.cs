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
}
