using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class RedistribucionUsoBL
    {
        private SSITSolicitudesRepository repoSol = null;
        private TransferenciasSolicitudesRepository repoTransf = null;
        private SSITSolicitudesOrigenRepository repoAmp = null;
        private ConsultaPadronSolicitudesBL repoCPadron = null;
        private ConsultaPadronUbicacionesBL repoCPadronUbicaciones = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RedistribucionUsoBL()
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
                    .ForMember(dest => dest.SSITDocumentosAdjuntosDTO, source => source.MapFrom(p => p.SSIT_DocumentosAdjuntos))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    //.ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.TipoTramiteDescripcion, source => source.MapFrom(p => p.TipoTramite.descripcion_tipotramite))
                    .ForMember(dest => dest.TipoExpedienteDescripcion, source => source.MapFrom(p => p.TipoExpediente.descripcion_tipoexpediente))
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    ;


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
                    //.ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.MapFrom(p => p.SSITSolicitudesOrigenDTO))
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

                cfg.CreateMap<SSIT_Solicitudes, SolicitudesAprobadasDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdSolicitudOrigen, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen.id_solicitud_origen))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    //.ForMember(dest => dest.EncomiendaDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.TipoTramiteDescripcion, source => source.MapFrom(p => p.TipoTramite.descripcion_tipotramite))
                    .ForMember(dest => dest.TipoExpedienteDescripcion, source => source.MapFrom(p => p.TipoExpediente.descripcion_tipoexpediente))
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente)
                    );

                cfg.CreateMap<Transf_Solicitudes, SolicitudesAprobadasDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.TipoTramiteDescripcion, source => source.MapFrom(p => p.TipoTramite.descripcion_tipotramite))
                    .ForMember(dest => dest.TipoExpedienteDescripcion, source => source.MapFrom(p => p.TipoExpediente.descripcion_tipoexpediente))
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente)
                    );


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
                    //.ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.tipoAnexo, source => source.MapFrom(p => p.tipo_anexo));
                    //.ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                   // .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes));

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
                    .ForMember(dest => dest.UbicacionesDTO, source => source.MapFrom(p => p.Ubicaciones));

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
                    .ForAllMembers(dest => dest.Ignore());

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



            });

            mapperBase = config.CreateMapper();
        }


        public int Insert(SSITSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {

                    repoSol = new SSITSolicitudesRepository(unitOfWork);
                    repoAmp = new SSITSolicitudesOrigenRepository(unitOfWork);

                    var elementEntitySol = mapperBase.Map<SSITSolicitudesDTO, SSIT_Solicitudes>(objectDto);

                    var insertSolOk = repoSol.Insert(elementEntitySol);
                    objectDto.IdSolicitud = elementEntitySol.id_solicitud;

                    if (objectDto.SSITSolicitudesOrigenDTO != null)
                    {
                        if (objectDto.SSITSolicitudesOrigenDTO.id_solicitud_origen.HasValue)
                            CopyFromSolicitud( objectDto.SSITSolicitudesOrigenDTO.id_solicitud_origen.Value,
                                objectDto.IdSolicitud, objectDto.CreateUser, unitOfWork);

                        if (objectDto.SSITSolicitudesOrigenDTO.id_transf_origen.HasValue)
                            CopyFromTransferencia(objectDto.SSITSolicitudesOrigenDTO.id_transf_origen.Value,
                                objectDto.IdSolicitud, objectDto.CreateUser, unitOfWork);

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

        private void CopyFromTransferencia(int idSolicitudOrigen, int idSolicitudDestino, Guid userid, IUnitOfWork unitOfWork)
        {
            #region "Transferencias"
            TransferenciasSolicitudesRepository repoTrasnf = new TransferenciasSolicitudesRepository(unitOfWork);
            ConsultaPadronUbicacionesRepository repoUbicCP = new ConsultaPadronUbicacionesRepository(unitOfWork);
            ConsultaPadronUbicacionesPuertasRepository repoUbicPuerCP = new ConsultaPadronUbicacionesPuertasRepository(unitOfWork);
            ConsultaPadronUbicacionPropiedadHorizontalRepository repoUbicPHCP = new ConsultaPadronUbicacionPropiedadHorizontalRepository(unitOfWork);

            SSITSolicitudesUbicacionesRepository repoUbic = new SSITSolicitudesUbicacionesRepository(unitOfWork);
            SSITSolicitudesUbicacionesPuertasRepository repoUbicPuer = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
            SSITSolicitudesUbicacionesPropiedadHorizontalRepository repoUbicPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);

            var transf = repoTrasnf.Single(idSolicitudOrigen);

            var lstUbicaciones_entity = repoUbicCP.GetByFKIdConsultaPadron(transf.id_cpadron).ToList();

            foreach (var itemUbic in lstUbicaciones_entity)
            {
                int id_solicitudubicacion_ant = itemUbic.id_cpadronubicacion;

                var itemNuevo = new SSIT_Solicitudes_Ubicaciones();
                itemNuevo.id_solicitud = idSolicitudDestino;
                itemNuevo.id_ubicacion = itemUbic.id_ubicacion;

                itemNuevo.id_subtipoubicacion = itemUbic.id_subtipoubicacion;
                itemNuevo.id_zonaplaneamiento = itemUbic.id_zonaplaneamiento;
                itemNuevo.local_subtipoubicacion = itemUbic.local_subtipoubicacion;
                itemNuevo.deptoLocal_ubicacion = itemUbic.deptoLocal_cpadronubicacion;
                itemNuevo.Torre = itemUbic.Torre;
                itemNuevo.Local = itemUbic.Local;
                itemNuevo.Depto = itemUbic.Depto;
                itemNuevo.CreateDate = DateTime.Now;
                itemNuevo.CreateUser = userid;
                repoUbic.Insert(itemNuevo);

                var lstUbicacionesPuertas_entity = repoUbicPuerCP.GetByFKIdConsultaPadronUbicacion(id_solicitudubicacion_ant).ToList();
                foreach (var itemPuer in lstUbicacionesPuertas_entity)
                {
                    var itemPuerNuevo = new SSIT_Solicitudes_Ubicaciones_Puertas();

                    itemPuerNuevo.id_solicitudubicacion = itemNuevo.id_solicitudubicacion;
                    itemPuerNuevo.codigo_calle = itemPuer.codigo_calle;
                    itemPuerNuevo.nombre_calle = itemPuer.nombre_calle;
                    itemPuerNuevo.NroPuerta = itemPuer.NroPuerta;

                    repoUbicPuer.Insert(itemPuerNuevo);
                }

                var lstUbicacionesPH_entity = repoUbicPHCP.GetByFKIdConsultaPadronUbicacion(id_solicitudubicacion_ant).ToList();
                foreach (var itemPH in lstUbicacionesPH_entity)
                {
                    var itemPHNuevo = new SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal();
                    itemPHNuevo.id_solicitudubicacion = itemNuevo.id_solicitudubicacion;
                    itemPHNuevo.id_propiedadhorizontal = itemPH.id_propiedadhorizontal;

                    repoUbicPH.Insert(itemPHNuevo);
                }
            }


            // Titulares

            TransferenciasTitularesPersonasFisicasRepository repoTransfTitPF = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);
            TransferenciasTitularesPersonasJuridicasRepository repoTransfTitPJ = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);
            TransferenciasFirmantesPersonasFisicasRepository repoTransfFirPF = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);
            TransferenciasFirmantesPersonasJuridicasRepository repoTransfFirPJ = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);
            TransferenciasTitularesPersonasJuridicasPersonasFisicasRepository repoTransfTitPJPF = new TransferenciasTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);

            SSITSolicitudesTitularesPersonasFisicasRepository repoSolTitPF = new SSITSolicitudesTitularesPersonasFisicasRepository(unitOfWork);
            SSITSolicitudesTitularesPersonasJuridicasRepository repoSolTitPJ = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
            SSITSolicitudesFirmantesPersonasFisicasRepository repoSolFirPF = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);
            SSITSolicitudesFirmantesPersonasJuridicasRepository repoSolFirPJ = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);
            SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository repoSolTitPJPF = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);


            var lstTitPF_entity = repoTransfTitPF.GetByFKIdSolicitud(idSolicitudOrigen).ToList();
            foreach (var itemTitPF in lstTitPF_entity)
            {

                SSIT_Solicitudes_Titulares_PersonasFisicas itemNuevoPF = new SSIT_Solicitudes_Titulares_PersonasFisicas();
                int id_personafisica_ant = itemTitPF.id_personafisica;

                itemNuevoPF.Apellido = itemTitPF.Apellido;
                itemNuevoPF.Calle = itemTitPF.Calle;
                itemNuevoPF.Codigo_Postal = itemTitPF.Codigo_Postal;
                itemNuevoPF.CreateDate = DateTime.Now;
                itemNuevoPF.CreateUser = itemTitPF.CreateUser;
                itemNuevoPF.Cuit = itemTitPF.Cuit;
                itemNuevoPF.Depto = itemTitPF.Depto;
                itemNuevoPF.Email = itemTitPF.Email;
                itemNuevoPF.Id_Localidad = itemTitPF.id_Localidad;
                itemNuevoPF.id_solicitud = idSolicitudDestino;
                itemNuevoPF.id_tipoiibb = itemTitPF.id_tipoiibb;
                itemNuevoPF.Ingresos_Brutos = itemTitPF.Ingresos_Brutos;
                itemNuevoPF.MismoFirmante = itemTitPF.MismoFirmante;
                itemNuevoPF.Nombres = itemTitPF.Nombres;
                itemNuevoPF.id_tipodoc_personal = itemTitPF.id_tipodoc_personal;
                itemNuevoPF.Nro_Documento = itemTitPF.Nro_Documento;
                itemNuevoPF.Nro_Puerta = itemTitPF.Nro_Puerta;
                itemNuevoPF.Piso = itemTitPF.Piso;
                itemNuevoPF.Telefono = itemTitPF.Telefono;

                repoSolTitPF.Insert(itemNuevoPF);


                var lstFirPF_entity = repoTransfFirPF.GetByFKIdPersonaFisica(id_personafisica_ant).ToList();
                foreach (var itemFirPF in lstFirPF_entity)
                {
                    SSIT_Solicitudes_Firmantes_PersonasFisicas itemFirNuevoPF = new SSIT_Solicitudes_Firmantes_PersonasFisicas();

                    itemFirNuevoPF.id_personafisica = itemNuevoPF.id_personafisica;
                    itemFirNuevoPF.id_solicitud = idSolicitudDestino;

                    itemFirNuevoPF.Apellido = itemFirPF.Apellido;
                    itemFirNuevoPF.Email = itemFirPF.Email;
                    itemFirNuevoPF.id_tipocaracter = itemFirPF.id_tipocaracter;
                    itemFirNuevoPF.id_tipodoc_personal = itemFirPF.id_tipodoc_personal;
                    itemFirNuevoPF.Nombres = itemFirPF.Nombres;
                    itemFirNuevoPF.Nro_Documento = itemFirPF.Nro_Documento;

                    repoSolFirPF.Insert(itemFirNuevoPF);
                }


            }

            var lstTitPJ_entity = repoTransfTitPJ.GetByFKIdSolicitud(idSolicitudOrigen).ToList();

            foreach (var itemTitPJ in lstTitPJ_entity)
            {
                SSIT_Solicitudes_Titulares_PersonasJuridicas itemTitNuevoPJ = new SSIT_Solicitudes_Titulares_PersonasJuridicas();

                int id_personajuridica_ant = itemTitPJ.id_personajuridica;
                itemTitNuevoPJ.Calle = itemTitPJ.Calle;
                itemTitNuevoPJ.Codigo_Postal = itemTitPJ.Codigo_Postal;
                itemTitNuevoPJ.CreateDate = DateTime.Now;
                itemTitNuevoPJ.CreateUser = itemTitPJ.CreateUser;
                itemTitNuevoPJ.CUIT = itemTitPJ.CUIT;
                itemTitNuevoPJ.Depto = itemTitPJ.Depto;
                itemTitNuevoPJ.Email = itemTitPJ.Email;
                itemTitNuevoPJ.id_localidad = itemTitPJ.id_localidad;
                itemTitNuevoPJ.id_solicitud = idSolicitudDestino;
                itemTitNuevoPJ.id_tipoiibb = itemTitPJ.id_tipoiibb;
                itemTitNuevoPJ.Id_TipoSociedad = itemTitPJ.Id_TipoSociedad;
                itemTitNuevoPJ.NroPuerta = itemTitPJ.NroPuerta;
                itemTitNuevoPJ.Nro_IIBB = itemTitPJ.Nro_IIBB;
                itemTitNuevoPJ.Piso = itemTitPJ.Piso;
                itemTitNuevoPJ.Razon_Social = itemTitPJ.Razon_Social;
                itemTitNuevoPJ.Telefono = itemTitPJ.Telefono;

                repoSolTitPJ.Insert(itemTitNuevoPJ);

                var lstFirPJ_entity = repoTransfFirPJ.GetByFKIdPersonaJuridica(id_personajuridica_ant).ToList();
                foreach (var itemFirPJ in lstFirPJ_entity)
                {

                    int id_firmante_pj_ant = itemFirPJ.id_firmante_pj;
                    SSIT_Solicitudes_Firmantes_PersonasJuridicas itemFirNuevoPJ = new SSIT_Solicitudes_Firmantes_PersonasJuridicas();

                    itemFirNuevoPJ.id_personajuridica = itemTitNuevoPJ.id_personajuridica;
                    itemFirNuevoPJ.id_solicitud = idSolicitudDestino;

                    itemFirNuevoPJ.Apellido = itemFirPJ.Apellido;
                    itemFirNuevoPJ.Email = itemFirPJ.Email;
                    itemFirNuevoPJ.id_tipocaracter = itemFirPJ.id_tipocaracter;
                    itemFirNuevoPJ.id_tipodoc_personal = itemFirPJ.id_tipodoc_personal;
                    itemFirNuevoPJ.Nombres = itemFirPJ.Nombres;
                    itemFirNuevoPJ.Nro_Documento = itemFirPJ.Nro_Documento;

                    repoSolFirPJ.Insert(itemFirNuevoPJ);

                    var lstTitPJPF_entity = repoTransfTitPJPF.GetByFKIdPersonaJuridica(id_personajuridica_ant).ToList();
                    foreach (var itemTitPJPF in lstTitPJPF_entity.Where(x => x.id_firmante_pj == id_firmante_pj_ant))
                    {
                        SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas itemTitNuevoPJPF = new SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas();

                        itemTitNuevoPJPF.id_personajuridica = itemTitNuevoPJ.id_personajuridica;
                        itemTitNuevoPJPF.id_solicitud = itemTitPJPF.id_solicitud;
                        itemTitNuevoPJPF.Apellido = itemTitPJPF.Apellido;
                        itemTitNuevoPJPF.Email = itemTitPJPF.Email;
                        itemTitNuevoPJPF.firmante_misma_persona = itemTitPJPF.firmante_misma_persona;
                        itemTitNuevoPJPF.id_firmante_pj = itemFirNuevoPJ.id_firmante_pj;
                        itemTitNuevoPJPF.id_tipodoc_personal = itemTitPJPF.id_tipodoc_personal;
                        itemTitNuevoPJPF.Nombres = itemTitPJPF.Nombres;
                        itemTitNuevoPJPF.Nro_Documento = itemTitPJPF.Nro_Documento;

                        repoSolTitPJPF.Insert(itemTitNuevoPJPF);
                    }

                }


            }

            #endregion
        }
        private void CopyFromSolicitud(int idSolicitudOrigen, int idSolicitudDestino, Guid userid, IUnitOfWork unitOfWork)
        {


            #region "Habilitaciones/ampliaciones/redistribuciones de uso"

            SSITSolicitudesUbicacionesRepository repoUbic = new SSITSolicitudesUbicacionesRepository(unitOfWork);
            SSITSolicitudesUbicacionesPuertasRepository repoUbicPuer = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
            SSITSolicitudesUbicacionesPropiedadHorizontalRepository repoUbicPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);

            var lstUbicaciones_entity = repoUbic.GetByFKIdSolicitud(idSolicitudOrigen).ToList();

            foreach (var itemUbic in lstUbicaciones_entity)
            {
                int id_solicitudubicacion_ant = itemUbic.id_solicitudubicacion;

                itemUbic.id_solicitud = idSolicitudDestino;
                itemUbic.CreateDate = DateTime.Now;
                itemUbic.CreateUser = userid;
                repoUbic.Insert(itemUbic);

                var lstUbicacionesPuertas_entity = repoUbicPuer.GetByFKIdSolicitudUbicacion(id_solicitudubicacion_ant).ToList();
                foreach (var itemPuer in lstUbicacionesPuertas_entity)
                {
                    itemPuer.id_solicitudubicacion = itemUbic.id_solicitudubicacion;

                    repoUbicPuer.Insert(itemPuer);
                }

                var lstUbicacionesPH_entity = repoUbicPH.GetByFKIdSolicitudUbicacion(id_solicitudubicacion_ant).ToList();
                foreach (var itemPH in lstUbicacionesPH_entity)
                {
                    itemPH.id_solicitudubicacion = itemPH.id_solicitudubicacion;
                    repoUbicPH.Insert(itemPH);
                }
            }

            SSITSolicitudesTitularesPersonasFisicasRepository repoTitPF = new SSITSolicitudesTitularesPersonasFisicasRepository(unitOfWork);
            SSITSolicitudesTitularesPersonasJuridicasRepository repoTitPJ = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
            SSITSolicitudesFirmantesPersonasFisicasRepository repoFirPF = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);
            SSITSolicitudesFirmantesPersonasJuridicasRepository repoFirPJ = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);
            SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository repoTitPJPF = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);

            var lstTitPF_entity = repoTitPF.GetByFKIdSolicitud(idSolicitudOrigen).ToList();
            foreach (var itemTitPF in lstTitPF_entity)
            {
                int id_personafisica_ant = itemTitPF.id_personafisica;
                itemTitPF.id_solicitud = idSolicitudDestino;
                itemTitPF.CreateDate = DateTime.Now;
                itemTitPF.CreateUser = userid;
                itemTitPF.LastupdateDate = null;
                itemTitPF.LastUpdateUser = null;

                repoTitPF.Insert(itemTitPF);

                var lstFirPF_entity = repoFirPF.GetByFKIdPersonaFisica(id_personafisica_ant).ToList();
                foreach (var itemFirPF in lstFirPF_entity)
                {
                    itemFirPF.id_personafisica = itemTitPF.id_personafisica;
                    itemFirPF.id_solicitud = idSolicitudDestino;
                    repoFirPF.Insert(itemFirPF);
                }

            }

            var lstTitPJ_entity = repoTitPJ.GetByFKIdSolicitud(idSolicitudOrigen).ToList();

            foreach (var itemTitPJ in lstTitPJ_entity)
            {
                int id_personajuridica_ant = itemTitPJ.id_personajuridica;
                itemTitPJ.id_solicitud = idSolicitudDestino;
                itemTitPJ.CreateDate = DateTime.Now;
                itemTitPJ.CreateUser = userid;
                itemTitPJ.LastUpdateDate = null;
                itemTitPJ.LastUpdateUser = null;

                repoTitPJ.Insert(itemTitPJ);

                var lstFirPJ_entity = repoFirPJ.GetByFKIdPersonaJuridica(id_personajuridica_ant).ToList();
                foreach (var itemFirPJ in lstFirPJ_entity)
                {
                    int id_firmante_pj_ant = itemFirPJ.id_firmante_pj;

                    itemFirPJ.id_personajuridica = itemTitPJ.id_personajuridica;
                    itemFirPJ.id_solicitud = idSolicitudDestino;
                    repoFirPJ.Insert(itemFirPJ);

                    var lstTitPJPF_entity = repoTitPJPF.GetByFKIdPersonaJuridica(id_personajuridica_ant).ToList();
                    foreach (var itemTitPJPF in lstTitPJPF_entity.Where(x => x.id_firmante_pj == id_firmante_pj_ant))
                    {
                        itemTitPJPF.id_personajuridica = itemTitPJ.id_personajuridica;
                        itemTitPJPF.id_solicitud = idSolicitudDestino;
                        itemTitPJPF.id_firmante_pj = itemFirPJ.id_firmante_pj;
                        repoTitPJPF.Insert(itemTitPJPF);
                    }

                }


            }
            #endregion

        }


        public IEnumerable<SolicitudesAprobadasDTO> GetSolicitudesAprobadas(int? Anio_Expediente, int? Nro_Expediente, int? nro_partida_matriz, string cuit)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoSol = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            repoTransf = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
            repoCPadron = new ConsultaPadronSolicitudesBL();
            repoCPadronUbicaciones = new ConsultaPadronUbicacionesBL();

            IEnumerable<SSIT_Solicitudes> elements = null;
            IEnumerable<Transf_Solicitudes> elements_transf = null;
            IEnumerable<SolicitudesAprobadasDTO> resultsSol = null;
            IEnumerable<SolicitudesAprobadasDTO> resultsTransf = null;
            IEnumerable<SolicitudesAprobadasDTO> results = null;

            if (Anio_Expediente.HasValue && Nro_Expediente.HasValue)
            {
                elements = repoSol.GetSolicitudesAprobadasxExpediente(Anio_Expediente.Value, Nro_Expediente.Value).ToList();
                elements_transf = repoTransf.GetSolicitudesAprobadasxExpediente(Anio_Expediente.Value, Nro_Expediente.Value).ToList();
            }


            if (nro_partida_matriz.HasValue)
            {
                var result = repoSol.GetSolicitudesAprobadasxPartidaMatriz(nro_partida_matriz.Value);
                var result_transf = repoTransf.GetSolicitudesAprobadasxPartidaMatriz(nro_partida_matriz.Value);

                if (elements != null)
                    elements.Union(result);
                else
                    elements = result;

                if (elements_transf != null)
                    elements_transf.Union(result_transf);
                else
                    elements_transf = result_transf;

            }

            if (!string.IsNullOrEmpty(cuit))
            {
                var result = repoSol.GetSolicitudesAprobadasxCUIT(cuit);
                var result_transf = repoTransf.GetSolicitudesAprobadasxCUIT(cuit);

                if (elements != null)
                    elements.Union(result);
                else
                    elements = result;

                if (elements_transf != null)
                    elements_transf.Union(result_transf);
                else
                    elements_transf = result_transf;

            }

            if (elements != null)
            {
                // Llenado de datos de solicitudes de Habilitación / Ampliacion / Redistribuciones de Uso.
                resultsSol = mapperBase.Map<IEnumerable<SSIT_Solicitudes>, IEnumerable<SolicitudesAprobadasDTO>>(elements);

                foreach (var item in resultsSol)
                {
                    Encomienda item_encomienda = null;
                    Encomienda_DatosLocal item_datos_local = null;

                    var item_sol = elements.FirstOrDefault(x => x.id_solicitud == item.IdSolicitud);
                    if (item_sol != null)
                        item_encomienda = item_sol.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Where(x => x.id_estado == (int)StaticClass.Constantes.Encomienda_Estados.Aprobada_por_el_consejo).OrderByDescending(o => o.id_encomienda).FirstOrDefault();

                    if (item_encomienda != null)
                    {
                        item_datos_local = item_encomienda.Encomienda_DatosLocal.FirstOrDefault();
                        item.IdEncomienda = item_encomienda.id_encomienda;
                    }

                    if (item_datos_local != null)
                        item.SuperficieTotal = item_datos_local.superficie_cubierta_dl + item_datos_local.superficie_descubierta_dl;

                    string titulares_pf = string.Join(" / ", item_sol.SSIT_Solicitudes_Titulares_PersonasFisicas.Select(s => s.Apellido + "," + s.Nombres).ToArray());
                    string titulares_pj = string.Join(" / ", item_sol.SSIT_Solicitudes_Titulares_PersonasJuridicas.Select(s => s.Razon_Social).ToArray());
                    item.Titulares = titulares_pf + titulares_pj;


                }
            }

            if (elements_transf != null)
            {
                // Llenado de datos de solicitudes de Transferencias
                resultsTransf = mapperBase.Map<IEnumerable<Transf_Solicitudes>, IEnumerable<SolicitudesAprobadasDTO>>(elements_transf);

                foreach (var item in resultsTransf)
                {
                    var item_transf = elements_transf.FirstOrDefault(x => x.id_solicitud == item.IdSolicitud);
                    var item_transfEncomiendaId = elements_transf
                    .SelectMany(x => x.Encomienda_Transf_Solicitudes
                        .Select(y => y.id_encomienda))
                    .FirstOrDefault();
                    var lstUbicaciones = item_transf.CPadron_Solicitudes.CPadron_Ubicaciones;
                    var item_datos_local = item_transf.CPadron_Solicitudes.CPadron_DatosLocal.FirstOrDefault();

                    item.SuperficieTotal = item_datos_local.superficie_cubierta_dl + item_datos_local.superficie_descubierta_dl;
                    item.IdEncomienda = item_transfEncomiendaId;


                    List<SSITSolicitudesUbicacionesDTO> lstUbicacionesDTO = new List<SSITSolicitudesUbicacionesDTO>();
                    foreach (var itemUbic in lstUbicaciones)
                    {
                        lstUbicacionesDTO.Add(new SSITSolicitudesUbicacionesDTO()
                        {
                            IdSolicitud = item.IdSolicitud,
                            CreateDate = itemUbic.CreateDate,
                            CreateUser = itemUbic.CreateUser,
                            IdSubtipoUbicacion = itemUbic.id_subtipoubicacion,
                            IdZonaPlaneamiento = itemUbic.id_zonaplaneamiento,
                            Depto = (string.IsNullOrEmpty(itemUbic.Depto) ? string.Empty : itemUbic.Depto),
                            DeptoLocalUbicacion = (string.IsNullOrEmpty(itemUbic.deptoLocal_cpadronubicacion) ? string.Empty : itemUbic.deptoLocal_cpadronubicacion),
                            IdUbicacion = itemUbic.id_ubicacion,
                            Local = (string.IsNullOrEmpty(itemUbic.Local) ? string.Empty : itemUbic.Local),
                            LocalSubtipoUbicacion = (string.IsNullOrEmpty(itemUbic.local_subtipoubicacion) ? string.Empty : itemUbic.local_subtipoubicacion),
                            Torre = (string.IsNullOrEmpty(itemUbic.Torre) ? string.Empty : itemUbic.Torre),
                        });

                    }

                    item.SSITSolicitudesUbicacionesDTO = lstUbicacionesDTO;


                    string titulares_pf = string.Join(" / ", item_transf.Transf_Titulares_PersonasFisicas.Select(s => s.Apellido + "," + s.Nombres).ToArray());
                    string titulares_pj = string.Join(" / ", item_transf.Transf_Titulares_PersonasJuridicas.Select(s => s.Razon_Social).ToArray());
                    item.Titulares = titulares_pf + titulares_pj;


                }

            }


            // Arma un solo resultado con todas las aprobaciones
            if (resultsSol != null)
                results = resultsSol;

            if (resultsTransf != null)
            {
                if (resultsSol != null)
                    results = results.Union(resultsTransf);
                else
                    results = resultsTransf;
            }


            return results;

        }

    }
}
