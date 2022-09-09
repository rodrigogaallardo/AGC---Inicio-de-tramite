using System;

namespace DataTransferObject
{
    public class ParametrosDTO
    {
        public int IdParam { get; set; }
        public string CodParam { get; set; }
        public string NomParam { get; set; }
        public string ValorcharParam { get; set; }
        public decimal? ValornumParam { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
