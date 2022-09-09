
namespace DataTransferObject
{
	public class EncomiendaRelTiposDestinosTiposUsosDTO
    {
        public int id_rel_tipodestino_tipouso { get; set; }
        public int id_tipo_destino { get; set; }
        public int id_tipo_uso { get; set; }
        public decimal valor_min_req { get; set; }
        public bool requiere_detalle { get; set; }
        public string texto_fijo_detalle { get; set; }
    }
}


