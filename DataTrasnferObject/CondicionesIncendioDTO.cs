using System.Collections.Generic;

namespace DataTransferObject
{
    public class CondicionesIncendioDTO
    {
        public int idCondicionIncendio { get; set; }
        public string codigo { get; set; }
        public decimal? superficie { get; set; }
        public decimal? superficieSubsuelo { get; set; }
    }
}
