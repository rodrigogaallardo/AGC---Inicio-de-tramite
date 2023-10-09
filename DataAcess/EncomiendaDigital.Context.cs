
namespace DataAcess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    
    using System.Linq;
    
    public partial class EncomiendadigitalEntityes : DbContext
    {
        public EncomiendadigitalEntityes()
            : base("name=EncomiendadigitalEntityes")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<Barrios> Barrios { get; set; }
        public DbSet<Comunas> Comunas { get; set; }
        public DbSet<ConsejoProfesional> ConsejoProfesional { get; set; }
        public DbSet<Encomienda_Estados> Encomienda_Estados { get; set; }
        public DbSet<Encomienda_HistorialEstados> Encomienda_HistorialEstados { get; set; }
        public DbSet<Encomienda_Rel_TiposDestinos_TiposUsos> Encomienda_Rel_TiposDestinos_TiposUsos { get; set; }
        public DbSet<Encomienda_Rel_TiposSobrecargas_TiposDestinos> Encomienda_Rel_TiposSobrecargas_TiposDestinos { get; set; }
        public DbSet<Encomienda_Sobrecargas> Encomienda_Sobrecargas { get; set; }
        public DbSet<Encomienda_Tipos_Certificados_Sobrecarga> Encomienda_Tipos_Certificados_Sobrecarga { get; set; }
        public DbSet<Encomienda_Tipos_Destinos> Encomienda_Tipos_Destinos { get; set; }
        public DbSet<Encomienda_Tipos_Sobrecargas> Encomienda_Tipos_Sobrecargas { get; set; }
        public DbSet<Encomienda_Tipos_Usos> Encomienda_Tipos_Usos { get; set; }
        public DbSet<Encomienda_TransicionEstados> Encomienda_TransicionEstados { get; set; }
        public DbSet<Encomienda_ZonasActualizadas> Encomienda_ZonasActualizadas { get; set; }
        public DbSet<EntidadNormativa> EntidadNormativa { get; set; }
        public DbSet<Escribano> Escribano { get; set; }
        public DbSet<GrupoConsejos> GrupoConsejos { get; set; }
        public DbSet<ImpactoAmbiental> ImpactoAmbiental { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PersonasInhibidas> PersonasInhibidas { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Rel_Encomienda_Rectificatoria> Rel_Encomienda_Rectificatoria { get; set; }
        public DbSet<Rel_Rubros_ImpactoAmbiental> Rel_Rubros_ImpactoAmbiental { get; set; }
        public DbSet<Rel_TipoExpediente_SubtipoExpediente> Rel_TipoExpediente_SubtipoExpediente { get; set; }
        public DbSet<Rel_TipoTramite_TipoExpediente> Rel_TipoTramite_TipoExpediente { get; set; }
        public DbSet<Rel_TipoTramite_TiposDeDocumentosRequeridos> Rel_TipoTramite_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<Rel_ZonasPlaneamiento_ZonasHabilitaciones> Rel_ZonasPlaneamiento_ZonasHabilitaciones { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos> Rubros_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<RubrosCondiciones> RubrosCondiciones { get; set; }
        public DbSet<RubrosZonasCondiciones> RubrosZonasCondiciones { get; set; }
        public DbSet<SubtipoExpediente> SubtipoExpediente { get; set; }
        public DbSet<SubTiposDeUbicacion> SubTiposDeUbicacion { get; set; }
        public DbSet<Tipo_Documentacion_Req> Tipo_Documentacion_Req { get; set; }
        public DbSet<tipo_iluminacion> tipo_iluminacion { get; set; }
        public DbSet<tipo_ventilacion> tipo_ventilacion { get; set; }
        public DbSet<TipoDestino> TipoDestino { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<TipoEstadoSolicitud> TipoEstadoSolicitud { get; set; }
        public DbSet<TipoExpediente> TipoExpediente { get; set; }
        public DbSet<TipoInhibicion> TipoInhibicion { get; set; }
        public DbSet<TiposDeCaracterLegal> TiposDeCaracterLegal { get; set; }
        public DbSet<TiposDeInformes> TiposDeInformes { get; set; }
        public DbSet<TiposDeIngresosBrutos> TiposDeIngresosBrutos { get; set; }
        public DbSet<TiposDePlanos> TiposDePlanos { get; set; }
        public DbSet<TiposDeUbicacion> TiposDeUbicacion { get; set; }
        public DbSet<TipoSector> TipoSector { get; set; }
        public DbSet<TipoSociedad> TipoSociedad { get; set; }
        public DbSet<TipoSuperficie> TipoSuperficie { get; set; }
        public DbSet<TipoVerificacion> TipoVerificacion { get; set; }
        public DbSet<Ubicaciones_Clausuras> Ubicaciones_Clausuras { get; set; }
        public DbSet<Ubicaciones_Inhibiciones> Ubicaciones_Inhibiciones { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal> Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<Ubicaciones_Puertas> Ubicaciones_Puertas { get; set; }
        public DbSet<Ubicaciones_ZonasComplementarias> Ubicaciones_ZonasComplementarias { get; set; }
        public DbSet<Zonas_Habilitaciones> Zonas_Habilitaciones { get; set; }
        public DbSet<Zonas_Planeamiento> Zonas_Planeamiento { get; set; }
        public DbSet<InicioTramite_Menues> InicioTramite_Menues { get; set; }
        public DbSet<InicioTramite_Perfiles> InicioTramite_Perfiles { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<CPadron_ConformacionLocal> CPadron_ConformacionLocal { get; set; }
        public DbSet<CPadron_Estados> CPadron_Estados { get; set; }
        public DbSet<CPadron_HistorialEstados> CPadron_HistorialEstados { get; set; }
        public DbSet<CPadron_Planos> CPadron_Planos { get; set; }
        public DbSet<CPadron_Solicitudes_Observaciones> CPadron_Solicitudes_Observaciones { get; set; }
        public DbSet<Rubros_Config_Incendio> Rubros_Config_Incendio { get; set; }
        public DbSet<ENG_Config_BandejaAsignacion> ENG_Config_BandejaAsignacion { get; set; }
        public DbSet<ENG_EquipoDeTrabajo> ENG_EquipoDeTrabajo { get; set; }
        public DbSet<ENG_GruposDeTareas> ENG_GruposDeTareas { get; set; }
        public DbSet<ENG_Rel_GruposDeTareas_Tareas> ENG_Rel_GruposDeTareas_Tareas { get; set; }
        public DbSet<ENG_Rel_Perfiles_Tareas> ENG_Rel_Perfiles_Tareas { get; set; }
        public DbSet<ENG_Rel_Resultados_Tareas> ENG_Rel_Resultados_Tareas { get; set; }
        public DbSet<ENG_Rel_Resultados_Tareas_Transiciones> ENG_Rel_Resultados_Tareas_Transiciones { get; set; }
        public DbSet<ENG_Resultados> ENG_Resultados { get; set; }
        public DbSet<ENG_Tareas> ENG_Tareas { get; set; }
        public DbSet<ENG_Transiciones> ENG_Transiciones { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<vis_Certificados> vis_Certificados { get; set; }
        public DbSet<Transf_Solicitudes_HistorialEstados> Transf_Solicitudes_HistorialEstados { get; set; }
        public DbSet<wsEscribanos_PersonasFisicas> wsEscribanos_PersonasFisicas { get; set; }
        public DbSet<wsEscribanos_PersonasJuridicas> wsEscribanos_PersonasJuridicas { get; set; }
        public DbSet<wsEscribanos_DerechoOcupacion> wsEscribanos_DerechoOcupacion { get; set; }
        public DbSet<wsEscribanos_InstrumentoJudicial> wsEscribanos_InstrumentoJudicial { get; set; }
        public DbSet<wsEscribanos_InstrumentoPrivado> wsEscribanos_InstrumentoPrivado { get; set; }
        public DbSet<wsEscribanos_InstrumentoPublico> wsEscribanos_InstrumentoPublico { get; set; }
        public DbSet<wsEscribanos_PersonasFisicas_Representantes> wsEscribanos_PersonasFisicas_Representantes { get; set; }
        public DbSet<wsEscribanos_PersonasJuridicas_Representantes> wsEscribanos_PersonasJuridicas_Representantes { get; set; }
        public DbSet<SGI_Tramites_Tareas> SGI_Tramites_Tareas { get; set; }
        public DbSet<SGI_Tramites_Tareas_CPADRON> SGI_Tramites_Tareas_CPADRON { get; set; }
        public DbSet<SGI_Tramites_Tareas_HAB> SGI_Tramites_Tareas_HAB { get; set; }
        public DbSet<SGI_Tramites_Tareas_TRANSF> SGI_Tramites_Tareas_TRANSF { get; set; }
        public DbSet<SGI_Perfiles> SGI_Perfiles { get; set; }
        public DbSet<SGI_Profiles> SGI_Profiles { get; set; }
        public DbSet<CAA_TiposDeCertificados> CAA_TiposDeCertificados { get; set; }
        public DbSet<Conceptos_BUI_Independientes> Conceptos_BUI_Independientes { get; set; }
        public DbSet<SGI_Tarea_Calificar_ObsGrupo> SGI_Tarea_Calificar_ObsGrupo { get; set; }
        public DbSet<ConsejoProfesional_RolesPermitidos> ConsejoProfesional_RolesPermitidos { get; set; }
        public DbSet<GrupoConsejos_Roles_Clasificacion> GrupoConsejos_Roles_Clasificacion { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Clausuras> Ubicaciones_PropiedadHorizontal_Clausuras { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Inhibiciones> Ubicaciones_PropiedadHorizontal_Inhibiciones { get; set; }
        public DbSet<EncomiendaExt_Empresas> EncomiendaExt_Empresas { get; set; }
        public DbSet<EncomiendaExt_Estados> EncomiendaExt_Estados { get; set; }
        public DbSet<EncomiendaExt_HistorialEstados> EncomiendaExt_HistorialEstados { get; set; }
        public DbSet<EncomiendaExt_Titulares_PersonasFisicas> EncomiendaExt_Titulares_PersonasFisicas { get; set; }
        public DbSet<EncomiendaExt_Titulares_PersonasJuridicas> EncomiendaExt_Titulares_PersonasJuridicas { get; set; }
        public DbSet<EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas> EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<EncomiendaExt_TransicionEstados> EncomiendaExt_TransicionEstados { get; set; }
        public DbSet<EncomiendaExt_Ubicaciones> EncomiendaExt_Ubicaciones { get; set; }
        public DbSet<EncomiendaExt_Ubicaciones_PropiedadHorizontal> EncomiendaExt_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<EncomiendaExt_Ubicaciones_Puertas> EncomiendaExt_Ubicaciones_Puertas { get; set; }
        public DbSet<Instructivos> Instructivos { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<Rel_TiposDeDocumentosRequeridos_TipoNormativa> Rel_TiposDeDocumentosRequeridos_TipoNormativa { get; set; }
        public DbSet<TipoNormativa> TipoNormativa { get; set; }
        public DbSet<Profesional_Perfiles_Inhibiciones> Profesional_Perfiles_Inhibiciones { get; set; }
        public DbSet<CPadron_DocumentosAdjuntos> CPadron_DocumentosAdjuntos { get; set; }
        public DbSet<CPadron_Normativas> CPadron_Normativas { get; set; }
        public DbSet<CPadron_Plantas> CPadron_Plantas { get; set; }
        public DbSet<CPadron_Rubros> CPadron_Rubros { get; set; }
        public DbSet<Encomienda_ConformacionLocal> Encomienda_ConformacionLocal { get; set; }
        public DbSet<Encomienda_DocumentosAdjuntos> Encomienda_DocumentosAdjuntos { get; set; }
        public DbSet<Encomienda_Firmantes_PersonasJuridicas> Encomienda_Firmantes_PersonasJuridicas { get; set; }
        public DbSet<Encomienda_Planos> Encomienda_Planos { get; set; }
        public DbSet<Encomienda_Plantas> Encomienda_Plantas { get; set; }
        public DbSet<Encomienda_Rectificatoria> Encomienda_Rectificatoria { get; set; }
        public DbSet<Encomienda_Rubros> Encomienda_Rubros { get; set; }
        public DbSet<Encomienda_Sobrecarga_Detalle1> Encomienda_Sobrecarga_Detalle1 { get; set; }
        public DbSet<Encomienda_Sobrecarga_Detalle2> Encomienda_Sobrecarga_Detalle2 { get; set; }
        public DbSet<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> Encomienda_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<Encomienda_Ubicaciones_PropiedadHorizontal> Encomienda_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<Encomienda_Ubicaciones_Puertas> Encomienda_Ubicaciones_Puertas { get; set; }
        public DbSet<SSIT_DocumentosAdjuntos> SSIT_DocumentosAdjuntos { get; set; }
        public DbSet<SSIT_Solicitudes_AvisoCaducidad> SSIT_Solicitudes_AvisoCaducidad { get; set; }
        public DbSet<SSIT_Solicitudes_Encomienda> SSIT_Solicitudes_Encomienda { get; set; }
        public DbSet<SSIT_Solicitudes_HistorialEstados> SSIT_Solicitudes_HistorialEstados { get; set; }
        public DbSet<SSIT_Solicitudes_HistorialUsuarios> SSIT_Solicitudes_HistorialUsuarios { get; set; }
        public DbSet<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal> SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<SSIT_Solicitudes_Ubicaciones_Puertas> SSIT_Solicitudes_Ubicaciones_Puertas { get; set; }
        public DbSet<Transf_DocumentosAdjuntos> Transf_DocumentosAdjuntos { get; set; }
        public DbSet<Transf_Firmantes_PersonasFisicas> Transf_Firmantes_PersonasFisicas { get; set; }
        public DbSet<Transf_Firmantes_PersonasJuridicas> Transf_Firmantes_PersonasJuridicas { get; set; }
        public DbSet<Transf_Titulares_PersonasFisicas> Transf_Titulares_PersonasFisicas { get; set; }
        public DbSet<Transf_Titulares_PersonasJuridicas> Transf_Titulares_PersonasJuridicas { get; set; }
        public DbSet<Transf_Titulares_PersonasJuridicas_PersonasFisicas> Transf_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<CPadron_Ubicaciones_Puertas> CPadron_Ubicaciones_Puertas { get; set; }
        public DbSet<Encomienda_Firmantes_PersonasFisicas> Encomienda_Firmantes_PersonasFisicas { get; set; }
        public DbSet<ANT_Encomiendas_Estados> ANT_Encomiendas_Estados { get; set; }
        public DbSet<ANT_Encomiendas_HistorialEstados> ANT_Encomiendas_HistorialEstados { get; set; }
        public DbSet<ANT_Encomiendas> ANT_Encomiendas { get; set; }
        public DbSet<APRA_TiposDeTramite> APRA_TiposDeTramite { get; set; }
        public DbSet<ANT_DocumentosAdjuntos> ANT_DocumentosAdjuntos { get; set; }
        public DbSet<ANT_Empresas> ANT_Empresas { get; set; }
        public DbSet<ANT_Empresas_PersonasFisicas> ANT_Empresas_PersonasFisicas { get; set; }
        public DbSet<ANT_Empresas_PersonasJuridicas> ANT_Empresas_PersonasJuridicas { get; set; }
        public DbSet<ANT_Empresas_PersonasJuridicas_Firmantes> ANT_Empresas_PersonasJuridicas_Firmantes { get; set; }
        public DbSet<ANT_Encomiendas_Titulares> ANT_Encomiendas_Titulares { get; set; }
        public DbSet<ANT_Rel_encreg_encrni> ANT_Rel_encreg_encrni { get; set; }
        public DbSet<ANT_Ubicaciones> ANT_Ubicaciones { get; set; }
        public DbSet<ANT_Ubicaciones_Coordenadas> ANT_Ubicaciones_Coordenadas { get; set; }
        public DbSet<ANT_Ubicaciones_PropiedadHorizontal> ANT_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<ANT_Ubicaciones_Puertas> ANT_Ubicaciones_Puertas { get; set; }
        public DbSet<ANT_Ubicaciones_Ubicacion> ANT_Ubicaciones_Ubicacion { get; set; }
        public DbSet<ANT_Ubicaciones_ViaPublica> ANT_Ubicaciones_ViaPublica { get; set; }
        public DbSet<APRA_TiposdeCertificados> APRA_TiposdeCertificados { get; set; }
        public DbSet<APRA_TiposDeDocumentosSistema> APRA_TiposDeDocumentosSistema { get; set; }
        public DbSet<SSIT_Solicitudes_Observaciones> SSIT_Solicitudes_Observaciones { get; set; }
        public DbSet<Transf_Solicitudes_Observaciones> Transf_Solicitudes_Observaciones { get; set; }
        public DbSet<Encomienda_Titulares_PersonasFisicas> Encomienda_Titulares_PersonasFisicas { get; set; }
        public DbSet<Encomienda_Titulares_PersonasJuridicas> Encomienda_Titulares_PersonasJuridicas { get; set; }
        public DbSet<SSIT_Solicitudes_Titulares_PersonasFisicas> SSIT_Solicitudes_Titulares_PersonasFisicas { get; set; }
        public DbSet<SSIT_Solicitudes_Titulares_PersonasJuridicas> SSIT_Solicitudes_Titulares_PersonasJuridicas { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasFisicas> CPadron_Titulares_Solicitud_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasJuridicas> CPadron_Titulares_Solicitud_PersonasJuridicas { get; set; }
        public DbSet<SSIT_Solicitudes_Ubicaciones> SSIT_Solicitudes_Ubicaciones { get; set; }
        public DbSet<CPadron_Ubicaciones> CPadron_Ubicaciones { get; set; }
        public DbSet<CPadron_Ubicaciones_PropiedadHorizontal> CPadron_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<CPadron_Titulares_PersonasFisicas> CPadron_Titulares_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_PersonasJuridicas> CPadron_Titulares_PersonasJuridicas { get; set; }
        public DbSet<wsPagos_BoletaUnica> wsPagos_BoletaUnica { get; set; }
        public DbSet<wsPagos_BoletaUnica_Estados> wsPagos_BoletaUnica_Estados { get; set; }
        public DbSet<wsEscribanos_ActaNotarial> wsEscribanos_ActaNotarial { get; set; }
        public DbSet<Encomienda_URLxROL> Encomienda_URLxROL { get; set; }
        public DbSet<TipoTramite> TipoTramite { get; set; }
        public DbSet<wsPagos> wsPagos { get; set; }
        public DbSet<SGI_Solicitudes_Pagos> SGI_Solicitudes_Pagos { get; set; }
        public DbSet<SSIT_Solicitudes_Pagos> SSIT_Solicitudes_Pagos { get; set; }
        public DbSet<SGI_Tarea_Calificar> SGI_Tarea_Calificar { get; set; }
        public DbSet<SGI_Tarea_Revision_DGHP> SGI_Tarea_Revision_DGHP { get; set; }
        public DbSet<SGI_Tarea_Revision_Gerente> SGI_Tarea_Revision_Gerente { get; set; }
        public DbSet<SGI_Tarea_Revision_SubGerente> SGI_Tarea_Revision_SubGerente { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_PersonasJuridicas_PersonasFisicas> CPadron_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<CAA_DocumentosAdjuntos> CAA_DocumentosAdjuntos { get; set; }
        public DbSet<CAA_Estados> CAA_Estados { get; set; }
        public DbSet<SGI_Tarea_Documentos_Adjuntos> SGI_Tarea_Documentos_Adjuntos { get; set; }
        public DbSet<Ley962_TiposDeDocumentosRequeridos> Ley962_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<Rubros_CircuitoAtomatico_Zonas> Rubros_CircuitoAtomatico_Zonas { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos_Zonas> Rubros_TiposDeDocumentosRequeridos_Zonas { get; set; }
        public DbSet<Rubros_InformacionRelevante> Rubros_InformacionRelevante { get; set; }
        public DbSet<TipoDocumentoPersonal> TipoDocumentoPersonal { get; set; }
        public DbSet<SGI_Tarea_Calificar_ObsDocs> SGI_Tarea_Calificar_ObsDocs { get; set; }
        public DbSet<TiposDeDocumentosRequeridos> TiposDeDocumentosRequeridos { get; set; }
        public DbSet<TiposDeDocumentosSistema> TiposDeDocumentosSistema { get; set; }
        public DbSet<ENG_Circuitos> ENG_Circuitos { get; set; }
        public DbSet<Encomienda_Certificado_Sobrecarga> Encomienda_Certificado_Sobrecarga { get; set; }
        public DbSet<Encomienda_Rubros_AT_Anterior> Encomienda_Rubros_AT_Anterior { get; set; }
        public DbSet<SSIT_Solicitudes_Origen> SSIT_Solicitudes_Origen { get; set; }
        public DbSet<ENG_Rel_Circuitos_TiposDeTramite> ENG_Rel_Circuitos_TiposDeTramite { get; set; }
        public DbSet<Rubros> Rubros { get; set; }
        public DbSet<ENG_Grupos_Circuitos> ENG_Grupos_Circuitos { get; set; }
        public DbSet<Rel_UsuariosProf_Roles_Clasificacion> Rel_UsuariosProf_Roles_Clasificacion { get; set; }
        public DbSet<Email_Estados> Email_Estados { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Emails_Origenes> Emails_Origenes { get; set; }
        public DbSet<Emails_Tipos> Emails_Tipos { get; set; }
        public DbSet<Eximicion_CAA> Eximicion_CAA { get; set; }
        public DbSet<Ubicaciones_DireccionesConformadas> Ubicaciones_DireccionesConformadas { get; set; }
        public DbSet<SSIT_Solicitudes_Nuevas> SSIT_Solicitudes_Nuevas { get; set; }
        public DbSet<CPadron_DatosLocal> CPadron_DatosLocal { get; set; }
        public DbSet<SSIT_Solicitudes_Notificaciones> SSIT_Solicitudes_Notificaciones { get; set; }
        public DbSet<TipoActividad> TipoActividad { get; set; }
        public DbSet<CPadron_Solicitudes> CPadron_Solicitudes { get; set; }
        public DbSet<SSIT_Solicitudes_Notificaciones_motivos> SSIT_Solicitudes_Notificaciones_motivos { get; set; }
        public DbSet<Rel_Rubros_Solicitudes_Nuevas> Rel_Rubros_Solicitudes_Nuevas { get; set; }
        public DbSet<SSIT_Solicitudes_Firmantes_PersonasJuridicas> SSIT_Solicitudes_Firmantes_PersonasJuridicas { get; set; }
        public DbSet<SSIT_Solicitudes_Firmantes_PersonasFisicas> SSIT_Solicitudes_Firmantes_PersonasFisicas { get; set; }
        public DbSet<Ubicaciones_ZonasMixtura> Ubicaciones_ZonasMixtura { get; set; }
        public DbSet<Ubicaciones_GruposDistritos> Ubicaciones_GruposDistritos { get; set; }
        public DbSet<Encomienda_Ubicaciones_Mixturas> Encomienda_Ubicaciones_Mixturas { get; set; }
        public DbSet<SSIT_Solicitudes_Ubicaciones_Mixturas> SSIT_Solicitudes_Ubicaciones_Mixturas { get; set; }
        public DbSet<ENG_Grupos_Circuitos_Tipo_Tramite> ENG_Grupos_Circuitos_Tipo_Tramite { get; set; }
        public DbSet<Calles> Calles { get; set; }
        public DbSet<RubrosImpactoAmbientalCN> RubrosImpactoAmbientalCN { get; set; }
        public DbSet<Encomienda_RubrosCN> Encomienda_RubrosCN { get; set; }
        public DbSet<Encomienda_RubrosCN_Subrubros> Encomienda_RubrosCN_Subrubros { get; set; }
        public DbSet<Rel_Usuarios_GrupoConsejo> Rel_Usuarios_GrupoConsejo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Encomienda_Ubicaciones> Encomienda_Ubicaciones { get; set; }
        public DbSet<CAA_Solicitudes> CAA_Solicitudes { get; set; }
        public DbSet<Ubicaciones_CatalogoDistritos> Ubicaciones_CatalogoDistritos { get; set; }
        public DbSet<RubrosCN_Subrubros> RubrosCN_Subrubros { get; set; }
        public DbSet<TiposdeTransmision> TiposdeTransmision { get; set; }
        public DbSet<Encomienda_RubrosCN_AT_Anterior> Encomienda_RubrosCN_AT_Anterior { get; set; }
        public DbSet<Transf_ConformacionLocal> Transf_ConformacionLocal { get; set; }
        public DbSet<Transf_DatosLocal> Transf_DatosLocal { get; set; }
        public DbSet<Transf_Normativas> Transf_Normativas { get; set; }
        public DbSet<Transf_Plantas> Transf_Plantas { get; set; }
        public DbSet<Transf_Rubros> Transf_Rubros { get; set; }
        public DbSet<Transf_Titulares_Solicitud_PersonasJuridicas> Transf_Titulares_Solicitud_PersonasJuridicas { get; set; }
        public DbSet<Transf_Ubicaciones> Transf_Ubicaciones { get; set; }
        public DbSet<Transf_Ubicaciones_Puertas> Transf_Ubicaciones_Puertas { get; set; }
        public DbSet<Transf_Firmantes_Solicitud_PersonasFisicas> Transf_Firmantes_Solicitud_PersonasFisicas { get; set; }
        public DbSet<Transf_Firmantes_Solicitud_PersonasJuridicas> Transf_Firmantes_Solicitud_PersonasJuridicas { get; set; }
        public DbSet<Transf_Titulares_Solicitud_PersonasFisicas> Transf_Titulares_Solicitud_PersonasFisicas { get; set; }
        public DbSet<Transf_Ubicaciones_PropiedadHorizontal> Transf_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<Transf_Solicitudes_Pagos> Transf_Solicitudes_Pagos { get; set; }
        public DbSet<Transf_Solicitudes> Transf_Solicitudes { get; set; }
        public DbSet<Transf_Ubicaciones_Mixturas> Transf_Ubicaciones_Mixturas { get; set; }
        public DbSet<Encomienda_SSIT_Solicitudes> Encomienda_SSIT_Solicitudes { get; set; }
        public DbSet<Encomienda_Transf_Solicitudes> Encomienda_Transf_Solicitudes { get; set; }
        public DbSet<SSIT_Solicitudes> SSIT_Solicitudes { get; set; }
        public DbSet<Transf_Ubicaciones_Distritos> Transf_Ubicaciones_Distritos { get; set; }
        public DbSet<Ubicaciones> Ubicaciones { get; set; }
        public DbSet<SGI_SADE_Procesos> SGI_SADE_Procesos { get; set; }
        public DbSet<Solicitud_planoVisado> Solicitud_planoVisado { get; set; }
        public DbSet<EncomiendaExt> EncomiendaExt { get; set; }
        public DbSet<Ubicaciones_CatalogoDistritos_Subzonas> Ubicaciones_CatalogoDistritos_Subzonas { get; set; }
        public DbSet<Ubicaciones_CatalogoDistritos_Zonas> Ubicaciones_CatalogoDistritos_Zonas { get; set; }
        public DbSet<Encomienda> Encomienda { get; set; }
        public DbSet<Encomienda_DatosLocal> Encomienda_DatosLocal { get; set; }
        public DbSet<Encomienda_Ubicaciones_Distritos> Encomienda_Ubicaciones_Distritos { get; set; }
        public DbSet<SSIT_Solicitudes_Ubicaciones_Distritos> SSIT_Solicitudes_Ubicaciones_Distritos { get; set; }
        public DbSet<AppConsole_ErrorLog> AppConsole_ErrorLog { get; set; }
        public DbSet<AreasHospitalarias> AreasHospitalarias { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<AuditoriaDeTablas> AuditoriaDeTablas { get; set; }
        public DbSet<AVH_visitas> AVH_visitas { get; set; }
        public DbSet<CAA_Rel_CAA_DocAdjuntos> CAA_Rel_CAA_DocAdjuntos { get; set; }
        public DbSet<Calles_Eliminadas> Calles_Eliminadas { get; set; }
        public DbSet<Calles_Excepcion> Calles_Excepcion { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Clanae> Clanae { get; set; }
        public DbSet<codigo_urbanistico> codigo_urbanistico { get; set; }
        public DbSet<Comisarias> Comisarias { get; set; }
        public DbSet<ConformacionLocal> ConformacionLocal { get; set; }
        public DbSet<CPadron_Solicitudes_AvisoCaducidad> CPadron_Solicitudes_AvisoCaducidad { get; set; }
        public DbSet<Cpadron_Solicitudes_Baja> Cpadron_Solicitudes_Baja { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Cursos_Excepciones_Usuarios> Cursos_Excepciones_Usuarios { get; set; }
        public DbSet<Cursos_Fechas> Cursos_Fechas { get; set; }
        public DbSet<Cursos_Inscriptos> Cursos_Inscriptos { get; set; }
        public DbSet<DatosBuscados> DatosBuscados { get; set; }
        public DbSet<Disposiciones_Texto> Disposiciones_Texto { get; set; }
        public DbSet<DistritosEscolares> DistritosEscolares { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public DbSet<Emails_Perfil_Config> Emails_Perfil_Config { get; set; }
        public DbSet<Emails_Perfil_Relacion_Config> Emails_Perfil_Relacion_Config { get; set; }
        public DbSet<Emails_Templates> Emails_Templates { get; set; }
        public DbSet<ENG_Tipos_Tareas> ENG_Tipos_Tareas { get; set; }
        public DbSet<Envio_Mail> Envio_Mail { get; set; }
        public DbSet<Envio_Mail_Estados> Envio_Mail_Estados { get; set; }
        public DbSet<Envio_Mail_Prioridades> Envio_Mail_Prioridades { get; set; }
        public DbSet<Envio_Mail_Proceso> Envio_Mail_Proceso { get; set; }
        public DbSet<Estadisticas> Estadisticas { get; set; }
        public DbSet<Estadisticas_Hoja1> Estadisticas_Hoja1 { get; set; }
        public DbSet<Estadisticas_Hoja2> Estadisticas_Hoja2 { get; set; }
        public DbSet<Estadisticas_Hoja3> Estadisticas_Hoja3 { get; set; }
        public DbSet<Estadisticas_Hoja6> Estadisticas_Hoja6 { get; set; }
        public DbSet<Estadisticas_Hoja7> Estadisticas_Hoja7 { get; set; }
        public DbSet<Estadisticas_Hoja7_2> Estadisticas_Hoja7_2 { get; set; }
        public DbSet<Estadisticas_Hoja8> Estadisticas_Hoja8 { get; set; }
        public DbSet<Estadisticas_Hoja8_2> Estadisticas_Hoja8_2 { get; set; }
        public DbSet<Estadisticas_Hoja9> Estadisticas_Hoja9 { get; set; }
        public DbSet<Fichas> Fichas { get; set; }
        public DbSet<Ley257_Estados> Ley257_Estados { get; set; }
        public DbSet<Ley257_Solicitudes> Ley257_Solicitudes { get; set; }
        public DbSet<Ley257_Solicitudes_HistorialEstados> Ley257_Solicitudes_HistorialEstados { get; set; }
        public DbSet<Ley257_Solicitudes_Pagos> Ley257_Solicitudes_Pagos { get; set; }
        public DbSet<MasBuscados> MasBuscados { get; set; }
        public DbSet<Niveles_Agrupamiento> Niveles_Agrupamiento { get; set; }
        public DbSet<Parametros_Bandeja_Rubro> Parametros_Bandeja_Rubro { get; set; }
        public DbSet<Parametros_Bandeja_Superficie> Parametros_Bandeja_Superficie { get; set; }
        public DbSet<Parametros_Observaciones> Parametros_Observaciones { get; set; }
        public DbSet<RegionesSanitarias> RegionesSanitarias { get; set; }
        public DbSet<Rel_TiposDeDocumentosRequeridos_ENG_Tareas> Rel_TiposDeDocumentosRequeridos_ENG_Tareas { get; set; }
        public DbSet<Rubros_CircuitoAtomatico_Zonas_Historial_Cambios> Rubros_CircuitoAtomatico_Zonas_Historial_Cambios { get; set; }
        public DbSet<Rubros_Config_Incendio_Historial_Cambios> Rubros_Config_Incendio_Historial_Cambios { get; set; }
        public DbSet<Rubros_Config_Incendio_TiposDeDocumentosRequeridos> Rubros_Config_Incendio_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<Rubros_Config_Incendio_TiposDeDocumentosRequeridos_Historial_Cambios> Rubros_Config_Incendio_TiposDeDocumentosRequeridos_Historial_Cambios { get; set; }
        public DbSet<Rubros_Historial_Cambios> Rubros_Historial_Cambios { get; set; }
        public DbSet<Rubros_Historial_Cambios_Estados> Rubros_Historial_Cambios_Estados { get; set; }
        public DbSet<Rubros_Historial_Cambios_UsuariosIntervinientes> Rubros_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Rubros_InformacionRelevante_Historial_Cambios> Rubros_InformacionRelevante_Historial_Cambios { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos_Historial_Cambios> Rubros_TiposDeDocumentosRequeridos_Historial_Cambios { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos_Zonas_Historial_Cambios> Rubros_TiposDeDocumentosRequeridos_Zonas_Historial_Cambios { get; set; }
        public DbSet<RubrosBicicletas> RubrosBicicletas { get; set; }
        public DbSet<RubrosCargasyDescargas> RubrosCargasyDescargas { get; set; }
        public DbSet<RubrosCN_Config_Incendio> RubrosCN_Config_Incendio { get; set; }
        public DbSet<RubrosCN_InformacionRelevante> RubrosCN_InformacionRelevante { get; set; }
        public DbSet<RubrosCN_TiposDeDocumentosRequeridos> RubrosCN_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<RubrosCondicionesCN> RubrosCondicionesCN { get; set; }
        public DbSet<RubrosEstacionamientos> RubrosEstacionamientos { get; set; }
        public DbSet<RubrosFormulasCN> RubrosFormulasCN { get; set; }
        public DbSet<RubrosZonasCondiciones_Historial_Cambios> RubrosZonasCondiciones_Historial_Cambios { get; set; }
        public DbSet<RULH_Log> RULH_Log { get; set; }
        public DbSet<SADE_Estados_Expedientes> SADE_Estados_Expedientes { get; set; }
        public DbSet<Sectores_SADE> Sectores_SADE { get; set; }
        public DbSet<SGI_FiltrosBusqueda> SGI_FiltrosBusqueda { get; set; }
        public DbSet<SGI_LIZA_Procesos> SGI_LIZA_Procesos { get; set; }
        public DbSet<SGI_LIZA_Ticket> SGI_LIZA_Ticket { get; set; }
        public DbSet<SGI_Menues> SGI_Menues { get; set; }
        public DbSet<SGI_Procesos_EE> SGI_Procesos_EE { get; set; }
        public DbSet<SGI_Tarea_Aprobado> SGI_Tarea_Aprobado { get; set; }
        public DbSet<SGI_Tarea_Asignar_Calificador> SGI_Tarea_Asignar_Calificador { get; set; }
        public DbSet<SGI_Tarea_Asignar_Inspector> SGI_Tarea_Asignar_Inspector { get; set; }
        public DbSet<SGI_Tarea_Calificacion_Tecnica_Legal> SGI_Tarea_Calificacion_Tecnica_Legal { get; set; }
        public DbSet<SGI_Tarea_Carga_Tramite> SGI_Tarea_Carga_Tramite { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Asignar_Profesional> SGI_Tarea_Dictamen_Asignar_Profesional { get; set; }
        public DbSet<SGI_Tarea_Dictamen_GEDO> SGI_Tarea_Dictamen_GEDO { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Realizar_Dictamen> SGI_Tarea_Dictamen_Realizar_Dictamen { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Revisar_Tramite> SGI_Tarea_Dictamen_Revisar_Tramite { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Revision> SGI_Tarea_Dictamen_Revision { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Revision_Gerente> SGI_Tarea_Dictamen_Revision_Gerente { get; set; }
        public DbSet<SGI_Tarea_Dictamen_Revision_SubGerente> SGI_Tarea_Dictamen_Revision_SubGerente { get; set; }
        public DbSet<SGI_Tarea_Ejecutiva> SGI_Tarea_Ejecutiva { get; set; }
        public DbSet<SGI_Tarea_Ejecutiva_NumeroGedo> SGI_Tarea_Ejecutiva_NumeroGedo { get; set; }
        public DbSet<SGI_Tarea_Entregar_Tramite> SGI_Tarea_Entregar_Tramite { get; set; }
        public DbSet<SGI_Tarea_Enviar_AVH> SGI_Tarea_Enviar_AVH { get; set; }
        public DbSet<SGI_Tarea_Enviar_DGFC> SGI_Tarea_Enviar_DGFC { get; set; }
        public DbSet<SGI_Tarea_Enviar_PVH> SGI_Tarea_Enviar_PVH { get; set; }
        public DbSet<SGI_Tarea_Fin_Tramite> SGI_Tarea_Fin_Tramite { get; set; }
        public DbSet<SGI_Tarea_Generar_Expediente> SGI_Tarea_Generar_Expediente { get; set; }
        public DbSet<SGI_Tarea_Generar_Expediente_Procesos> SGI_Tarea_Generar_Expediente_Procesos { get; set; }
        public DbSet<SGI_Tarea_Generar_Ticket_Liza> SGI_Tarea_Generar_Ticket_Liza { get; set; }
        public DbSet<SGI_Tarea_Gestion_Documental> SGI_Tarea_Gestion_Documental { get; set; }
        public DbSet<SGI_Tarea_Informar_Doc_Sade> SGI_Tarea_Informar_Doc_Sade { get; set; }
        public DbSet<SGI_Tarea_Obtener_Ticket_Liza> SGI_Tarea_Obtener_Ticket_Liza { get; set; }
        public DbSet<SGI_Tarea_Pagos_log> SGI_Tarea_Pagos_log { get; set; }
        public DbSet<SGI_Tarea_Rechazo_En_SADE> SGI_Tarea_Rechazo_En_SADE { get; set; }
        public DbSet<SGI_Tarea_Resultado_Inspector> SGI_Tarea_Resultado_Inspector { get; set; }
        public DbSet<SGI_Tarea_Revision_Dictamenes> SGI_Tarea_Revision_Dictamenes { get; set; }
        public DbSet<SGI_Tarea_Revision_Director> SGI_Tarea_Revision_Director { get; set; }
        public DbSet<SGI_Tarea_Revision_Pagos> SGI_Tarea_Revision_Pagos { get; set; }
        public DbSet<SGI_Tarea_Revision_Tecnica_Legal> SGI_Tarea_Revision_Tecnica_Legal { get; set; }
        public DbSet<SGI_Tarea_Validar_Zonificacion> SGI_Tarea_Validar_Zonificacion { get; set; }
        public DbSet<SGI_Tarea_Verificacion_AVH> SGI_Tarea_Verificacion_AVH { get; set; }
        public DbSet<SGI_Tarea_Visado> SGI_Tarea_Visado { get; set; }
        public DbSet<SGI_Tareas_Pases_Sectores> SGI_Tareas_Pases_Sectores { get; set; }
        public DbSet<SGI_Tramites_Tareas_Dispo_Considerando> SGI_Tramites_Tareas_Dispo_Considerando { get; set; }
        public DbSet<SolicitudDocumento> SolicitudDocumento { get; set; }
        public DbSet<SolicitudExpediente> SolicitudExpediente { get; set; }
        public DbSet<SolicitudHistorialEstados> SolicitudHistorialEstados { get; set; }
        public DbSet<SolicitudObservaciones> SolicitudObservaciones { get; set; }
        public DbSet<SolicitudPartida> SolicitudPartida { get; set; }
        public DbSet<SolicitudPuerta> SolicitudPuerta { get; set; }
        public DbSet<SolicitudRubro> SolicitudRubro { get; set; }
        public DbSet<SolicitudTipoActividad> SolicitudTipoActividad { get; set; }
        public DbSet<SolicitudZonasActualizadas> SolicitudZonasActualizadas { get; set; }
        public DbSet<SSIT_Permisos_DatosAdicionales> SSIT_Permisos_DatosAdicionales { get; set; }
        public DbSet<SSIT_Solicitudes_Baja> SSIT_Solicitudes_Baja { get; set; }
        public DbSet<SSIT_Solicitudes_DatosLocal> SSIT_Solicitudes_DatosLocal { get; set; }
        public DbSet<SSIT_Solicitudes_Normativas> SSIT_Solicitudes_Normativas { get; set; }
        public DbSet<SSIT_Solicitudes_RubrosCN> SSIT_Solicitudes_RubrosCN { get; set; }
        public DbSet<Sugerencias> Sugerencias { get; set; }
        public DbSet<TipoPersona> TipoPersona { get; set; }
        public DbSet<TiposMotivoBaja> TiposMotivoBaja { get; set; }
        public DbSet<TitularInhibido> TitularInhibido { get; set; }
        public DbSet<Transf_Solicitudes_AvisoCaducidad> Transf_Solicitudes_AvisoCaducidad { get; set; }
        public DbSet<Transf_Solicitudes_Baja> Transf_Solicitudes_Baja { get; set; }
        public DbSet<TransicionEstadoSolicitud> TransicionEstadoSolicitud { get; set; }
        public DbSet<Ubicaciones_Acciones> Ubicaciones_Acciones { get; set; }
        public DbSet<Ubicaciones_Distritos_Excepciones_Mixturas> Ubicaciones_Distritos_Excepciones_Mixturas { get; set; }
        public DbSet<Ubicaciones_Distritos_Excepciones_RubrosCN> Ubicaciones_Distritos_Excepciones_RubrosCN { get; set; }
        public DbSet<Ubicaciones_Distritos_temp> Ubicaciones_Distritos_temp { get; set; }
        public DbSet<Ubicaciones_Estados> Ubicaciones_Estados { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios> Ubicaciones_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios_Estados> Ubicaciones_Historial_Cambios_Estados { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios_UsuariosIntervinientes> Ubicaciones_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Ubicaciones_Operaciones> Ubicaciones_Operaciones { get; set; }
        public DbSet<Ubicaciones_Operaciones_Detalle> Ubicaciones_Operaciones_Detalle { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios> Ubicaciones_PropiedadHorizontal_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios_Estados> Ubicaciones_PropiedadHorizontal_Historial_Cambios_Estados { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios_UsuariosIntervinientes> Ubicaciones_PropiedadHorizontal_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Ubicaciones_Puertas_Historial_Cambios> Ubicaciones_Puertas_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_Puertas_temp> Ubicaciones_Puertas_temp { get; set; }
        public DbSet<Ubicaciones_temp> Ubicaciones_temp { get; set; }
        public DbSet<Ubicaciones_ZonasComplementarias_Historial_Cambios> Ubicaciones_ZonasComplementarias_Historial_Cambios { get; set; }
        public DbSet<Votos> Votos { get; set; }
        public DbSet<wsEscribanos_Tipos_Escrituras> wsEscribanos_Tipos_Escrituras { get; set; }
        public DbSet<wsPagos_BatchLog> wsPagos_BatchLog { get; set; }
        public DbSet<wsPagos_BoletaUnica_Errores> wsPagos_BoletaUnica_Errores { get; set; }
        public DbSet<wsPagos_BoletaUnica_HistorialEstados> wsPagos_BoletaUnica_HistorialEstados { get; set; }
        public DbSet<wsPagos_BoletaUnica_HistorialEstados2> wsPagos_BoletaUnica_HistorialEstados2 { get; set; }
        public DbSet<wsPagos_BoletaUnica_HistorialEstados3> wsPagos_BoletaUnica_HistorialEstados3 { get; set; }
        public DbSet<wsPagos_Conceptos> wsPagos_Conceptos { get; set; }
        public DbSet<wsPagos_PagoElectronico> wsPagos_PagoElectronico { get; set; }
        public DbSet<BackupCalles> BackupCalles { get; set; }
        public DbSet<BackupEscribanos> BackupEscribanos { get; set; }
        public DbSet<dtproperties> dtproperties { get; set; }
        public DbSet<Encomienda_bk> Encomienda_bk { get; set; }
        public DbSet<Encomienda_DocumentosAdjuntos_borrados> Encomienda_DocumentosAdjuntos_borrados { get; set; }
        public DbSet<Envio_Mail_Log> Envio_Mail_Log { get; set; }
        public DbSet<Envio_Mail_Log_Errores> Envio_Mail_Log_Errores { get; set; }
        public DbSet<Envio_Mail_Send_Estado> Envio_Mail_Send_Estado { get; set; }
        public DbSet<Envio_Mail_Send_Filtrados> Envio_Mail_Send_Filtrados { get; set; }
        public DbSet<RubrosCN_Historial> RubrosCN_Historial { get; set; }
        public DbSet<SSIT_Solicitudes_bk> SSIT_Solicitudes_bk { get; set; }
        public DbSet<wsDatosCallejeroUSIG> wsDatosCallejeroUSIG { get; set; }
        public DbSet<wsPagos_Parametros> wsPagos_Parametros { get; set; }
        public DbSet<wsPagos_Procesados> wsPagos_Procesados { get; set; }
        public DbSet<wsPagos_Procesados_PagadosSinFecha> wsPagos_Procesados_PagadosSinFecha { get; set; }
        public DbSet<Ubicaciones_Distritos> Ubicaciones_Distritos { get; set; }
        public DbSet<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<Encomienda_RubrosCN_Deposito> Encomienda_RubrosCN_Deposito { get; set; }
        public DbSet<RubrosDepositosCategoriasCN> RubrosDepositosCategoriasCN { get; set; }
        public DbSet<RubrosDepositosCN> RubrosDepositosCN { get; set; }
        public DbSet<RubrosDepositosCN_RangosSuperficie> RubrosDepositosCN_RangosSuperficie { get; set; }
        public DbSet<Profesional> Profesional { get; set; }
        public DbSet<Profesional_historial> Profesional_historial { get; set; }
        public DbSet<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas> SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<Encomienda_Normativas> Encomienda_Normativas { get; set; }
        public DbSet<Transf_Solicitudes_Notificaciones> Transf_Solicitudes_Notificaciones { get; set; }
        public DbSet<CondicionesIncendio> CondicionesIncendio { get; set; }
        public DbSet<RubrosCN> RubrosCN { get; set; }
        public DbSet<CPadron_RubrosCN> CPadron_RubrosCN { get; set; }
        public DbSet<Login_Tad_Token> Login_Tad_Token { get; set; }
    
        public virtual int ENG_Bandeja_Asignar(Nullable<int> id_tramitetarea, Nullable<System.Guid> userid_a_asignar, Nullable<System.Guid> userid_asignador)
        {
            var id_tramitetareaParameter = id_tramitetarea.HasValue ?
                new ObjectParameter("id_tramitetarea", id_tramitetarea) :
                new ObjectParameter("id_tramitetarea", typeof(int));
    
            var userid_a_asignarParameter = userid_a_asignar.HasValue ?
                new ObjectParameter("userid_a_asignar", userid_a_asignar) :
                new ObjectParameter("userid_a_asignar", typeof(System.Guid));
    
            var userid_asignadorParameter = userid_asignador.HasValue ?
                new ObjectParameter("userid_asignador", userid_asignador) :
                new ObjectParameter("userid_asignador", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENG_Bandeja_Asignar", id_tramitetareaParameter, userid_a_asignarParameter, userid_asignadorParameter);
        }
    
        public virtual int ENG_Crear_Tarea(Nullable<int> id_tramite, Nullable<int> id_tarea, Nullable<System.Guid> createUser, ObjectParameter id_tramitetarea)
        {
            var id_tramiteParameter = id_tramite.HasValue ?
                new ObjectParameter("id_tramite", id_tramite) :
                new ObjectParameter("id_tramite", typeof(int));
    
            var id_tareaParameter = id_tarea.HasValue ?
                new ObjectParameter("id_tarea", id_tarea) :
                new ObjectParameter("id_tarea", typeof(int));
    
            var createUserParameter = createUser.HasValue ?
                new ObjectParameter("CreateUser", createUser) :
                new ObjectParameter("CreateUser", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENG_Crear_Tarea", id_tramiteParameter, id_tareaParameter, createUserParameter, id_tramitetarea);
        }
    
        public virtual int ENG_Finalizar_Tarea(Nullable<int> id_tramitetarea, Nullable<int> id_resultado, Nullable<int> id_proxima_tarea, Nullable<System.Guid> userid, ObjectParameter id_tramitetarea_nuevo)
        {
            var id_tramitetareaParameter = id_tramitetarea.HasValue ?
                new ObjectParameter("id_tramitetarea", id_tramitetarea) :
                new ObjectParameter("id_tramitetarea", typeof(int));
    
            var id_resultadoParameter = id_resultado.HasValue ?
                new ObjectParameter("id_resultado", id_resultado) :
                new ObjectParameter("id_resultado", typeof(int));
    
            var id_proxima_tareaParameter = id_proxima_tarea.HasValue ?
                new ObjectParameter("id_proxima_tarea", id_proxima_tarea) :
                new ObjectParameter("id_proxima_tarea", typeof(int));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENG_Finalizar_Tarea", id_tramitetareaParameter, id_resultadoParameter, id_proxima_tareaParameter, useridParameter, id_tramitetarea_nuevo);
        }
    
        public virtual ObjectResult<ENG_GetTransicionesxResultado_Result> ENG_GetTransicionesxResultado(Nullable<int> id_tarea, Nullable<int> id_resultado, Nullable<int> id_tramitetarea)
        {
            var id_tareaParameter = id_tarea.HasValue ?
                new ObjectParameter("id_tarea", id_tarea) :
                new ObjectParameter("id_tarea", typeof(int));
    
            var id_resultadoParameter = id_resultado.HasValue ?
                new ObjectParameter("id_resultado", id_resultado) :
                new ObjectParameter("id_resultado", typeof(int));
    
            var id_tramitetareaParameter = id_tramitetarea.HasValue ?
                new ObjectParameter("id_tramitetarea", id_tramitetarea) :
                new ObjectParameter("id_tramitetarea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ENG_GetTransicionesxResultado_Result>("ENG_GetTransicionesxResultado", id_tareaParameter, id_resultadoParameter, id_tramitetareaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SGI_ResignarTarea(Nullable<int> id_tramitetarea, Nullable<System.Guid> userid)
        {
            var id_tramitetareaParameter = id_tramitetarea.HasValue ?
                new ObjectParameter("id_tramitetarea", id_tramitetarea) :
                new ObjectParameter("id_tramitetarea", typeof(int));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SGI_ResignarTarea", id_tramitetareaParameter, useridParameter);
        }
    
        public virtual int SSIT_Solicitudes_ActualizarEstadoCompleto(Nullable<int> id_solicitud, Nullable<System.Guid> userid, ObjectParameter msgError)
        {
            var id_solicitudParameter = id_solicitud.HasValue ?
                new ObjectParameter("id_solicitud", id_solicitud) :
                new ObjectParameter("id_solicitud", typeof(int));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SSIT_Solicitudes_ActualizarEstadoCompleto", id_solicitudParameter, useridParameter, msgError);
        }
    
        public virtual int Encomienda_TraerEncomiendasConsejo(Nullable<int> id_grupoconsejo, string matricula, string apenom, string cuit, string estados, Nullable<int> p_id_encomienda, Nullable<int> p_nroEncomiendaconsejo)
        {
            var id_grupoconsejoParameter = id_grupoconsejo.HasValue ?
                new ObjectParameter("id_grupoconsejo", id_grupoconsejo) :
                new ObjectParameter("id_grupoconsejo", typeof(int));
    
            var matriculaParameter = matricula != null ?
                new ObjectParameter("matricula", matricula) :
                new ObjectParameter("matricula", typeof(string));
    
            var apenomParameter = apenom != null ?
                new ObjectParameter("apenom", apenom) :
                new ObjectParameter("apenom", typeof(string));
    
            var cuitParameter = cuit != null ?
                new ObjectParameter("cuit", cuit) :
                new ObjectParameter("cuit", typeof(string));
    
            var estadosParameter = estados != null ?
                new ObjectParameter("estados", estados) :
                new ObjectParameter("estados", typeof(string));
    
            var p_id_encomiendaParameter = p_id_encomienda.HasValue ?
                new ObjectParameter("p_id_encomienda", p_id_encomienda) :
                new ObjectParameter("p_id_encomienda", typeof(int));
    
            var p_nroEncomiendaconsejoParameter = p_nroEncomiendaconsejo.HasValue ?
                new ObjectParameter("p_nroEncomiendaconsejo", p_nroEncomiendaconsejo) :
                new ObjectParameter("p_nroEncomiendaconsejo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Encomienda_TraerEncomiendasConsejo", id_grupoconsejoParameter, matriculaParameter, apenomParameter, cuitParameter, estadosParameter, p_id_encomiendaParameter, p_nroEncomiendaconsejoParameter);
        }
    
        public virtual int Encomienda_TraerEncomiendasConsejo_ANT(string matricula, string apenom, string cuit, string estados, Nullable<int> p_id_encomienda)
        {
            var matriculaParameter = matricula != null ?
                new ObjectParameter("matricula", matricula) :
                new ObjectParameter("matricula", typeof(string));
    
            var apenomParameter = apenom != null ?
                new ObjectParameter("apenom", apenom) :
                new ObjectParameter("apenom", typeof(string));
    
            var cuitParameter = cuit != null ?
                new ObjectParameter("cuit", cuit) :
                new ObjectParameter("cuit", typeof(string));
    
            var estadosParameter = estados != null ?
                new ObjectParameter("estados", estados) :
                new ObjectParameter("estados", typeof(string));
    
            var p_id_encomiendaParameter = p_id_encomienda.HasValue ?
                new ObjectParameter("p_id_encomienda", p_id_encomienda) :
                new ObjectParameter("p_id_encomienda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Encomienda_TraerEncomiendasConsejo_ANT", matriculaParameter, apenomParameter, cuitParameter, estadosParameter, p_id_encomiendaParameter);
        }
    
        public virtual int EncomiendaAnt_ActualizarEstado(Nullable<int> id_encomienda, Nullable<int> id_estado, Nullable<System.Guid> userId)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            var id_estadoParameter = id_estado.HasValue ?
                new ObjectParameter("id_estado", id_estado) :
                new ObjectParameter("id_estado", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EncomiendaAnt_ActualizarEstado", id_encomiendaParameter, id_estadoParameter, userIdParameter);
        }
    
        public virtual int SGI_Tarea_EliminarTarea(Nullable<int> id_tramitetarea, Nullable<int> id_tipoTramite)
        {
            var id_tramitetareaParameter = id_tramitetarea.HasValue ?
                new ObjectParameter("id_tramitetarea", id_tramitetarea) :
                new ObjectParameter("id_tramitetarea", typeof(int));
    
            var id_tipoTramiteParameter = id_tipoTramite.HasValue ?
                new ObjectParameter("id_tipoTramite", id_tipoTramite) :
                new ObjectParameter("id_tipoTramite", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SGI_Tarea_EliminarTarea", id_tramitetareaParameter, id_tipoTramiteParameter);
        }
    
        public virtual int wsEscribanos_CopiarDeAPRA(Nullable<int> id_encomienda, Nullable<int> id_caa)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            var id_caaParameter = id_caa.HasValue ?
                new ObjectParameter("id_caa", id_caa) :
                new ObjectParameter("id_caa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("wsEscribanos_CopiarDeAPRA", id_encomiendaParameter, id_caaParameter);
        }
    
        public virtual ObjectResult<SSIT_Listado_Profesionales_Result> SSIT_Listado_Profesionales(Nullable<int> circuito)
        {
            var circuitoParameter = circuito.HasValue ?
                new ObjectParameter("circuito", circuito) :
                new ObjectParameter("circuito", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SSIT_Listado_Profesionales_Result>("SSIT_Listado_Profesionales", circuitoParameter);
        }
    
        public virtual int SSIT_Transferir_Usuario(Nullable<System.Guid> userid_nuevo, Nullable<System.Guid> userid_anterior)
        {
            var userid_nuevoParameter = userid_nuevo.HasValue ?
                new ObjectParameter("userid_nuevo", userid_nuevo) :
                new ObjectParameter("userid_nuevo", typeof(System.Guid));
    
            var userid_anteriorParameter = userid_anterior.HasValue ?
                new ObjectParameter("userid_anterior", userid_anterior) :
                new ObjectParameter("userid_anterior", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SSIT_Transferir_Usuario", userid_nuevoParameter, userid_anteriorParameter);
        }
    
        public virtual int GetMixDistritoZonaySubZonaByEncomienda(Nullable<int> encomienda, ObjectParameter result)
        {
            var encomiendaParameter = encomienda.HasValue ?
                new ObjectParameter("encomienda", encomienda) :
                new ObjectParameter("encomienda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetMixDistritoZonaySubZonaByEncomienda", encomiendaParameter, result);
        }
    
        public virtual int GetMixDistritoZonaySubZonaBySolicitud(Nullable<int> idSolicitud, ObjectParameter result)
        {
            var idSolicitudParameter = idSolicitud.HasValue ?
                new ObjectParameter("idSolicitud", idSolicitud) :
                new ObjectParameter("idSolicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetMixDistritoZonaySubZonaBySolicitud", idSolicitudParameter, result);
        }
    
        public virtual int GetMixDistritoZonaySubZonaByTR(Nullable<int> idSolicitud, ObjectParameter result)
        {
            var idSolicitudParameter = idSolicitud.HasValue ?
                new ObjectParameter("idSolicitud", idSolicitud) :
                new ObjectParameter("idSolicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetMixDistritoZonaySubZonaByTR", idSolicitudParameter, result);
        }
    
        public virtual int Encomienda_Actualizar_TipoSubtipo_Expediente_CN(Nullable<int> id_encomienda)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Encomienda_Actualizar_TipoSubtipo_Expediente_CN", id_encomiendaParameter);
        }
    
        public virtual int Encomienda_Actualizar_TipoSubtipo_Expediente(Nullable<int> id_encomienda)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Encomienda_Actualizar_TipoSubtipo_Expediente", id_encomiendaParameter);
        }
    
        public virtual int SPGetIdCircuitoByEncomienda(Nullable<int> id_encomienda, ObjectParameter id_circuito)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPGetIdCircuitoByEncomienda", id_encomiendaParameter, id_circuito);
        }
    
        public virtual int SPEncomiendaActualizarTipoSubtipoExpediente(Nullable<int> id_encomienda, Nullable<int> id_tipoexpediente, Nullable<int> id_subtipoexpediente)
        {
            var id_encomiendaParameter = id_encomienda.HasValue ?
                new ObjectParameter("id_encomienda", id_encomienda) :
                new ObjectParameter("id_encomienda", typeof(int));
    
            var id_tipoexpedienteParameter = id_tipoexpediente.HasValue ?
                new ObjectParameter("id_tipoexpediente", id_tipoexpediente) :
                new ObjectParameter("id_tipoexpediente", typeof(int));
    
            var id_subtipoexpedienteParameter = id_subtipoexpediente.HasValue ?
                new ObjectParameter("id_subtipoexpediente", id_subtipoexpediente) :
                new ObjectParameter("id_subtipoexpediente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPEncomiendaActualizarTipoSubtipoExpediente", id_encomiendaParameter, id_tipoexpedienteParameter, id_subtipoexpedienteParameter);
        }
    
        public virtual int SPGetIdCircuitoHAB(Nullable<int> id_solicitud, ObjectParameter circuito)
        {
            var id_solicitudParameter = id_solicitud.HasValue ?
                new ObjectParameter("id_solicitud", id_solicitud) :
                new ObjectParameter("id_solicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPGetIdCircuitoHAB", id_solicitudParameter, circuito);
        }
    
        public virtual ObjectResult<RubrosDepositosCN_Evaluar_Result> RubrosDepositosCN_Evaluar(Nullable<int> id_tramite, Nullable<int> idDeposito, Nullable<decimal> superficieCubierta, Nullable<int> zonaMixtura, string sistema)
        {
            var id_tramiteParameter = id_tramite.HasValue ?
                new ObjectParameter("id_tramite", id_tramite) :
                new ObjectParameter("id_tramite", typeof(int));
    
            var idDepositoParameter = idDeposito.HasValue ?
                new ObjectParameter("IdDeposito", idDeposito) :
                new ObjectParameter("IdDeposito", typeof(int));
    
            var superficieCubiertaParameter = superficieCubierta.HasValue ?
                new ObjectParameter("SuperficieCubierta", superficieCubierta) :
                new ObjectParameter("SuperficieCubierta", typeof(decimal));
    
            var zonaMixturaParameter = zonaMixtura.HasValue ?
                new ObjectParameter("ZonaMixtura", zonaMixtura) :
                new ObjectParameter("ZonaMixtura", typeof(int));
    
            var sistemaParameter = sistema != null ?
                new ObjectParameter("Sistema", sistema) :
                new ObjectParameter("Sistema", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RubrosDepositosCN_Evaluar_Result>("RubrosDepositosCN_Evaluar", id_tramiteParameter, idDepositoParameter, superficieCubiertaParameter, zonaMixturaParameter, sistemaParameter);
        }
    
        public virtual int Transf_Solicitudes_Historial_LibradoUso_INSERT(Nullable<int> id_solicitud, Nullable<System.DateTime> fechaLibrado, Nullable<System.DateTime> createDate, Nullable<System.Guid> createUser)
        {
            var id_solicitudParameter = id_solicitud.HasValue ?
                new ObjectParameter("id_solicitud", id_solicitud) :
                new ObjectParameter("id_solicitud", typeof(int));
    
            var fechaLibradoParameter = fechaLibrado.HasValue ?
                new ObjectParameter("fechaLibrado", fechaLibrado) :
                new ObjectParameter("fechaLibrado", typeof(System.DateTime));
    
            var createDateParameter = createDate.HasValue ?
                new ObjectParameter("createDate", createDate) :
                new ObjectParameter("createDate", typeof(System.DateTime));
    
            var createUserParameter = createUser.HasValue ?
                new ObjectParameter("createUser", createUser) :
                new ObjectParameter("createUser", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Transf_Solicitudes_Historial_LibradoUso_INSERT", id_solicitudParameter, fechaLibradoParameter, createDateParameter, createUserParameter);
        }
    
    }
}
