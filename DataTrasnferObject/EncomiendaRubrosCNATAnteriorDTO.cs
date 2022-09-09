using System;

namespace DataTransferObject
{
    public class EncomiendaRubrosCNATAnteriorDTO
    {
        public int id_encomiendarubro { get; set; }
        public int id_encomienda { get; set; }
        public int IdRubro { get; set; }
        public string CodigoRubro { get; set; }
        public string NombreRubro { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdTipoExpediente { get; set; }
        public decimal SuperficieHabilitar { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? CreateUser { get; set; }
        public int? idImpactoAmbiental { get; set; }
    }
}
