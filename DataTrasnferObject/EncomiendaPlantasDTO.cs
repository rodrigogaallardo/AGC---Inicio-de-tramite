
namespace DataTransferObject
{
    //public class EncomiendaPlantasDTO
    //{
    //    public int id_encomiendatiposector { get; set; }
    //    public int id_encomienda { get; set; }
    //    public int id_tiposector { get; set; }
    //    public string detalle_encomiendatiposector { get; set; }
    //}
    public class EncomiendaPlantasDTO
    {
        public int IdTipoSector { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public bool MuestraCampoAdicional { get; set; }
        public string Detalle {get;set;}
        public int TamanoCampoAdicional { get; set; }
        public bool? Ocultar {get;set;}
        public int id_encomiendatiposector { get; set; }
        public int id_encomienda { get; set; }
        public string detalle_encomiendatiposector { get; set; }
        public virtual TipoSectorDTO TipoSectorDTO { get; set; }
    }
    public class ConsultaPadronPlantasModelDTO
    {
        public int? IdConsultaPadronTipoSector {get;set;}
        public int IdTipoSector { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public bool MuestraCampoAdicional { get; set; }
        public string Detalle { get; set; }
        public int TamanoCampoAdicional { get; set; }
        public bool? Ocultar { get; set; }
    }
    public class TransferenciasPlantasModelDTO
    {
        public int? IdTransferenciaTipoSector { get; set; }
        public int IdTipoSector { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public bool MuestraCampoAdicional { get; set; }
        public string Detalle { get; set; }
        public int TamanoCampoAdicional { get; set; }
        public bool? Ocultar { get; set; }
    }
}


