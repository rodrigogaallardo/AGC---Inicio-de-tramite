using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class EncomiendaTitularesPersonasJuridicasDTO
    {
        public int IdPersonaJuridica { get; set; }
        public int IdEncomienda { get; set; }
        public int IdTipoSociedad { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public int IdTipoIb { get; set; }
        public string NroIb { get; set; }
        public string Calle { get; set; }
        public int? NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public int IdLocalidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Torre { get; set; }

        public ICollection<FirmantesSHDTO> firmantesSH { get; set; }
        public ICollection<TitularesSHDTO> titularesSH { get; set; }

        public virtual ICollection<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO { get; set; }
        public ICollection<EncomiendaFirmantesPersonasJuridicasDTO> EncomiendaFirmantesPersonasJuridicasDTO { get; set; }
        public virtual LocalidadDTO LocalidadDTO { get; set; }
        public virtual TiposDeIngresosBrutosDTO TiposDeIngresosBrutosDTO { get; set; }
        public virtual TipoSociedadDTO TipoSociedadDTO { get; set; }

    }
}


