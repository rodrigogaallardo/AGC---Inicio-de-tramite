using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class ConsultaPadronSolicitudesDTO
    {
        public int IdConsultaPadron { get; set; }
        public string CodigoSeguridad { get; set; }
        public int IdTipoTramite { get; set; }
        public int IdTipoExpediente { get; set; }
        public int IdSubTipoExpediente { get; set; }
        public int IdEstado { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public string ObservacionesInternas { get; set; }
        public string ObservacionesContribuyente { get; set; }
        public string ZonaDeclarada { get; set; }
        public string NroExpedienteAnterior { get; set; }
        public string Observaciones { get; set; }
        public string NombreApellidoEscribano { get; set; }
        public int? NroMatriculaEscribano { get; set; }
        public string NroExpedienteSade { get; set; }
        public Nullable<int> idTAD { get; set; }

        public TipoTramiteDTO TipoTramite { get; set; }
        public TipoExpedienteDTO TipoExpediente { get; set; }
        public SubTipoExpedienteDTO SubTipoExpediente { get; set; }
        public ConsultaPadronEstadosDTO Estado { get; set; }

        public ICollection<ConsultaPadronNormativasDTO> Normativa { get; set; }
        public ICollection<ConsultaPadronUbicacionesDTO> Ubicaciones { get; set; }
        public ICollection<ConsultaPadronDatosLocalDTO> DatosLocal { get; set; }
        public ICollection<ConsultaPadronPlantasDTO> Plantas { get; set; }
        public ICollection<ConsultaPadronRubrosDTO> Rubros { get; set; }
        public ICollection<ConsultaPadronSolicitudesObservacionesDTO> ObservacionesDTO { get; set; }
        public ICollection<SGITramitesTareasConsultaPadronDTO> TramitesTarea { get; set; }

        public ICollection<ConsultaPadronSolicitudesObservacionesDTO> ObservacionesTareas { get; set; }
        public ICollection<ConsultaPadronDocumentosAdjuntosDTO> DocumentosAdjuntos { get; set; }

        public ICollection<ConsultaPadronDocumentosAdjuntosDTO> DocumentosAdjuntosConInformeFinTramite { get; set; }
        public ICollection<ConsultaPadronTitularesPersonasFisicasDTO> TitularesPersonasFisicas { get; set; }
        public ICollection<ConsultaPadronTitularesPersonasJuridicasDTO> TitularesPersonasJuridicas { get; set; }

        public ICollection<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> TitularesSolicitudPersonasFisicas { get; set; }
        public ICollection<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> TitularesPersonasSolicitudJuridicas { get; set; }
        public ICollection<ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> TitularesPersonasSolicitudJuridicasTitulares { get; set; }

    }
}


