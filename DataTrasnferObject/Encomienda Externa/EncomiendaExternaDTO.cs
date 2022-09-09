using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaExternaTitularesDTO
    {
        public int id_encomienda { get; set; }
        public string ApellidoNomRazon { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int id_tipodoc_personal { get; set; }
        public string Nro_Documento { get; set; }
        public string Cuit { get; set; }
        public string Calle { get; set; }
        public Nullable<int> NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Email { get; set; }
        public string Torre { get; set; }
        public string TipoPersona { get; set; }
    }

    public class EncomiendaExternaDTO : EncomiendaDTO
    {
        public bool Bloqueada { get; set; }
        public int nroTramite { get; set; }
        public string MotivoRechazo { get; set; }
        public string NroDGROC { get; set; }
        public int? id_file { get; set; }
        public AspnetUserDTO Usuario { get; set; }
        public ConsejoProfesionalDTO Consejo { get; set; }
        public ICollection<EncomiendaExternaTitularesPersonasFisicasDTO> EncomiendaExternaTitularesPersonasFisicas { get; set; }
        public ICollection<EncomiendaExternaTitularesPersonasJuridicasDTO> EncomiendaExternaTitularesPersonasJuridicas { get; set; }
        public ICollection<EncomiendaExternaTitularesPersonasJuridicasPersonasFisicasDTO> EncomiendaExternaTitularesPersonasJuridicasPersonasFisicas { get; set; }

        public ICollection<EncomiendaExternaUbicacionesDTO> EncomiendaExternaUbicaciones { get; set; }
        public ICollection<EncomiendaExternaTitularesDTO> Titulares { get;set; }
    }
}
