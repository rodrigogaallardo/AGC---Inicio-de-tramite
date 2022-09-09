using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class EncomiendaTitularesPersonasFisicasDTO
    {
        public int IdPersonaFisica { get; set; }
        public int IdEncomienda { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int IdTipoDocPersonal { get; set; }
        public string NroDocumento { get; set; }
        public string Cuit { get; set; }
        public int IdTipoiibb { get; set; }
        public string IngresosBrutos { get; set; }
        public string Calle { get; set; }
        public int NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public int IdLocalidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string TelefonoMovil { get; set; }
        public string Sms { get; set; }
        public string Email { get; set; }
        public bool MismoFirmante { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Torre { get; set; }

        public virtual ICollection<EncomiendaFirmantesPersonasFisicasDTO> EncomiendaFirmantesPersonasFisicasDTO { get; set; }

        public virtual LocalidadDTO LocalidadDTO { get; set; }
        public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonalDTO { get; set; }
        public virtual TiposDeIngresosBrutosDTO TiposDeIngresosBrutosDTO { get; set; }

    }		
}


