﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DGHP_SolicitudesEntities : DbContext
    {
        public DGHP_SolicitudesEntities()
            : base("name=DGHP_SolicitudesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<ASR_Rel_Elevadores_Inspeccion> ASR_Rel_Elevadores_Inspeccion { get; set; }
        public DbSet<ASR_Rel_Elevadores_Pagos> ASR_Rel_Elevadores_Pagos { get; set; }
        public DbSet<ASR_Rel_Empresas_Profesionales> ASR_Rel_Empresas_Profesionales { get; set; }
        public DbSet<ASR_Rel_Empresas_Profesionales_Historial> ASR_Rel_Empresas_Profesionales_Historial { get; set; }
        public DbSet<ASR_Resultado_Inspeccion> ASR_Resultado_Inspeccion { get; set; }
        public DbSet<ASR_Tipo_Informe> ASR_Tipo_Informe { get; set; }
        public DbSet<ASR_TiposDeElevadores> ASR_TiposDeElevadores { get; set; }
        public DbSet<ASR_Ubicaciones_Elevadores> ASR_Ubicaciones_Elevadores { get; set; }
        public DbSet<AuditoriaDeTablas> AuditoriaDeTablas { get; set; }
        public DbSet<AVH_visitas> AVH_visitas { get; set; }
        public DbSet<BackupCalles> BackupCalles { get; set; }
        public DbSet<BackupEscribanos> BackupEscribanos { get; set; }
        public DbSet<Barrios> Barrios { get; set; }
        public DbSet<CAA_Rel_CAA_DocAdjuntos> CAA_Rel_CAA_DocAdjuntos { get; set; }
        public DbSet<Calles> Calles { get; set; }
        public DbSet<Calles_Eliminadas> Calles_Eliminadas { get; set; }
        public DbSet<Calles_Excepcion> Calles_Excepcion { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Comisarias> Comisarias { get; set; }
        public DbSet<Comunas> Comunas { get; set; }
        public DbSet<Conceptos_BUI_Independientes> Conceptos_BUI_Independientes { get; set; }
        public DbSet<ConformacionLocal> ConformacionLocal { get; set; }
        public DbSet<ConsejoProfesional> ConsejoProfesional { get; set; }
        public DbSet<ConsejoProfesional_RolesPermitidos> ConsejoProfesional_RolesPermitidos { get; set; }
        public DbSet<CPadron_ConformacionLocal> CPadron_ConformacionLocal { get; set; }
        public DbSet<CPadron_DatosLocal> CPadron_DatosLocal { get; set; }
        public DbSet<CPadron_DocumentosAdjuntos> CPadron_DocumentosAdjuntos { get; set; }
        public DbSet<CPadron_Estados> CPadron_Estados { get; set; }
        public DbSet<CPadron_HistorialEstados> CPadron_HistorialEstados { get; set; }
        public DbSet<CPadron_Normativas> CPadron_Normativas { get; set; }
        public DbSet<CPadron_Planos> CPadron_Planos { get; set; }
        public DbSet<CPadron_Plantas> CPadron_Plantas { get; set; }
        public DbSet<CPadron_Rubros> CPadron_Rubros { get; set; }
        public DbSet<CPadron_Solicitudes> CPadron_Solicitudes { get; set; }
        public DbSet<CPadron_Solicitudes_Observaciones> CPadron_Solicitudes_Observaciones { get; set; }
        public DbSet<CPadron_Titulares_PersonasFisicas> CPadron_Titulares_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_PersonasJuridicas> CPadron_Titulares_PersonasJuridicas { get; set; }
        public DbSet<CPadron_Titulares_PersonasJuridicas_PersonasFisicas> CPadron_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasFisicas> CPadron_Titulares_Solicitud_PersonasFisicas { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasJuridicas> CPadron_Titulares_Solicitud_PersonasJuridicas { get; set; }
        public DbSet<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas> CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<CPadron_Ubicaciones> CPadron_Ubicaciones { get; set; }
        public DbSet<CPadron_Ubicaciones_PropiedadHorizontal> CPadron_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<CPadron_Ubicaciones_Puertas> CPadron_Ubicaciones_Puertas { get; set; }
        public DbSet<DatosBuscados> DatosBuscados { get; set; }
        public DbSet<DGFYCO_Ubicaciones> DGFYCO_Ubicaciones { get; set; }
        public DbSet<DGFYCO_Ubicaciones_DatosAdicionales> DGFYCO_Ubicaciones_DatosAdicionales { get; set; }
        public DbSet<DGFYCO_Ubicaciones_Puertas> DGFYCO_Ubicaciones_Puertas { get; set; }
        public DbSet<DistritosEscolares> DistritosEscolares { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<dtproperties> dtproperties { get; set; }
        public DbSet<Encomienda> Encomienda { get; set; }
        public DbSet<Encomienda_Certificado_Sobrecarga> Encomienda_Certificado_Sobrecarga { get; set; }
        public DbSet<Encomienda_ConformacionLocal> Encomienda_ConformacionLocal { get; set; }
        public DbSet<Encomienda_DatosLocal> Encomienda_DatosLocal { get; set; }
        public DbSet<Encomienda_DocumentosAdjuntos> Encomienda_DocumentosAdjuntos { get; set; }
        public DbSet<Encomienda_Estados> Encomienda_Estados { get; set; }
        public DbSet<Encomienda_Firmantes_PersonasFisicas> Encomienda_Firmantes_PersonasFisicas { get; set; }
        public DbSet<Encomienda_Firmantes_PersonasJuridicas> Encomienda_Firmantes_PersonasJuridicas { get; set; }
        public DbSet<Encomienda_HistorialEstados> Encomienda_HistorialEstados { get; set; }
        public DbSet<Encomienda_Normativas> Encomienda_Normativas { get; set; }
        public DbSet<Encomienda_Planos> Encomienda_Planos { get; set; }
        public DbSet<Encomienda_Plantas> Encomienda_Plantas { get; set; }
        public DbSet<Encomienda_Rel_TiposDestinos_TiposUsos> Encomienda_Rel_TiposDestinos_TiposUsos { get; set; }
        public DbSet<Encomienda_Rel_TiposSobrecargas_TiposDestinos> Encomienda_Rel_TiposSobrecargas_TiposDestinos { get; set; }
        public DbSet<Encomienda_Rubros> Encomienda_Rubros { get; set; }
        public DbSet<Encomienda_Sobrecarga_Detalle1> Encomienda_Sobrecarga_Detalle1 { get; set; }
        public DbSet<Encomienda_Sobrecarga_Detalle2> Encomienda_Sobrecarga_Detalle2 { get; set; }
        public DbSet<Encomienda_Sobrecargas> Encomienda_Sobrecargas { get; set; }
        public DbSet<Encomienda_Tipos_Certificados_Sobrecarga> Encomienda_Tipos_Certificados_Sobrecarga { get; set; }
        public DbSet<Encomienda_Tipos_Destinos> Encomienda_Tipos_Destinos { get; set; }
        public DbSet<Encomienda_Tipos_Sobrecargas> Encomienda_Tipos_Sobrecargas { get; set; }
        public DbSet<Encomienda_Tipos_Usos> Encomienda_Tipos_Usos { get; set; }
        public DbSet<Encomienda_Titulares_PersonasFisicas> Encomienda_Titulares_PersonasFisicas { get; set; }
        public DbSet<Encomienda_Titulares_PersonasJuridicas> Encomienda_Titulares_PersonasJuridicas { get; set; }
        public DbSet<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> Encomienda_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<Encomienda_TransicionEstados> Encomienda_TransicionEstados { get; set; }
        public DbSet<Encomienda_Ubicaciones> Encomienda_Ubicaciones { get; set; }
        public DbSet<Encomienda_Ubicaciones_PropiedadHorizontal> Encomienda_Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<Encomienda_Ubicaciones_Puertas> Encomienda_Ubicaciones_Puertas { get; set; }
        public DbSet<Encomienda_URLxROL> Encomienda_URLxROL { get; set; }
        public DbSet<Encomienda_ZonasActualizadas> Encomienda_ZonasActualizadas { get; set; }
        public DbSet<EncomiendaExt> EncomiendaExt { get; set; }
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
        public DbSet<EntidadNormativa> EntidadNormativa { get; set; }
        public DbSet<Escribano> Escribano { get; set; }
        public DbSet<GrupoConsejos> GrupoConsejos { get; set; }
        public DbSet<GrupoConsejos_Roles_Clasificacion> GrupoConsejos_Roles_Clasificacion { get; set; }
        public DbSet<Instructivos> Instructivos { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<MasBuscados> MasBuscados { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PersonasInhibidas> PersonasInhibidas { get; set; }
        public DbSet<Profesional> Profesional { get; set; }
        public DbSet<Profesional_Perfiles_Inhibiciones> Profesional_Perfiles_Inhibiciones { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<RegionesSanitarias> RegionesSanitarias { get; set; }
        public DbSet<Rel_Encomienda_Rectificatoria> Rel_Encomienda_Rectificatoria { get; set; }
        public DbSet<Rel_Rubros_Fichas> Rel_Rubros_Fichas { get; set; }
        public DbSet<Rel_Rubros_ImpactoAmbiental> Rel_Rubros_ImpactoAmbiental { get; set; }
        public DbSet<Rel_TipoExpediente_SubtipoExpediente> Rel_TipoExpediente_SubtipoExpediente { get; set; }
        public DbSet<Rel_TipoTramite_TipoExpediente> Rel_TipoTramite_TipoExpediente { get; set; }
        public DbSet<Rel_TipoTramite_TiposDeDocumentosRequeridos> Rel_TipoTramite_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<Rel_Usuarios_AS_Empresas> Rel_Usuarios_AS_Empresas { get; set; }
        public DbSet<Rel_Usuarios_GrupoConsejo> Rel_Usuarios_GrupoConsejo { get; set; }
        public DbSet<Rel_UsuariosProf_Roles_Clasificacion> Rel_UsuariosProf_Roles_Clasificacion { get; set; }
        public DbSet<Rel_ZonasPlaneamiento_ZonasHabilitaciones> Rel_ZonasPlaneamiento_ZonasHabilitaciones { get; set; }
        public DbSet<Rubros> Rubros { get; set; }
        public DbSet<Rubros_Config_Incendio> Rubros_Config_Incendio { get; set; }
        public DbSet<Rubros_Historial_Cambios> Rubros_Historial_Cambios { get; set; }
        public DbSet<Rubros_Historial_Cambios_Estados> Rubros_Historial_Cambios_Estados { get; set; }
        public DbSet<Rubros_Historial_Cambios_UsuariosIntervinientes> Rubros_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Rubros_InformacionRelevante> Rubros_InformacionRelevante { get; set; }
        public DbSet<Rubros_InformacionRelevante_Historial_Cambios> Rubros_InformacionRelevante_Historial_Cambios { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos> Rubros_TiposDeDocumentosRequeridos { get; set; }
        public DbSet<Rubros_TiposDeDocumentosRequeridos_Historial_Cambios> Rubros_TiposDeDocumentosRequeridos_Historial_Cambios { get; set; }
        public DbSet<RubrosCondiciones> RubrosCondiciones { get; set; }
        public DbSet<RubrosZonasCondiciones> RubrosZonasCondiciones { get; set; }
        public DbSet<RubrosZonasCondiciones_Historial_Cambios> RubrosZonasCondiciones_Historial_Cambios { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<SolicitudDocumento> SolicitudDocumento { get; set; }
        public DbSet<SolicitudExpediente> SolicitudExpediente { get; set; }
        public DbSet<SolicitudHistorialEstados> SolicitudHistorialEstados { get; set; }
        public DbSet<SolicitudObservaciones> SolicitudObservaciones { get; set; }
        public DbSet<SolicitudPartida> SolicitudPartida { get; set; }
        public DbSet<SolicitudPuerta> SolicitudPuerta { get; set; }
        public DbSet<SolicitudRubro> SolicitudRubro { get; set; }
        public DbSet<SolicitudTipoActividad> SolicitudTipoActividad { get; set; }
        public DbSet<SolicitudZonasActualizadas> SolicitudZonasActualizadas { get; set; }
        public DbSet<SSIS_Log> SSIS_Log { get; set; }
        public DbSet<SSIT_Solicitudes> SSIT_Solicitudes { get; set; }
        public DbSet<SSIT_Solicitudes_Encomienda> SSIT_Solicitudes_Encomienda { get; set; }
        public DbSet<SSIT_Solicitudes_HistorialEstados> SSIT_Solicitudes_HistorialEstados { get; set; }
        public DbSet<SSIT_Solicitudes_Observaciones> SSIT_Solicitudes_Observaciones { get; set; }
        public DbSet<SSIT_Solicitudes_RULH> SSIT_Solicitudes_RULH { get; set; }
        public DbSet<SubtipoExpediente> SubtipoExpediente { get; set; }
        public DbSet<SubTiposDeUbicacion> SubTiposDeUbicacion { get; set; }
        public DbSet<Sugerencias> Sugerencias { get; set; }
        public DbSet<Tipo_Documentacion_Req> Tipo_Documentacion_Req { get; set; }
        public DbSet<tipo_iluminacion> tipo_iluminacion { get; set; }
        public DbSet<tipo_ventilacion> tipo_ventilacion { get; set; }
        public DbSet<TipoActividad> TipoActividad { get; set; }
        public DbSet<TipoDestino> TipoDestino { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<TipoDocumentoPersonal> TipoDocumentoPersonal { get; set; }
        public DbSet<TipoEstadoSolicitud> TipoEstadoSolicitud { get; set; }
        public DbSet<TipoExpediente> TipoExpediente { get; set; }
        public DbSet<TipoInhibicion> TipoInhibicion { get; set; }
        public DbSet<TipoNormativa> TipoNormativa { get; set; }
        public DbSet<TiposDeCaracterLegal> TiposDeCaracterLegal { get; set; }
        public DbSet<TiposDeDocumentosRequeridos> TiposDeDocumentosRequeridos { get; set; }
        public DbSet<TiposDeDocumentosSistema> TiposDeDocumentosSistema { get; set; }
        public DbSet<TiposDeInformes> TiposDeInformes { get; set; }
        public DbSet<TiposDeIngresosBrutos> TiposDeIngresosBrutos { get; set; }
        public DbSet<TiposDePlanos> TiposDePlanos { get; set; }
        public DbSet<TiposDeUbicacion> TiposDeUbicacion { get; set; }
        public DbSet<TipoSector> TipoSector { get; set; }
        public DbSet<TipoSociedad> TipoSociedad { get; set; }
        public DbSet<TipoSuperficie> TipoSuperficie { get; set; }
        public DbSet<TipoTramite> TipoTramite { get; set; }
        public DbSet<TipoVerificacion> TipoVerificacion { get; set; }
        public DbSet<TitularInhibido> TitularInhibido { get; set; }
        public DbSet<Transf_DocumentosAdjuntos> Transf_DocumentosAdjuntos { get; set; }
        public DbSet<Transf_Firmantes_PersonasFisicas> Transf_Firmantes_PersonasFisicas { get; set; }
        public DbSet<Transf_Firmantes_PersonasJuridicas> Transf_Firmantes_PersonasJuridicas { get; set; }
        public DbSet<Transf_Solicitudes> Transf_Solicitudes { get; set; }
        public DbSet<Transf_Solicitudes_HistorialEstados> Transf_Solicitudes_HistorialEstados { get; set; }
        public DbSet<Transf_Solicitudes_Observaciones> Transf_Solicitudes_Observaciones { get; set; }
        public DbSet<Transf_Titulares_PersonasFisicas> Transf_Titulares_PersonasFisicas { get; set; }
        public DbSet<Transf_Titulares_PersonasJuridicas> Transf_Titulares_PersonasJuridicas { get; set; }
        public DbSet<Transf_Titulares_PersonasJuridicas_PersonasFisicas> Transf_Titulares_PersonasJuridicas_PersonasFisicas { get; set; }
        public DbSet<TransicionEstadoSolicitud> TransicionEstadoSolicitud { get; set; }
        public DbSet<Ubicaciones> Ubicaciones { get; set; }
        public DbSet<Ubicaciones_Clausuras> Ubicaciones_Clausuras { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios> Ubicaciones_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios_Estados> Ubicaciones_Historial_Cambios_Estados { get; set; }
        public DbSet<Ubicaciones_Historial_Cambios_UsuariosIntervinientes> Ubicaciones_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Ubicaciones_Inhibiciones> Ubicaciones_Inhibiciones { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal> Ubicaciones_PropiedadHorizontal { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Clausuras> Ubicaciones_PropiedadHorizontal_Clausuras { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios> Ubicaciones_PropiedadHorizontal_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios_Estados> Ubicaciones_PropiedadHorizontal_Historial_Cambios_Estados { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Historial_Cambios_UsuariosIntervinientes> Ubicaciones_PropiedadHorizontal_Historial_Cambios_UsuariosIntervinientes { get; set; }
        public DbSet<Ubicaciones_PropiedadHorizontal_Inhibiciones> Ubicaciones_PropiedadHorizontal_Inhibiciones { get; set; }
        public DbSet<Ubicaciones_Puertas> Ubicaciones_Puertas { get; set; }
        public DbSet<Ubicaciones_Puertas_Historial_Cambios> Ubicaciones_Puertas_Historial_Cambios { get; set; }
        public DbSet<Ubicaciones_ZonasComplementarias> Ubicaciones_ZonasComplementarias { get; set; }
        public DbSet<Ubicaciones_ZonasComplementarias_Historial_Cambios> Ubicaciones_ZonasComplementarias_Historial_Cambios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<wsDatosCallejeroUSIG> wsDatosCallejeroUSIG { get; set; }
        public DbSet<wsEscribanos_ActaNotarial> wsEscribanos_ActaNotarial { get; set; }
        public DbSet<wsEscribanos_DerechoOcupacion> wsEscribanos_DerechoOcupacion { get; set; }
        public DbSet<wsEscribanos_Files> wsEscribanos_Files { get; set; }
        public DbSet<wsEscribanos_Files_TRANSF> wsEscribanos_Files_TRANSF { get; set; }
        public DbSet<wsEscribanos_InstrumentoJudicial> wsEscribanos_InstrumentoJudicial { get; set; }
        public DbSet<wsEscribanos_InstrumentoPrivado> wsEscribanos_InstrumentoPrivado { get; set; }
        public DbSet<wsEscribanos_InstrumentoPublico> wsEscribanos_InstrumentoPublico { get; set; }
        public DbSet<wsEscribanos_PersonasFisicas> wsEscribanos_PersonasFisicas { get; set; }
        public DbSet<wsEscribanos_PersonasFisicas_Representantes> wsEscribanos_PersonasFisicas_Representantes { get; set; }
        public DbSet<wsEscribanos_PersonasJuridicas> wsEscribanos_PersonasJuridicas { get; set; }
        public DbSet<wsEscribanos_PersonasJuridicas_Representantes> wsEscribanos_PersonasJuridicas_Representantes { get; set; }
        public DbSet<wsEscribanos_Tipos_Escrituras> wsEscribanos_Tipos_Escrituras { get; set; }
        public DbSet<wsPagos> wsPagos { get; set; }
        public DbSet<wsPagos_BatchLog> wsPagos_BatchLog { get; set; }
        public DbSet<wsPagos_BoletaUnica> wsPagos_BoletaUnica { get; set; }
        public DbSet<wsPagos_BoletaUnica_Errores> wsPagos_BoletaUnica_Errores { get; set; }
        public DbSet<wsPagos_BoletaUnica_Estados> wsPagos_BoletaUnica_Estados { get; set; }
        public DbSet<wsPagos_BoletaUnica_HistorialEstados> wsPagos_BoletaUnica_HistorialEstados { get; set; }
        public DbSet<wsPagos_BoletaUnica_HistorialEstados2> wsPagos_BoletaUnica_HistorialEstados2 { get; set; }
        public DbSet<wsPagos_Conceptos> wsPagos_Conceptos { get; set; }
        public DbSet<wsPagos_PagoElectronico> wsPagos_PagoElectronico { get; set; }
        public DbSet<Zonas_Habilitaciones> Zonas_Habilitaciones { get; set; }
        public DbSet<Zonas_Planeamiento> Zonas_Planeamiento { get; set; }
    }
}
