
namespace DataTransferObject
{
	public class EncomiendaSobrecargaDetalle1DTO
    {
        public int id_sobrecarga_detalle1 { get; set; }
        public int id_sobrecarga { get; set; }
        public int id_tipo_destino { get; set; }
        public int id_tipo_uso { get; set; }
        public decimal valor { get; set; }
        public string detalle { get; set; }
        public int id_encomiendatiposector { get; set; }
        public string losa_sobre { get; set; }
    }
}


