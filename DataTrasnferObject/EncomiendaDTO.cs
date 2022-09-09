using System;
using System.Collections.Generic;
using System.Linq;
namespace DataTransferObject
{

    public class EncomiendaAntenasDTO : EncomiendaDTO
    {
        public int IdTipoCertificado { get; set; }
    }
    public class EncomiendaDTO
	{
        //Solo se usa para el rubro de Espacio Cultural Independiente
        public static string CodRubroECI = "2.1.1";
        
        public int IdEncomienda { get; set; }
		public DateTime FechaEncomienda { get; set; }
		public int NroEncomiendaConsejo { get; set; }
		public int IdConsejo { get; set; }
		public int IdProfesional { get; set; }
		public string ZonaDeclarada { get; set; }
		public int IdTipoTramite { get; set; }
		public int IdTipoExpediente { get; set; }
		public int IdSubTipoExpediente { get; set; }       
		public int IdEstado { get; set; }
		public string CodigoSeguridad { get; set; }
		public string ObservacionesPlantas { get; set; }
		public string ObservacionesRubros { get; set; }
        public string ObservacionesRubrosATAnterior { get; set; }
        public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
        public string TipoTramiteDescripcion { get; set; }
        public string Domicilio { get; set; }
        public bool ProTeatro { get; set; }
        public int IdSolicitud { get; set; }
        public string tipoAnexo { get; set; }
        public bool Servidumbre_paso { get; set; }
        public bool DeclaraOficinaComercial { get; set; }
        public Nullable<bool> Consecutiva_Supera_10 { get; set; }
        public Nullable<bool> Contiene_galeria_paseo { get; set; }
        public bool CumpleArticulo521 { get; set; }
        public bool Asistentes350 { get; set; }     
        public bool InformaModificacion { get; set; }
        public string DetalleModificacion { get; set; }

        //Solo se usan siempre que se haya declarado el rubro 2.1.1 (Espacio Cultural Independiente) 
        public Nullable<bool> EsActBaile { get; set; }
        public Nullable<bool> EsLuminaria { get; set; }
        public bool ProductosInflamables { get; set; }
        public bool EsECI
        {
            get
            {
                bool r = false;
                if (EncomiendaRubrosCNDTO!=null)
                    r = EncomiendaRubrosCNDTO.Any(x=>x.CodigoRubro== CodRubroECI) ;
                return r;
            }
        }

        public virtual EncomiendaEstadosDTO Estado { get; set; }
        public virtual TipoTramiteDTO TipoTramite { get; set; }
        public virtual TipoExpedienteDTO TipoExpediente { get; set; }
        public virtual SubTipoExpedienteDTO SubTipoExpediente { get; set; }

        public ItemDirectionDTO Direccion { get; set; }
        public virtual ICollection<EncomiendaTitularesPersonasJuridicasDTO> EncomiendaTitularesPersonasJuridicasDTO { get; set; }
        public virtual ICollection<EncomiendaTitularesPersonasFisicasDTO> EncomiendaTitularesPersonasFisicasDTO { get; set; }

        public virtual ProfesionalDTO ProfesionalDTO { get; set; }
        public virtual ICollection<EncomiendaRubrosDTO> EncomiendaRubrosDTO { get; set; }
        public virtual ICollection<EncomiendaRubrosCNDTO> EncomiendaRubrosCNDTO { get; set; }
        public virtual ICollection<EncomiendaConformacionLocalDTO> EncomiendaConformacionLocalDTO { get; set; }
        public virtual ICollection<EncomiendaDatosLocalDTO> EncomiendaDatosLocalDTO { get; set; }
        public virtual ICollection<EncomiendaDocumentosAdjuntosDTO> EncomiendaDocumentosAdjuntosDTO { get; set; }
        public virtual ICollection<EncomiendaPlanosDTO> EncomiendaPlanosDTO { get; set; }
        public virtual ICollection<EncomiendaPlantasDTO> EncomiendaPlantasDTO { get; set; }
        public virtual ICollection<EncomiendaRectificatoriaDTO> EncomiendaRectificatoriaDTO { get; set; }
        //public virtual SSITSolicitudesDTO SSITSolicitudesDTO { get; set; }
        public virtual ICollection<EncomiendaUbicacionesDTO> EncomiendaUbicacionesDTO { get; set; }
        public virtual ICollection<EncomiendaNormativasDTO> EncomiendaNormativasDTO { get; set; }
        public virtual EntidadNormativaDTO EntidadNormativaDTO { get; set; }
        //Descripcion acumulada del tipo y subtipo de expendiente
        public string TipoExpedienteDescripcion { get; set; }
        public virtual ICollection<wsEscribanosActaNotarialDTO> ActasNotariales { get; set; }
        public virtual ICollection<EncomiendaSSITSolicitudesDTO> EncomiendaSSITSolicitudesDTO { get; set; }
        public virtual ICollection<EncomiendaTransfSolicitudesDTO> EncomiendaTransfSolicitudesDTO { get; set; }
        public virtual ICollection<Encomienda_RubrosCN_DepositoDTO> Encomienda_RubrosCN_DepositoDTO { get; set; }
        
    }
}


