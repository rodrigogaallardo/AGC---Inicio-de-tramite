using AutoMapper;
using BaseRepository;
using BaseRepository.Engine;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using IBusinessLayer;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesBL : ISSITSolicitudesBL<SSITSolicitudesDTO>
    {
        private SSITSolicitudesRepository repo = null;
        private ItemDirectionRepository itemRepo = null;
        public static string solicitud_certificado_caa_inexistente = "";
        public static string solicitud_Anexo_Notarial_Inexistente = "";
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper MapperBaseEncomienda;

        public SSITSolicitudesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region "SSIT_Solicitudes"
                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Telefono, source => source.MapFrom(p => p.telefono))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))

                    .ForMember(dest => dest.TipoTramiteDescripcion, source => source.MapFrom(p => p.TipoTramite.descripcion_tipotramite))
                    .ForMember(dest => dest.TipoExpedienteDescripcion, source => source.MapFrom(p => p.TipoExpediente.descripcion_tipoexpediente))
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente))

                    //.ForMember(dest => dest.SubTipoExpedienteDTO, source => source.MapFrom(p => p.SubtipoExpediente))
                    //.ForMember(dest => dest.TipoExpedienteDTO, source => source.MapFrom(p => p.TipoExpediente))
                    //.ForMember(dest => dest.TipoTramiteDTO, source => source.MapFrom(p => p.TipoTramite))
                    //.ForMember(dest => dest.SSITSolicitudesPagosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Pagos))
                    .ForMember(dest => dest.SSITDocumentosAdjuntosDTO, source => source.MapFrom(p => p.SSIT_DocumentosAdjuntos))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    //.ForMember(dest => dest.SSITSolicitudesHistorialEstadosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_HistorialEstados))
                    .ForMember(dest => dest.TitularesPersonasFisicas, source => source.MapFrom(p => p.SSIT_Solicitudes_Titulares_PersonasFisicas))
                    .ForMember(dest => dest.TitularesPersonasJuridicas, source => source.MapFrom(p => p.SSIT_Solicitudes_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.FirmantesPersonasFisicas, source => source.MapFrom(p => p.SSIT_Solicitudes_Firmantes_PersonasFisicas))
                    .ForMember(dest => dest.FirmantesPersonasJuridicas, source => source.MapFrom(p => p.SSIT_Solicitudes_Firmantes_PersonasJuridicas))
                    //.ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    .ForMember(dest => dest.SSITSolicitudesRubrosCNDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_RubrosCN))
                    .ForMember(dest => dest.SSITSolicitudesDatosLocalDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_DatosLocal))
                    .ForMember(dest => dest.SSITPermisosDatosAdicionalesDTO, source => source.MapFrom(p => p.SSIT_Permisos_DatosAdicionales))
                    ;

                cfg.CreateMap<SSIT_Solicitudes_RubrosCN, SSITSolicitudesRubrosCNDTO>()
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.NombreRubro)).ReverseMap();

                cfg.CreateMap<SSIT_Solicitudes_DatosLocal, SSITSolicitudesDatosLocalDTO>().ReverseMap();

                cfg.CreateMap<SSITSolicitudesDTO, SSIT_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.telefono, source => source.MapFrom(p => p.Telefono))
                    .ForMember(dest => dest.TipoEstadoSolicitud, source => source.Ignore())
                    .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SGI_Tramites_Tareas_HAB, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_AvisoCaducidad, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Encomienda, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Firmantes_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Firmantes_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Observaciones, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Pagos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Pagos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_DocumentosAdjuntos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_HistorialEstados, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.MapFrom(p => p.SSITSolicitudesOrigenDTO))
                    .ForMember(dest => dest.SSIT_Permisos_DatosAdicionales, source => source.MapFrom(p => p.SSITPermisosDatosAdicionalesDTO))
                    ;



                cfg.CreateMap<SSIT_Solicitudes_Origen, SSITSolicitudesOrigenDTO>()
                 .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                 .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                 .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
               ;
                cfg.CreateMap<SSITSolicitudesOrigenDTO, SSIT_Solicitudes_Origen>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                    .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
                ;

                cfg.CreateMap<SSITPermisosDatosAdicionalesDTO, SSIT_Permisos_DatosAdicionales>().ReverseMap();


                #endregion
                cfg.CreateMap<EncomiendaDTO, Encomienda>()
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.nroEncomiendaconsejo, source => source.MapFrom(p => p.NroEncomiendaConsejo))
                    .ForMember(dest => dest.id_consejo, source => source.MapFrom(p => p.IdConsejo))
                    .ForMember(dest => dest.id_profesional, source => source.MapFrom(p => p.IdProfesional))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.Observaciones_plantas, source => source.MapFrom(p => p.ObservacionesPlantas))
                    .ForMember(dest => dest.Observaciones_rubros, source => source.MapFrom(p => p.ObservacionesRubros))
                    .ForMember(dest => dest.Pro_teatro, source => source.MapFrom(p => p.ProTeatro))
                    //.ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.tipo_anexo, source => source.MapFrom(p => p.tipoAnexo));
                //.ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.MapFrom(p => p.EncomiendaSSITSolicitudesDTO))
                //.ForMember(dest => dest.Encomienda_Transf_Solicitudes, source => source.MapFrom(p => p.EncomiendaTransfSolicitudesDTO));

                cfg.CreateMap<Encomienda, EncomiendaDTO>()
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.NroEncomiendaConsejo, source => source.MapFrom(p => p.nroEncomiendaconsejo))
                    .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_consejo))
                    .ForMember(dest => dest.IdProfesional, source => source.MapFrom(p => p.id_profesional))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.ObservacionesPlantas, source => source.MapFrom(p => p.Observaciones_plantas))
                    .ForMember(dest => dest.ObservacionesRubros, source => source.MapFrom(p => p.Observaciones_rubros))
                    .ForMember(dest => dest.ProTeatro, source => source.MapFrom(p => p.Pro_teatro))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes != null ? p.Encomienda_Transf_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault() : p.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault()))
                    .ForMember(dest => dest.tipoAnexo, source => source.MapFrom(p => p.tipo_anexo))
                    .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes));

                cfg.CreateMap<SSIT_DocumentosAdjuntos, SSITDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosSistemaDTO, source => source.MapFrom(p => p.TiposDeDocumentosSistema))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));

                cfg.CreateMap<SSITDocumentosAdjuntosDTO, SSIT_DocumentosAdjuntos>()
                    .ForAllMembers(dest => dest.Ignore());

                #region "SSIT_Solicitudes_Ubicaciones"
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones, SSITSolicitudesUbicacionesDTO>()
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubtipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalUbicacion, source => source.MapFrom(p => p.deptoLocal_ubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesPropiedadHorizontalDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesPuertasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Puertas))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.ZonasPlaneamientoDTO, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.UbicacionesDTO, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDistritosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Distritos))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesMixturasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Mixturas));
                //.ForMember(dest => dest.UbicacionesPropiedadhorizontalDTO, source => source.Ignore());

                cfg.CreateMap<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>()
                   .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal"
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal, SSITSolicitudesUbicacionesPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdSolicitudPropiedadHorizontal, source => source.MapFrom(p => p.id_solicitudprophorizontal))
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                    .ForMember(dest => dest.UbicacionesPropiedadhorizontalDTO, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Ubicaciones_Puertas"
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Puertas, SSITSolicitudesUbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdSolicitudPuerta, source => source.MapFrom(p => p.id_solicitudpuerta))
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle));

                cfg.CreateMap<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SubTipoExpediente"
                cfg.CreateMap<SubtipoExpediente, SubTipoExpedienteDTO>()
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpediente, source => source.MapFrom(p => p.cod_subtipoexpediente))
                    .ForMember(dest => dest.DescripcionSubTipoExpediente, source => source.MapFrom(p => p.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpedienteWs, source => source.MapFrom(p => p.cod_subtipoexpediente_ws));

                cfg.CreateMap<SubTipoExpedienteDTO, SubtipoExpediente>()
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente, source => source.MapFrom(p => p.CodSubTipoExpediente))
                    .ForMember(dest => dest.descripcion_subtipoexpediente, source => source.MapFrom(p => p.DescripcionSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente_ws, source => source.MapFrom(p => p.CodSubTipoExpedienteWs));
                #endregion
                #region "SubTiposDeUbicacion"
                cfg.CreateMap<SubTipoUbicacionesDTO, SubTiposDeUbicacion>()
                    .ForAllMembers(dest => dest.Ignore());


                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                   .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));
                #endregion

                #region "TipoEstadoSolicitud"
                cfg.CreateMap<TipoEstadoSolicitud, TipoEstadoSolicitudDTO>();

                cfg.CreateMap<TipoEstadoSolicitudDTO, TipoEstadoSolicitud>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "TipoExpediente"
                cfg.CreateMap<TipoExpediente, TipoExpedienteDTO>()
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpediente, source => source.MapFrom(p => p.cod_tipoexpediente))
                    .ForMember(dest => dest.DescripcionTipoExpediente, source => source.MapFrom(p => p.descripcion_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpedienteWs, source => source.MapFrom(p => p.cod_tipoexpediente_ws));

                cfg.CreateMap<TipoExpedienteDTO, TipoExpediente>()
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente, source => source.MapFrom(p => p.CodTipoExpediente))
                    .ForMember(dest => dest.descripcion_tipoexpediente, source => source.MapFrom(p => p.DescripcionTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente_ws, source => source.MapFrom(p => p.CodTipoExpedienteWs));
                #endregion
                #region "TipoTramite"
                cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                    .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                    .ForMember(dest => dest.CodTipoTramiteWs, source => source.MapFrom(p => p.cod_tipotramite_ws));

                cfg.CreateMap<TipoTramiteDTO, TipoTramite>()
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite, source => source.MapFrom(p => p.CodTipoTramite))
                    .ForMember(dest => dest.descripcion_tipotramite, source => source.MapFrom(p => p.DescripcionTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite_ws, source => source.MapFrom(p => p.CodTipoTramiteWs));
                #endregion
                #region "TiposDeDocumentosSistema"
                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>().ReverseMap();
                #endregion
                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>().ReverseMap();
                #endregion
                #region "TiposDeUbicacion"
                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>().ReverseMap()
                    //.ForAllMembers(dest => dest.Ignore());
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));

                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion));
                #endregion
                #region "Ubicaciones"
                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                     .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                     .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "Ubicaciones_PropiedadHorizontal"
                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>()
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "Zonas_Planeamiento"
                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Ubicaciones_Mixturas"
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Mixturas, SSITSolicitudesUbicacionesMixturasDTO>()
                .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura));

                cfg.CreateMap<SSITSolicitudesUbicacionesMixturasDTO, SSIT_Solicitudes_Ubicaciones_Mixturas>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Ubicaciones_Distritos"
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Distritos, SSITSolicitudesUbicacionesDistritoDTO>()
                .ForMember(dest => dest.UbicacionesCatalogoDistritosDTO, source => source.MapFrom(p => p.Ubicaciones_CatalogoDistritos));

                cfg.CreateMap<SSITSolicitudesUbicacionesDistritoDTO, SSIT_Solicitudes_Ubicaciones_Distritos>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "Ubicaciones_ZonasMixtura"
                cfg.CreateMap<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>()
                     .ForMember(dest => dest.IdZona, source => source.MapFrom(p => p.IdZonaMixtura));
                cfg.CreateMap<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>()
                     .ForMember(dest => dest.IdZonaMixtura, source => source.MapFrom(p => p.IdZona));
                #endregion
                #region "Ubicaciones_CatalogoDistritos"
                cfg.CreateMap<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>();
                cfg.CreateMap<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>();
                #endregion
                #region "Encomienda_SSIT_Solicitudes"
                cfg.CreateMap<Encomienda_SSIT_Solicitudes, EncomiendaSSITSolicitudesDTO>()
                .ForMember(dest => dest.EncomiendaDTO, source => source.MapFrom(p => p.Encomienda));

                cfg.CreateMap<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Baja"
                cfg.CreateMap<SSIT_Solicitudes_Baja, SSIT_Solicitudes_BajaDTO>().ReverseMap();
                #endregion

                cfg.CreateMap<Encomienda_RubrosCN_DepositoDTO, Encomienda_RubrosCN_Deposito>().ReverseMap();
                cfg.CreateMap<RubrosCNDTO, RubrosCN>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();

                cfg.CreateMap<SSITSolicitudesTitularesPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));

                cfg.CreateMap<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));

                cfg.CreateMap<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));

                cfg.CreateMap<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));
            });

            mapperBase = config.CreateMapper();
        }

        public bool isRubroCur(int idSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            return repo.isRubroCur(idSolicitud);
        }

        public string GetMixDistritoZonaySubZonaBySolicitud(int idSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                return repo.GetMixDistritoZonaySubZonaBySolicitud(idSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarServidumbreDePaso(SSITSolicitudesDTO sol, bool value)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new SSITSolicitudesRepository(unitOfWork);
                var solicitudEntity = repo.Single(sol.IdSolicitud);

                solicitudEntity.Servidumbre_paso = value;

                repo.Update(solicitudEntity);
                unitOfWork.Commit();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIDSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesDTO> GetListaIdSolicitud(List<int> listIDSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetListaIdSolicitud(listIDSolicitud);
                var solicitudesDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes>, IEnumerable<SSITSolicitudesDTO>>(elements);
                return solicitudesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<SSITSolicitudesDTO> GetRangoIdSolicitud(int id_solicitudDesde, int id_solicitudHasta)
        {
            try
            {
                int inicio = id_solicitudDesde;
                int fin = id_solicitudHasta;

                if (id_solicitudDesde >= id_solicitudHasta)
                {
                    inicio = id_solicitudHasta;
                    fin = id_solicitudDesde;
                }

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRangoIdSolicitud(inicio, fin);
                var solicitudesDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes>, IEnumerable<SSITSolicitudesDTO>>(elements);
                return solicitudesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SSITSolicitudesDTO Single(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitud);

                //var entityDto = MapperBaseEncomienda.Map<SSIT_Solicitudes, SSITSolicitudesDTO>(entity);
                var entityDto = mapperBase.Map<SSIT_Solicitudes, SSITSolicitudesDTO>(entity);
                if (entityDto != null)
                {
                    switch (entityDto.IdTipoTramite)
                    {
                        case (int)Constantes.TipoDeTramite.Habilitacion:
                            entityDto.TipoTramiteDescripcion = "Habilitación";
                            break;
                        case (int)Constantes.TipoDeTramite.Transferencia:
                            entityDto.TipoTramiteDescripcion = "Transferencia";
                            break;
                        case (int)Constantes.TipoDeTramite.Ampliacion:
                            entityDto.TipoTramiteDescripcion = "Ampliación de rubro y/o superficie";
                            break;
                        case (int)Constantes.TipoDeTramite.RedistribucionDeUso:
                            entityDto.TipoTramiteDescripcion = "Redistribución de Uso";
                            break;

                        case (int)Constantes.TipoDeTramite.RectificatoriaHabilitacion:
                            entityDto.TipoTramiteDescripcion = "Rectificatoria de Habilitación";
                            break;
                    }
                    switch (entityDto.IdTipoExpediente)
                    {
                        case (int)Constantes.TipoDeExpediente.Simple:
                            entityDto.TipoExpedienteDescripcion = "Simple";
                            break;
                        case (int)Constantes.TipoDeExpediente.Especial:
                            entityDto.TipoExpedienteDescripcion = "Especial";
                            break;
                        case (int)Constantes.TipoDeExpediente.NoDefinido:
                            entityDto.TipoExpedienteDescripcion = "Indeterminado";
                            break;
                    }
                    if (entityDto.IdSubTipoExpediente != (int)Constantes.SubtipoDeExpediente.NoDefinido)
                    {
                        switch (entityDto.IdSubTipoExpediente)
                        {
                            case (int)Constantes.SubtipoDeExpediente.InspeccionPrevia:
                                entityDto.TipoExpedienteDescripcion += " Inspección Previa";
                                break;
                            case (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia:
                                entityDto.TipoExpedienteDescripcion += " Habilitación Previa";
                                break;
                            case (int)Constantes.SubtipoDeExpediente.ConPlanos:
                                entityDto.TipoExpedienteDescripcion += " (con planos)";
                                break;
                            case (int)Constantes.SubtipoDeExpediente.SinPlanos:
                                entityDto.TipoExpedienteDescripcion += " (sin planos)";
                                break;
                        }
                    }
                }

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public SSITSolicitudesDTO GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<SSIT_Solicitudes, SSITSolicitudesDTO>(elements);
            return elementsDto;
        }

        public SSITSolicitudesDTO GetAnteriorByFKIdEncomienda(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetAnteriorByFKIdEncomienda(id_encomienda);
                var entityDto = mapperBase.Map<SSIT_Solicitudes, SSITSolicitudesDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// devuelve un ItemDirectionDTO con la solicitud y todas su puertas para SSIT
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        public IEnumerable<ItemDirectionDTO> GetDireccionesSSIT(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lstSolicitudes).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();
                return convertDirecciones(LstDoorsDirection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<ItemDirectionDTO> convertDirecciones(List<ItemPuertaEntity> LstDoorsDirection)
        {
            List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

            int id_solicitud_ant = 0;
            string calle_ant = "";
            string Direccion_armada = "";
            string Local = "";
            string Depto = "";
            string Torre = "";
            string Otros = "";


            int[] arrSol = LstDoorsDirection.Select(t => t.id_solicitud).Distinct().ToArray();

            foreach (var idSol in arrSol)
            {
                id_solicitud_ant = idSol;

                calle_ant = "";
                Direccion_armada = "";

                Local = "";
                Depto = "";
                Torre = "";
                Otros = "";

                int?[] arrIdUbic = LstDoorsDirection.Where(t => t.id_solicitud == idSol).Select(t => t.idUbicacion).Distinct().ToArray();

                SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();

                foreach (var idubicacion in arrIdUbic)
                {
                    var ubicacionDTO = LstDoorsDirection.Where(x => x.idUbicacion == idubicacion && x.id_solicitud == idSol).ToList();
                    foreach (var direccion in ubicacionDTO)
                    {
                        int idUbi = direccion.idUbicacion ?? 0;

                        //if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idUbi))
                        //{
                        //    direccion.puerta += "t";
                        //}
                        if (Direccion_armada.Length == 0)
                        {
                            Direccion_armada += direccion.calle + " " + direccion.puerta;
                        }
                        else
                        {
                            if (calle_ant == direccion.calle)
                            {
                                Direccion_armada += " / " + direccion.puerta;
                            }
                            else
                            {
                                Direccion_armada += " - " + direccion.calle + " " + direccion.puerta;
                            }
                        }
                        calle_ant = direccion.calle;
                        Local = string.IsNullOrEmpty(direccion.local != null ? direccion.local.Trim() : "") ? "" : " Local: " + direccion.local.Trim();
                        Depto = string.IsNullOrEmpty(direccion.depto != null ? direccion.depto.Trim() : "") ? "" : " Depto: " + direccion.depto.Trim();
                        Torre = string.IsNullOrEmpty(direccion.torre != null ? direccion.torre.Trim() : "") ? "" : " Torre: " + direccion.torre.Trim();
                        Otros = string.IsNullOrEmpty(direccion.otros != null ? direccion.otros.Trim() : "") ? "" : " " + direccion.otros.Trim();

                    }


                    calle_ant = string.Empty;
                    Direccion_armada += Local + Depto + Torre + Otros;

                }
                if (Direccion_armada.Length > 0)
                {
                    ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                    itemDireccion.id_solicitud = id_solicitud_ant;
                    itemDireccion.direccion = Direccion_armada;
                    lstDirecciones.Add(itemDireccion);
                    Direccion_armada = "";
                }
            }
            return lstDirecciones;

        }

        public string GetDireccionSSIT(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lstSolicitudes).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();


                string calle_ant = "";
                string Direccion_armada = "";
                int? id_ubicacion_ant = 0;
                string Local = "";
                string Depto = "";
                string Torre = "";
                string Otros = "";

                if (LstDoorsDirection.Count() > 0)
                {

                    calle_ant = LstDoorsDirection[0].calle;
                }

                foreach (var puerta in LstDoorsDirection)
                {
                    if (id_ubicacion_ant != puerta.id_solicitud)
                    {
                        Direccion_armada += Local + Depto + Torre + Otros;
                        Local = string.Empty;
                        Depto = string.Empty;
                        Torre = string.Empty;
                        Otros = string.Empty;

                    }

                    Local = string.IsNullOrEmpty(puerta.local != null ? puerta.local.Trim() : "") ? "" : " Local: " + puerta.local.Trim();
                    Depto = string.IsNullOrEmpty(puerta.depto != null ? puerta.depto.Trim() : "") ? "" : " Depto: " + puerta.depto.Trim();
                    Torre = string.IsNullOrEmpty(puerta.torre != null ? puerta.torre.Trim() : "") ? "" : " Torre: " + puerta.torre.Trim();
                    Otros = string.IsNullOrEmpty(puerta.otros != null ? puerta.otros.Trim() : "") ? "" : puerta.otros.Trim();

                    if (Direccion_armada.Length == 0 || puerta.calle != calle_ant)
                    {
                        if (Direccion_armada.Length > 0)
                            Direccion_armada += " -";

                        Direccion_armada += puerta.calle + " " + puerta.puerta;
                    }
                    else
                    {
                        Direccion_armada += " / " + puerta.puerta;
                    }

                    calle_ant = puerta.calle;
                    id_ubicacion_ant = puerta.idUbicacion;

                }

                Direccion_armada += Local + Depto + Torre + Otros;

                return Direccion_armada;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public int Insert(SSITSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);


                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRepository(unitOfWork);
                    SSITSolicitudesOrigenRepository repoAmp = new SSITSolicitudesOrigenRepository(unitOfWork);

                    var elementEntitySol = mapperBase.Map<SSITSolicitudesDTO, SSIT_Solicitudes>(objectDto);
                    var insertSolOk = repo.Insert(elementEntitySol);

                    if(elementEntitySol.FechaLibrado != null)
                    {
                        unitOfWork.Db.SSIT_Solicitudes_Historial_LibradoUso_INSERT(elementEntitySol.id_solicitud, elementEntitySol.FechaLibrado, DateTime.Now, elementEntitySol.CreateUser);
                    }


                    unitOfWork.Commit();
                    objectDto.IdSolicitud = elementEntitySol.id_solicitud;
                    return elementEntitySol.id_solicitud;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITSolicitudesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesDTO, SSIT_Solicitudes>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SSITSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesDTO, SSIT_Solicitudes>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region acciones
        public bool anularSolicitud(int id_solicitud, Guid userid)
        {
            bool confirmar = false;
            SSITSolicitudesDTO sol = Single(id_solicitud);
            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU)
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ANU;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    Update(sol);

                    EngineBL blEng = new EngineBL();

                    SGITramitesTareasDTO tramite = blEng.GetUltimaTareaHabilitacionAbierta(sol.IdSolicitud);
                    int id_tarea;
                    int idTramiteTarea;
                    int idResultado;

                    if (tramite != null)
                    {
                        idResultado = (int)Constantes.ENG_Resultados.SolicitudAnulada;
                        blEng.FinalizarTarea(tramite.IdTramiteTarea, idResultado, 0, userid);
                        id_tarea = blEng.getTareaFinTramite(sol.IdSolicitud);
                        idTramiteTarea = blEng.CrearTarea(sol.IdSolicitud, id_tarea, userid);
                        idResultado = (int)Constantes.ENG_Resultados.Realizado;
                        blEng.FinalizarTarea(idTramiteTarea, idResultado, 0, userid);
                    }
                    unitOfWork.Commit();
                }
                confirmar = true;
            }
            return confirmar;
        }
        public bool confirmarSolicitud(int id_solicitud, Guid userid)
        {
            bool confirmar = false;
            SSITSolicitudesDTO sol = Single(id_solicitud);
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    Update(sol);
                    unitOfWork.Commit();
                }
                confirmar = true;

            }
            return confirmar;
        }

        public bool presentarSolicitud(int id_solicitud, Guid userid, byte[] oblea, String emailUsuario)
        {
            bool presento = false;
            //if (ValidacionSolicitudes(id_solicitud))
            //{
            ws_Encuesta encuesta = null;
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new SSITSolicitudesRepository(unitOfWork);

                bool estaLibrado = false;

                var solicitudEntity = repo.Single(id_solicitud);

                var encomiendasEntity = solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda);

                var listEnc = encomiendasEntity.Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).ToList();

                if (!listEnc.Any()) throw new Exception("El trámite no posee anexos técnicos aprobados.");

                var encomienda = listEnc.OrderByDescending(x => x.id_encomienda).First();

                EncomiendaBL encbl = new EncomiendaBL();
                EncomiendaUbicacionesBL encubicbl = new EncomiendaUbicacionesBL();
                ParametrosBL paramBL = new ParametrosBL();
                EncomiendaRubrosCNBL rubCNbl = new EncomiendaRubrosCNBL();

                int nroSolReferencia = 0;
                int.TryParse(paramBL.GetParametroChar("NroSolicitudReferencia"), out nroSolReferencia);

                if (id_solicitud < nroSolReferencia)
                    encbl.Encomienda_Actualizar_TipoSubtipo_Expediente(encomienda);
                else
                    rubCNbl.ActualizarSubTipoExpediente(encomienda.id_encomienda);

                solicitudEntity = repo.Single(id_solicitud);

                encomiendasEntity = solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda);

                #region TildeServidumbre de Paso
                if (solicitudEntity.id_estado == (int)Constantes.TipoTramite.AMPLIACION ||
                    solicitudEntity.id_estado == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    solicitudEntity.Servidumbre_paso = encomienda.Encomienda_Ubicaciones.Count > 1;
                }
                #endregion

                //Pone todas las observaciones en historicas
                SGITareaCalificarObsGrupoRepository repoObserGrupo = new SGITareaCalificarObsGrupoRepository(unitOfWork);
                SGITareaCalificarObsDocsRepository repoObser = new SGITareaCalificarObsDocsRepository(unitOfWork);
                var listGrupoObs = repoObserGrupo.GetByFKIdSolicitud(id_solicitud);
                foreach (var gru in listGrupoObs)
                {
                    var listObser = repoObser.GetByFKIdObs(gru.id_ObsGrupo);
                    foreach (var obs in listObser)
                    {
                        if (obs.Actual.Value)
                        {
                            obs.Actual = false;
                            repoObser.Update(obs);
                        }
                    }
                }
                EngineBL blEng = new EngineBL();
                if (solicitudEntity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                    || solicitudEntity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                {
                    // ------------------------------------------------------------------------
                    // --Se establece la fecha de librado al uso, oblea y se envia el mail si es que no tiene normativa
                    //------------------------------------------------------------------------

                    var repoNor = new EncomiendaNormativasRepository(unitOfWork);
                    var listNor = repoNor.GetByFKIdEncomienda(encomienda.id_encomienda);
                    //aqui..
                    if (listNor.Count() == 0 && !encubicbl.PoseeDistritosU(encomienda.id_encomienda) &&
                        !encubicbl.EsInmuebleCatalogado(encomienda.id_encomienda))
                    {
                        itemRepo = new ItemDirectionRepository(unitOfWork);
                        List<int> lisSol = new List<int>();
                        lisSol.Add(id_solicitud);
                        List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lisSol).ToList();
                        var listU = convertDirecciones(LstDoorsDirection);

                        string Direccion = listU.First().direccion;

                        if (solicitudEntity.FechaLibrado == null &&
                            solicitudEntity.id_subtipoexpediente != (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia &&
                            !TienePlanoDeIncendio(id_solicitud) && !AcogeBeneficiosUERESGP(id_solicitud))
                        {
                            solicitudEntity.FechaLibrado = DateTime.Now;
                            estaLibrado = true;
                            encuesta = getEncuesta(solicitudEntity, Direccion);
                        }

                        #region envio mail y notificacion
                        if (solicitudEntity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                        {
                            MailMessages mailer = new MailMessages();
                            string htmlBody = mailer.MailDisponibilzarQR(id_solicitud);
                            string asunto = "Solicitud de trámite N°: " + id_solicitud + " - " + Direccion;

                            EmailServiceBL mailService = new EmailServiceBL();
                            EmailEntity emailEntity = new EmailEntity();
                            emailEntity.Email = emailUsuario;
                            emailEntity.Html = htmlBody;
                            emailEntity.Asunto = asunto;
                            emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                            emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.WebSGI_AprobacionDG;
                            emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                            emailEntity.CantIntentos = 3;
                            emailEntity.CantMaxIntentos = 3;
                            emailEntity.FechaAlta = DateTime.Now;
                            emailEntity.Prioridad = 1;

                            int idMail = mailService.SendMail(emailEntity);
                            SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();

                            var solDTO = mapperBase.Map<SSITSolicitudesDTO>(solicitudEntity);
                            int idMotivoNotificacion = (int)Constantes.MotivosNotificaciones.InicioHabilitación;
                            notifBL.InsertNotificacionByIdSolicitud(solDTO, idMail, idMotivoNotificacion);
                        }
                        #endregion
                    }

                    #region oblea

                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    var repoDoc = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    string arch = "Oblea-" + id_solicitud.ToString() + ".pdf";
                    int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;

                    var DocAdj = repoDoc.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

                    if (DocAdj == null)
                    {
                        int id_file = esf.addFile(arch, oblea);
                        DocAdj = new SSIT_DocumentosAdjuntos();
                        DocAdj.id_solicitud = id_solicitud;
                        DocAdj.id_tdocreq = 0;
                        DocAdj.tdocreq_detalle = "";
                        DocAdj.generadoxSistema = true;
                        DocAdj.CreateDate = DateTime.Now;
                        DocAdj.CreateUser = userid;
                        DocAdj.nombre_archivo = arch;
                        DocAdj.id_file = id_file;
                        DocAdj.id_tipodocsis = id_tipodocsis;
                        repoDoc.Insert(DocAdj);
                    }
                    #endregion

                    #region SetearFechasPresentadoAdjuntos&Planos
                    SSITDocumentosAdjuntosBL ssitDocAdjBL = new SSITDocumentosAdjuntosBL();
                    EncomiendaDocumentosAdjuntosBL encDocAdjBL = new EncomiendaDocumentosAdjuntosBL();
                    EncomiendaPlanosBL encPlajBL = new EncomiendaPlanosBL();

                    ssitDocAdjBL.SetFechaPresentado(solicitudEntity);
                    encDocAdjBL.SetFechaPresentado(solicitudEntity);
                    encPlajBL.SetFechaPresentado(solicitudEntity);
                    #endregion
                }

                int id_estado_ant = solicitudEntity.id_estado;

                //Actualizo el tramite

                solicitudEntity.LastUpdateUser = userid;
                solicitudEntity.LastUpdateDate = DateTime.Now;

                bool PerteneceJava = string.IsNullOrEmpty(solicitudEntity.NroExpediente) ? false : solicitudEntity.NroExpediente.Length > 0;

                if (id_solicitud <= Constantes.SOLICITUDES_NUEVAS_MAYORES_A && PerteneceJava)
                {
                    solicitudEntity.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.PING;
                    repo.Update(solicitudEntity);
                    if (estaLibrado)
                    {
                        unitOfWork.Db.SSIT_Solicitudes_Historial_LibradoUso_INSERT(solicitudEntity.id_solicitud, solicitudEntity.FechaLibrado, DateTime.Now, solicitudEntity.CreateUser);
                    }
                }
                else
                {
                    if (id_estado_ant != (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN)
                        solicitudEntity.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    repo.Update(solicitudEntity);
                    if (estaLibrado)
                    {
                        unitOfWork.Db.SSIT_Solicitudes_Historial_LibradoUso_INSERT(solicitudEntity.id_solicitud, solicitudEntity.FechaLibrado, DateTime.Now, solicitudEntity.CreateUser);
                    }
                    if (id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                    {
                        SGITramitesTareasDTO tramite = blEng.GetUltimaTareaHabilitacionAbierta(solicitudEntity.id_solicitud);

                        int id_tarea;
                        int idTramiteTarea;

                        if (tramite == null)
                        {
                            id_tarea = blEng.getTareaSolicitudHabilitacion(solicitudEntity.id_solicitud);
                            idTramiteTarea = blEng.CrearTarea(solicitudEntity.id_solicitud, id_tarea, userid);
                        }
                        else
                        {
                            id_tarea = tramite.IdTarea;
                            idTramiteTarea = tramite.IdTramiteTarea;
                        }
                        int idResultado = (int)Constantes.ENG_Resultados.Realizado;
                        //busco la proxima tarea
                        var tareasSig = blEng.GetTareasSiguientes(id_tarea, idResultado, idTramiteTarea);

                        //Tomo la primera
                        int idProximaTarea = tareasSig.Count() > 0 ? tareasSig.First().id_tarea : 0;
                        //Esto es por la adaptacion de los circuito
                        //Si es calificar pero no existe una anterior va a asignacion del calificador
                        if (blEng.isCalificar(idProximaTarea)
                                                && blEng.GetTareaHabilitacion(solicitudEntity.id_solicitud, idProximaTarea).Count() == 0)
                        {
                            var id_circuito = blEng.GetIdCircuito(idProximaTarea);
                            string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_AsginacionCalificador;
                            idProximaTarea = blEng.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
                        }

                        blEng.FinalizarTarea(idTramiteTarea, idResultado, idProximaTarea, userid);

                    }
                    else if (id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                            || id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN
                            )
                    {
                        SGITramitesTareasDTO tramite = blEng.GetUltimaTareaHabilitacionAbierta(solicitudEntity.id_solicitud);
                        if (tramite == null)
                            throw new Exception(Errors.SSIT_SOLICITUD_SIN_TAREA_ABIERTA);
                        int idResultado = (int)Constantes.ENG_Resultados.Realizado;

                        //hay que chequear si cambio de circuito
                        int id_cicuito_sol = blEng.GetIdCircuitoBySolicitud(solicitudEntity.id_solicitud);
                        int id_cicuito_tarea = blEng.GetIdCircuito(tramite.IdTarea);
                        if (id_cicuito_sol == id_cicuito_tarea)
                        {
                            //busco la proxima tarea
                            var tareasSig = blEng.GetTareasSiguientes(tramite.IdTarea, idResultado, tramite.IdTramiteTarea);

                            //Tomo la primera
                            int idProximaTarea = tareasSig.Count() > 0 ? tareasSig.First().id_tarea : 0;

                            //Esto es por la adaptacion de los circuito
                            //Si es calificar pero no existe una anterior va a asignacion del calificador
                            SGITramitesTareasDTO transtarea = null;
                            bool isCalificar = blEng.isCalificar(idProximaTarea);
                            if (isCalificar)
                            {
                                transtarea = blEng.GetTareaHabilitacionCalificar(solicitudEntity.id_solicitud).OrderByDescending(x => x.IdTramiteTarea).FirstOrDefault();
                                if (transtarea == null)
                                {
                                    var id_circuito = blEng.GetIdCircuitoBySolicitud(solicitudEntity.id_solicitud);
                                    string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_AsginacionCalificador;
                                    idProximaTarea = blEng.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
                                }
                            }

                            int idTramiteTarea = blEng.FinalizarTarea(tramite.IdTramiteTarea, idResultado, idProximaTarea, userid);

                            //Si es calificar asigno el calificador anterior
                            if (isCalificar)
                            {
                                if (transtarea != null)
                                {
                                    blEng.AsignarTarea(idTramiteTarea, transtarea.UsuarioAsignadoTramiteTarea.Value, unitOfWork);
                                }
                            }
                        }
                        else
                        {
                            //se finaliza la ultima tarea y se crea la tarea de asignar
                            int idProximaTarea = 0;
                            if (id_cicuito_sol == (int)Constantes.ENG_Circuitos.ESCU_HP || id_cicuito_sol == (int)Constantes.ENG_Circuitos.ESCU_IP)
                                idProximaTarea = blEng.getTareaGenerarExpediente(solicitudEntity.id_solicitud);
                            else
                                idProximaTarea = blEng.getTareaAsignacionCalificador(solicitudEntity.id_solicitud);

                            int idTramiteTarea = blEng.FinalizarTarea(tramite.IdTramiteTarea, idResultado, idProximaTarea, userid);
                        }
                    }
                }
                unitOfWork.Commit();
                presento = true;
            }
            if (encuesta != null && presento)
            {
                ExternalServiceEncuesta service = new ExternalServiceEncuesta();
                service.enviar(encuesta);
            }

            //}
            return presento;
        }

        public ws_Encuesta getEncuesta(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var solicitudEntity = repo.Single(id_solicitud);
            itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
            List<int> lisSol = new List<int>();
            lisSol.Add(id_solicitud);
            List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lisSol).ToList();
            var listU = convertDirecciones(LstDoorsDirection);

            string Direccion = listU.First().direccion;
            return getEncuesta(solicitudEntity, Direccion);
        }

        private ws_Encuesta getEncuesta(SSIT_Solicitudes solicitudEntity, string Direccion)
        {
            ws_Encuesta encuesta = new ws_Encuesta();

            if (solicitudEntity.SSIT_Solicitudes_Titulares_PersonasFisicas.Count() > 0)
            {
                var tit = solicitudEntity.SSIT_Solicitudes_Titulares_PersonasFisicas.First();
                encuesta.apellido = tit.Apellido;
                encuesta.nombre = tit.Nombres;
                encuesta.email = tit.Email;
            }
            else
            {
                var tit = solicitudEntity.SSIT_Solicitudes_Titulares_PersonasJuridicas.First();
                var fir = tit.SSIT_Solicitudes_Firmantes_PersonasJuridicas.First();
                encuesta.apellido = fir.Apellido;
                encuesta.nombre = fir.Nombres;
                encuesta.email = tit.Email;
            }
            encuesta.domicilio = Direccion;
            encuesta.f_ingreso = solicitudEntity.CreateDate;
            encuesta.f_liberado = solicitudEntity.FechaLibrado;
            encuesta.solicitud = solicitudEntity.id_solicitud.ToString();
            encuesta.tTramite = solicitudEntity.TipoExpediente.cod_tipoexpediente + " " + solicitudEntity.SubtipoExpediente.cod_subtipoexpediente;
            encuesta.f_fin = solicitudEntity.Fecha_Habilitacion;
            encuesta.tEstablecimiento = "";
            encuesta.un_transporte = "";
            return encuesta;
        }
        private Constantes.BUI_EstadoPago GetEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            Constantes.BUI_EstadoPago ret = Constantes.BUI_EstadoPago.SinPagar;
            var repoParam = new ParametrosRepository(this.uowF.GetUnitOfWork());

            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                servicio.Url = repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                var lstBUIsCAA = servicio.Get_BUIs_CAA(repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User"),
                    repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password"), id_solicitud, ref ws_resultado_BUI);

                servicio.Dispose();


                if (ws_resultado_BUI.ErrorCode != 0)
                {
                    throw new Exception("No se ha podido recuperar las BUI/s relacionadas al CAA.");
                }
                else
                {
                    if (lstBUIsCAA.Count() > 0)
                    {
                        if (lstBUIsCAA.Count(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado) > 0)
                            ret = Constantes.BUI_EstadoPago.Pagado;
                        else
                        {
                            ret = (Constantes.BUI_EstadoPago)lstBUIsCAA.LastOrDefault().EstadoId;
                        }
                    }
                }
            }
            else
            {
                ExternalServicePagos servicePagos = new ExternalServicePagos();

                SSITSolicitudesPagosBL blPagos = new SSITSolicitudesPagosBL();
                SGISolicitudesPagosBL pagos = new SGISolicitudesPagosBL();

                List<int> arr_id_pagos = blPagos.GetByFKIdSolicitud(id_solicitud).Select(s => s.id_pago).ToList();
                arr_id_pagos.AddRange(pagos.GetHab(id_solicitud).Select(s => s.id_pago).ToList());

                var lstBUIsHAB = servicePagos.ObtenerBoletas(arr_id_pagos);

                if (lstBUIsHAB.Count() > 0)
                {
                    if (lstBUIsHAB.Count(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado) > 0)
                        ret = Constantes.BUI_EstadoPago.Pagado;
                    else
                        ret = (Constantes.BUI_EstadoPago)lstBUIsHAB.LastOrDefault().EstadoId;
                }

            }
            return ret;
        }

        #endregion
        #region Validaciones
        public bool ValidacionSolicitudes(int id_solicitud)
        {
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var SSITentity = repo.Single(id_solicitud);
            //if (SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
            //    && isEscuela(SSITentity.id_solicitud)
            //    && string.IsNullOrEmpty(SSITentity.NroExpedienteSadeRelacionado))
            //    throw new Exception(Errors.SSIT_SOLICITUD_ESCUELA_SIN_NUMERO_EXPEDIENTE_RELACIONADO_SADE);

            if (SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG)
            {
                #region Validaciones comunes
                var listEnc = SSITentity.Encomienda_SSIT_Solicitudes.Where(x => x.Encomienda.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                                                                             || x.Encomienda.id_estado == (int)Constantes.Encomienda_Estados.Vencida).Select(y => y.Encomienda).ToList();

                var UltimaA = listEnc.Where(x => x.tipo_anexo == Constantes.TipoAnexo_A).OrderByDescending(x => x.id_encomienda).FirstOrDefault();
                var listaEncomiendas = listEnc.Where(x => x.id_encomienda >= UltimaA.id_encomienda).ToList();

                if (listaEncomiendas.Count() == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_TECNICO_INEXISTENTE);

                if (ExisteAnexosEnCurso(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_EN_CURSO);

                var encomienda = listaEncomiendas.OrderByDescending(x => x.id_encomienda).First();

                var ubiClausuras = SSITentity.SSIT_Solicitudes_Ubicaciones
                                        .Any(x => x.Ubicaciones.Ubicaciones_Clausuras
                                                        .Any(w => w.fecha_alta_clausura < DateTime.Now
                                                                    && (w.fecha_baja_clausura > DateTime.Now || w.fecha_baja_clausura == null)));

                var horClausuras = SSITentity.SSIT_Solicitudes_Ubicaciones
                                    .Any(ubic => ubic.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
                                                    .Any(ssitHor => ssitHor.Ubicaciones_PropiedadHorizontal.Ubicaciones_PropiedadHorizontal_Inhibiciones
                                                           .Any(inhi => inhi.fecha_vencimiento > DateTime.Now)));

                if (ubiClausuras || horClausuras)
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_CLAUSURAS);

                var ubiInhibiciones = SSITentity.SSIT_Solicitudes_Ubicaciones.Any(solUbi => solUbi.Ubicaciones.Ubicaciones_Inhibiciones
                                                                                                    .Any(uc => uc.fecha_vencimiento > DateTime.Now));

                var horInhibiciones = SSITentity.SSIT_Solicitudes_Ubicaciones
                                                .Any(solUbi => solUbi.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
                                                            .Any(horUbi => horUbi.Ubicaciones_PropiedadHorizontal.Ubicaciones_PropiedadHorizontal_Inhibiciones
                                                                        .Any(hor => hor.fecha_vencimiento > DateTime.Now)));

                if (ubiInhibiciones || horInhibiciones)
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_INHIBIDAS);
                #endregion

                #region Rubros
                List<int> lstEncomiendasRelacionadas = SSITentity.Encomienda_SSIT_Solicitudes.Select(s => s.id_encomienda).ToList();

                int id_encomienda = listEnc.LastOrDefault().id_encomienda;
                EncomiendaBL encBL = new EncomiendaBL();
                EncomiendaDTO encDTO = encBL.Single(id_encomienda);
                RubrosBL rubBL = new RubrosBL();
                RubrosCNBL rubcnBL = new RubrosCNBL();

                var lstRubros = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());

                var lstRubrosCN = rubcnBL.GetByListCodigo(encDTO.EncomiendaRubrosCNDTO.Select(s => s.CodigoRubro).ToList());

                bool tieneRubroEstadio = lstRubros.Where(x => x.EsEstadio).Any();

                //138775: JADHE 53248 - SGI - REQ ABM Rubros CUR - Documentacion obligatoria
                int nroSolReferencia = 0;
                int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

                if (id_solicitud > nroSolReferencia)
                {
                    var listRubros = lstRubrosCN.Select(r => r.IdRubro).ToList();

                    if (!encBL.ValidarRequerimientosDocumentosRubros(id_solicitud, listRubros))
                    {
                        var listarubrosSinAdjuntos = rubcnBL.GetDescripcionDocumentosFaltantesByRubros(id_solicitud, lstRubrosCN);
                        throw new Exception(string.Format(Errors.SSIT_SOLICITUD_INGRESAR_DOC_RUBROS, listarubrosSinAdjuntos));
                    }
                }

                DtoCAA solicitud_caa = new DtoCAA();
                bool EximirCAA = repo.GetEximir_CAA(SSITentity.id_solicitud, SSITentity.id_tipotramite);

                if (EximirCAA == false && SSITentity.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    solicitud_caa = ValidarCAA(lstEncomiendasRelacionadas, listEnc, encomienda, tieneRubroEstadio);
                    if (solicitud_caa == null)
                    {
                        throw new Exception(Errors.SSIT_SOLICITUD_CAA_INEXISTENTE);
                    }
                }

                if (!CompareWithEncomienda(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_TITULARES_UBICACIONES_DIFERENTES);
                #endregion

                #region Pagos
                var repoDoc = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var listDocSsit = repoDoc.GetByFKIdSolicitud(id_solicitud);
                if (BoletaCeroActiva() == false)
                {
                    if (SSITentity.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    {
                        //0144521: JADHE 56637 - SSIT - RDU del 2018 pide BUI
                        DateTime fechaValida = new DateTime(2020, 1, 1);
                        if (SSITentity.CreateDate > fechaValida)
                            ValidarPagoSSIT(id_solicitud);
                    }

                    if (SSITentity.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    {
                        SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                        var ssitDTO = ssitBL.Single(encDTO.IdSolicitud); //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()

                        int ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.SinExencion;

                        bool tieneRubroProTeatro = lstRubros.Where(x => x.EsProTeatro).Any() ||
                                                   lstRubrosCN.Where(x => x.Codigo == Constantes.RubrosCN.Teatro_Independiente).Any();
                        if (tieneRubroProTeatro)
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.ProTeatro;

                        if (tieneRubroEstadio)
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.Estadio;

                        /*******************************************************************************************
                        // 0139531: JADHE 53779 - SSIT - REQ - Eximir pago BUI Centro Culturales por Sociedad civil
                        // PENDIENTE DE CONFIRMACION POR FALTA DE TIPIFICADO
                        bool esSocCivil = false;
                        SSITSolicitudesTitularesPersonasJuridicasBL persJuridicas = new SSITSolicitudesTitularesPersonasJuridicasBL();
                        var solicPersJuridicas = persJuridicas.GetByFKIdSolicitud(encDTO.IdSolicitud);
                        foreach (var pj in solicPersJuridicas)
                        {
                            if (pj.IdTipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Civil)
                            {
                                esSocCivil = true;
                                break;
                            }
                        }
                        bool tieneRubroCCultural = lstRubros.Where(x => x.EsCentroCultural).Any() ||
                            lstRubrosCN.Where(x => x.Codigo == Constantes.RubrosCN.Centro_Cultural_A ||
                            x.Codigo == Constantes.RubrosCN.Centro_Cultural_B ||
                            x.Codigo == Constantes.RubrosCN.Centro_Cultural_C).Any(); 
                        if (tieneRubroCCultural) // && esSocCivil (Pendiente de confirmacion)
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;

                        ***********************************************************************************************/

                        bool tieneRubroCCultural = lstRubros.Where(x => x.EsCentroCultural).Any();
                        if (tieneRubroCCultural)
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;

                        //Valido que sea una ECI 
                        bool esEci = (encDTO != null && encDTO.EsECI && SSITentity != null && (bool)SSITentity.EsECI);
                        if (esEci)
                        {
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.EsECI;
                        }
                        switch (ExcepcionRubro)
                        {
                            case (int)Constantes.TieneRubroConExencionPago.ProTeatro:
                                bool tieneDocProTeatro = listDocSsit.Any(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES ||
                                                          x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro);
                                if (!tieneDocProTeatro)
                                {
                                    ValidarPagoSSIT(id_solicitud);
                                    if (EximirCAA == false)
                                        ValidarPagoCAA(solicitud_caa);
                                }
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.Estadio:
                                bool tieneChkEstadio = ssitDTO.ExencionPago;
                                if (!tieneChkEstadio)
                                    ValidarPagoSSIT(id_solicitud);

                                if (solicitud_caa.id_solicitud > 0 && EximirCAA == false)
                                    ValidarPagoCAA(solicitud_caa);
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                                bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                                if (!tieneDocCCultural)
                                {
                                    ValidarPagoSSIT(id_solicitud);
                                    if (EximirCAA == false)
                                        ValidarPagoCAA(solicitud_caa);
                                }
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.EsECI:
                                //Valido que solo tenga mas de ese rubro para que no sea excepcion de pago
                                if (encDTO.EncomiendaRubrosCNDTO.Count > 1 && encDTO.IdTipoTramite != (int)Constantes.TipoTramite.HabilitacionECIAdecuacion)
                                {
                                    ValidarPagoSSIT(id_solicitud);
                                    if (EximirCAA == false)
                                        ValidarPagoCAA(solicitud_caa);
                                }
                                else
                                {
                                    ValidarPagoCAA(solicitud_caa);
                                }
                                break;
                            default:
                                ValidarPagoSSIT(id_solicitud);
                                if (EximirCAA == false)
                                    ValidarPagoCAA(solicitud_caa);
                                break;
                        }
                    }
                }
                #endregion

                #region Ubicacion
                //Si es un edificio protegido debe adjuntar la dispo de DGUIUR
                if (SSITentity.SSIT_Solicitudes_Ubicaciones.Where(x => x.Ubicaciones.EsUbicacionProtegida).Any() &&
                    (SSITentity.SSIT_DocumentosAdjuntos.Count <= 0 ||
                    !SSITentity.SSIT_DocumentosAdjuntos.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Disposicion_DGIUR).Any()) &&
                    (encDTO.EncomiendaDocumentosAdjuntosDTO.Count <= 0 ||
                    !encDTO.EncomiendaDocumentosAdjuntosDTO.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Disposicion_DGIUR).Any()))
                    throw new ValidationException(Errors.SSIT_SOLICITUD_UBICACION_PROTEGIDA);

                #endregion

                if (id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
                    ValidacionSolicitudesNuevas(id_solicitud, id_encomienda);
                else
                    ValidacionSolicitudesViejas(SSITentity);


                //Valido si es una eci y tiene el documento Certificación de Espacio Cultural Independiente
                //Busco el codigo del codigo del anexo
                RubrosTiposDeDocReqRepository RubDocReq = new RubrosTiposDeDocReqRepository(this.uowF.GetUnitOfWork());
                var rrDoc = RubDocReq.GetTipoDocumentosRequeridosByCodigoRubro(EncomiendaDTO.CodRubroECI).FirstOrDefault();

                if (
                    (encDTO.EsECI && !listDocSsit.Any(x => x.id_tdocreq == rrDoc.id_tdocreq))
                    ||
                    ((bool)SSITentity.EsECI && !listDocSsit.Any(x => x.id_tdocreq == rrDoc.id_tdocreq))
                    )
                {
                    throw new ValidationException(string.Format(Errors.SSIT_SOLICITUD_ECI_SIN_DOC_ANEXO, id_solicitud));
                }

                return true;
            }
            return false;
        }

        public bool EximidoCAA(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
            var SSITentity = repo.Single(id_solicitud);
            return repo.GetEximir_CAA(SSITentity.id_solicitud, SSITentity.id_tipotramite);
        }

        private DtoCAA ValidarCAA(List<int> lstEncomiendasRelacionadas, List<Encomienda> listEnc, Encomienda encomienda, bool tieneRubroEstadio)
        {
            // se obtiene el ultimo CAA aprobado
            ws_Interface_AGC servicio = new ws_Interface_AGC();
            ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();

            var repoParam = new ParametrosRepository(this.uowF.GetUnitOfWork());
            servicio.Url = repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
            string username_servicio = repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
            string password_servicio = repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
            DtoCAA[] lstDocCAA = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lstEncomiendasRelacionadas.ToArray(), ref ws_resultado_CAA);

            var ultimoCAANoAnulado = lstDocCAA.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Anulado)
                                                .OrderByDescending(o => o.id_estado)
                                                .FirstOrDefault();
            if (tieneRubroEstadio)
            {
                if (ultimoCAANoAnulado != null)
                    return ultimoCAANoAnulado;
                else
                    return null;
            }

            #region 132130: JADHE YYYYY - SSIT - Modificar validacion de CAA
            var ultimoCAAAprobado = lstDocCAA.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado)
                            .OrderByDescending(o => o.id_caa)
                            .FirstOrDefault();

            if (ultimoCAAAprobado != null)
            {
                return ultimoCAAAprobado;
            }
            #endregion

            List<int> estados = new List<int>();
            estados.Add((int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
            estados.Add((int)Constantes.Encomienda_Estados.Vencida);

            var lstEncomiendasAprobadas = listEnc.Where(x => estados.Contains(x.id_estado)).OrderByDescending(o => o.id_encomienda);

            List<int> lstIdEncomiendaValidas = new List<int>(); ;

            foreach (var item in lstEncomiendasAprobadas)
            {
                if (item.tipo_anexo == Constantes.TipoAnexo_A)
                {
                    lstIdEncomiendaValidas.Add(item.id_encomienda);
                    break;
                }
                else
                    lstIdEncomiendaValidas.Add(item.id_encomienda);
            }

            if (!StaticClass.Funciones.isDesarrollo())
            {
                if (!lstDocCAA.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado && lstIdEncomiendaValidas.Contains(x.id_encomienda)).Any())
                {
                    throw new Exception(Errors.SSIT_SOLICITUD_CAA_INEXISTENTE);
                }
            }
            else
            {
                lstDocCAA = new DtoCAA[1];
                return lstDocCAA[0];
            }
            var ret = lstDocCAA.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado && lstIdEncomiendaValidas.Contains(x.id_encomienda))
                                .OrderByDescending(o => o.id_caa)
                                .FirstOrDefault();

            return ret;
        }

        private void ValidacionSolicitudesViejas(SSIT_Solicitudes SSITentity)
        {
            List<int> lstEstadosPosibles = new List<int>();
            lstEstadosPosibles.Add((int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF);
            lstEstadosPosibles.Add((int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO);

            bool PerteneceJava = string.IsNullOrEmpty(SSITentity.NroExpediente) ? false : SSITentity.NroExpediente.Length > 0;

            if (PerteneceJava)
            {
                if (!lstEstadosPosibles.Contains(SSITentity.id_estado))
                    throw new Exception(Errors.SSIT_SOLICITUD_ESTADO_INVALIDO_PRESENTAR);
            }

            var repoObser = new SSITSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
            if (repoObser.ExistenObservacionesdetalleSinProcesar(SSITentity.id_solicitud))
                throw new Exception(Errors.SSIT_SOLICITUD_OBSERVACIONES_SIN_PROCESAR);


        }
        private void ValidacionSolicitudesNuevas(int id_solicitud, int id_encomienda)
        {
            var repoDocEnc = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
            var repoTipoDocReq = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
            var repoDoc = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
            var repoNor = new EncomiendaNormativasRepository(this.uowF.GetUnitOfWork());

            var listNor = repoNor.GetByFKIdEncomienda(id_encomienda);

            if (listNor.Count() > 0)
            {
                var normativa = listNor.LastOrDefault();
                var listTipoDoc = repoTipoDocReq.GetByFKIdTipoNormativa(normativa.id_tiponormativa);

                var listDocSsitNorm = repoDoc.GetByFKIdSolicitud(id_solicitud).Where(x => listTipoDoc.Select(s => s.id_tdocreq).Contains(x.id_tdocreq)).Any();
                var listDocEncNorm = repoDocEnc.GetByFKIdEncomienda(id_encomienda).Where(x => listTipoDoc.Select(s => s.id_tdocreq).Contains(x.id_tdocreq)).Any();

                if (!listDocEncNorm && !listDocSsitNorm)
                    throw new Exception(Errors.SSIT_SOLICITUD_NORMATIVA_ANEXO_SIN_DOCUMENTO);
            }


            var repoObser = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
            if (repoObser.ExistenObservacionesdetalleSinProcesar(id_solicitud))
                throw new Exception(Errors.SSIT_SOLICITUD_OBSERVACIONES_SIN_PROCESAR);
        }

        private void ValidarPagoCAA(DtoCAA caa)
        {
            if (StaticClass.Funciones.isDesarrollo())
            {
                return;
            }

            //139533: JADHE YYYYY -SSIT - Pago excento CAA
            if (caa.ExentoBUI)
            {
                return;
            }

            if (!(caa.id_tipotramite == (int)Constantes.TiposDeTramiteCAA.CAA_ESP))
            {
                var estado_pago = GetEstadoPago(Constantes.PagosTipoTramite.CAA, caa.id_solicitud);

                if (estado_pago != Constantes.BUI_EstadoPago.Pagado)
                    throw new Exception(Errors.SSIT_SOLICITUD_PAGO_CAA);
            }
            else if (caa.CAA_Especiales_Datos_Verificacion == null)
            {
                throw new Exception(Errors.SSIT_SOLICITUD_PAGO_CAA_ESP);
            }
        }

        private void ValidarPagoSSIT(int IdSolicitud)
        {
            if (GetEstadoPago(Constantes.PagosTipoTramite.HAB, IdSolicitud) != Constantes.BUI_EstadoPago.Pagado)
                throw new Exception(Errors.SSIT_SOLICITUD_PAGO);
        }

        public bool ExisteAnexosEnCurso(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var listEnc = repo.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Incompleta
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Completa
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Confirmada
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Ingresada_al_consejo).ToList();
            return listEnc.Count() > 0;
        }
        public bool ExisteAnexosTipoAAprobada(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var listEnc = repo.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                    && x.tipo_anexo == Constantes.TipoAnexo_A).ToList();
            return listEnc.Count() > 0;
        }



        private bool ExisteAnexosNotarialAprobada(List<Encomienda> listEnc)
        {
            int? Ultima_enc_A = listEnc.Where(x => x.tipo_anexo == Constantes.TipoAnexo_A)
                                            .OrderByDescending(o => o.id_encomienda)
                                            .Select(s => s.id_encomienda)
                                            .FirstOrDefault();

            if (Ultima_enc_A.HasValue)
            {
                var lstActa = listEnc.Where(x => x.id_encomienda >= Ultima_enc_A.Value).SelectMany(s => s.wsEscribanos_ActaNotarial);

                if (lstActa.Where(x => x.anulada == false).Any())
                    return true;
                else if (lstActa.Where(x => x.anulada == true).Any())
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_ANULADO);
                else
                    return false;
            }
            else
                return false;

        }
        public bool ExisteAnexosNotarialAprobada(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var listEncAprob = repo.GetByFKIdSolicitud(id_solicitud)
                            .Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                            .ToList();

            int? Ultima_enc_A = listEncAprob.Where(x => x.tipo_anexo == Constantes.TipoAnexo_A)
                                            .OrderByDescending(o => o.id_encomienda)
                                            .Select(s => s.id_encomienda)
                                            .FirstOrDefault();

            if (Ultima_enc_A.HasValue)
                return listEncAprob.Where(x => x.id_encomienda >= Ultima_enc_A.Value).SelectMany(s => s.wsEscribanos_ActaNotarial).Where(an => an.anulada == false).Any();
            else
                return false;
        }
        public bool CompareWithEncomienda(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
            var compare = repo.CompareWithEncomienda(id_solicitud);
            return compare;
        }

        public bool CompareTitularesWithSolicitud(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
            var compare = repo.CompareTitularesWithEncomienda(id_solicitud);
            return compare;
        }

        #endregion
        public bool isProTeatro(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var listEnc = repo.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).ToList();
            if (listEnc.Count() == 0)
                return false;
            var encomienda = listEnc.OrderByDescending(x => x.id_encomienda).First();
            return encomienda.Pro_teatro;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string ActualizarEstado(int IdSolicitud, Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var sol = repo.Single(IdSolicitud);

            if (sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                var hasTitulares = sol.SSIT_Solicitudes_Firmantes_PersonasFisicas.Any() || sol.SSIT_Solicitudes_Firmantes_PersonasJuridicas.Any();
                if (!hasTitulares)
                {
                    return "Falta declarar el/los Titular/es";
                }

                var hasUbicaciones = sol.SSIT_Solicitudes_Ubicaciones.Any();
                if (!hasUbicaciones)
                {
                    return "Falta declarar la/s Ubicacion/es";
                }

                if (sol.id_tipotramite == (int)Constantes.TipoTramite.PERMISO
                    && sol.id_tipoexpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                {
                    if (sol.SSIT_Solicitudes_DatosLocal == null)
                        return "Falta declarar los datos del local";

                    if (!sol.SSIT_Solicitudes_RubrosCN.Any())
                        return "Falta declarar los Rubros";
                }


                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRepository(unitOfWorkTran);
                    sol = repo.Single(IdSolicitud);
                    sol.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.COMP;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    repo.Update(sol);
                    unitOfWorkTran.Commit();
                }

                if (sol.idTAD != null)
                {
                    ParametrosRepository parametrosRepo = new ParametrosRepository(this.uowF.GetUnitOfWork());

                    string _urlESB = parametrosRepo.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosRepo.GetParametroChar("Trata.Habilitacion");
                    if (sol.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION)
                        trata = parametrosRepo.GetParametroChar("Trata.Ampliacion");
                    else if (sol.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        trata = parametrosRepo.GetParametroChar("Trata.RedistribucionDeUso");

                    string _noESB = parametrosRepo.GetParametroChar("SSIT.NO.ESB");
                    bool.TryParse(_noESB, out bool noESB);

                    itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                    List<int> lisSol = new List<int>();
                    lisSol.Add(IdSolicitud);
                    List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lisSol).ToList();
                    var listU = convertDirecciones(LstDoorsDirection);

                    string Direccion = listU.First().direccion;
                    if (!noESB)
                    {
                        enviarActualizacionTramite(_urlESB, sol, trata, Direccion);
                    }

                }
            }
            else if (sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN)
            {
                var listEncomiendas = sol.Encomienda_SSIT_Solicitudes.Select(y => y.Encomienda).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
                if (listEncomiendas.Any())
                {
                    var encApro = listEncomiendas.OrderByDescending(x => x.id_encomienda).First();
                    if (encApro != null)
                    {
                        if (sol.id_tipoexpediente != encApro.id_tipoexpediente
                        || sol.id_subtipoexpediente != encApro.id_subtipoexpediente)
                        {
                            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                            using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                            {
                                repo = new SSITSolicitudesRepository(unitOfWorkTran);
                                sol = repo.Single(IdSolicitud);
                                sol.id_tipoexpediente = encApro.id_tipoexpediente;
                                sol.id_subtipoexpediente = encApro.id_subtipoexpediente;
                                sol.LastUpdateUser = userid;
                                sol.LastUpdateDate = DateTime.Now;
                                repo.Update(sol);
                                unitOfWorkTran.Commit();
                            }
                        }
                    }
                }
            }
            else if (sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
                    !sol.SSIT_Solicitudes_Ubicaciones.Any())
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRepository(unitOfWorkTran);
                    sol = repo.Single(IdSolicitud);
                    sol.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    repo.Update(sol);
                    unitOfWorkTran.Commit();
                }
            }
            return string.Empty;
        }

        private void enviarActualizacionTramite(string _urlESB, SSIT_Solicitudes sol, string trata, string Direccion)
        {
            try
            {
                wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.id_solicitud, sol.NroExpedienteSade, trata, Direccion);
            }
            catch (Exception)
            {
            }
        }
        public bool SetOblea(int id_solicitud, Guid userid, int id_file, string FileName)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new SSITSolicitudesRepository(unitOfWork);
                var solicitudEntity = repo.Single(id_solicitud);

                var repoDoc = new SSITDocumentosAdjuntosRepository(unitOfWork);
                int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;

                var DocAdj = repoDoc.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

                if (DocAdj == null)
                {
                    #region oblea
                    DocAdj = new SSIT_DocumentosAdjuntos();
                    DocAdj.id_solicitud = id_solicitud;
                    DocAdj.id_tdocreq = 0;
                    DocAdj.tdocreq_detalle = "";
                    DocAdj.generadoxSistema = true;
                    DocAdj.CreateDate = DateTime.Now;
                    DocAdj.CreateUser = userid;
                    DocAdj.nombre_archivo = FileName;
                    DocAdj.id_file = id_file;
                    DocAdj.id_tipodocsis = id_tipodocsis;
                    repoDoc.Insert(DocAdj);

                    #endregion
                    solicitudEntity.FechaLibrado = DateTime.Now;
                    solicitudEntity.LastUpdateUser = userid;
                    solicitudEntity.LastUpdateDate = DateTime.Now;
                    repo.Update(solicitudEntity);

                    unitOfWork.Db.SSIT_Solicitudes_Historial_LibradoUso_INSERT(solicitudEntity.id_solicitud, DateTime.Now , DateTime.Now, userid);

                    unitOfWork.Commit();
                }
            }
            return true;
        }

        public bool isEscuela(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
            EngineBL blEng = new EngineBL();
            bool is_escuela = false;
            try
            {
                var solicitudEntity = repo.Single(id_solicitud);
                int id_circuito = blEng.GetIdCircuitoBySolicitud(id_solicitud);
                is_escuela = id_circuito == (int)Constantes.ENG_Circuitos.ESCU_HP || id_circuito == (int)Constantes.ENG_Circuitos.ESCU_IP;
            }
            catch (Exception e)
            {
                is_escuela = false;
            }
            return is_escuela;
        }

        public bool ProvieneSolicitudAnterior(int id_solicitud)
        {
            bool ret = false;

            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var sol = repo.GetByFKIdSolicitud(id_solicitud).FirstOrDefault();
            ret = sol.SSIT_Solicitudes_Origen != null;

            return ret;
        }
        public List<string> CompareWithCAA(int id_solicitud, DtoCAA solCAA)
        {

            List<string> lstErrores = new List<string>();

            string valorStringCAA = null;
            string valorStringHAB = null;
            string nombreCampo = null;

            uowF = new TransactionScopeUnitOfWorkFactory();
            var repoSSITSolicitudes = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var repoZonasPlaneamiento = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
            var repoPartidasHorizontales = new UbicacionesPropiedadhorizontalRepository(this.uowF.GetUnitOfWork());
            var repoTipoDocumentosPersonal = new TipoDocumentoPersonalRepository(this.uowF.GetUnitOfWork());
            var repoTipoIngresosBrutos = new TiposDeIngresosBrutosRepository(this.uowF.GetUnitOfWork());
            var repoEncomienda = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var repoSolicitudesRubros = new SSITSolicitudesRubrosCNRepository(this.uowF.GetUnitOfWork());
            var repoSolicitudesDatosLocal = new SSITSolicitudesDatosLocalRepository(this.uowF.GetUnitOfWork());

            var sol = repoSSITSolicitudes.Single(id_solicitud);

            ParametrosRepository parametrosRepo = new ParametrosRepository(this.uowF.GetUnitOfWork());
            int nroSolicitudReferencia = Convert.ToInt32(parametrosRepo.GetParametroNum("NroSolicitudReferencia"));

            if (sol != null)
            {

                #region "Comparacion de ubicaciones"
                var lstUbicacionesCAA = solCAA.Ubicaciones.ToList();
                var lstUbicacionesHAB = sol.SSIT_Solicitudes_Ubicaciones.ToList();

                if (lstUbicacionesCAA.Count != lstUbicacionesHAB.Count)
                    lstErrores.Add("La cantidad de ubicaciones de la solicitud de CAA es distinta a la de la solicitud actual.");
                int NroUbicacion = 0;
                foreach (var itemUbicHAB in lstUbicacionesHAB)
                {
                    NroUbicacion++;
                    var itemUbicCAA = lstUbicacionesCAA.FirstOrDefault(x => x.id_ubicacion == itemUbicHAB.id_ubicacion && x.id_subtipoubicacion == itemUbicHAB.id_subtipoubicacion);
                    if (itemUbicCAA == null)
                        //if (itemUbicCAA != null)
                        lstErrores.Add(string.Format("No se encuentra la ubicación {0} en el CAA.", NroUbicacion));
                    else
                    {
                        if (id_solicitud < nroSolicitudReferencia)
                        {
                            //evaluamos la zonificacion (Zona de Planeamiento).
                            if (itemUbicCAA.id_zonaplaneamiento == null)
                            {
                                lstErrores.Add(string.Format("La solicitud no posee el dato zonificación de la ubicación. Número de ubicación: {0}.", NroUbicacion));
                            }
                            else
                            {
                                if (itemUbicCAA.id_zonaplaneamiento != itemUbicHAB.id_zonaplaneamiento)
                                {
                                    var zonaPlaCAA = repoZonasPlaneamiento.Single(itemUbicCAA.id_zonaplaneamiento);
                                    lstErrores.Add(string.Format("La zonificación de la ubicación {0} es diferente, en el CAA es {1} y en la solicitud de HAB es {2}.", NroUbicacion, zonaPlaCAA.CodZonaPla, itemUbicHAB.Zonas_Planeamiento.CodZonaPla));
                                }
                            }

                        }
                        else
                        {
                            // evaluamos mixturas y distritos
                            var lstDistritosHAB = itemUbicHAB.SSIT_Solicitudes_Ubicaciones_Distritos.ToList();
                            var lstMixturasHAB = itemUbicHAB.SSIT_Solicitudes_Ubicaciones_Mixturas.ToList();

                            var cantidadDistritosHAB = (lstDistritosHAB.Select(t => t.IdDistrito).Distinct().Count());
                            var cantidadMixturasHAB = (lstMixturasHAB.Select(t => t.IdZonaMixtura).Distinct().Count());

                            //Comparación de Distritos
                            if (cantidadDistritosHAB != itemUbicCAA.Distritos.Count())
                            {
                                lstErrores.Add(string.Format("La cantidad de Distritos de la ubicación {0} es diferente, en el CAA es/son '{1}' y en la solicitud de HAB es/son '{2}'.", NroUbicacion, itemUbicCAA.Distritos.Count(), lstDistritosHAB.Count()));
                            }
                            else if ((cantidadDistritosHAB == itemUbicCAA.Distritos.Count()))
                            {
                                foreach (var itemDistrito in lstDistritosHAB)
                                {
                                    var itemDistritoCAA = itemUbicCAA.Distritos.FirstOrDefault(x => x.IdDistrito == itemDistrito.IdDistrito);
                                    if (itemDistritoCAA == null)
                                    {
                                        lstErrores.Add(string.Format("El distrito '{1} ' de la ubicación {0} no se encuentra en la solicitud de CAA.", NroUbicacion, itemDistrito.IdDistrito));
                                    }
                                }
                            }

                            //Comparación de Mixturas
                            if ((cantidadMixturasHAB != itemUbicCAA.Mixturas.Count()))
                            {
                                lstErrores.Add(string.Format("La cantidad de Distritos de la ubicación {0} es diferente, en el CAA es/son '{1}' y en la solicitud de HAB es/son '{2}'.", NroUbicacion, itemUbicCAA.Distritos.Count(), lstDistritosHAB.Count()));
                            }
                            else if (cantidadMixturasHAB == itemUbicCAA.Mixturas.Count())
                            {
                                foreach (var itemMixtura in lstMixturasHAB)
                                {
                                    var itemMixturaCAA = itemUbicCAA.Mixturas.FirstOrDefault(x => x.IdZonaMixtura == itemMixtura.IdZonaMixtura);
                                    if (itemMixturaCAA == null)
                                    {
                                        lstErrores.Add(string.Format("La mixtura '{1} ' de la ubicación {0} no se encuentra en la solicitud de CAA.", NroUbicacion, itemMixtura.IdZonaMixtura));
                                    }
                                }
                            }
                        }

                        string local_subtipoubicacion_caa = "";
                        string local_subtipoubicacion_hab = "";
                        if (!string.IsNullOrEmpty(itemUbicCAA.local_subtipoubicacion))
                            local_subtipoubicacion_caa = itemUbicCAA.local_subtipoubicacion.Trim();
                        if (!string.IsNullOrEmpty(itemUbicHAB.local_subtipoubicacion))
                            local_subtipoubicacion_hab = itemUbicHAB.local_subtipoubicacion.Trim();

                        if (local_subtipoubicacion_caa != local_subtipoubicacion_hab)
                        {
                            lstErrores.Add(string.Format("El campo local del subtipo de ubicación en la ubicación {0} es diferente, en el CAA es '{1}' y en la solicitud de HAB es '{2}'.", NroUbicacion, itemUbicCAA.local_subtipoubicacion.Trim(), itemUbicHAB.local_subtipoubicacion.Trim()));
                        }

                        //Comparación de las puertas
                        if (itemUbicCAA.Puertas.Count() != itemUbicHAB.SSIT_Solicitudes_Ubicaciones_Puertas.Count())
                        {
                            lstErrores.Add(string.Format("La cantidad de puertas de la ubicación {0} es diferente, en el CAA es/son '{1}' y en la solicitud de HAB es/son '{2}'.", NroUbicacion, itemUbicCAA.Puertas.Count(), itemUbicHAB.SSIT_Solicitudes_Ubicaciones_Puertas.Count()));
                        }

                        foreach (var itemPuerta in itemUbicHAB.SSIT_Solicitudes_Ubicaciones_Puertas)
                        {
                            var itemPuertaCAA = itemUbicCAA.Puertas.FirstOrDefault(x => x.codigo_calle == itemPuerta.codigo_calle && x.NroPuerta == itemPuerta.NroPuerta);
                            if (itemPuertaCAA == null)
                            {
                                lstErrores.Add(string.Format("La puerta '{1} {2}' de la ubicación {0} no se encuentra en la solicitud de CAA.", NroUbicacion, itemPuerta.nombre_calle.Trim(), itemPuerta.NroPuerta));
                            }
                        }

                        //Comparación de las partidas horizontales
                        if (itemUbicCAA.PropiedadesHorizontales.Count() != itemUbicHAB.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal.Count())
                        {
                            lstErrores.Add(string.Format("La cantidad de partidas horizontales de la ubicación {0} es diferente, en el CAA es/son '{1}' y en la solicitud de HAB es/son '{2}'.", NroUbicacion, itemUbicCAA.PropiedadesHorizontales.Count(), itemUbicHAB.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal.Count()));
                        }

                        foreach (var itemPHorHAB in itemUbicHAB.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal)
                        {
                            var itemPHorCAA = itemUbicCAA.PropiedadesHorizontales.FirstOrDefault(x => x.id_propiedadhorizontal == itemPHorHAB.id_propiedadhorizontal);
                            if (itemPHorCAA == null)
                            {
                                lstErrores.Add(string.Format("La partida horizontal Nro. {1} de la ubicación {0} no se encuentra en la solicitud de CAA.", NroUbicacion, itemPHorHAB.Ubicaciones_PropiedadHorizontal.NroPartidaHorizontal));
                            }
                        }
                    }

                }
                #endregion

                #region "Comparacion de titulares"
                #region "Titulares Personas Fisicas"
                int cantidadTitPFCAA = (solCAA.TitularesPersonasFisicas != null ? solCAA.TitularesPersonasFisicas.Count() : 0);
                int cantidadTitPFHAB = sol.SSIT_Solicitudes_Titulares_PersonasFisicas.Count;

                if (cantidadTitPFCAA != cantidadTitPFHAB)
                    lstErrores.Add("La cantidad de titulares (persona/s física/s de la solicitud de CAA es distinta a la de la solicitud actual.");
                else if (cantidadTitPFCAA > 0)
                {
                    var lstTitPfCAA = solCAA.TitularesPersonasFisicas.ToList();
                    var lstTitPfHAB = sol.SSIT_Solicitudes_Titulares_PersonasFisicas.ToList();
                    foreach (var itemTitPfHAB in lstTitPfHAB)
                    {
                        var itemTitPfCAA = lstTitPfCAA.FirstOrDefault(x => x.Cuit.Replace("-", "") == itemTitPfHAB.Cuit);
                        if (itemTitPfCAA == null)
                            lstErrores.Add(string.Format("No se encuentra el titular con CUIT {0} en el CAA.", itemTitPfHAB.Cuit));

                        //Mantis 0161160: Se eliminó validacion por Apellido, Nombres, TipoDoc, NroDoc

                    }
                }


                #endregion

                #region "Titulares Personas Juridicas"

                int cantidadTitPJCAA = (solCAA.TitularesPersonasJuridicas != null ? solCAA.TitularesPersonasJuridicas.Count() : 0);
                int cantidadTitPJHAB = sol.SSIT_Solicitudes_Titulares_PersonasJuridicas.Count;

                if (cantidadTitPJCAA != cantidadTitPJHAB)
                    lstErrores.Add("La cantidad de titulares de la solicitud de CAA es distinta a la de la solicitud actual.");
                else if (cantidadTitPJCAA > 0)
                {
                    var lstTitPjCAA = solCAA.TitularesPersonasJuridicas.ToList();
                    var lstTitPjHAB = sol.SSIT_Solicitudes_Titulares_PersonasJuridicas.ToList();

                    foreach (var itemTitPjHAB in lstTitPjHAB)
                    {
                        var itemTitPjCAA = lstTitPjCAA.FirstOrDefault(x => x.CUIT.Replace("-", "") == itemTitPjHAB.CUIT);
                        if (itemTitPjCAA == null)
                            lstErrores.Add(string.Format("No se encuentra el titular con CUIT {0} en el CAA.", itemTitPjHAB.CUIT));
                        else
                        {
                            //Mantis 0161160: Se eliminó validacion por Razon Social


                            //Comparacion de los titulares de Sociedades de Hecho
                            //--
                            int cantidadTitPJPFCAA = (itemTitPjCAA.TitularesPersonasFisicasPersonasJuridicas != null ? itemTitPjCAA.TitularesPersonasFisicasPersonasJuridicas.Count() : 0);
                            int cantidadTitPJPFHAB = itemTitPjHAB.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Count;
                            if (cantidadTitPJPFCAA != cantidadTitPJPFHAB)
                                lstErrores.Add(string.Format("La cantidad de titulares (persona/s física/s) pertenecientes a la persona jurídica '{0}' de la solicitud de CAA es distinta a la de la solicitud actual.", itemTitPjCAA.Razon_Social));
                            else if (cantidadTitPJPFCAA > 0)
                            {
                                var lstTitPjPfCAA = itemTitPjCAA.TitularesPersonasFisicasPersonasJuridicas.ToList();
                                var lstTitPjPfHAB = itemTitPjHAB.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.ToList();

                                foreach (var itemTitPjPfHAB in lstTitPjPfHAB)
                                {
                                    var itemTitPjPfCAA = lstTitPjPfCAA.FirstOrDefault(x => x.id_tipodoc_personal == itemTitPjPfHAB.id_tipodoc_personal && x.Nro_Documento == itemTitPjPfHAB.Nro_Documento);
                                    var TipoDocHAB = repoTipoDocumentosPersonal.Single(itemTitPjPfHAB.id_tipodoc_personal);

                                    if (itemTitPjPfCAA == null)
                                    {
                                        lstErrores.Add(string.Format("No se encuentra el titular de la Sociedad con Documento {0} {1} en el CAA.", TipoDocHAB.Nombre, itemTitPjPfHAB.Nro_Documento));
                                    }
                                    else
                                    {

                                        nombreCampo = "Apellido";
                                        valorStringCAA = (!string.IsNullOrEmpty(itemTitPjPfCAA.Apellido) ? itemTitPjPfCAA.Apellido.Trim().ToLower().Replace(" ", "") : string.Empty);
                                        valorStringHAB = (!string.IsNullOrEmpty(itemTitPjPfHAB.Apellido) ? itemTitPjPfHAB.Apellido.Trim().ToLower().Replace(" ", "") : string.Empty);

                                        if (valorStringCAA != valorStringHAB)
                                        {
                                            lstErrores.Add(string.Format("El campo {0} del titular con Tipo y Nro de Documento {1} {2} es diferente, en el CAA posee el valor '{3}' y en la solicitud de HAB el valor es '{4}'.", nombreCampo, TipoDocHAB.Nombre, itemTitPjPfHAB.Nro_Documento, valorStringCAA, valorStringHAB));
                                        }


                                        nombreCampo = "Nombres";
                                        valorStringCAA = (!string.IsNullOrEmpty(itemTitPjPfCAA.Nombres) ? itemTitPjPfCAA.Nombres.Trim().ToLower().Replace(" ", "") : string.Empty);
                                        valorStringHAB = (!string.IsNullOrEmpty(itemTitPjPfHAB.Nombres) ? itemTitPjPfHAB.Nombres.Trim().ToLower().Replace(" ", "") : string.Empty);

                                        if (valorStringCAA != valorStringHAB)
                                        {
                                            lstErrores.Add(string.Format("El campo {0} del titular con Tipo y Nro de Documento {1} {2} es diferente, en el CAA posee el valor '{3}' y en la solicitud de HAB el valor es '{4}'.", nombreCampo, TipoDocHAB.Nombre, itemTitPjPfHAB.Nro_Documento, valorStringCAA, valorStringHAB));
                                        }

                                    }
                                }
                            }

                        }
                    }
                }


                #endregion
                #endregion

                #region "Comparación de Rubros y Superficie"
                if (sol.id_tipotramite == (int)Constantes.TipoTramite.PERMISO)
                {
                    var DatosLocal = repoSolicitudesDatosLocal.GetByFKIdSolicitud(id_solicitud).FirstOrDefault();

                    if (DatosLocal == null)
                        lstErrores.Add("No es posible comparar los datos del local debido a que no se enconctraron cargados en la solicitud.");
                    else
                    {
                        // Compara las Superficies
                        decimal SuperficieSolicitud = 0;
                        decimal SuperficieCAA = (solCAA.SuperficieCubierta + solCAA.SuperficieDescubierta);

                        if (DatosLocal != null)
                        {
                            SuperficieSolicitud = (DatosLocal.superficie_cubierta_dl + DatosLocal.superficie_descubierta_dl);
                        }

                        if (SuperficieCAA < SuperficieSolicitud)
                            lstErrores.Add(string.Format("La Superficie total de la solicitud de CAA es menor a la de la solicitud de AGC. El valor en el CAA es {0} y en la solicitud del permiso es de {1}", SuperficieCAA, SuperficieSolicitud));

                    }

                    // Compara Rubros
                    var lstRubrosHAB = repoSolicitudesRubros.GetRubrosCN(id_solicitud).ToList();

                    // Compara los Rubros.
                    int cantidadRubrosCAA = (solCAA.Rubros != null ? solCAA.Rubros.Count() : 0);
                    int cantidadRubrosHAB = lstRubrosHAB.Count();

                    //if (cantidadRubrosCAA != cantidadRubrosHAB)
                    //{
                    //    lstErrores.Add("La cantidad de rubros de la solicitud de CAA es distinta a la de la solicitud de AGC.");
                    //}
                    //if (cantidadRubrosCAA > 0)
                    //{
                    var lstRubrosCAA = solCAA.Rubros.ToList();

                    foreach (var itemRubroHAB in lstRubrosHAB)
                    {
                        var itemRubroCAA = lstRubrosCAA.FirstOrDefault(x => x.cod_rubro == itemRubroHAB.CodigoRubro);
                        if (itemRubroCAA == null)
                            lstErrores.Add(string.Format("No se encuentra el rubro {0} en el CAA.", itemRubroHAB.CodigoRubro));
                        else
                        {
                            decimal valorDecimalCAA = 0;
                            decimal valorDecimalHAB = 0;

                            nombreCampo = "Superficie a habilitar";
                            valorDecimalCAA = itemRubroCAA.SuperficieHabilitar;
                            valorDecimalHAB = itemRubroHAB.SuperficieHabilitar;

                            if (valorDecimalCAA < valorDecimalHAB)
                            {
                                lstErrores.Add(string.Format("El campo {0} del rubro {1} es menor en el CAA '{2}' que en la solicitud de HAB '{3}'.", nombreCampo, itemRubroHAB.CodigoRubro, valorDecimalCAA, valorDecimalHAB));
                            }
                        }
                    }

                    //}

                }
                else
                {
                    #region Encomienda

                    var encomienda = repoEncomienda.GetUltimaEncomiendaAprobada(id_solicitud).FirstOrDefault();

                    if (encomienda == null)
                        lstErrores.Add("No es posible comparar los rubros debido a que no se encontró una encomienda aprobada.");
                    else
                    {
                        // Compara las Superficies
                        decimal SuperficieEncomienda = 0;
                        decimal SuperficieCAA = (solCAA.SuperficieCubierta + solCAA.SuperficieDescubierta);
                        var datos_local = encomienda.Encomienda_DatosLocal.FirstOrDefault();
                        if (datos_local != null)
                        {
                            if (datos_local.ampliacion_superficie.HasValue && datos_local.ampliacion_superficie.Value)
                                SuperficieEncomienda = (datos_local.superficie_cubierta_amp.HasValue ? datos_local.superficie_cubierta_amp.Value : 0) +
                                                       (datos_local.superficie_descubierta_amp.HasValue ? datos_local.superficie_descubierta_amp.Value : 0);
                            else
                                SuperficieEncomienda = (datos_local.superficie_cubierta_dl.HasValue ? datos_local.superficie_cubierta_dl.Value : 0) +
                                                       (datos_local.superficie_descubierta_dl.HasValue ? datos_local.superficie_descubierta_dl.Value : 0);
                        }

                        if (SuperficieEncomienda != SuperficieCAA)
                            lstErrores.Add(string.Format("La Superficie total de la solicitud de CAA es distinta a la de la ultima encomienda aprobada. El valor en el CAA es {0} y en la encomienda es de {1}", SuperficieCAA, SuperficieEncomienda));


                        // Compara los Rubros.
                        int cantidadRubrosCAA = (solCAA.Rubros != null ? solCAA.Rubros.Count() : 0);
                        int cantidadRubrosHAB = 0;

                        if (id_solicitud <= nroSolicitudReferencia)
                            cantidadRubrosHAB = encomienda.Encomienda_Rubros.Count();
                        else
                            cantidadRubrosHAB = encomienda.Encomienda_RubrosCN.Count();



                        if (cantidadRubrosCAA != cantidadRubrosHAB)
                        {
                            lstErrores.Add("La cantidad de rubros de la solicitud de CAA es distinta a la de la ultima encomienda aprobada.");
                        }
                        else if (cantidadRubrosCAA > 0)
                        {
                            if (id_solicitud <= nroSolicitudReferencia)
                            {
                                var lstRubrosCAA = solCAA.Rubros.ToList();
                                var lstRubrosHAB = encomienda.Encomienda_Rubros.ToList();
                                foreach (var itemRubroHAB in lstRubrosHAB)
                                {
                                    var itemRubroCAA = lstRubrosCAA.FirstOrDefault(x => x.cod_rubro == itemRubroHAB.cod_rubro);
                                    if (itemRubroCAA == null)
                                        lstErrores.Add(string.Format("No se encuentra el rubro {0} en el CAA.", itemRubroHAB.cod_rubro));
                                    else
                                    {
                                        decimal valorDecimalCAA = 0;
                                        decimal valorDecimalHAB = 0;

                                        nombreCampo = "Superficie a habilitar";
                                        valorDecimalCAA = itemRubroCAA.SuperficieHabilitar;
                                        valorDecimalHAB = itemRubroHAB.SuperficieHabilitar;

                                        if (valorDecimalCAA != valorDecimalHAB)
                                        {
                                            lstErrores.Add(string.Format("El campo {0} del rubro {1} es diferente, en el CAA posee el valor '{2}' y en la solicitud de HAB el valor es '{3}'.", nombreCampo, itemRubroHAB.cod_rubro, valorDecimalCAA, valorDecimalHAB));
                                        }
                                    }
                                }
                            }
                            else
                            {

                                var lstRubrosCAA = solCAA.Rubros.ToList();
                                var lstRubrosHAB = encomienda.Encomienda_RubrosCN.ToList();
                                foreach (var itemRubroHAB in lstRubrosHAB)
                                {
                                    var itemRubroCAA = lstRubrosCAA.FirstOrDefault(x => x.cod_rubro == itemRubroHAB.CodigoRubro);
                                    if (itemRubroCAA == null)
                                        lstErrores.Add(string.Format("No se encuentra el rubro {0} en el CAA.", itemRubroHAB.CodigoRubro));
                                    else
                                    {
                                        decimal valorDecimalCAA = 0;
                                        decimal valorDecimalHAB = 0;

                                        nombreCampo = "Superficie a habilitar";
                                        valorDecimalCAA = itemRubroCAA.SuperficieHabilitar;
                                        valorDecimalHAB = itemRubroHAB.SuperficieHabilitar;

                                        if (valorDecimalCAA != valorDecimalHAB)
                                        {
                                            lstErrores.Add(string.Format("El campo {0} del rubro {1} es diferente, en el CAA posee el valor '{2}' y en la solicitud de HAB el valor es '{3}'.", nombreCampo, itemRubroHAB.CodigoRubro, valorDecimalCAA, valorDecimalHAB));
                                        }
                                    }
                                }

                            }

                        }
                    }

                    #endregion
                }

                #endregion
            }
            else
            {
                lstErrores.Add("No se encontró la solicitud de habilitación.");
            }


            return lstErrores;

        }

        public void ActualizaTipoSubtipoExp(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            EncomiendaBL encBL = new EncomiendaBL();

            var solicitudEntity = repo.Single(id_solicitud);

            var encomiendasEntity = solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda);

            if (solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Count() > 0)
            {
                var listEnc = encomiendasEntity.Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        && x.tipo_anexo == Constantes.TipoAnexo_A).ToList();

                if (listEnc.Count > 0)
                {
                    var encomienda = listEnc.OrderByDescending(x => x.id_encomienda).First();

                    EncomiendaDTO enc = encBL.Single(encomienda.id_encomienda);
                    ActualizaTipoSubtipoExpSSIT(enc);
                }
            }
        }

        public void ActualizaTipoSubtipoExpSSIT(EncomiendaDTO enc)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var gcttRepo = new ENGGruposCircuitosTipoTramiteRepository(this.uowF.GetUnitOfWork());
            EngineBL blEng = new EngineBL();
            var repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            SSITSolicitudesDTO soli = new SSITSolicitudesDTO();


            try
            {
                int Idcir = blEng.GetIdCircuitoByEncomienda(enc.IdEncomienda);
                var circuito = repoEngTareas.GetCircuito(Idcir);
                var grupoTipoT = gcttRepo.GetByFKIdGrupo(circuito.nombre_grupo);

                if (grupoTipoT != null)
                {
                    soli = Single(enc.IdSolicitud); //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                    soli.IdTipoExpediente = grupoTipoT.id_tipo_expediente;
                    soli.IdSubTipoExpediente = grupoTipoT.id_sub_tipo_expediente;

                    //Solo actualizo en la solicitud, si es una encomienda con CECI
                    if (enc.EsECI)
                        soli.EsECI = enc.EsECI;
                    Update(soli);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DescargarDispo(string nroDispo, int idSolicitud, Guid userid)
        {
            byte[] docPdf = new byte[0];

            ParametrosBL blParam = new ParametrosBL();
            ws_ExpedienteElectronico serviceEE = new ws_ExpedienteElectronico();
            serviceEE.Url = blParam.GetParametroChar("SGI.Url.Service.ExpedienteElectronico");
            string username_servicio_EE = blParam.GetParametroChar("SGI.UserName.Service.ExpedienteElectronico");
            string pass_servicio_EE = blParam.GetParametroChar("SGI.Pwd.Service.ExpedienteElectronico");
            string usu_Sade = blParam.GetParametroChar("SGI.Username.Director.Habilitaciones");

            try
            {
                docPdf = serviceEE.GetPdfDisposicionNroEspecial(username_servicio_EE, pass_servicio_EE, usu_Sade, nroDispo.Trim());

                if (docPdf.Length == 0)
                {
                    docPdf = serviceEE.GetPdfDisposicionNroGedo(username_servicio_EE, pass_servicio_EE, nroDispo, usu_Sade);

                    if (docPdf.Length == 0)
                    {
                        serviceEE.Dispose();
                        throw new Exception("El documento está en blanco.");
                    }
                }
                else
                {

                    SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    SSITDocumentosAdjuntosDTO solDocDTO;

                    int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION;
                    var DocAdj = solDocBL.GetByFKIdSolicitudTipoDocSis(idSolicitud, id_tipodocsis).FirstOrDefault();

                    if (DocAdj == null)
                    {
                        int id_file = esf.addFile("Disposicion.pdf", docPdf);

                        solDocDTO = new SSITDocumentosAdjuntosDTO();
                        solDocDTO.id_solicitud = idSolicitud;
                        solDocDTO.id_tipodocsis = id_tipodocsis;
                        solDocDTO.id_tdocreq = 0;
                        solDocDTO.generadoxSistema = true;
                        solDocDTO.CreateDate = DateTime.Now;
                        solDocDTO.CreateUser = userid;
                        solDocDTO.nombre_archivo = "Disposición";
                        solDocDTO.id_file = id_file;
                        solDocBL.Insert(solDocDTO, false);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceEE.Dispose();
                throw new Exception("No se ha podido Descargar la Disposición");
            }
        }

        public bool PasoGenerarExpediente(int id_solicitud, int id_tarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repoSol = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
            bool PAsoGenExp = false;
            try
            {
                PAsoGenExp = repoSol.ExisteTareaSolicitud(id_solicitud, id_tarea);
            }
            catch
            {
                PAsoGenExp = false;
            }
            return PAsoGenExp;
        }

        public void DarBajaPorECI(SSITSolicitudesDTO solicitud, Guid userid, string observaciones)
        {
            //Valido que sean un estado valido
            if (
                solicitud.IdEstado == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.ETRA ||
                solicitud.IdEstado == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                )
            {
                //Busco el usario procesos
                AspnetUsersRepository repoUser = new AspnetUsersRepository(this.uowF.GetUnitOfWork());
                ParametrosBL par = new ParametrosBL();
                var uProcesos = repoUser.Get(par.GetParametroChar("userProcesoBajaECI"));
                Guid useridProceso = uProcesos.UserId;
                EngineBL eng = new EngineBL();
                repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var tareas = repo.getTareasSolicitud(solicitud.IdSolicitud);
                var ultimaTarea = tareas.Where(z => z.FechaCierre_tramitetarea == null).OrderByDescending(x => x.id_tramitetarea).FirstOrDefault();

                int id_circuito = ultimaTarea.ENG_Tareas.id_circuito;
                int codtf = id_circuito * 100 + 33;
                var tfin = eng.GetIdTarea(codtf);
                int id_tipo_motivo_baja = 5; //'Baja por ECI'

                int idTarea_actual = ultimaTarea.id_tarea;
                int idTramiteTarea = ultimaTarea.id_tramitetarea;
                int idResultado = 0;

                //Cierra ultima tarea y proxima tarea es baja
                int id_tramitetarea_nuevo = eng.FinalizarTarea(idTramiteTarea, idResultado, tfin, useridProceso);
                eng.AsignarTarea(id_tramitetarea_nuevo, null, null);
                //Cierra la tarea baja
                int id_tramitetarea_baja = eng.FinalizarTarea(id_tramitetarea_nuevo, idResultado, 0, useridProceso);

                tareas = repo.getTareasSolicitud(solicitud.IdSolicitud);
                ultimaTarea = tareas.Where(z => z.FechaCierre_tramitetarea == null).OrderByDescending(x => x.id_tramitetarea).FirstOrDefault();
                eng.FinalizarTarea(ultimaTarea.id_tramitetarea, idResultado, 0, userid);

                ActualizarEstadoBaja(solicitud.IdSolicitud, userid);
                DarBaja(solicitud, id_tipo_motivo_baja, observaciones, useridProceso);
            }
        }

        private void ActualizarEstadoBaja(int id_solicitud, Guid userid)
        {
            SSITSolicitudesDTO sol = Single(id_solicitud);
            //uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.BAJAADMECI;
            sol.LastUpdateUser = userid;
            sol.LastUpdateDate = DateTime.Now;
            Update(sol);
        }

        private int DarBaja(SSITSolicitudesDTO solicitud, int id_tipo_motivo_baja, string observaciones, Guid userid)
        {
            IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.Unspecified);
            SSIT_Solicitudes_BajaDTO baja = new SSIT_Solicitudes_BajaDTO();
            baja.id_solicitud = solicitud.IdSolicitud;
            baja.id_tipo_motivo_baja = id_tipo_motivo_baja;
            baja.observaciones = observaciones;
            baja.CreateUser = userid;
            baja.fecha_baja = System.DateTime.Now;
            baja.CreateDate = System.DateTime.Now;

            var repoBaja = new SSIT_Solicitudes_BajaRepository(unitOfWorkTran);
            var elementEntityBaja = mapperBase.Map<SSIT_Solicitudes_BajaDTO, SSIT_Solicitudes_Baja>(baja);
            var insertSolOk = repoBaja.Insert(elementEntityBaja);
            unitOfWorkTran.Commit();
            unitOfWorkTran.Dispose();
            return elementEntityBaja.id_baja;
        }

        private bool BoletaCeroActiva()
        {
            string boletaCero_FechaDesde = System.Configuration.ConfigurationManager.AppSettings["boletaCero_FechaDesde"];
            DateTime boletaCeroDate = DateTime.ParseExact(boletaCero_FechaDesde,
                                                            "yyyyMMdd",
                                                            System.Globalization.CultureInfo.InvariantCulture);
            if (DateTime.Now > boletaCeroDate)
                return true;

            return false;
        }

        private bool TienePlanoDeIncendio(int id_solicitud)
        {
            EncomiendaSSITSolicitudesBL encSolBL = new EncomiendaSSITSolicitudesBL();
            int id_encomienda = encSolBL.GetByFKIdSolicitud(id_solicitud).Max(x => x.id_encomienda);
            EncomiendaPlanosBL encDocBL = new EncomiendaPlanosBL();
            var DocAdjAT = encDocBL.GetByFKIdEncomiendaTipoPlano(id_encomienda, 2).FirstOrDefault();
            SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
            var DocAdjSSIT = ssitDocBL.GetByFKIdSolicitudTipoDocReq(id_solicitud, 66).FirstOrDefault();
            return DocAdjAT != null || DocAdjSSIT != null;
        }

        private bool AcogeBeneficiosUERESGP(int id_solicitud)
        {
            EncomiendaBL encBl = new EncomiendaBL();
            var datoSolicitudEnc = encBl.GetByFKIdSolicitud(id_solicitud);
            var enc = datoSolicitudEnc.OrderByDescending(x => x.IdEncomienda).FirstOrDefault();
            return enc.AcogeBeneficios;
        }
    }
}
