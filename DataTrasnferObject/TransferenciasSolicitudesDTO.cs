using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TransferenciasSolicitudesDTO
	{
		public int IdSolicitud { get; set; }
		public int IdConsultaPadron { get; set; }
		public int IdTipoTramite { get; set; }
		public int IdTipoExpediente { get; set; }
		public int IdSubTipoExpediente { get; set; }
		public int IdEstado { get; set; }
        public int? idTipoTransmision { get; set; }
        public string NumeroExpedienteSade { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
		public string CodigoSeguridad { get; set; }
        public Nullable<int> idTAD { get; set; }
        public Nullable<int> idSolicitudRef { get; set; }
        public ICollection<TransferenciasDocumentosAdjuntosDTO> Documentos { get; set; }
        public TipoTramiteDTO TipoTramite { get; set; }
        public TipoExpedienteDTO TipoExpediente { get; set; }
        public SubTipoExpedienteDTO SubTipoExpediente { get; set; }
        public TipoEstadoSolicitudDTO Estado { get; set; }
        public TipodeTransmisionDTO TipoTransmision { get; set; }
        public virtual ICollection<TransferenciasTitularesPersonasFisicasDTO> TitularesPersonasFisicas { get; set; }
        public virtual ICollection<TransferenciasTitularesPersonasJuridicasDTO> TitularesPersonasJuridicas { get; set; }
        public virtual ICollection<TransferenciasFirmantesPersonasFisicasDTO> FirmantesPersonasFisicas { get; set; }
        public virtual ICollection<TransferenciasFirmantesPersonasJuridicasDTO> FirmantesPersonasJuridicas { get; set; }
        public ICollection<TransferenciasTitularesSolicitudPersonasJuridicasDTO> TitularesPersonasSolicitudJuridicas { get; set; }
        public ICollection<TransferenciasTitularesSolicitudPersonasFisicasDTO> TitularesPersonasSolicitudFisicas { get; set; }
        public ICollection<TransferenciaUbicacionesDTO> Ubicaciones { get; set; }
        public ICollection<TransferenciasPlantasDTO> Plantas { get; set; }
        public ICollection<TransferenciasSolicitudesObservacionesDTO> Observaciones { get; set; }
        public ItemDirectionDTO Direccion { get; set; }
        public virtual ICollection<EncomiendaTransfSolicitudesDTO> EncomiendaTransfSolicitudesDTO { get; set; }
    }				
}


