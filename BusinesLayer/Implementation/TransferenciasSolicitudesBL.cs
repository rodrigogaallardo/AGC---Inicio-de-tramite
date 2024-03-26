using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;
using static StaticClass.Errors;
using ExternalService;

namespace BusinesLayer.Implementation
{
    public class TransferenciasSolicitudesBL : ITransferenciasSolicitudesBL<TransferenciasSolicitudesDTO>
    {

        public static string ssit_transferencia_consulta_padron_no_aprobada = "";
        public static string ssit_transferencia_solicitud_iniciada = "";
        private TransferenciasSolicitudesRepository repo = null;
        private ConsultaPadronSolicitudesRepository repoConsultaPadron = null;
        private SSITSolicitudesRepository repoSolicitud = null;
        private SGITramitesTareasTransferenciasRepository repoSGITramitesTareas = null;

        private ItemDirectionRepository itemRepo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciasSolicitudesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transf_Solicitudes, TransferenciasSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.NumeroExpedienteSade, source => source.MapFrom(p => p.NroExpedienteSade))
                    .ForMember(dest => dest.Documentos, source => source.MapFrom(p => p.Transf_DocumentosAdjuntos))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.SubTipoExpediente, source => source.MapFrom(p => p.SubtipoExpediente))
                    .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    .ForMember(dest => dest.TitularesPersonasFisicas, source => source.MapFrom(p => p.Transf_Titulares_PersonasFisicas))
                    .ForMember(dest => dest.TitularesPersonasJuridicas, source => source.MapFrom(p => p.Transf_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.FirmantesPersonasFisicas, source => source.MapFrom(p => p.Transf_Firmantes_PersonasFisicas))
                    .ForMember(dest => dest.FirmantesPersonasJuridicas, source => source.MapFrom(p => p.Transf_Firmantes_PersonasJuridicas))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.Transf_Solicitudes_Observaciones))
                    .ForMember(dest => dest.TipoTransmision, source => source.MapFrom(p => p.TiposdeTransmision))
                    .ForMember(dest => dest.TitularesPersonasSolicitudFisicas, source => source.MapFrom(p => p.Transf_Titulares_Solicitud_PersonasFisicas))
                    .ForMember(dest => dest.TitularesPersonasSolicitudJuridicas, source => source.MapFrom(p => p.Transf_Titulares_Solicitud_PersonasJuridicas))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Transf_Ubicaciones))
                    .ForMember(dest => dest.Plantas, source => source.MapFrom(p => p.Transf_Plantas))
                    .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes))
                    ;

                cfg.CreateMap<TransferenciasSolicitudesDTO, Transf_Solicitudes>()
                .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                .ForMember(dest => dest.NroExpedienteSade, source => source.MapFrom(p => p.NumeroExpedienteSade))
                .ForMember(dest => dest.Transf_DocumentosAdjuntos, source => source.Ignore())
                .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                .ForMember(dest => dest.TipoEstadoSolicitud, source => source.Ignore())
                .ForMember(dest => dest.Transf_Titulares_PersonasFisicas, source => source.Ignore())
                .ForMember(dest => dest.Transf_Titulares_PersonasJuridicas, source => source.Ignore())
                .ForMember(dest => dest.Transf_Titulares_PersonasJuridicas_PersonasFisicas, source => source.Ignore())
                .ForMember(dest => dest.Transf_Solicitudes_Observaciones, source => source.Ignore())
                .ForMember(dest => dest.TiposdeTransmision, source => source.Ignore())
                .ForMember(dest => dest.Transf_Titulares_Solicitud_PersonasFisicas, source => source.Ignore())
                .ForMember(dest => dest.Transf_Titulares_Solicitud_PersonasJuridicas, source => source.Ignore())
                .ForMember(dest => dest.Transf_Ubicaciones, source => source.MapFrom(p => p.Ubicaciones))
                .ForMember(dest => dest.Transf_Plantas, source => source.MapFrom(p => p.Plantas))
                .ForMember(dest => dest.Encomienda_Transf_Solicitudes, source => source.MapFrom(p => p.EncomiendaTransfSolicitudesDTO))
                ;

                cfg.CreateMap<Transf_DocumentosAdjuntos, TransferenciasDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_docadjunto))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tdocreq))
                    .ForMember(dest => dest.TipoDocumentoRequeridoDetalle, source => source.MapFrom(p => p.tdocreq_detalle))
                    .ForMember(dest => dest.IdTipoDocsis, source => source.MapFrom(p => p.id_tipodocsis))
                    .ForMember(dest => dest.IdFile, source => source.MapFrom(p => p.id_file))
                    .ForMember(dest => dest.GeneradoxSistema, source => source.MapFrom(p => p.generadoxSistema))
                    .ForMember(dest => dest.NombreArchivo, source => source.MapFrom(p => p.nombre_archivo))
                    .ForMember(dest => dest.IdAgrupamiento, source => source.MapFrom(p => p.id_agrupamiento))
                    .ForMember(dest => dest.TipoDocumentoRequerido, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));

                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_Solicitud_PersonasFisicas));

                cfg.CreateMap<TransferenciaFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.TipoCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_PersonasFisicas));

                cfg.CreateMap<TransferenciaFirmantePersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));

                cfg.CreateMap<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>().ReverseMap();
                cfg.CreateMap<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>().ReverseMap();


                cfg.CreateMap<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_PersonasJuridicas));

                cfg.CreateMap<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Email));

                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>()
               .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
               .ForMember(dest => dest.Transf_Firmantes_Solicitud_PersonasJuridicas, source => source.MapFrom(p => p.Firmantes));

                cfg.CreateMap<Transf_Titulares_Solicitud_PersonasJuridicas, TransferenciasTitularesSolicitudPersonasJuridicasDTO>()
              .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_Solicitud_PersonasJuridicas))
              .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social));

                cfg.CreateMap<TransferenciaFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.TipoCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>()
                    .ForMember(dest => dest.id_docadjunto, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tdocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.tdocreq_detalle, source => source.MapFrom(p => p.TipoDocumentoRequeridoDetalle))
                    .ForMember(dest => dest.id_tipodocsis, source => source.MapFrom(p => p.IdTipoDocsis))
                    .ForMember(dest => dest.id_file, source => source.MapFrom(p => p.IdFile))
                    .ForMember(dest => dest.generadoxSistema, source => source.MapFrom(p => p.GeneradoxSistema))
                    .ForMember(dest => dest.nombre_archivo, source => source.MapFrom(p => p.NombreArchivo))
                    .ForMember(dest => dest.id_agrupamiento, source => source.MapFrom(p => p.IdAgrupamiento))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.Ignore());

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();

                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();


                #region "TipoTramite"
                cfg.CreateMap<TipoTramiteDTO, TipoTramite>()
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite, source => source.MapFrom(p => p.CodTipoTramite))
                    .ForMember(dest => dest.descripcion_tipotramite, source => source.MapFrom(p => p.DescripcionTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite_ws, source => source.MapFrom(p => p.CodTipoTramiteWs));

                cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                    .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                    .ForMember(dest => dest.CodTipoTramiteWs, source => source.MapFrom(p => p.cod_tipotramite_ws));
                #endregion
                #region "TipoExpediente"
                cfg.CreateMap<TipoExpedienteDTO, TipoExpediente>()
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente, source => source.MapFrom(p => p.CodTipoExpediente))
                    .ForMember(dest => dest.descripcion_tipoexpediente, source => source.MapFrom(p => p.DescripcionTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente_ws, source => source.MapFrom(p => p.CodTipoExpedienteWs));

                cfg.CreateMap<TipoExpediente, TipoExpedienteDTO>()
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpediente, source => source.MapFrom(p => p.cod_tipoexpediente))
                    .ForMember(dest => dest.DescripcionTipoExpediente, source => source.MapFrom(p => p.descripcion_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpedienteWs, source => source.MapFrom(p => p.cod_tipoexpediente_ws));
                #endregion
                #region "SubTipoExpediente"
                cfg.CreateMap<SubTipoExpedienteDTO, SubtipoExpediente>()
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente, source => source.MapFrom(p => p.CodSubTipoExpediente))
                    .ForMember(dest => dest.descripcion_subtipoexpediente, source => source.MapFrom(p => p.DescripcionSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente_ws, source => source.MapFrom(p => p.CodSubTipoExpedienteWs));

                cfg.CreateMap<SubtipoExpediente, SubTipoExpedienteDTO>()
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpediente, source => source.MapFrom(p => p.cod_subtipoexpediente))
                    .ForMember(dest => dest.DescripcionSubTipoExpediente, source => source.MapFrom(p => p.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpedienteWs, source => source.MapFrom(p => p.cod_subtipoexpediente_ws));
                #endregion

                #region "TipoTransmision"
                cfg.CreateMap<TipodeTransmisionDTO, TiposdeTransmision>();

                cfg.CreateMap<TiposdeTransmision, TipodeTransmisionDTO>();
                #endregion

                cfg.CreateMap<TipoEstadoSolicitud, TipoEstadoSolicitudDTO>();

                cfg.CreateMap<TipoEstadoSolicitudDTO, TipoEstadoSolicitud>();

                cfg.CreateMap<Transf_Titulares_PersonasFisicas, TransferenciasTitularesPersonasFisicasDTO>()
                   .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                   .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                   .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                   .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                   .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                   .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                   .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_Localidad))
                   .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                   .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_PersonasFisicas));

                cfg.CreateMap<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Transf_Firmantes_PersonasFisicas, source => source.MapFrom(p => p.Firmantes));

                cfg.CreateMap<Transf_Titulares_PersonasJuridicas, TransferenciasTitularesPersonasJuridicasDTO>()

                 .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                 .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                 .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                 .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                 .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.CUIT))
                 .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                 .ForMember(dest => dest.NumeroIibb, source => source.MapFrom(p => p.Nro_IIBB))
                 .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                 .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                 .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                 .ForMember(dest => dest.Firmantes, source => source.MapFrom(p => p.Transf_Firmantes_PersonasJuridicas));

                cfg.CreateMap<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>()
                .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                .ForMember(dest => dest.CUIT, source => source.MapFrom(p => p.Cuit))
                .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NumeroIibb))
                .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                .ForMember(dest => dest.Transf_Firmantes_PersonasJuridicas, source => source.MapFrom(p => p.Firmantes));

                cfg.CreateMap<Transf_Firmantes_PersonasJuridicas, TransferenciaFirmantePersonasJuridicasDTO>()
                    .ForMember(dest => dest.TipoCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<Transf_Firmantes_PersonasFisicas, TransferenciaFirmantePersonasFisicasDTO>()
                    .ForMember(dest => dest.TipoCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>();
                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>();

                cfg.CreateMap<TiposDeCaracterLegal, TiposDeCaracterLegalDTO>()
                .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                .ForMember(dest => dest.CodTipoCaracter, source => source.MapFrom(p => p.cod_tipocaracter))
                .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                .ForMember(dest => dest.DisponibilidadTipoCaracter, source => source.MapFrom(p => p.disponibilidad_tipocaracter))
                .ForMember(dest => dest.MuestraCargoTipoCaracter, source => source.MapFrom(p => p.muestracargo_tipocaracter));

                cfg.CreateMap<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>()
                .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                .ForMember(dest => dest.cod_tipocaracter, source => source.MapFrom(p => p.CodTipoCaracter))
                .ForMember(dest => dest.nom_tipocaracter, source => source.MapFrom(p => p.NomTipoCaracter))
                .ForMember(dest => dest.disponibilidad_tipocaracter, source => source.MapFrom(p => p.DisponibilidadTipoCaracter))
                .ForMember(dest => dest.muestracargo_tipocaracter, source => source.MapFrom(p => p.MuestraCargoTipoCaracter));

                cfg.CreateMap<TransferenciasSolicitudesObservacionesDTO, Transf_Solicitudes_Observaciones>().ReverseMap()
                   .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_solobs))
                   .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                   .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                   .ForMember(dest => dest.Leido, source => source.MapFrom(p => p.leido))
                   .ForMember(dest => dest.Usuario, source => source.MapFrom(p => p.aspnet_Users.Usuario))
                   .ForMember(dest => dest.UsuarioSGI, source => source.MapFrom(p => p.aspnet_Users.SGI_Profiles));

                cfg.CreateMap<Transf_Solicitudes_Observaciones, TransferenciasSolicitudesObservacionesDTO>().ReverseMap()
                    .ForMember(dest => dest.id_solobs, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.observaciones, source => source.MapFrom(p => p.Observaciones))
                    .ForMember(dest => dest.leido, source => source.MapFrom(p => p.Leido));

                cfg.CreateMap<Usuario, UsuarioDTO>();

                cfg.CreateMap<SGI_Profiles, UsuarioDTO>()
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.Nombres))
                    .ForMember(dest => dest.Apellido, source => source.MapFrom(p => p.Apellido));


                cfg.CreateMap<Transf_Ubicaciones, TransferenciaUbicacionesDTO>()
                   .ForMember(dest => dest.IdTransferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                   .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                   .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                   .ForMember(dest => dest.IdSubTipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                   .ForMember(dest => dest.LocalSubTipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                   .ForMember(dest => dest.DeptoLocalTransferenciaUbicacion, source => source.MapFrom(p => p.deptoLocal_transfubicacion))
                   .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                   .ForMember(dest => dest.PropiedadesHorizontales, source => source.MapFrom(p => p.Transf_Ubicaciones_PropiedadHorizontal))
                   .ForMember(dest => dest.Puertas, source => source.MapFrom(p => p.Transf_Ubicaciones_Puertas))
                   .ForMember(dest => dest.ZonaPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                   .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                   .ForMember(dest => dest.SubTipoUbicacion, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                   .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                   .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                   .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local))
                   .ForMember(dest => dest.TransferenciaUbicacionesDistritosDTO, source => source.MapFrom(p => p.Transf_Ubicaciones_Distritos))
                   .ForMember(dest => dest.TransferenciaUbicacionesMixturasDTO, source => source.MapFrom(p => p.Transf_Ubicaciones_Mixturas));

                cfg.CreateMap<TransferenciaUbicacionesDTO, Transf_Ubicaciones>()
                    .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTransferenciaUbicacion))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubTipoUbicacion))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Ubicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubTipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_transfubicacion, source => source.MapFrom(p => p.DeptoLocalTransferenciaUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Transf_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.PropiedadesHorizontales))
                    .ForMember(dest => dest.Transf_Ubicaciones_Puertas, source => source.MapFrom(p => p.Puertas))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local));

                cfg.CreateMap<Transf_Ubicaciones_Puertas, TransferenciasUbicacionesPuertasDTO>()
                   .ForMember(dest => dest.IdTranferenciaPuerta, source => source.MapFrom(p => p.id_transfpuerta))
                   .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                   .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                   .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>()
                  .ForMember(dest => dest.id_transfpuerta, source => source.MapFrom(p => p.IdTranferenciaPuerta))
                  .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTranferenciaUbicacion))
                  .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                  .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                  .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));

                cfg.CreateMap<Transf_Ubicaciones_PropiedadHorizontal, TransferenciasUbicacionPropiedadHorizontalDTO>()
                   .ForMember(dest => dest.IdTranferenciaPropiedadHorizontal, source => source.MapFrom(p => p.id_transfprophorizontal))
                   .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                   .ForMember(dest => dest.UbicacionPropiedadaHorizontal, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<TransferenciasUbicacionPropiedadHorizontalDTO, Transf_Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_transfprophorizontal, source => source.MapFrom(p => p.IdTranferenciaPropiedadHorizontal))
                   .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTranferenciaUbicacion))
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));

                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>()
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento));

                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.CoordenadaX, source => source.MapFrom(p => p.Coordenada_X))
                    .ForMember(dest => dest.CoordenadaY, source => source.MapFrom(p => p.Coordenada_Y))
                    .ForMember(dest => dest.InhibidaObservacion, source => source.MapFrom(p => p.Inhibida_Observacion))
                    .ForMember(dest => dest.BajaLogica, source => source.MapFrom(p => p.baja_logica))
                    .ForMember(dest => dest.PisosBajoRasante, source => source.MapFrom(p => p.pisos_bajo_rasante))
                    .ForMember(dest => dest.PisosSobreRasante, source => source.MapFrom(p => p.pisos_sobre_rasante))
                    .ForMember(dest => dest.Unidades, source => source.MapFrom(p => p.unidades))
                    .ForMember(dest => dest.Locales, source => source.MapFrom(p => p.locales))
                    .ForMember(dest => dest.CantPh, source => source.MapFrom(p => p.cant_ph))
                    .ForMember(dest => dest.Vuc, source => source.MapFrom(p => p.vuc))
                    .ForMember(dest => dest.IdComuna, source => source.MapFrom(p => p.id_comuna))
                    .ForMember(dest => dest.IdBarrio, source => source.MapFrom(p => p.id_barrio))
                    .ForMember(dest => dest.IdAreaHospitalaria, source => source.MapFrom(p => p.id_areahospitalaria))
                    .ForMember(dest => dest.IdComisaria, source => source.MapFrom(p => p.id_comisaria))
                    .ForMember(dest => dest.IdDistritoEscolar, source => source.MapFrom(p => p.id_distritoescolar))
                    .ForMember(dest => dest.FechaUltimaActualizacionUsig, source => source.MapFrom(p => p.FechaUltimaActualizacionUSIG))
                    .ForMember(dest => dest.CantiActualizacionesUsig, source => source.MapFrom(p => p.CantiActualizacionesUSIG))
                    .ForMember(dest => dest.TipoPersonaTitularAgip, source => source.MapFrom(p => p.TipoPersonaTitularAGIP))
                    .ForMember(dest => dest.TitularAgip, source => source.MapFrom(p => p.TitularAGIP))
                    .ForMember(dest => dest.FechaAltaAgip, source => source.MapFrom(p => p.FechaAltaAGIP))
                    .ForMember(dest => dest.ZonasPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.UbicacionesPuertas, source => source.MapFrom(p => p.Ubicaciones_Puertas));

                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Coordenada_X, source => source.MapFrom(p => p.CoordenadaX))
                    .ForMember(dest => dest.Coordenada_Y, source => source.MapFrom(p => p.CoordenadaY))
                    .ForMember(dest => dest.Inhibida_Observacion, source => source.MapFrom(p => p.InhibidaObservacion))
                    .ForMember(dest => dest.baja_logica, source => source.MapFrom(p => p.BajaLogica))
                    .ForMember(dest => dest.pisos_bajo_rasante, source => source.MapFrom(p => p.PisosBajoRasante))
                    .ForMember(dest => dest.pisos_sobre_rasante, source => source.MapFrom(p => p.PisosSobreRasante))
                    .ForMember(dest => dest.unidades, source => source.MapFrom(p => p.Unidades))
                    .ForMember(dest => dest.locales, source => source.MapFrom(p => p.Locales))
                    .ForMember(dest => dest.cant_ph, source => source.MapFrom(p => p.CantPh))
                    .ForMember(dest => dest.vuc, source => source.MapFrom(p => p.Vuc))
                    .ForMember(dest => dest.id_comuna, source => source.MapFrom(p => p.IdComuna))
                    .ForMember(dest => dest.id_barrio, source => source.MapFrom(p => p.IdBarrio))
                    .ForMember(dest => dest.id_areahospitalaria, source => source.MapFrom(p => p.IdAreaHospitalaria))
                    .ForMember(dest => dest.id_comisaria, source => source.MapFrom(p => p.IdComisaria))
                    .ForMember(dest => dest.id_distritoescolar, source => source.MapFrom(p => p.IdDistritoEscolar))
                    .ForMember(dest => dest.FechaUltimaActualizacionUSIG, source => source.MapFrom(p => p.FechaUltimaActualizacionUsig))
                    .ForMember(dest => dest.CantiActualizacionesUSIG, source => source.MapFrom(p => p.CantiActualizacionesUsig))
                    .ForMember(dest => dest.TipoPersonaTitularAGIP, source => source.MapFrom(p => p.TipoPersonaTitularAgip))
                    .ForMember(dest => dest.TitularAGIP, source => source.MapFrom(p => p.TitularAgip))
                    .ForMember(dest => dest.FechaAltaAGIP, source => source.MapFrom(p => p.FechaAltaAgip))
                    .ForMember(dest => dest.Zonas_Planeamiento, source => source.MapFrom(p => p.ZonasPlaneamiento))
                    .ForMember(dest => dest.Ubicaciones_Puertas, source => source.MapFrom(p => p.UbicacionesPuertas));

                cfg.CreateMap<Ubicaciones_Puertas, UbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.IdUbicacionPuerta, source => source.MapFrom(p => p.id_ubic_puerta))
                    .ForMember(dest => dest.NroPuertaUbic, source => source.MapFrom(p => p.NroPuerta_ubic))
                    .ForMember(dest => dest.TipoPuerta, source => source.MapFrom(p => p.tipo_puerta));

                cfg.CreateMap<UbicacionesPuertasDTO, Ubicaciones_Puertas>()
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.id_ubic_puerta, source => source.MapFrom(p => p.IdUbicacionPuerta))
                    .ForMember(dest => dest.NroPuerta_ubic, source => source.MapFrom(p => p.NroPuertaUbic))
                    .ForMember(dest => dest.tipo_puerta, source => source.MapFrom(p => p.TipoPuerta));


                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));

                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>();


                cfg.CreateMap<Transf_Plantas, TransferenciasPlantasDTO>()
                    .ForMember(dest => dest.DetalleTransferenciaTipoSector, source => source.MapFrom(p => p.detalle_transftiposector))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTransferenciaTipoSector, source => source.MapFrom(p => p.id_transftiposector))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<TransferenciasPlantasDTO, Transf_Plantas>()
                    .ForMember(dest => dest.detalle_transftiposector, source => source.MapFrom(p => p.DetalleTransferenciaTipoSector))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_transftiposector, source => source.MapFrom(p => p.IdTransferenciaTipoSector))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

                #region "TipoSector"
                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
                #endregion

                #region "SSIT_Solicitudes_Ubicaciones_Mixturas"
                cfg.CreateMap<Transf_Ubicaciones_Mixturas, TransferenciaUbicacionesMixturasDTO>()
                .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura));

                cfg.CreateMap<TransferenciaUbicacionesMixturasDTO, Transf_Ubicaciones_Mixturas>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion
                #region "SSIT_Solicitudes_Ubicaciones_Distritos"
                cfg.CreateMap<Transf_Ubicaciones_Distritos, TransferenciaUbicacionesDistritosDTO>()
                .ForMember(dest => dest.UbicacionesCatalogoDistritosDTO, source => source.MapFrom(p => p.Ubicaciones_CatalogoDistritos));

                cfg.CreateMap<TransferenciaUbicacionesDistritosDTO, Transf_Ubicaciones_Distritos>()
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

                #region "Encomienda_Transf_Solicitudes"
                cfg.CreateMap<Encomienda_Transf_Solicitudes, EncomiendaTransfSolicitudesDTO>()
                .ForMember(dest => dest.EncomiendaDTO, source => source.MapFrom(p => p.Encomienda));
                cfg.CreateMap<EncomiendaTransfSolicitudesDTO, Encomienda_Transf_Solicitudes>()
                .ForMember(dest => dest.Encomienda, source => source.MapFrom(p => p.EncomiendaDTO));
                #endregion

                #region "Encomienda"
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
                   .ForMember(dest => dest.tipo_anexo, source => source.MapFrom(p => p.tipoAnexo))
                   .ForMember(dest => dest.Encomienda_RubrosCN_Deposito, source => source.MapFrom(p => p.Encomienda_RubrosCN_DepositoDTO))
                   ;

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
                    .ForMember(dest => dest.Encomienda_RubrosCN_DepositoDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN_Deposito))
                    ;

                cfg.CreateMap<Encomienda_RubrosCN_DepositoDTO, Encomienda_RubrosCN_Deposito>()
                .ForMember(dest => dest.RubrosDepositosCN, source => source.MapFrom(p => p.RubrosDepositosCNDTO))
                .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosCNDTO));

                cfg.CreateMap<Encomienda_RubrosCN_Deposito, Encomienda_RubrosCN_DepositoDTO>()
               .ForMember(dest => dest.RubrosDepositosCNDTO, source => source.MapFrom(p => p.RubrosDepositosCN))
               .ForMember(dest => dest.RubrosCNDTO, source => source.MapFrom(p => p.RubrosCN));

                cfg.CreateMap<RubrosCN, RubrosCNDTO>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();

                //.ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                //.ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes));
                #endregion
            });
            mapperBase = config.CreateMapper();
        }

        public string GetMixDistritoZonaySubZonaByTr(int idSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
            return repo.GetMixDistritoZonaySubZonaByTr(idSolicitud);
        }

        public IEnumerable<TransferenciasSolicitudesDTO> GetRangoIdSolicitud(int desde, int hasta)
        {
            try
            {
                int inicio = desde;
                int fin = hasta;

                if (desde >= hasta)
                {
                    inicio = desde;
                    fin = hasta;
                }

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRangoIdSolicitud(inicio, fin);
                var solicitudesDTO = mapperBase.Map<IEnumerable<Transf_Solicitudes>, IEnumerable<TransferenciasSolicitudesDTO>>(elements);
                return solicitudesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TransferenciasSolicitudesDTO> GetListaIdSolicitudTransf(List<int> lista)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetListaIdSolicitudTransf(lista);
                var solicitudesDTO = mapperBase.Map<IEnumerable<Transf_Solicitudes>, IEnumerable<TransferenciasSolicitudesDTO>>(elements);
                return solicitudesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el ultimo id_tramitetarea para volver a regenerar una boleta vencida con ese mismo id
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public int GetIdTramiteTareaPago(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repo = new TransferenciasSolicitudesRepository(unitOfWork);

                int id_tramitetarea = repo.GetIdTramiteTareaPago(IdSolicitud);

                if (id_tramitetarea == 0)
                    throw new Exception(Errors.SSIT_TRANSFERENCIAS_SIN_TAREAS_REVISION);

                return id_tramitetarea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TransferenciasSolicitudesDTO Single(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repo = new TransferenciasSolicitudesRepository(unitOfWork);

                var entity = repo.Single(IdSolicitud);

                var entityDto = mapperBase.Map<Transf_Solicitudes, TransferenciasSolicitudesDTO>(entity);

                var direcciones = GetDireccionesTransf(new List<int>() { entity.id_solicitud });

                entityDto.Direccion = direcciones.FirstOrDefault();

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
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasSolicitudesDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Solicitudes>, IEnumerable<TransferenciasSolicitudesDTO>>(elements);
            return elementsDto;
        }

        /// <summary>
        /// devuelve un ItemDirectionDTO con la solicitud y todas su puertas 
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        public IEnumerable<ItemDirectionDTO> GetDireccionesTransf(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesTransf(lstSolicitudes).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();
                List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

                int id_solicitud_ant = 0;
                string calle_ant = "";
                string Direccion_armada = "";

                if (LstDoorsDirection.Count() > 0)
                {
                    id_solicitud_ant = LstDoorsDirection[0].id_solicitud;
                    calle_ant = LstDoorsDirection[0].calle;
                }
                SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();

                foreach (var puerta in LstDoorsDirection)
                {
                    if (id_solicitud_ant != puerta.id_solicitud)
                    {
                        ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                        itemDireccion.id_solicitud = id_solicitud_ant;
                        itemDireccion.direccion = Direccion_armada;
                        lstDirecciones.Add(itemDireccion);
                        Direccion_armada = "";
                        id_solicitud_ant = puerta.id_solicitud;
                    }

                    int idSub = puerta.idUbicacion ?? 0;

                    if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idSub))
                    {
                        puerta.puerta += "t";
                    }

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


                }

                if (Direccion_armada.Length > 0)
                {
                    ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                    itemDireccion.id_solicitud = id_solicitud_ant;
                    itemDireccion.direccion = Direccion_armada;
                    lstDirecciones.Add(itemDireccion);
                    Direccion_armada = "";
                }

                return lstDirecciones;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDireccionTransf(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesTransf(lstSolicitudes).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();
                List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

                int id_solicitud_ant = 0;
                string calle_ant = "";
                string Direccion_armada = "";

                if (LstDoorsDirection.Count() > 0)
                {
                    id_solicitud_ant = LstDoorsDirection[0].id_solicitud;
                    calle_ant = LstDoorsDirection[0].calle;
                }

                SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();

                foreach (var puerta in LstDoorsDirection)
                {
                    if (id_solicitud_ant != puerta.id_solicitud)
                    {
                        ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                        itemDireccion.id_solicitud = id_solicitud_ant;
                        itemDireccion.direccion = Direccion_armada;
                        lstDirecciones.Add(itemDireccion);
                        Direccion_armada = "";
                        id_solicitud_ant = puerta.id_solicitud;
                    }

                    int idSub = puerta.idUbicacion ?? 0;

                    if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idSub))
                    {
                        puerta.puerta += "t";
                    }

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


                }
                return Direccion_armada;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetOblea(int id_solicitud, Guid userid, int id_file, string FileName)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new TransferenciasSolicitudesRepository(unitOfWork);
                var solicitudEntity = repo.Single(id_solicitud);

                var repoDoc = new TransferenciasDocumentosAdjuntosRepository(unitOfWork);
                int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;

                var DocAdj = repoDoc.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

                if (DocAdj == null)
                {
                    #region oblea
                    DocAdj = new Transf_DocumentosAdjuntos();
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
                    //solicitudEntity.FechaLibrado = DateTime.Now;
                    solicitudEntity.LastUpdateUser = userid;
                    solicitudEntity.LastUpdateDate = DateTime.Now;
                    repo.Update(solicitudEntity);
                    unitOfWork.Commit();
                }
            }
            return true;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(TransferenciasSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(objectDto);
                    var insertOk = repo.Insert(elementDto);

                    unitOfWork.Commit();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(TransferenciasSolicitudesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(TransferenciasSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWork);
                    var repoT = new SGITramitesTareasTransferenciasRepository(unitOfWork);
                    var repoTT = new SGITramitesTareasRepository(unitOfWork);
                    var TramitesTarea = repoT.GetByFKIdSolicitud(objectDto.IdSolicitud);

                    foreach (var ttc in TramitesTarea)
                    {
                        var elementEntityTT = repoTT.Single(ttc.id_tramitetarea);
                        repoT.Delete(ttc);
                        repoTT.Delete(elementEntityTT);
                    }

                    var elementDto = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(objectDto);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void ActualizarEstadoCompleto(TransferenciasSolicitudesDTO transferencia, Guid userId)
        {
            try
            {
                ParametrosBL paramBL = new ParametrosBL();
                int nroTrReferencia = 0;
                int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);
                if (transferencia.IdSolicitud > nroTrReferencia)
                {
                    if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    {
                        if ((!transferencia.TitularesPersonasJuridicas.Any() && !transferencia.TitularesPersonasFisicas.Any())
                            || !transferencia.Ubicaciones.Any())
                            return;

                        transferencia.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.COMP;
                        transferencia.LastUpdateUser = userId;
                        transferencia.LastUpdateDate = DateTime.Now;

                        var entity = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(transferencia);

                        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                        using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                        {
                            repo = new TransferenciasSolicitudesRepository(unitOfWorkTran);
                            repo.Update(entity);
                            unitOfWorkTran.Commit();
                        }
                    }
                    else if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
                            !transferencia.Ubicaciones.Any())
                    {
                        transferencia.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                        transferencia.LastUpdateUser = userId;
                        transferencia.LastUpdateDate = DateTime.Now;

                        var entity = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(transferencia);

                        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                        using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                        {
                            repo = new TransferenciasSolicitudesRepository(unitOfWorkTran);
                            repo.Update(entity);
                            unitOfWorkTran.Commit();
                        }
                    }
                }
                else
                {
                    if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    {
                        if (!transferencia.TitularesPersonasJuridicas.Any() && !transferencia.TitularesPersonasFisicas.Any())
                            return;


                        transferencia.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.COMP;
                        transferencia.LastUpdateUser = userId;
                        transferencia.LastUpdateDate = DateTime.Now;

                        var entity = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(transferencia);

                        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                        using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                        {
                            repo = new TransferenciasSolicitudesRepository(unitOfWorkTran);
                            repo.Update(entity);
                            unitOfWorkTran.Commit();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        public void ValidarSolicitud(TransferenciasSolicitudesDTO transferencia)
        {
            //
            ConsultaPadronSolicitudesBL CPadronBL = new ConsultaPadronSolicitudesBL();

            var cp = CPadronBL.Single(transferencia.IdConsultaPadron);

            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                if (!transferencia.TitularesPersonasJuridicas.Any() && !transferencia.TitularesPersonasFisicas.Any())
                    throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_SIN_TITULARES);

                if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR)
                {
                    var documentos = transferencia.Documentos;

                    if (!documentos.Any(p => p.IdTipoDocumentoRequerido == (int)Constantes.TipoDocumentoRequerido.Acta_Notarial))
                        throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_SIN_ACTA_NOTARIAL);

                    if (!documentos.Any(p => p.IdTipoDocumentoRequerido == (int)Constantes.TipoDocumentoRequerido.Edicto))
                        throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_SIN_EDICTOS);
                }

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="userId"></param>
        public void Confirmar(int IdSolicitud, Guid userId)
        {
            try
            {
                ParametrosBL paramBL = new ParametrosBL();
                int nroTrReferencia = 0;
                int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);
                if (IdSolicitud > nroTrReferencia)
                {
                    ConfirmarTransmision(IdSolicitud, userId);
                }
                else
                {
                    int id_estado_sig = ConfirmarInterno(IdSolicitud, userId);

                    if (id_estado_sig == (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL)
                    {
                        ConfirmarInterno(IdSolicitud, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConfirmarSol(int IdSolicitud, Guid userId)
        {
            try
            {
                TransferenciasSolicitudesDTO sol = Single(IdSolicitud);
                if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
                {
                    uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                    using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                    {
                        sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;
                        sol.LastUpdateUser = userId;
                        sol.LastUpdateDate = DateTime.Now;

                        Update(sol);
                        unitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="userId"></param>
        public int ConfirmarInterno(int IdSolicitud, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWork);
                    repoSGITramitesTareas = new SGITramitesTareasTransferenciasRepository(unitOfWork);

                    var entity = repo.Single(IdSolicitud);
                    int id_estado_sig = 0;

                    if (entity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
                    {
                        var tramites = repoSGITramitesTareas.GetByFKIdSolicitud(IdSolicitud);


                        if (tramites.Count() == 1)
                        {
                            id_estado_sig = (int)Constantes.TipoEstadoSolicitudEnum.VALESCR;
                        }
                        else
                        {
                            id_estado_sig = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                        }
                    }
                    else if (entity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR)
                        id_estado_sig = (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL;
                    else if (entity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL)
                        id_estado_sig = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;

                    EngineBL engine = new EngineBL();

                    var tarea = engine.GetUltimaTareaTransferencia(IdSolicitud);
                    int id_estado_confirmado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    int id_estado_Val_Escr = (int)Constantes.TipoEstadoSolicitudEnum.VALESCR;
                    int id_estado_anulado = (int)Constantes.TipoEstadoSolicitudEnum.ANU;
                    int id_estado_Escr_Real = (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL;
                    int id_resultado_actual = 0;

                    IEnumerable<EngineResultadoTareaDTO> tareaResult = null;

                    if (id_estado_sig == id_estado_Val_Escr)
                        id_resultado_actual = (int)Constantes.TareasResultados.EnviaraEscribano; //--Enviar a Escribano
                    else if (id_estado_sig == id_estado_Escr_Real)
                        id_resultado_actual = (int)Constantes.TareasResultados.EscrituraRealizada;// --Escritura Realizada	        
                    else if (id_estado_sig == id_estado_anulado)
                        id_resultado_actual = (int)Constantes.TareasResultados.SolicitudAnulada;// --Solicitud Anulada

                    else if (id_estado_sig == id_estado_confirmado)
                        id_resultado_actual = (int)Constantes.TareasResultados.SolicitudConfirmada;// --Solicitud Confirmada
                    else
                    {
                        tareaResult = engine.GetResultadoTarea(tarea.IdTarea);
                        if (tareaResult.Any())
                            id_resultado_actual = tareaResult.FirstOrDefault().id_resultado;
                    }

                    //mantis 126058: JADHE 47052 - SGI - Ingreso tramite incompleto
                    var listaAdjuntos = entity.Transf_DocumentosAdjuntos.ToList();

                    var adjuntosEdictos = listaAdjuntos.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Edicto)
                                            .Select(x => x).Count();

                    var AdjuntosAN = listaAdjuntos.Where(t => t.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Acta_Notarial)
                                            .Select(x => x).Count();

                    if (!(adjuntosEdictos >= 1 && AdjuntosAN >= 1))
                        throw new Exception(Errors.SSIT_TRANSFERENCIAS_SIN_ADJUNTOS);

                    if (tarea != null)
                    {
                        engine.FinalizarTarea(tarea.IdTramiteTarea, id_resultado_actual, 0, userId);

                        var tareasDTO = engine.GetTareasSiguientes(tarea.IdTarea, id_resultado_actual, tarea.IdTramiteTarea);

                        foreach (var tareaDTO in tareasDTO)
                        {
                            int id_tramitetarea_actual = engine.CrearTarea(IdSolicitud, tareaDTO.id_tarea, userId, unitOfWork);

                            if (tareaDTO.id_tarea == engine.GetIdTarea((int)Constantes.CodigoTareas.CalificarTrámite))
                            {
                                var transtarea = engine.GetTareaTransferencia(IdSolicitud, tareaDTO.id_tarea).Where(x => x.UsuarioAsignadoTramiteTarea.HasValue).OrderByDescending(o => o.IdTramiteTarea);
                                if (transtarea.Any())
                                    engine.AsignarTarea(id_tramitetarea_actual, transtarea.FirstOrDefault().UsuarioAsignadoTramiteTarea.Value, unitOfWork);

                                if (id_estado_sig == id_estado_anulado)
                                    engine.FinalizarTarea(id_tramitetarea_actual, 0, 0, null);

                            }
                        }
                    }
                    entity.id_estado = id_estado_sig;
                    entity.LastUpdateUser = userId;
                    entity.LastUpdateDate = DateTime.Now;
                    repo.Update(entity);
                    unitOfWork.Commit();
                    return id_estado_sig;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ConfirmarTransmision(int IdSolicitud, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWork);
                    repoSGITramitesTareas = new SGITramitesTareasTransferenciasRepository(unitOfWork);

                    //estado librado
                    bool estaLibrado = false;

                    var repoObser = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
                    if (repoObser.ExistenObservacionesdetalleSinProcesarTR(IdSolicitud))
                        throw new Exception(Errors.SSIT_SOLICITUD_OBSERVACIONES_SIN_PROCESAR);

                    var entity = repo.Single(IdSolicitud);
                    int id_estado_sig = 0;

                    if (entity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                        entity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                    {
                        id_estado_sig = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    }

                    EngineBL engine = new EngineBL();

                    var tarea = engine.GetUltimaTareaTransferencia(IdSolicitud);
                    int id_estado_anulado = (int)Constantes.TipoEstadoSolicitudEnum.ANU;
                    int id_resultado_actual = 0;

                    IEnumerable<EngineResultadoTareaDTO> tareaResult = null;

                    id_resultado_actual = (int)Constantes.TareasResultados.SolicitudConfirmada;// --Solicitud Confirmada
                    
                    if (tarea != null)
                    {
                        engine.FinalizarTarea(tarea.IdTramiteTarea, id_resultado_actual, 0, userId);

                        var tareasDTO = engine.GetTareasSiguientes(tarea.IdTarea, id_resultado_actual, tarea.IdTramiteTarea);

                        foreach (var tareaDTO in tareasDTO)
                        {
                            int id_tramitetarea_actual = engine.CrearTarea(IdSolicitud, tareaDTO.id_tarea, userId, unitOfWork);

                            if (tareaDTO.id_tarea == engine.GetIdTarea((int)Constantes.CodigoTareas.CalificarTrámite) ||
                                tareaDTO.id_tarea == engine.GetIdTarea((int)Constantes.CodigoTareas.CalificarTramiteTransmision))
                            {
                                var transtarea = engine.GetTareaTransferencia(IdSolicitud, tareaDTO.id_tarea).Where(x => x.UsuarioAsignadoTramiteTarea.HasValue).OrderByDescending(o => o.IdTramiteTarea);
                                if (transtarea.Any())
                                    engine.AsignarTarea(id_tramitetarea_actual, transtarea.FirstOrDefault().UsuarioAsignadoTramiteTarea.Value, unitOfWork);

                                if (id_estado_sig == id_estado_anulado)
                                    engine.FinalizarTarea(id_tramitetarea_actual, 0, 0, null);

                            }
                        }
                    }

                    //Agregacion de prueba para librado al uso / No valido por habilitacion previa ni por plano de incendio ni si acoge a los beneficios porque
                    // esas validaciones se hicieron previamente al iniciar la habilitacion
                    if (entity.FechaLibrado == null)
                    {
                        entity.FechaLibrado = DateTime.Now;
                        estaLibrado = true;
                        //encuesta = getEncuesta(solicitudEntity, Direccion);
                    }

                    entity.id_estado = id_estado_sig;
                    entity.LastUpdateUser = userId;
                    entity.LastUpdateDate = DateTime.Now;

                    //Pone todas las observaciones en historicas
                    SGITareaCalificarObsGrupoRepository repoObserGrupo = new SGITareaCalificarObsGrupoRepository(unitOfWork);
                    SGITareaCalificarObsDocsRepository repoObserb = new SGITareaCalificarObsDocsRepository(unitOfWork);
                    var listGrupoObs = repoObserGrupo.GetByFKIdSolicitudtr(IdSolicitud);
                    foreach (var gru in listGrupoObs)
                    {
                        var listObser = repoObserb.GetByFKIdObs(gru.id_ObsGrupo);
                        foreach (var obs in listObser)
                        {
                            if (obs.Actual.Value)
                            {
                                obs.Actual = false;
                                repoObserb.Update(obs);
                            }
                        }
                    }                   
                    repo.Update(entity);

                    //LLamo al sp para cargar la tabla de historico de librados al uso para tranferencias.
                    if (estaLibrado)
                    {
                        unitOfWork.Db.Transf_Solicitudes_Historial_LibradoUso_INSERT(entity.id_solicitud, entity.FechaLibrado, DateTime.Now, entity.CreateUser);
                    }

                    unitOfWork.Commit();

                    return id_estado_sig;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="userId"></param>
        public void Anular(int IdSolicitud, Guid userId)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new TransferenciasSolicitudesRepository(unitOfWork);

                var entity = repo.Single(IdSolicitud);

                entity.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.ANU;
                entity.LastUpdateUser = userId;
                entity.LastUpdateDate = DateTime.Now;

                EngineBL blEng = new EngineBL();

                SGITramitesTareasDTO tramite = blEng.GetUltimaTareaTransferencia(IdSolicitud);
                int id_tarea;
                int idTramiteTarea;
                int idResultado;

                if (tramite != null)
                {
                    idResultado = (int)Constantes.ENG_Resultados.SolicitudAnulada;
                    blEng.FinalizarTarea(tramite.IdTramiteTarea, idResultado, 0, userId);
                    id_tarea = blEng.getTareaFinTramite(IdSolicitud);
                    idTramiteTarea = blEng.CrearTarea(IdSolicitud, id_tarea, userId);
                    idResultado = (int)Constantes.ENG_Resultados.Realizado;
                    blEng.FinalizarTarea(idTramiteTarea, idResultado, 0, userId);
                }

                repo.Update(entity);
                unitOfWork.Commit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <param name="CodigoSeguridad"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CrearTransferencia(int IdConsultaPadron, string CodigoSeguridad, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repoConsultaPadron = new ConsultaPadronSolicitudesRepository(unitOfWork);

                var consultaPadron = repoConsultaPadron.Single(IdConsultaPadron);

                if (consultaPadron != null)
                {
                    if (!consultaPadron.CodigoSeguridad.Equals(CodigoSeguridad))
                        throw new Exception(Errors.SSIT_TRANSFERENCIAS_CPADRON_SIN_COINCIDENCIAS);
                }
                else
                {
                    throw new Exception(Errors.SSIT_TRANSFERENCIAS_CPADRON_SIN_COINCIDENCIAS);
                }

                if (consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.APRO)
                {
                    var estado = string.Empty;
                    switch (consultaPadron.id_estado)
                    {
                        case (int)Constantes.ConsultaPadronEstados.ANU:
                            estado = "Trámite Anulado";
                            break;
                        case (int)Constantes.ConsultaPadronEstados.COMP:
                            estado = "Trámite Completo";
                            break;
                        case (int)Constantes.ConsultaPadronEstados.INCOM:
                            estado = "Trámite Incompleto";
                            break;
                        case (int)Constantes.ConsultaPadronEstados.OBS:
                            estado = "Trámite Observado";
                            break;
                        case (int)Constantes.ConsultaPadronEstados.PING:
                            estado = "Trámite Confirmado";
                            break;
                        case (int)Constantes.ConsultaPadronEstados.VIS:
                            estado = "Trámite Visado";
                            break;
                    }
                    ssit_transferencia_consulta_padron_no_aprobada = string.Format("La Consulta al Padrón NO se encuentra aprobada. Su estado actual es {0}, no es posible iniciar el trámite, para iniciarlo la misma debe encontrarse aprobada.", estado);
                    throw new Exception(ssit_transferencia_consulta_padron_no_aprobada);
                }
                var transferencia = consultaPadron.Transf_Solicitudes.Where(p => p.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.ANU && p.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.VENCIDA).FirstOrDefault();

                if (transferencia != null)
                {
                    ssit_transferencia_solicitud_iniciada = string.Format("Ya existe una soliciud iniciada con este número de consulta al padron, por favor continúe con la misma. La solicitud es la Nº " + transferencia.id_solicitud);

                    throw new Exception(ssit_transferencia_solicitud_iniciada);
                }

                string codigo = Funciones.getGenerarCodigoSeguridadEncomiendas();
                TransferenciasSolicitudesDTO transferenciaDTO = new TransferenciasSolicitudesDTO();
                transferenciaDTO.CodigoSeguridad = codigo;
                transferenciaDTO.CreateDate = DateTime.Now;
                transferenciaDTO.CreateUser = userId;
                transferenciaDTO.IdConsultaPadron = IdConsultaPadron;
                transferenciaDTO.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                transferenciaDTO.IdTipoTramite = (int)Constantes.TipoDeTramite.Transferencia;
                transferenciaDTO.IdSubTipoExpediente = consultaPadron.id_subtipoexpediente;
                transferenciaDTO.IdTipoExpediente = consultaPadron.id_tipoexpediente;

                var elementTransferencia = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(transferenciaDTO);

                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWorkTran);

                    repo.Insert(elementTransferencia);

                    EngineBL engine = new EngineBL();
                    int IdTarea = engine.GetIdTarea((int)Constantes.CodigoTareas.SolicituddeTransferencias, unitOfWorkTran);
                    int IdTareaCreada = engine.CrearTarea(elementTransferencia.id_solicitud, IdTarea, transferenciaDTO.CreateUser, unitOfWorkTran);

                    if (IdTareaCreada > 0)
                        engine.AsignarTarea(IdTareaCreada, transferenciaDTO.CreateUser, unitOfWorkTran);
                    else
                        throw new Exception(Errors.SSIT_TRANSFERENCIAS_TAREA_NO_CREADA);


                    unitOfWorkTran.Commit();

                    return elementTransferencia.id_solicitud;
                }
            }
            catch
            {
                throw;
            }
        }

        public int CrearTransicion(int IdConsultaPadron, string CodigoSeguridad, Guid userId, int tipoTransmision)
        {
            int idSolicitudTrans = 0;

            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repoConsultaPadron = new ConsultaPadronSolicitudesRepository(unitOfWork);

                var consultaPadron = repoConsultaPadron.Single(IdConsultaPadron);

                if (consultaPadron == null)
                {
                    throw new Exception(Errors.SSIT_TRANSFERENCIAS_CPADRON_SIN_COINCIDENCIAS);
                }

                string codigo = Funciones.getGenerarCodigoSeguridadEncomiendas();

                TransferenciasSolicitudesDTO transferenciaDTO = new TransferenciasSolicitudesDTO();
                transferenciaDTO.CodigoSeguridad = codigo;
                transferenciaDTO.CreateDate = DateTime.Now;
                transferenciaDTO.CreateUser = userId;
                transferenciaDTO.IdConsultaPadron = IdConsultaPadron;
                transferenciaDTO.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                transferenciaDTO.IdTipoTramite = (int)Constantes.TipoDeTramite.Transferencia;
                transferenciaDTO.IdSubTipoExpediente = consultaPadron.id_subtipoexpediente;
                transferenciaDTO.IdTipoExpediente = consultaPadron.id_tipoexpediente;
                transferenciaDTO.idTipoTransmision = tipoTransmision;
                //transferenciaDTO.ZonaDeclarada = consultaPadron.ZonaDeclarada;



                var elementTransferencia = mapperBase.Map<TransferenciasSolicitudesDTO, Transf_Solicitudes>(transferenciaDTO);

                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasSolicitudesRepository(unitOfWorkTran);

                    repo.Insert(elementTransferencia);

                    EngineBL engine = new EngineBL();
                    int IdTarea = engine.GetIdTarea((int)Constantes.CodigoTareas.SolicituddeTransferencias, unitOfWorkTran);
                    int IdTareaCreada = engine.CrearTarea(elementTransferencia.id_solicitud, IdTarea, transferenciaDTO.CreateUser, unitOfWorkTran);

                    if (IdTareaCreada > 0)
                        engine.AsignarTarea(IdTareaCreada, transferenciaDTO.CreateUser, unitOfWorkTran);
                    else
                        throw new Exception(Errors.SSIT_TRANSFERENCIAS_TAREA_NO_CREADA);


                    unitOfWorkTran.Commit();

                    idSolicitudTrans = elementTransferencia.id_solicitud;
                }

                return idSolicitudTrans;
            }
            catch
            {
                throw;
            }
        }

        public void copiarDatos(int id_solicitud, int id_transf, Guid userid, int idTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaBL encBL = new EncomiendaBL();
                    ParametrosBL paramBL = new ParametrosBL();
                    TransferenciaUbicacionesBL bltu = new TransferenciaUbicacionesBL();
                    TransferenciasTitularesSolicitudPersonasJuridicasBL blcppj = new TransferenciasTitularesSolicitudPersonasJuridicasBL();
                    TransferenciasTitularesSolicitudPersonasFisicasBL blcppf = new TransferenciasTitularesSolicitudPersonasFisicasBL();
                    UbicacionesZonasMixturasBL blMix = new UbicacionesZonasMixturasBL();
                    UbicacionesCatalogoDistritosBL blDis = new UbicacionesCatalogoDistritosBL();
                    SSITDocumentosAdjuntosBL blDoc = new SSITDocumentosAdjuntosBL();
                    TransferenciasDocumentosAdjuntosBL blDocTr = new TransferenciasDocumentosAdjuntosBL();

                    int idEncomienda = 0;
                    int idFile = 0;
                    int idTipoDocReq = 0;
                    string nombreArc = "";
                    string tdocDeta = "";

                    if (idTipoTramite == (int)Constantes.TipoDeTramite.Habilitacion ||
                        idTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion ||
                        idTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso)
                    {
                        idEncomienda = encBL.GetUltimaEncomiendaAprobada(id_solicitud).IdEncomienda;
                        #region ubicacion
                        SSITSolicitudesUbicacionesBL blUbi = new SSITSolicitudesUbicacionesBL();
                        SSITSolicitudesUbicacionesPropiedadHorizontalBL blHor = new SSITSolicitudesUbicacionesPropiedadHorizontalBL();
                        SSITSolicitudesUbicacionesPuertasBL blPuer = new SSITSolicitudesUbicacionesPuertasBL();

                        var lubi = blUbi.GetByFKIdSolicitud(id_solicitud);
                        foreach (var ubi in lubi)
                        {
                            TransferenciaUbicacionesDTO u = new TransferenciaUbicacionesDTO();
                            u.DeptoLocalTransferenciaUbicacion = ubi.DeptoLocalUbicacion;
                            u.Depto = ubi.Depto;
                            u.Local = ubi.Local;
                            u.Torre = ubi.Torre;
                            u.IdSolicitud = id_transf;
                            u.IdSubTipoUbicacion = ubi.IdSubtipoUbicacion;
                            u.IdUbicacion = ubi.IdUbicacion;
                            u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                            u.LocalSubTipoUbicacion = ubi.LocalSubtipoUbicacion;
                            var lhor = blHor.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                            u.PropiedadesHorizontales = new List<TransferenciasUbicacionPropiedadHorizontalDTO>();
                            foreach (var hor in lhor)
                            {
                                TransferenciasUbicacionPropiedadHorizontalDTO h = new TransferenciasUbicacionPropiedadHorizontalDTO();
                                h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                u.PropiedadesHorizontales.Add(h);
                            }
                            var lpuer = blPuer.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                            u.Puertas = new List<TransferenciasUbicacionesPuertasDTO>();
                            foreach (var puer in lpuer)
                            {
                                TransferenciasUbicacionesPuertasDTO p = new TransferenciasUbicacionesPuertasDTO();
                                p.CodigoCalle = puer.CodigoCalle;
                                p.NumeroPuerta = puer.NroPuerta;
                                u.Puertas.Add(p);
                            }
                            var lmix = blMix.GetZonasUbicacion(ubi.IdSolicitudUbicacion);
                            u.TransferenciaUbicacionesMixturasDTO = new List<TransferenciaUbicacionesMixturasDTO>();
                            foreach (var mix in lmix)
                            {
                                TransferenciaUbicacionesMixturasDTO m = new TransferenciaUbicacionesMixturasDTO();
                                m.IdZonaMixtura = mix.IdZona;
                                u.TransferenciaUbicacionesMixturasDTO.Add(m);
                            }
                            var ldis = blDis.GetDistritosUbicacion(ubi.IdSolicitudUbicacion);
                            u.TransferenciaUbicacionesDistritosDTO = new List<TransferenciaUbicacionesDistritosDTO>();
                            foreach (var dis in ldis)
                            {
                                TransferenciaUbicacionesDistritosDTO d = new TransferenciaUbicacionesDistritosDTO();
                                d.IdDistrito = dis.IdDistrito;
                                u.TransferenciaUbicacionesDistritosDTO.Add(d);
                            }
                            u.CreateDate = DateTime.Now;
                            u.CreateUser = userid;
                            bltu.Insert(u);
                        }
                        // unitOfWork.Commit();
                        #endregion

                        #region Titulares CP
                        SSITSolicitudesTitularesPersonasFisicasBL blPfs = new SSITSolicitudesTitularesPersonasFisicasBL();
                        SSITSolicitudesFirmantesPersonasFisicasBL blPffs = new SSITSolicitudesFirmantesPersonasFisicasBL();
                        var listPf = blPfs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPf)
                        {
                            TransferenciasTitularesSolicitudPersonasFisicasDTO n = new TransferenciasTitularesSolicitudPersonasFisicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdSolicitud = id_transf;
                            n.Apellido = p.Apellido;
                            n.Nombres = p.Nombres;
                            n.IdTipoDocumentoPersonal = p.IdTipodocPersonal;
                            n.CUIT = p.Cuit;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.IngresosBrutos = p.IngresosBrutos;
                            n.Calle = p.Calle;
                            n.NumeroPuerta = p.NroPuerta;
                            n.Piso = p.Piso;
                            n.Depto = p.Depto;
                            n.IdLocalidad = p.IdLocalidad;
                            n.CodigoPostal = p.CodigoPostal;
                            n.Telefono = p.Telefono;
                            n.TelefonoMovil = p.TelefonoMovil;
                            n.Email = p.Email;
                            n.Sms = p.Sms;
                            n.MismoFirmante = p.MismoFirmante;
                            n.NumeroDocumento = p.NroDocumento;
                            n.Torre = p.Torre;

                            n.DtoFirmantes = new TransferenciasFirmantesSolicitudPersonasFisicasDTO();
                            p.DtoFirmantes = blPffs.GetByIdSolicitudIdPersonaFisica(id_solicitud, p.IdPersonaFisica).FirstOrDefault();
                            if (p.DtoFirmantes != null)
                            {
                                n.DtoFirmantes.id_solicitud = id_transf;
                                n.DtoFirmantes.Apellido = p.DtoFirmantes.Apellido;
                                n.DtoFirmantes.Nombres = p.DtoFirmantes.Nombres;
                                n.DtoFirmantes.id_tipodoc_personal = p.DtoFirmantes.IdTipoDocPersonal;
                                n.DtoFirmantes.Nro_Documento = p.DtoFirmantes.NroDocumento;
                                n.DtoFirmantes.id_tipocaracter = p.DtoFirmantes.IdTipoCaracter;
                                n.DtoFirmantes.Cuit = p.DtoFirmantes.Cuit;
                            }
                            blcppf.Insert(n);
                        }

                        SSITSolicitudesTitularesPersonasJuridicasBL blPjs = new SSITSolicitudesTitularesPersonasJuridicasBL();
                        SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL blcppjpf = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL();
                        SSITSolicitudesFirmantesPersonasJuridicasBL blsoljpf = new SSITSolicitudesFirmantesPersonasJuridicasBL();

                        var listPj = blPjs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPj)
                        {
                            TransferenciasTitularesSolicitudPersonasJuridicasDTO n = new TransferenciasTitularesSolicitudPersonasJuridicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdSolicitud = id_transf;
                            n.IdTipoSociedad = p.IdTipoSociedad;
                            n.RazonSocial = p.RazonSocial;
                            n.CUIT = p.CUIT;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.Numeroiibb = p.NroIibb;
                            n.Calle = p.Calle;
                            n.NroPuerta = p.NroPuerta;
                            n.Piso = p.Piso;
                            n.Depto = p.Depto;
                            n.IdLocalidad = p.IdLocalidad;
                            n.CodigoPostal = p.CodigoPostal;
                            n.Telefono = p.Telefono;
                            n.Email = p.Email;
                            n.Torre = p.Torre;

                            n.titularesSH = new List<TitularesSHDTO>();

                            var list = blcppjpf.GetByFKIdSolicitud(id_solicitud);

                            foreach (var f in list)
                            {
                                TitularesSHDTO t = new TitularesSHDTO();
                                //t.RowId = f.RowId;
                                t.Apellidos = f.Apellido;
                                t.Nombres = f.Nombres;
                                t.TipoDoc = f.IdTipoDocPersonal.ToString();
                                t.NroDoc = f.NroDocumento;
                                t.IdTipoDocPersonal = f.IdTipoDocPersonal;
                                t.Email = f.Email;
                                n.titularesSH.Add(t);
                            }

                            var listf = blsoljpf.GetByFKIdSolicitud(id_solicitud);
                            n.encFirDTO = new List<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>();
                            n.firmantesSH = new List<FirmantesSHDTO>();
                            foreach (var item in listf)
                            {
                                TransferenciasFirmantesSolicitudPersonasJuridicasDTO a = new TransferenciasFirmantesSolicitudPersonasJuridicasDTO();
                                a.id_solicitud = id_transf;
                                a.Apellido = item.Apellido;
                                a.Nombres = item.Nombres;
                                a.id_tipodoc_personal = item.IdTipoDocPersonal;
                                a.Nro_Documento = item.NroDocumento;
                                a.Cuit = item.Cuit;
                                a.Email = item.Email;
                                a.id_tipocaracter = item.IdTipoCaracter;
                                a.cargo_firmante_pj = item.CargoFirmantePj;
                                n.encFirDTO.Add(a);

                                FirmantesSHDTO firmantesSHDTO = new FirmantesSHDTO();
                                firmantesSHDTO.Apellidos = item.Apellido;
                                firmantesSHDTO.Nombres = item.Nombres;
                                firmantesSHDTO.NroDoc = item.NroDocumento;
                                firmantesSHDTO.id_tipodoc_personal = item.IdTipoDocPersonal;
                                firmantesSHDTO.id_tipocaracter = item.IdTipoCaracter;
                                firmantesSHDTO.cargo_firmante = item.CargoFirmantePj;
                                firmantesSHDTO.email = item.Email;
                                n.firmantesSH.Add(firmantesSHDTO);
                            }

                            blcppj.Insert(n);
                        }
                        #endregion
                    }
                    else if (idTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                    {
                        int nroTrReferencia = 0;
                        int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);
                        var enco = encBL.GetUltimaEncomiendaAprobadaTR(id_solicitud);
                        idEncomienda = enco != null ? enco.IdEncomienda : 0;
                        if (id_solicitud > nroTrReferencia)
                        {
                            #region ubicacion
                            TransferenciasUbicacionesPropiedadHorizontalBL blHorTR = new TransferenciasUbicacionesPropiedadHorizontalBL();
                            TransferenciasUbicacionesPuertasBL blPuerTR = new TransferenciasUbicacionesPuertasBL();

                            var lubi = bltu.GetByFKIdSolicitud(id_solicitud);
                            foreach (var ubi in lubi)
                            {
                                TransferenciaUbicacionesDTO u = new TransferenciaUbicacionesDTO();
                                u.DeptoLocalTransferenciaUbicacion = ubi.DeptoLocalTransferenciaUbicacion;
                                u.Depto = ubi.Depto;
                                u.Local = ubi.Local;
                                u.Torre = ubi.Torre;
                                u.IdSolicitud = id_transf;
                                u.IdSubTipoUbicacion = ubi.IdSubTipoUbicacion;
                                u.IdUbicacion = ubi.IdUbicacion;
                                u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                                u.LocalSubTipoUbicacion = ubi.LocalSubTipoUbicacion;
                                var lhor = blHorTR.GetByFKIdSolicitudUbicacion(ubi.IdTransferenciaUbicacion);
                                u.PropiedadesHorizontales = new List<TransferenciasUbicacionPropiedadHorizontalDTO>();
                                foreach (var hor in lhor)
                                {
                                    TransferenciasUbicacionPropiedadHorizontalDTO h = new TransferenciasUbicacionPropiedadHorizontalDTO();
                                    h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                    u.PropiedadesHorizontales.Add(h);
                                }
                                var lpuer = blPuerTR.GetByFKIdTransferenciaUbicacion(ubi.IdTransferenciaUbicacion);
                                u.Puertas = new List<TransferenciasUbicacionesPuertasDTO>();
                                foreach (var puer in lpuer)
                                {
                                    TransferenciasUbicacionesPuertasDTO p = new TransferenciasUbicacionesPuertasDTO();
                                    p.CodigoCalle = puer.CodigoCalle;
                                    p.NumeroPuerta = puer.NumeroPuerta;
                                    u.Puertas.Add(p);
                                }
                                var lmix = blMix.GetZonasUbicacion(ubi.IdTransferenciaUbicacion);
                                u.TransferenciaUbicacionesMixturasDTO = new List<TransferenciaUbicacionesMixturasDTO>();
                                foreach (var mix in lmix)
                                {
                                    TransferenciaUbicacionesMixturasDTO m = new TransferenciaUbicacionesMixturasDTO();
                                    m.IdZonaMixtura = mix.IdZona;
                                    u.TransferenciaUbicacionesMixturasDTO.Add(m);
                                }
                                var ldis = blDis.GetDistritosUbicacion(ubi.IdTransferenciaUbicacion);
                                u.TransferenciaUbicacionesDistritosDTO = new List<TransferenciaUbicacionesDistritosDTO>();
                                foreach (var dis in ldis)
                                {
                                    TransferenciaUbicacionesDistritosDTO d = new TransferenciaUbicacionesDistritosDTO();
                                    d.IdDistrito = dis.IdDistrito;
                                    u.TransferenciaUbicacionesDistritosDTO.Add(d);
                                }
                                u.CreateDate = DateTime.Now;
                                u.CreateUser = userid;
                                bltu.Insert(u);
                            }
                            // unitOfWork.Commit();
                            #endregion
                        }
                        else
                        {
                            #region ubicacion
                            TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
                            int idCpadron = trBL.Single(id_solicitud).IdConsultaPadron;
                            ConsultaPadronUbicacionesBL blUbiTR = new ConsultaPadronUbicacionesBL();
                            ConsultaPadronUbicacionPropiedadHorizontalBL blHorCP = new ConsultaPadronUbicacionPropiedadHorizontalBL();
                            ConsultaPadronUbicacionesPuertasBL blPuerCP = new ConsultaPadronUbicacionesPuertasBL();

                            var lubi = blUbiTR.GetByFKIdConsultaPadron(idCpadron);
                            foreach (var ubi in lubi)
                            {
                                TransferenciaUbicacionesDTO u = new TransferenciaUbicacionesDTO();
                                u.DeptoLocalTransferenciaUbicacion = ubi.DeptoLocalConsultaPadronUbicacion;
                                u.Depto = ubi.Depto;
                                u.Local = ubi.Local;
                                u.Torre = ubi.Torre;
                                u.IdSolicitud = id_transf;
                                u.IdSubTipoUbicacion = ubi.IdSubTipoUbicacion;
                                u.IdUbicacion = ubi.IdUbicacion;
                                u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                                u.LocalSubTipoUbicacion = ubi.LocalSubTipoUbicacion;
                                var lhor = blHorCP.GetByFKIdConsultaPadronUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.PropiedadesHorizontales = new List<TransferenciasUbicacionPropiedadHorizontalDTO>();
                                foreach (var hor in lhor)
                                {
                                    TransferenciasUbicacionPropiedadHorizontalDTO h = new TransferenciasUbicacionPropiedadHorizontalDTO();
                                    h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                    u.PropiedadesHorizontales.Add(h);
                                }
                                var lpuer = blPuerCP.GetByFKIdConsultaPadronUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.Puertas = new List<TransferenciasUbicacionesPuertasDTO>();
                                foreach (var puer in lpuer)
                                {
                                    TransferenciasUbicacionesPuertasDTO p = new TransferenciasUbicacionesPuertasDTO();
                                    p.CodigoCalle = puer.CodigoCalle;
                                    p.NumeroPuerta = puer.NumeroPuerta;
                                    u.Puertas.Add(p);
                                }
                                var lmix = blMix.GetZonasUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.TransferenciaUbicacionesMixturasDTO = new List<TransferenciaUbicacionesMixturasDTO>();
                                foreach (var mix in lmix)
                                {
                                    TransferenciaUbicacionesMixturasDTO m = new TransferenciaUbicacionesMixturasDTO();
                                    m.IdZonaMixtura = mix.IdZona;
                                    u.TransferenciaUbicacionesMixturasDTO.Add(m);
                                }
                                var ldis = blDis.GetDistritosUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.TransferenciaUbicacionesDistritosDTO = new List<TransferenciaUbicacionesDistritosDTO>();
                                foreach (var dis in ldis)
                                {
                                    TransferenciaUbicacionesDistritosDTO d = new TransferenciaUbicacionesDistritosDTO();
                                    d.IdDistrito = dis.IdDistrito;
                                    u.TransferenciaUbicacionesDistritosDTO.Add(d);
                                }
                                u.CreateDate = DateTime.Now;
                                u.CreateUser = userid;
                                bltu.Insert(u);
                            }
                            // unitOfWork.Commit();
                            #endregion

                            #region Plantas                            
                            ConsultaPadronPlantasBL blPlantasCP = new ConsultaPadronPlantasBL();
                            TransferenciasPlantasBL blPlantasT = new TransferenciasPlantasBL();

                            var listPlantas = blPlantasCP.GetByFKIdConsultaPadron(idCpadron);
                            var listPlantasNew = new List<TransferenciasPlantasDTO>();
                            foreach (var planta in listPlantas)
                            {
                                TransferenciasPlantasDTO p = new TransferenciasPlantasDTO();
                                p.IdSolicitud = id_transf;
                                p.IdTipoSector = planta.IdTipoSector;
                                p.DetalleTransferenciaTipoSector = planta.DetalleConsultaPadronTipoSector;
                                blPlantasT.Insert(p);
                                listPlantasNew.Add(p);
                            }
                            #endregion

                            #region Datos Del Local                    
                            ConsultaPadronDatosLocalBL blDatos = new ConsultaPadronDatosLocalBL();
                            TransferenciasDatosLocalBL blDatosT = new TransferenciasDatosLocalBL();

                            var dato = blDatos.GetByFKIdConsultaPadron(idCpadron);

                            foreach (var item in dato)
                            {
                                TransferenciasDatosLocalDTO d = new TransferenciasDatosLocalDTO();
                                d.CantidadOperariosDl = item.CantidadOperariosDl;
                                d.CantidadSanitariosDl = item.CantidadSanitariosDl;
                                d.CreateDate = DateTime.Now;
                                d.CreateUser = userid;
                                d.CroquisUbicacionDl = item.CroquisUbicacionDl;
                                d.DimesionFrenteDl = item.DimesionFrenteDl;
                                d.EstacionamientoDl = item.EstacionamientoDl;
                                d.FondoDl = item.FondoDl;
                                d.FrenteDl = item.FrenteDl;
                                d.IdSolicitud = id_transf;
                                d.LateralDerechoDl = item.LateralDerechoDl;
                                d.LateralIzquierdoDl = item.LateralIzquierdoDl;
                                d.Local_venta = Convert.ToDecimal(item.Local_venta);
                                d.LugarCargaDescargaDl = item.LugarCargaDescargaDl;
                                d.MaterialesParedesDl = item.MaterialesParedesDl;
                                d.MaterialesPisosDl = item.MaterialesPisosDl;
                                d.MaterialesRevestimientosDl = item.MaterialesRevestimientosDl;
                                d.MaterialesTechosDl = item.MaterialesTechosDl;
                                d.RedTransitoPesadoDl = item.RedTransitoPesadoDl;
                                d.SanitariosDistanciaDl = item.SanitariosDistanciaDl;
                                d.SanitariosUbicacionDl = item.SanitariosUbicacionDl;

                                d.Dj_certificado_sobrecarga = item.Dj_certificado_sobrecarga;

                                d.SobreAvenidaDl = item.SobreAvenidaDl;

                                d.SuperficieCubiertaDl = item.SuperficieCubiertaDl;
                                d.SuperficieDescubiertaDl = item.SuperficieDescubiertaDl;
                                d.SuperficieSanitariosDl = item.SuperficieSanitariosDl;

                                blDatosT.Insert(d);
                            }
                            #endregion

                            //#region Rubros
                            //ConsultaPadronRubrosBL blRubros = new ConsultaPadronRubrosBL();
                            //TransferenciasRubrosBL blRubrost = new TransferenciasRubrosBL();
                            //var lstRubros = blRubros.GetByFKIdConsultaPadron(idCpadron);
                            //foreach (var rub in lstRubros)
                            //{
                            //    TransferenciasRubrosDTO r = new TransferenciasRubrosDTO();
                            //    r.CodidoRubro = rub.CodidoRubro;
                            //    r.CreateDate = DateTime.Now;
                            //    r.DescripcionRubro = rub.DescripcionRubro;
                            //    r.EsAnterior = rub.EsAnterior;
                            //    r.IdSolicitud = id_transf;
                            //    r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                            //    r.IdTipoActividad = rub.IdTipoActividad;
                            //    r.IdTipoDocumentoReq = rub.IdTipoDocumentoReq;
                            //    r.LocalVenta = rub.LocalVenta;
                            //    r.RestriccionSup = rub.RestriccionSup;
                            //    r.RestriccionZona = rub.RestriccionZona;
                            //    r.SuperficieHabilitar = rub.SuperficieHabilitar;
                            //    blRubrost.Insert(r);
                            //}

                            //#endregion

                            #region Normativas
                            /*ConsultaPadronNormativasBL blNor = new ConsultaPadronNormativasBL();
                            EncomiendaNormativasBL blNort = new EncomiendaNormativasBL();
                            var listNor = blNort.GetByFKIdEncomienda(idEncomienda);
                            foreach (var nor in listNor)
                            {
                                ConsultaPadronNormativasDTO n = new ConsultaPadronNormativasDTO();
                                n.CreateDate = DateTime.Now;
                                n.CreateUser = userid;
                                n.IdConsultaPadron = idCP;
                                n.IdEntidadNormativa = nor.IdEntidadNormativa;
                                n.IdTipoNormativa = nor.IdTipoNormativa;
                                n.NumeroNormativa = nor.NroNormativa;
                                blNor.Insert(n);
                            }*/
                            #endregion
                        }
                        #region Titulares CP
                        TransferenciasTitularesPersonasFisicasBL blPfs = new TransferenciasTitularesPersonasFisicasBL();
                        TransferenciasFirmantesPersonasFisicasBL blPff = new TransferenciasFirmantesPersonasFisicasBL();
                        TransferenciasFirmantesSolicitudPersonasFisicasBL blPffs = new TransferenciasFirmantesSolicitudPersonasFisicasBL();
                        var listPf = blPfs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPf)
                        {
                            TransferenciasTitularesSolicitudPersonasFisicasDTO n = new TransferenciasTitularesSolicitudPersonasFisicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdSolicitud = id_transf;
                            n.Apellido = p.Apellido;
                            n.Nombres = p.Nombres;
                            n.IdTipoDocumentoPersonal = p.IdTipodocPersonal;
                            n.CUIT = p.Cuit;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.IngresosBrutos = p.IngresosBrutos;
                            n.Calle = p.Calle;
                            n.NumeroPuerta = p.NumeroPuerta;
                            n.Piso = p.Piso;
                            n.Depto = p.Depto;
                            n.IdLocalidad = p.IdLocalidad;
                            n.CodigoPostal = p.CodigoPostal;
                            n.Telefono = p.Telefono;
                            n.TelefonoMovil = p.Celular;
                            n.Email = p.Email;
                            //n.Sms = p.Sms;
                            n.MismoFirmante = p.MismoFirmante;
                            n.NumeroDocumento = p.NumeroDocumento;
                            n.Torre = p.Torre;

                            n.DtoFirmantes = new TransferenciasFirmantesSolicitudPersonasFisicasDTO();
                            p.DtoFirmantes = blPff.GetByFKIdSolicitudIdPersonaFisica(id_solicitud, p.IdPersonaFisica).FirstOrDefault();
                            if (p.DtoFirmantes != null)
                            {
                                n.DtoFirmantes.id_solicitud = id_transf;
                                n.DtoFirmantes.Apellido = p.DtoFirmantes.Apellido;
                                n.DtoFirmantes.Nombres = p.DtoFirmantes.Nombres;
                                n.DtoFirmantes.id_tipodoc_personal = p.DtoFirmantes.IdTipoDocumentoPersonal;
                                n.DtoFirmantes.Nro_Documento = p.DtoFirmantes.NumeroDocumento;
                                n.DtoFirmantes.id_tipocaracter = p.DtoFirmantes.IdTipoCaracter;
                                //n.DtoFirmantes.Cuit = p.DtoFirmantes.c;
                            }
                            blcppf.Insert(n);
                        }

                        TransferenciasTitularesPersonasJuridicasBL blPjs = new TransferenciasTitularesPersonasJuridicasBL();
                        TransferenciasTitularesPersonasJuridicasPersonasFisicasBL blcppjpf = new TransferenciasTitularesPersonasJuridicasPersonasFisicasBL();
                        TransferenciasFirmantesPersonasJuridicasBL blsoljpf = new TransferenciasFirmantesPersonasJuridicasBL();
                        var listPj = blPjs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPj)
                        {
                            TransferenciasTitularesSolicitudPersonasJuridicasDTO n = new TransferenciasTitularesSolicitudPersonasJuridicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdSolicitud = id_transf;
                            n.IdTipoSociedad = p.IdTipoSociedad;
                            n.RazonSocial = p.RazonSocial;
                            n.CUIT = p.Cuit;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.Numeroiibb = p.NumeroIibb;
                            n.Calle = p.Calle;
                            n.NroPuerta = p.NumeroPuerta;
                            n.Piso = p.Piso;
                            n.Depto = p.Depto;
                            n.IdLocalidad = p.IdLocalidad;
                            n.CodigoPostal = p.CodigoPostal;
                            n.Telefono = p.Telefono;
                            n.Email = p.Email;
                            n.Torre = p.Torre;

                            n.titularesSH = new List<TitularesSHDTO>();

                            var list = blcppjpf.GetByFKIdSolicitud(id_solicitud);

                            foreach (var f in list)
                            {
                                TitularesSHDTO t = new TitularesSHDTO();
                                //t.RowId = f.RowId;
                                t.Apellidos = f.Apellido;
                                t.Nombres = f.Nombres;
                                t.TipoDoc = f.IdTipoDocumentoPersonal.ToString();
                                t.NroDoc = f.NumeroDocumento;
                                t.IdTipoDocPersonal = f.IdTipoDocumentoPersonal;
                                t.Email = f.Email;
                                n.titularesSH.Add(t);
                            }

                            var listf = blsoljpf.GetByFKIdSolicitud(id_solicitud);
                            n.encFirDTO = new List<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>();
                            foreach (var item in listf)
                            {
                                TransferenciasFirmantesSolicitudPersonasJuridicasDTO a = new TransferenciasFirmantesSolicitudPersonasJuridicasDTO();
                                a.id_solicitud = id_transf;
                                a.Apellido = item.Apellido;
                                a.Nombres = item.Nombres;
                                a.id_tipodoc_personal = item.IdTipoDocumentoPersonal;
                                a.Nro_Documento = item.NumeroDocumento;
                                //a.Cuit = item.Cuit;
                                a.Email = item.Email;
                                a.id_tipocaracter = item.IdTipoCaracter;
                                a.cargo_firmante_pj = item.CargoFirmantePersonaJuridica;
                                n.encFirDTO.Add(a);
                            }

                            blcppj.Insert(n);
                        }
                        #endregion
                    }

                    if (idEncomienda > 0)
                    {
                        #region Plantas
                        EncomiendaPlantasBL blPlantas = new EncomiendaPlantasBL();
                        TransferenciasPlantasBL blPlantasT = new TransferenciasPlantasBL();

                        var listPlantas = blPlantas.GetByFKIdEncomienda(idEncomienda);
                        var listPlantasNew = new List<TransferenciasPlantasDTO>();
                        foreach (var planta in listPlantas)
                        {
                            TransferenciasPlantasDTO p = new TransferenciasPlantasDTO();
                            p.IdSolicitud = id_transf;
                            p.IdTipoSector = planta.IdTipoSector;
                            p.DetalleTransferenciaTipoSector = planta.detalle_encomiendatiposector;
                            blPlantasT.Insert(p);
                            listPlantasNew.Add(p);
                        }
                        #endregion

                        #region Datos Del Local                    
                        EncomiendaDatosLocalBL blDatos = new EncomiendaDatosLocalBL();
                        TransferenciasDatosLocalBL blDatosT = new TransferenciasDatosLocalBL();

                        var dato = blDatos.GetByFKIdEncomienda(idEncomienda);

                        if (dato != null)
                        {
                            TransferenciasDatosLocalDTO d = new TransferenciasDatosLocalDTO();
                            d.CantidadOperariosDl = dato.cantidad_operarios_dl;
                            d.CantidadSanitariosDl = dato.cantidad_sanitarios_dl;
                            d.CreateDate = DateTime.Now;
                            d.CreateUser = userid;
                            d.CroquisUbicacionDl = dato.croquis_ubicacion_dl;
                            d.DimesionFrenteDl = dato.dimesion_frente_dl;
                            d.EstacionamientoDl = dato.estacionamiento_dl;
                            d.FondoDl = dato.fondo_dl;
                            d.FrenteDl = dato.frente_dl;
                            d.IdSolicitud = id_transf;
                            d.LateralDerechoDl = dato.lateral_derecho_dl;
                            d.LateralIzquierdoDl = dato.lateral_izquierdo_dl;
                            d.Local_venta = Convert.ToDecimal(dato.local_venta);
                            d.LugarCargaDescargaDl = dato.lugar_carga_descarga_dl;
                            d.MaterialesParedesDl = dato.materiales_paredes_dl;
                            d.MaterialesPisosDl = dato.materiales_pisos_dl;
                            d.MaterialesRevestimientosDl = dato.materiales_revestimientos_dl;
                            d.MaterialesTechosDl = dato.materiales_techos_dl;
                            d.RedTransitoPesadoDl = dato.red_transito_pesado_dl;
                            d.SanitariosDistanciaDl = dato.sanitarios_distancia_dl;
                            d.SanitariosUbicacionDl = dato.sanitarios_ubicacion_dl.Value;

                            d.Dj_certificado_sobrecarga = dato.dj_certificado_sobrecarga.Value;

                            d.SobreAvenidaDl = dato.sobre_avenida_dl;

                            d.SuperficieCubiertaDl = dato.superficie_cubierta_dl;
                            d.SuperficieDescubiertaDl = dato.superficie_descubierta_dl;
                            d.SuperficieSanitariosDl = dato.superficie_sanitarios_dl;

                            blDatosT.Insert(d);
                        }
                        #endregion

                        #region Normativas
                        /*ConsultaPadronNormativasBL blNor = new ConsultaPadronNormativasBL();
                        EncomiendaNormativasBL blNort = new EncomiendaNormativasBL();
                        var listNor = blNort.GetByFKIdEncomienda(idEncomienda);
                        foreach (var nor in listNor)
                        {
                            ConsultaPadronNormativasDTO n = new ConsultaPadronNormativasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdConsultaPadron = idCP;
                            n.IdEntidadNormativa = nor.IdEntidadNormativa;
                            n.IdTipoNormativa = nor.IdTipoNormativa;
                            n.NumeroNormativa = nor.NroNormativa;
                            blNor.Insert(n);
                        }*/
                        #endregion
                    }

                    int tipo_doc_req = 0;
                    if (idTipoTramite == (int)Constantes.TipoDeTramite.Transferencia)
                    {
                        TransferenciasDocumentosAdjuntosDTO lstDocTr;
                        lstDocTr = blDocTr.GetByFKIdSolicitud(id_solicitud).Where(x => x.IdTipoDocsis == (int)Constantes.TiposDeDocumentosSistema.PLANCHETA_TRANSFERENCIA).FirstOrDefault();
                        if (lstDocTr != null)
                        {
                            idFile = lstDocTr.IdFile;
                            nombreArc = lstDocTr.NombreArchivo;
                            tdocDeta = lstDocTr.TipoDocumentoRequeridoDetalle;
                            tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_JPG;
                        }
                    }
                    else
                    {
                        SSITDocumentosAdjuntosDTO lstDoc;
                        lstDoc = blDoc.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.PLANCHETA_HABILITACION).FirstOrDefault();
                        tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_JPG;
                        if (lstDoc == null)
                        {
                            lstDoc = blDoc.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD).FirstOrDefault();
                            tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_PDF;

                            if (lstDoc == null)
                            {
                                lstDoc = blDoc.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION).FirstOrDefault();
                                tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_JPG;
                            }

                            if (lstDoc == null)
                            {
                                lstDoc = blDoc.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION).FirstOrDefault();
                                tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_PDF;
                            }
                        }
                        if (lstDoc != null)
                        {
                            idFile = lstDoc.id_file;
                            nombreArc = lstDoc.nombre_archivo;
                            tdocDeta = lstDoc.tdocreq_detalle;
                            tipo_doc_req = (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_PDF;
                            idTipoDocReq = tipo_doc_req;
                        }
                    }
                    if (idFile > 0)
                    {
                        var doc = new TransferenciasDocumentosAdjuntosDTO();
                        doc.CreateDate = DateTime.Now;
                        doc.CreateUser = userid;
                        doc.IdFile = idFile;
                        doc.GeneradoxSistema = false;
                        doc.IdSolicitud = id_transf;
                        doc.IdTipoDocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION;
                        doc.IdTipoDocumentoRequerido = idTipoDocReq;
                        doc.NombreArchivo = nombreArc;
                        doc.TipoDocumentoRequeridoDetalle = tdocDeta;

                        blDocTr.Insert(doc, false);
                    }
                    unitOfWork.Commit();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ExisteAnexosEnCurso(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var listEnc = repo.GetByFKIdSolicitudTransf(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Incompleta
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Completa
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Confirmada
                    || x.id_estado == (int)Constantes.Encomienda_Estados.Ingresada_al_consejo).ToList();
            return listEnc.Count() > 0;
        }

        public void validarEncomienda(int id_solicitud)
        {
            var transf = Single(id_solicitud);
            if (transf.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || transf.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || transf.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN
                || transf.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG)
            {
                var listEnc = transf.EncomiendaTransfSolicitudesDTO.Where(x => x.EncomiendaDTO.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).Select(y => y.EncomiendaDTO).ToList();

                var lstEncA = listEnc.Where(x => x.tipoAnexo == Constantes.TipoAnexo_A).ToList();
                if (lstEncA.Count() == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_TECNICO_INEXISTENTE);

                if (ExisteAnexosEnCurso(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_EN_CURSO);

                var encomienda = lstEncA.OrderByDescending(x => x.IdEncomienda).First();
            }
        }
    }
}

