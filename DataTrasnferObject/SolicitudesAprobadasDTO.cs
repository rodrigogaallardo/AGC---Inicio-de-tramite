using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SolicitudesAprobadasDTO
    {
        public int IdSolicitud { get; set; }
        public int IdEncomienda { get; set; }
        public Nullable<int> IdSolicitudOrigen { get; set; }
        public string CodigoSeguridad { get; set; }
        public int IdTipoTramite { get; set; }
        public int IdTipoExpediente { get; set; }
        public int IdSubTipoExpediente { get; set; }
        public int IdEstado { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public string NroExpediente { get; set; }
        public string NroExpedienteSade { get; set; }
        public string TipoTramiteDescripcion { get; set; }
        public string TipoExpedienteDescripcion { get; set; }
        public string SubTipoExpedienteDescripcion { get; set; }
        public decimal? SuperficieTotal { get; set; }
        public bool Servidumbre_paso { get; set; }
        public string Titulares { get; set; }
        
        public virtual SSITSolicitudesOrigenDTO SSITSolicitudesOrigenDTO { get; set; }
        public virtual ICollection<EncomiendaDTO> EncomiendaDTO { get; set; }
        public virtual ICollection<SSITSolicitudesUbicacionesDTO> SSITSolicitudesUbicacionesDTO { get; set; }
        public virtual ICollection<ConsultaPadronUbicacionesDTO> ConsultaPadronUbicacionesDTO { get; set; }
        public virtual TipoEstadoSolicitudDTO TipoEstadoSolicitudDTO { get; set; }
    }
}

