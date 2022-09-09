using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransferObject
{
    public class CallesDTO
    {
        public int id_calle { get; set; }
        public int Codigo_calle { get; set; }
        public string NombreOficial_calle { get; set; }
        public int? AlturaIzquierdaInicio_calle { get; set; }
        public int? AlturaIzquierdaFin_calle { get; set; }
        public int? AlturaDerechaInicio_calle { get; set; }
        public int? AlturaDerechaFin_calle { get; set; }
        public string NombreAnterior_calle { get; set; }
        public string TipoCalle_calle { get; set; }
        public string Observaciones_calle { get; set; }
        public int? Longitud_calle { get; set; }
        public string NombreGeografico_calle { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}

