using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class SSITSolicitudesDTO
    {
        public int IdSolicitud { get; set; }
        public string CodigoSeguridad { get; set; }
        public int IdTipoTramite { get; set; }
        public int IdTipoExpediente { get; set; }
        public int IdSubTipoExpediente { get; set; }
        public int? MatriculaEscribano { get; set; }
        public string NroExpediente { get; set; }
        public int IdEstado { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public string NroExpedienteSade { get; set; }
        public string NroExpedienteSadeRelacionado { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaLibrado { get; set; }
        public string TipoTramiteDescripcion { get; set; }
        public string TipoExpedienteDescripcion { get; set; }
        public string SubTipoExpedienteDescripcion { get; set; }
        public bool Servidumbre_paso { get; set; }
        public string CodArea { get; set; }
        public string Prefijo { get; set; }
        public string Sufijo { get; set; }
        public string NroDisposicionSADE { get; set; }
        public Nullable<System.DateTime> FechaDisposicion { get; set; }
        public bool ExencionPago { get; set; }
        public string NroExpedienteCAA { get; set; }
        public string NroEspecialSADE { get; set; }

        public Nullable<int> idTAD { get; set; }
        public bool EsECI { get; set; }
        //public virtual ICollection<EncomiendaDTO> EncomiendaDTO { get; set; }
        public virtual ICollection<EncomiendaSSITSolicitudesDTO> EncomiendaSSITSolicitudesDTO { get; set; }
        public virtual SSITSolicitudesOrigenDTO SSITSolicitudesOrigenDTO { get; set; }
        public virtual ICollection<SSITSolicitudesTitularesPersonasJuridicasDTO> TitularesPersonasJuridicas { get; set; }
        public virtual ICollection<SSITSolicitudesTitularesPersonasFisicasDTO> TitularesPersonasFisicas { get; set; }
        public virtual ICollection<SSITSolicitudesFirmantesPersonasJuridicasDTO> FirmantesPersonasJuridicas { get; set; }
        public virtual ICollection<SSITSolicitudesFirmantesPersonasFisicasDTO> FirmantesPersonasFisicas { get; set; }
        //public virtual ICollection<SSITSolicitudesHistorialEstadosDTO> SSITSolicitudesHistorialEstadosDTO { get; set; }
        public virtual ICollection<SSITSolicitudesUbicacionesDTO> SSITSolicitudesUbicacionesDTO { get; set; }
        public virtual ICollection<SSITDocumentosAdjuntosDTO> SSITDocumentosAdjuntosDTO { get; set; }
        public virtual ICollection<SSITSolicitudesRubrosCNDTO> SSITSolicitudesRubrosCNDTO { get; set; }

        //public virtual ICollection<SSITSolicitudesPagosDTO> SSITSolicitudesPagosDTO { get; set; }
        public virtual TipoEstadoSolicitudDTO TipoEstadoSolicitudDTO { get; set; }
        public virtual SSITSolicitudesDatosLocalDTO SSITSolicitudesDatosLocalDTO { get; set; }
        public virtual SSITPermisosDatosAdicionalesDTO SSITPermisosDatosAdicionalesDTO { get; set; }



    }

}


