
using System;

namespace DataTransferObject
{
	public class EncomiendaCertificadoSobrecargaDTO
    {
        public int id_sobrecarga { get; set; }
        public int id_encomienda_datoslocal { get; set; }
        public int id_tipo_certificado { get; set; }
        public int id_tipo_sobrecarga { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual EncomiendaTiposCertificadosSobrecargaDTO EncomiendaTiposCertificadosSobrecargaDTO { get; set; }
        public virtual EncomiendaTiposSobrecargasDTO EncomiendaTiposSobrecargasDTO { get; set; }

    }
}


