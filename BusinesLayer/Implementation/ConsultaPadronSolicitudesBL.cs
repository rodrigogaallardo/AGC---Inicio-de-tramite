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
using DataAcess.EntityCustom;
using StaticClass;
using BusinesLayer.MappingConfig;

namespace BusinesLayer.Implementation
{
    public class ConsultaPadronSolicitudesBL : IConsultaPadronSolicitudesBL<ConsultaPadronSolicitudesDTO>
    {
        private ConsultaPadronSolicitudesRepository repo = null;
        private ConsultaPadronRubrosRepository repoRubros = null;
        private ConsultaPadronDocumentosAdjuntosRepository repoDoc = null;
        private IUnitOfWorkFactory uowF = null;
        private ItemDirectionRepository itemRepo = null;

        IMapper mapperBase;

        public ConsultaPadronSolicitudesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>()
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.ObservacionesInternas, source => source.MapFrom(p => p.observaciones_internas))
                    .ForMember(dest => dest.ObservacionesContribuyente, source => source.MapFrom(p => p.observaciones_contribuyente))
                    .ForMember(dest => dest.NroExpedienteAnterior, source => source.MapFrom(p => p.nro_expediente_anterior))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                    .ForMember(dest => dest.NombreApellidoEscribano, source => source.MapFrom(p => p.nombre_apellido_escribano))
                    .ForMember(dest => dest.NroMatriculaEscribano, source => source.MapFrom(p => p.nro_matricula_escribano))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.SubTipoExpediente, source => source.MapFrom(p => p.SubtipoExpediente))
                    .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.CPadron_Estados))
                    .ForMember(dest => dest.Normativa, source => source.MapFrom(p => p.CPadron_Normativas))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.CPadron_Ubicaciones))
                    .ForMember(dest => dest.DatosLocal, source => source.MapFrom(p => p.CPadron_DatosLocal))
                    .ForMember(dest => dest.Plantas, source => source.MapFrom(p => p.CPadron_Plantas))
                    .ForMember(dest => dest.Rubros, source => source.MapFrom(p => p.CPadron_Rubros))
                    .ForMember(dest => dest.ObservacionesDTO, source => source.MapFrom(p => p.CPadron_Solicitudes_Observaciones))
                    .ForMember(dest => dest.TramitesTarea, source => source.MapFrom(p => p.SGI_Tramites_Tareas_CPADRON))
                    .ForMember(dest => dest.DocumentosAdjuntos, source => source.MapFrom(p => p.CPadron_DocumentosAdjuntos))
                    .ForMember(dest => dest.TitularesPersonasFisicas, source => source.MapFrom(p => p.CPadron_Titulares_PersonasFisicas))
                    .ForMember(dest => dest.TitularesPersonasJuridicas, source => source.MapFrom(p => p.CPadron_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.TitularesPersonasSolicitudJuridicas, source => source.MapFrom(p => p.CPadron_Titulares_Solicitud_PersonasJuridicas))
                    .ForMember(dest => dest.TitularesPersonasSolicitudJuridicasTitulares, source => source.MapFrom(p => p.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas))
                    .ForMember(dest => dest.TitularesSolicitudPersonasFisicas, source => source.MapFrom(p => p.CPadron_Titulares_Solicitud_PersonasFisicas));

                cfg.CreateMap<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>()
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.observaciones_internas, source => source.MapFrom(p => p.ObservacionesInternas))
                    .ForMember(dest => dest.observaciones_contribuyente, source => source.MapFrom(p => p.ObservacionesContribuyente))
                    .ForMember(dest => dest.nro_expediente_anterior, source => source.MapFrom(p => p.NroExpedienteAnterior))
                    .ForMember(dest => dest.observaciones, source => source.MapFrom(p => p.Observaciones))
                    .ForMember(dest => dest.nombre_apellido_escribano, source => source.MapFrom(p => p.NombreApellidoEscribano))
                    .ForMember(dest => dest.nro_matricula_escribano, source => source.MapFrom(p => p.NroMatriculaEscribano))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Estados, source => source.MapFrom(p => p.Estado))
                    .ForMember(dest => dest.CPadron_Normativas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Ubicaciones, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_DatosLocal, source => source.MapFrom(p => p.DatosLocal))
                    .ForMember(dest => dest.CPadron_Plantas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Rubros, source => source.MapFrom(p => p.Rubros))
                    .ForMember(dest => dest.CPadron_Solicitudes_Observaciones, source => source.Ignore())
                    .ForMember(dest => dest.SGI_Tramites_Tareas_CPADRON, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_DocumentosAdjuntos, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Titulares_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Titulares_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Titulares_Solicitud_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Titulares_Solicitud_PersonasFisicas, source => source.Ignore());

                cfg.CreateMap<CPadron_Estados, ConsultaPadronEstadosDTO>()
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                    .ForMember(dest => dest.NomEstadoUsuario, source => source.MapFrom(p => p.nom_estado_usuario))
                    .ForMember(dest => dest.NomEstadoInterno, source => source.MapFrom(p => p.nom_estado_interno));

                cfg.CreateMap<ConsultaPadronEstadosDTO, CPadron_Estados>()
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                    .ForMember(dest => dest.nom_estado_usuario, source => source.MapFrom(p => p.NomEstadoUsuario))
                    .ForMember(dest => dest.nom_estado_interno, source => source.MapFrom(p => p.NomEstadoInterno));

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
                #region "Ubicaciones"
                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                     .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));
                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                     .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion));
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

                cfg.CreateMap<CPadron_Normativas, ConsultaPadronNormativasDTO>()
                  .ForMember(dest => dest.IdConsultaPadronTipoNormativa, source => source.MapFrom(p => p.id_cpadrontiponormativa))
                  .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                  .ForMember(dest => dest.IdTipoNormativa, source => source.MapFrom(p => p.id_tiponormativa))
                  .ForMember(dest => dest.IdEntidadNormativa, source => source.MapFrom(p => p.id_entidadnormativa))
                  .ForMember(dest => dest.NumeroNormativa, source => source.MapFrom(p => p.nro_normativa))
                  .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativa))
                  .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();

                cfg.CreateMap<CPadron_Ubicaciones, ConsultaPadronUbicacionesDTO>()
                   .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                   .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                   .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                   .ForMember(dest => dest.IdSubTipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                   .ForMember(dest => dest.LocalSubTipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                   .ForMember(dest => dest.DeptoLocalConsultaPadronUbicacion, source => source.MapFrom(p => p.deptoLocal_cpadronubicacion))
                   .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                   .ForMember(dest => dest.PropiedadesHorizontales, source => source.MapFrom(p => p.CPadron_Ubicaciones_PropiedadHorizontal))
                   .ForMember(dest => dest.Puertas, source => source.MapFrom(p => p.CPadron_Ubicaciones_Puertas))
                   .ForMember(dest => dest.ZonaPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                   .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                   .ForMember(dest => dest.SubTipoUbicacion, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                   .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                   .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                   .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local));

                cfg.CreateMap<ConsultaPadronUbicacionesDTO, CPadron_Ubicaciones>()
                    .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubTipoUbicacion))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Ubicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubTipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_cpadronubicacion, source => source.MapFrom(p => p.DeptoLocalConsultaPadronUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.CPadron_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.PropiedadesHorizontales))
                    .ForMember(dest => dest.CPadron_Ubicaciones_Puertas, source => source.MapFrom(p => p.Puertas))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local));

                cfg.CreateMap<CPadron_Ubicaciones_Puertas, ConsultaPadronUbicacionesPuertasDTO>()
                   .ForMember(dest => dest.IdConsultaPadronPuerta, source => source.MapFrom(p => p.id_cpadronpuerta))
                   .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                   .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                   .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>()
                  .ForMember(dest => dest.id_cpadronpuerta, source => source.MapFrom(p => p.IdConsultaPadronPuerta))
                  .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                  .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                  .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                  .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));

                cfg.CreateMap<CPadron_Ubicaciones_PropiedadHorizontal, ConsultaPadronUbicacionPropiedadHorizontalDTO>()
                   .ForMember(dest => dest.IdConsultaPadronPropiedadHorizontal, source => source.MapFrom(p => p.id_cpadronprophorizontal))
                   .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                   .ForMember(dest => dest.UbicacionPropiedadaHorizontal, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<ConsultaPadronUbicacionPropiedadHorizontalDTO, CPadron_Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_cpadronprophorizontal, source => source.MapFrom(p => p.IdConsultaPadronPropiedadHorizontal))
                   .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));

                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

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

                cfg.CreateMap<Ubicaciones_Puertas, UbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.IdUbicacionPuerta, source => source.MapFrom(p => p.id_ubic_puerta))
                    .ForMember(dest => dest.NroPuertaUbic, source => source.MapFrom(p => p.NroPuerta_ubic))
                    .ForMember(dest => dest.TipoPuerta, source => source.MapFrom(p => p.tipo_puerta));


                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));

                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>();


                cfg.CreateMap<CPadron_DatosLocal, ConsultaPadronDatosLocalDTO>()
                  .ForMember(dest => dest.IdConsultaPadronDatosLocal, source => source.MapFrom(p => p.id_cpadrondatoslocal))
                  .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                  .ForMember(dest => dest.SuperficieCubiertaDl, source => source.MapFrom(p => p.superficie_cubierta_dl))
                  .ForMember(dest => dest.SuperficieDescubiertaDl, source => source.MapFrom(p => p.superficie_descubierta_dl))
                  .ForMember(dest => dest.DimesionFrenteDl, source => source.MapFrom(p => p.dimesion_frente_dl))
                  .ForMember(dest => dest.LugarCargaDescargaDl, source => source.MapFrom(p => p.lugar_carga_descarga_dl))
                  .ForMember(dest => dest.EstacionamientoDl, source => source.MapFrom(p => p.estacionamiento_dl))
                  .ForMember(dest => dest.RedTransitoPesadoDl, source => source.MapFrom(p => p.red_transito_pesado_dl))
                  .ForMember(dest => dest.SobreAvenidaDl, source => source.MapFrom(p => p.sobre_avenida_dl))
                  .ForMember(dest => dest.MaterialesPisosDl, source => source.MapFrom(p => p.materiales_pisos_dl))
                  .ForMember(dest => dest.MaterialesParedesDl, source => source.MapFrom(p => p.materiales_paredes_dl))
                  .ForMember(dest => dest.MaterialesTechosDl, source => source.MapFrom(p => p.materiales_techos_dl))
                  .ForMember(dest => dest.MaterialesRevestimientosDl, source => source.MapFrom(p => p.materiales_revestimientos_dl))
                  .ForMember(dest => dest.SanitariosUbicacionDl, source => source.MapFrom(p => p.sanitarios_ubicacion_dl))
                  .ForMember(dest => dest.SanitariosDistanciaDl, source => source.MapFrom(p => p.sanitarios_distancia_dl))
                  .ForMember(dest => dest.CroquisUbicacionDl, source => source.MapFrom(p => p.croquis_ubicacion_dl))
                  .ForMember(dest => dest.CantidadSanitariosDl, source => source.MapFrom(p => p.cantidad_sanitarios_dl))
                  .ForMember(dest => dest.SuperficieSanitariosDl, source => source.MapFrom(p => p.superficie_sanitarios_dl))
                  .ForMember(dest => dest.FrenteDl, source => source.MapFrom(p => p.frente_dl))
                  .ForMember(dest => dest.FondoDl, source => source.MapFrom(p => p.fondo_dl))
                  .ForMember(dest => dest.LateralIzquierdoDl, source => source.MapFrom(p => p.lateral_izquierdo_dl))
                  .ForMember(dest => dest.LateralDerechoDl, source => source.MapFrom(p => p.lateral_derecho_dl))
                  .ForMember(dest => dest.CantidadOperariosDl, source => source.MapFrom(p => p.cantidad_operarios_dl));

                cfg.CreateMap<ConsultaPadronDatosLocalDTO, CPadron_DatosLocal>()
                    .ForMember(dest => dest.id_cpadrondatoslocal, source => source.MapFrom(p => p.IdConsultaPadronDatosLocal))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.superficie_cubierta_dl, source => source.MapFrom(p => p.SuperficieCubiertaDl))
                    .ForMember(dest => dest.superficie_descubierta_dl, source => source.MapFrom(p => p.SuperficieDescubiertaDl))
                    .ForMember(dest => dest.dimesion_frente_dl, source => source.MapFrom(p => p.DimesionFrenteDl))
                    .ForMember(dest => dest.lugar_carga_descarga_dl, source => source.MapFrom(p => p.LugarCargaDescargaDl))
                    .ForMember(dest => dest.estacionamiento_dl, source => source.MapFrom(p => p.EstacionamientoDl))
                    .ForMember(dest => dest.red_transito_pesado_dl, source => source.MapFrom(p => p.RedTransitoPesadoDl))
                    .ForMember(dest => dest.sobre_avenida_dl, source => source.MapFrom(p => p.SobreAvenidaDl))
                    .ForMember(dest => dest.materiales_pisos_dl, source => source.MapFrom(p => p.MaterialesPisosDl))
                    .ForMember(dest => dest.materiales_paredes_dl, source => source.MapFrom(p => p.MaterialesParedesDl))
                    .ForMember(dest => dest.materiales_techos_dl, source => source.MapFrom(p => p.MaterialesTechosDl))
                    .ForMember(dest => dest.materiales_revestimientos_dl, source => source.MapFrom(p => p.MaterialesRevestimientosDl))
                    .ForMember(dest => dest.sanitarios_ubicacion_dl, source => source.MapFrom(p => p.SanitariosUbicacionDl))
                    .ForMember(dest => dest.sanitarios_distancia_dl, source => source.MapFrom(p => p.SanitariosDistanciaDl))
                    .ForMember(dest => dest.croquis_ubicacion_dl, source => source.MapFrom(p => p.CroquisUbicacionDl))
                    .ForMember(dest => dest.cantidad_sanitarios_dl, source => source.MapFrom(p => p.CantidadSanitariosDl))
                    .ForMember(dest => dest.superficie_sanitarios_dl, source => source.MapFrom(p => p.SuperficieSanitariosDl))
                    .ForMember(dest => dest.frente_dl, source => source.MapFrom(p => p.FrenteDl))
                    .ForMember(dest => dest.fondo_dl, source => source.MapFrom(p => p.FondoDl))
                    .ForMember(dest => dest.lateral_izquierdo_dl, source => source.MapFrom(p => p.LateralIzquierdoDl))
                    .ForMember(dest => dest.lateral_derecho_dl, source => source.MapFrom(p => p.LateralDerechoDl))
                    .ForMember(dest => dest.cantidad_operarios_dl, source => source.MapFrom(p => p.CantidadOperariosDl));

                cfg.CreateMap<CPadron_Plantas, ConsultaPadronPlantasDTO>()
                    .ForMember(dest => dest.DetalleConsultaPadronTipoSector, source => source.MapFrom(p => p.detalle_cpadrontiposector))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdConsultaPadronTipoSector, source => source.MapFrom(p => p.id_cpadrontiposector))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

                #region "TipoSector"
                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
                #endregion

                cfg.CreateMap<CPadron_Rubros, ConsultaPadronRubrosDTO>()
                   .ForMember(dest => dest.IdConsultaPadronRubro, source => source.MapFrom(p => p.id_cpadronrubro))
                   .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                   .ForMember(dest => dest.CodidoRubro, source => source.MapFrom(p => p.cod_rubro))
                   .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                   .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                   .ForMember(dest => dest.IdTipoDocumentoReq, source => source.MapFrom(p => p.id_tipodocreq))
                   .ForMember(dest => dest.SuperficieHabilitar, source => source.MapFrom(p => p.SuperficieHabilitar))
                   .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental))
                   .ForMember(dest => dest.TipoActividad, source => source.MapFrom(p => p.TipoActividad))
                   .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbiental))
                   .ForMember(dest => dest.TipoDocumentacionRequerida, source => source.MapFrom(p => p.Tipo_Documentacion_Req));

                cfg.CreateMap<ConsultaPadronRubrosDTO, CPadron_Rubros>()
                    .ForMember(dest => dest.id_cpadronrubro, source => source.MapFrom(p => p.IdConsultaPadronRubro))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.CodidoRubro))
                    .ForMember(dest => dest.desc_rubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentoReq))
                    .ForMember(dest => dest.SuperficieHabilitar, source => source.MapFrom(p => p.SuperficieHabilitar))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental))
                    .ForMember(dest => dest.TipoActividad, source => source.MapFrom(p => p.TipoActividad))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbiental))
                    .ForMember(dest => dest.Tipo_Documentacion_Req, source => source.MapFrom(p => p.TipoDocumentacionRequerida));

                cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>();
                cfg.CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>();

                cfg.CreateMap<TipoActividadDTO, TipoActividad>();
                cfg.CreateMap<TipoActividad, TipoActividadDTO>();

                cfg.CreateMap<TipoDocumentacionRequeridaDTO, Tipo_Documentacion_Req>();
                cfg.CreateMap<Tipo_Documentacion_Req, TipoDocumentacionRequeridaDTO>();

                cfg.CreateMap<CPadron_Solicitudes_Observaciones, ConsultaPadronSolicitudesObservacionesDTO>()
                  .ForMember(dest => dest.IdConsultaPadronObservacion, source => source.MapFrom(p => p.id_cpadron_observacion))
                  .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                  .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                  .ForMember(dest => dest.Leido, source => source.MapFrom(p => p.leido))
                  .ForMember(dest => dest.User, source => source.MapFrom(p => p.aspnet_Users));

                cfg.CreateMap<aspnet_Users, AspnetUserDTO>()
                    .ForMember(dest => dest.SGIProfile, source => source.MapFrom(p => p.SGI_Profiles))
                    .ForMember(dest => dest.Usuario, source => source.MapFrom(p => p.Usuario));

                cfg.CreateMap<SGI_Profiles, SGIProfileDTO>();

                cfg.CreateMap<Usuario, UsuarioDTO>();

                cfg.CreateMap<SGI_Tramites_Tareas_CPADRON, SGITramitesTareasConsultaPadronDTO>().ReverseMap();

                cfg.CreateMap<CPadron_DocumentosAdjuntos, ConsultaPadronDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_docadjunto))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipodocumentoRequerido, source => source.MapFrom(p => p.id_tdocreq))
                    .ForMember(dest => dest.TipodocumentoRequeridoDetalle, source => source.MapFrom(p => p.tdocreq_detalle))
                    .ForMember(dest => dest.IdTipoDocumentoSistema, source => source.MapFrom(p => p.id_tipodocsis))
                    .ForMember(dest => dest.IdFile, source => source.MapFrom(p => p.id_file))
                    .ForMember(dest => dest.GeneradoxSistema, source => source.MapFrom(p => p.generadoxSistema))
                    .ForMember(dest => dest.NombreArchivo, source => source.MapFrom(p => p.nombre_archivo))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<ConsultaPadronDocumentosAdjuntosDTO, CPadron_DocumentosAdjuntos>()
                    .ForMember(dest => dest.id_docadjunto, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tdocreq, source => source.MapFrom(p => p.IdTipodocumentoRequerido))
                    .ForMember(dest => dest.tdocreq_detalle, source => source.MapFrom(p => p.TipodocumentoRequeridoDetalle))
                    .ForMember(dest => dest.id_tipodocsis, source => source.MapFrom(p => p.IdTipoDocumentoSistema))
                    .ForMember(dest => dest.id_file, source => source.MapFrom(p => p.IdFile))
                    .ForMember(dest => dest.generadoxSistema, source => source.MapFrom(p => p.GeneradoxSistema))
                    .ForMember(dest => dest.nombre_archivo, source => source.MapFrom(p => p.NombreArchivo))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();

                cfg.CreateMap<TiposDeDocumentosSistemaDTO, TiposDeDocumentosSistema>().ReverseMap();

                cfg.CreateMap<CPadron_Titulares_PersonasFisicas, ConsultaPadronTitularesPersonasFisicasDTO>()
                   .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                   .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                   .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                   .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                   .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                   .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                   .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                   .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));

                cfg.CreateMap<CPadron_Titulares_PersonasJuridicas, ConsultaPadronTitularesPersonasJuridicasDTO>()
                  .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                  .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                  .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                  .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                  .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                  .ForMember(dest => dest.NumeroroIibb, source => source.MapFrom(p => p.Nro_IIBB))
                  .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                  .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                  .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<ConsultaPadronTitularesPersonasJuridicasDTO, CPadron_Titulares_PersonasJuridicas>()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NumeroroIibb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasFisicas, ConsultaPadronTitularesSolicitudPersonasFisicasDTO>()
                 .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                 .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                 .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                 .ForMember(dest => dest.CUIT, source => source.MapFrom(p => p.Cuit))
                 .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                 .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                 .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                 .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                 .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                 .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.CUIT))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasJuridicas, ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>()
                   .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                   .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                   .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                   .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                   .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                   .ForMember(dest => dest.Numeroiibb, source => source.MapFrom(p => p.Nro_IIBB))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                   .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                   .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas>()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.Numeroiibb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>()
                      .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                      .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                      .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                      .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                      .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                      .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBase = config.CreateMapper();


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <param name="IdEstado"></param>
        /// <param name="userId"></param>
        public void ConfirmarTramite(int IdConsultaPadron, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var cp = repo.Single(IdConsultaPadron);

                    cp.LastUpdateUser = userId;
                    cp.LastUpdateDate = DateTime.Now;
                    cp.id_estado = (int)Constantes.ConsultaPadronEstados.PING;

                    repo.Update(cp);

                    EngineBL engineBL = new EngineBL();

                    SGITramitesTareasDTO tramite = engineBL.GetUltimaTareaConsultaPadron(IdConsultaPadron);
                    int id_resultado_actual = (int)Constantes.TareasResultados.SolicitudConfirmada;
                    engineBL.FinalizarTarea(tramite.IdTramiteTarea, id_resultado_actual, 0, userId);
                    var tareasDTO = engineBL.GetTareasSiguientes(tramite.IdTarea, id_resultado_actual, tramite.IdTramiteTarea);

                    foreach (var tareaDTO in tareasDTO)
                    {

                        int id_tarea_actual = engineBL.CrearTarea(IdConsultaPadron, tareaDTO.id_tarea, userId);

                        //-- si es Fin de Trámite se crea cerrada
                        if (tareaDTO.id_tarea == (int)Constantes.ENG_Tareas.CP_Carga_Informacion)
                        {
                            var transtarea = engineBL.GetTareaConsultaPadron(IdConsultaPadron, tareaDTO.id_tarea).Where(x => x.FechaCierreTramiteTarea.HasValue).OrderByDescending(o => o.IdTramiteTarea).FirstOrDefault();
                            if (transtarea != null)
                            {
                                engineBL.AsignarTarea(id_tarea_actual, transtarea.UsuarioAsignadoTramiteTarea.Value, unitOfWork);
                            }
                        }
                        //-- si es Fin de Trámite se crea cerrada
                        if (tareaDTO.id_tarea == (int)Constantes.ENG_Tareas.CP_Fin_Tramite)
                            engineBL.FinalizarTarea(id_tarea_actual, id_resultado_actual, 0, userId);
                    }
                    unitOfWork.Commit();
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
        /// <param name="IdConsultaPadron"></param>
        /// <param name="userId"></param>
        public void AnularTramite(int IdConsultaPadron, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);

                    var enc = repo.Single(IdConsultaPadron);

                    enc.LastUpdateUser = userId;
                    enc.LastUpdateDate = DateTime.Now;
                    enc.id_estado = (int)Constantes.Encomienda_Estados.Anulada;

                    repo.Update(enc);
                    unitOfWork.Commit();
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
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public ConsultaPadronSolicitudesDTO Single(int IdConsultaPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                var entity = repo.Single(IdConsultaPadron);
                var entityDto = mapperBase.Map<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>(entity);
                repoRubros = new ConsultaPadronRubrosRepository(unitOfWork);
                var rubros = repoRubros.GetRubros(entityDto.IdConsultaPadron);

                foreach (var rubro in entityDto.Rubros)
                {
                    var rubroEntity = rubros.Where(p => p.IdConsultaPadronRubro == rubro.IdConsultaPadronRubro).FirstOrDefault();
                    if (rubroEntity != null)
                    {
                        rubro.RestriccionZona = rubroEntity.RestriccionZona;//subruba.cod_rubro != null ? subRub.cod_ZonaHab.Contains(relzona.CodZonaLey449) ? "tilde.png" : "Prohibido.png" : "",
                        rubro.RestriccionSup = rubroEntity.RestriccionSup;//subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                    }
                }
                if (entityDto.ObservacionesDTO.Any())
                {
                    var query = (from tcp in entity.SGI_Tramites_Tareas_CPADRON
                                 where tcp.SGI_Tramites_Tareas.id_tarea == (int)Constantes.ENG_Tareas.CP_Correccion_Solicitud
                                 select tcp);
                    if (query.Count() > 0)
                    {
                        var fecha_maxima = query.Select(x => x.SGI_Tramites_Tareas.FechaInicio_tramitetarea).Max();
                        entityDto.ObservacionesTareas = entityDto.ObservacionesDTO.Where(p => p.CreateDate <= fecha_maxima).ToList();
                    }

                }
                if (entityDto.DocumentosAdjuntos.Any())
                {
                    ICollection<CPadron_DocumentosAdjuntos> doc = new List<CPadron_DocumentosAdjuntos>();
                    if (entity.SGI_Tramites_Tareas_CPADRON.Any(x => x.SGI_Tramites_Tareas.id_tarea == (int)Constantes.ENG_Tareas.CP_Fin_Tramite))
                    {
                        doc = entity.CPadron_DocumentosAdjuntos;
                    }
                    else
                        doc = entity.CPadron_DocumentosAdjuntos.Where(p => p.id_tipodocsis != (int)Constantes.TiposDeDocumentosSistema.INFORMES_CPADRON).ToList();

                    if (entityDto.TramitesTarea.Any())
                    {
                        repoDoc = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);
                        doc.Concat(repoDoc.Get(entityDto.IdConsultaPadron));
                    }

                    entityDto.DocumentosAdjuntosConInformeFinTramite = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(doc).ToList();
                }

                return entityDto;
            }
            catch
            {
                throw;
            }
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(ConsultaPadronSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(objectDto);
                    repo.Insert(elementDto);

                    EngineBL engine = new EngineBL();

                    int IdTarea = engine.GetIdTarea((int)Constantes.CodigoTareas.SolicitudConsultaPadrón, unitOfWork);
                    int IdTareaCreada = engine.CrearTarea(elementDto.id_cpadron, IdTarea, objectDto.CreateUser, unitOfWork);

                    if (IdTareaCreada > 0)
                        engine.AsignarTarea(IdTareaCreada, objectDto.CreateUser, unitOfWork);
                    else
                        throw new Exception(Errors.SSIT_CPADRON_NO_SE_PUEDE_CREAR);


                    unitOfWork.Commit();
                    objectDto.IdConsultaPadron = elementDto.id_cpadron;
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
        public void Update(ConsultaPadronSolicitudesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEstado(int id_cpadron, int id_estado)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var cpadronEntity = repo.Single(id_cpadron);
                    cpadronEntity.LastUpdateUser = cpadronEntity.CreateUser;
                    cpadronEntity.id_estado = id_estado;
                    repo.Update(cpadronEntity);
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
        public void Delete(ConsultaPadronSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var repoT = new SGITramitesTareasCpadronRepository(unitOfWork);
                    var repoTT = new SGITramitesTareasRepository(unitOfWork);

                    foreach (var ttc in objectDto.TramitesTarea)
                    {
                        var elementEntityTT = repoTT.Single(ttc.id_tramitetarea);
                        var elementEntityTTCpadron = mapperBase.Map<SGITramitesTareasConsultaPadronDTO, SGI_Tramites_Tareas_CPADRON>(ttc);
                        repoT.Delete(elementEntityTTCpadron);
                        repoTT.Delete(elementEntityTT);
                    }

                    var elementDto = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(objectDto);
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
        /// devuelve un ItemDirectionDTO con la solicitud y todas su puertas 
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        public IEnumerable<ItemDirectionDTO> GetDireccionesCpadron(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesCpadron(lstSolicitudes).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();
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

                            Direccion_armada = string.Empty;
                            Local = string.Empty;
                            Depto = string.Empty;
                            Torre = string.Empty;
                            Otros = string.Empty;

                            int idSub = direccion.idUbicacion ?? 0;

                            if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idSub))
                            {
                                direccion.puerta += "t";
                            }

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Zona"></param>
        public void ActualizarZonaDeclarada(int IdSolicitud, string Zona)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronSolicitudesRepository(this.uowF.GetUnitOfWork());
                var consultaPadron = repo.Single(IdSolicitud);
                var elementDto = mapperBase.Map<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>(consultaPadron);
                if (elementDto.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP
                    && elementDto.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM
                    && elementDto.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);


                elementDto.ZonaDeclarada = Zona;
                consultaPadron = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(elementDto);

                repo.Update(consultaPadron);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public string ActualizarEstadoConsultaPadron(ref ConsultaPadronSolicitudesDTO consultaPadronDTO, Guid userId)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();

            repo = new ConsultaPadronSolicitudesRepository(unitOfWork);
            var consultaPadron = repo.Single(consultaPadronDTO.IdConsultaPadron);

            if (consultaPadron.id_estado == (int)Constantes.ConsultaPadronEstados.INCOM)
            {
                if (string.IsNullOrWhiteSpace(consultaPadron.nro_expediente_anterior))
                    return "No se ingreso el Nro Expediente.";

                if (!consultaPadron.CPadron_Ubicaciones.Any())
                    return "No se ingreso la Ubicacion.";

                if (!consultaPadron.CPadron_Rubros.Any())
                    return "Debe ingresar las actividades comerciales.";

                if (!consultaPadron.CPadron_Titulares_PersonasFisicas.Any() && !consultaPadron.CPadron_Titulares_PersonasJuridicas.Any())
                    return "No se ingreso los titulares habilitacion.";

                if (!consultaPadron.CPadron_Titulares_Solicitud_PersonasFisicas.Any() && !consultaPadron.CPadron_Titulares_Solicitud_PersonasJuridicas.Any())
                    return "No se ingreso los titulares de la solicitud.";

                if (!consultaPadron.CPadron_DocumentosAdjuntos.Any())
                    return "No se ingreso ningun documento.";

                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesRepository(unitOfWorkTran);
                    var consulta = repo.Single(consultaPadronDTO.IdConsultaPadron);

                    consulta.LastUpdateDate = DateTime.Now;
                    consulta.LastUpdateUser = userId;
                    consulta.id_estado = (int)Constantes.ConsultaPadronEstados.COMP;
                    repo.Update(consulta);

                    unitOfWorkTran.Commit();
                }
                consultaPadron = repo.Single(consultaPadronDTO.IdConsultaPadron);
                consultaPadronDTO = mapperBase.Map<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>(consultaPadron);
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <param name="IdTipoTramite"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronSolicitudesDTO> GetListaInformeCpadron(List<int> listIDCPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetListaIdCPadron(listIDCPadron);
                var consultaPadronDTO = mapperBase.Map<IEnumerable<CPadron_Solicitudes>, IEnumerable<ConsultaPadronSolicitudesDTO>>(elements);
                return consultaPadronDTO;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_cpadronDesde"></param>
        /// <param name="id_cpadronHasta"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronSolicitudesDTO> GetRangoIdCpadron(int id_cpadronDesde, int id_cpadronHasta)
        {
            try
            {
                int inicio = id_cpadronDesde;
                int fin = id_cpadronHasta;

                if (id_cpadronDesde >= id_cpadronHasta)
                {
                    inicio = id_cpadronHasta;
                    fin = id_cpadronDesde;
                }

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronSolicitudesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRangoIdCpadron(inicio, fin);
                var consultaPadronDTO = mapperBase.Map<IEnumerable<CPadron_Solicitudes>, IEnumerable<ConsultaPadronSolicitudesDTO>>(elements);
                return consultaPadronDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void copiarDatos(int id_solicitud, int idCP, Guid userid, int idTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaBL encBL = new EncomiendaBL();
                    ConsultaPadronUbicacionesBL bltu = new ConsultaPadronUbicacionesBL();
                    ConsultaPadronTitularesPersonasFisicasBL blcppf = new ConsultaPadronTitularesPersonasFisicasBL();
                    ConsultaPadronTitularesPersonasJuridicasBL blcppj = new ConsultaPadronTitularesPersonasJuridicasBL();
                    ParametrosBL paramBL = new ParametrosBL();

                    int idEncomienda = 0;
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
                            ConsultaPadronUbicacionesDTO u = new ConsultaPadronUbicacionesDTO();
                            u.DeptoLocalConsultaPadronUbicacion = ubi.DeptoLocalUbicacion;
                            u.Depto = ubi.Depto;
                            u.Local = ubi.Local;
                            u.Torre = ubi.Torre;
                            u.IdConsultaPadron = idCP;
                            u.IdSubTipoUbicacion = ubi.IdSubtipoUbicacion;
                            u.IdUbicacion = ubi.IdUbicacion;
                            u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                            u.LocalSubTipoUbicacion = ubi.LocalSubtipoUbicacion;
                            var lhor = blHor.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                            u.PropiedadesHorizontales = new List<ConsultaPadronUbicacionPropiedadHorizontalDTO>();
                            foreach (var hor in lhor)
                            {
                                ConsultaPadronUbicacionPropiedadHorizontalDTO h = new ConsultaPadronUbicacionPropiedadHorizontalDTO();
                                h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                u.PropiedadesHorizontales.Add(h);
                            }
                            var lpuer = blPuer.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                            u.Puertas = new List<ConsultaPadronUbicacionesPuertasDTO>();
                            foreach (var puer in lpuer)
                            {
                                ConsultaPadronUbicacionesPuertasDTO p = new ConsultaPadronUbicacionesPuertasDTO();
                                p.CodigoCalle = puer.CodigoCalle;
                                p.NumeroPuerta = puer.NroPuerta;
                                u.Puertas.Add(p);
                            }
                            u.CreateDate = DateTime.Now;
                            u.CreateUser = userid;
                            bltu.Insert(u);
                        }
                        // unitOfWork.Commit();
                        #endregion

                        #region Titulares CP
                        SSITSolicitudesTitularesPersonasFisicasBL blPfs = new SSITSolicitudesTitularesPersonasFisicasBL();

                        var listPf = blPfs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPf)
                        {
                            ConsultaPadronTitularesPersonasFisicasDTO n = new ConsultaPadronTitularesPersonasFisicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdConsultaPadron = idCP;
                            n.Apellido = p.Apellido;
                            n.Nombres = p.Nombres;
                            n.IdTipoDocumentoPersonal = p.IdTipodocPersonal;
                            n.Cuit = p.Cuit;
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
                            n.Sms = p.Sms;
                            n.Email = p.Email;
                            n.MismoFirmante = p.MismoFirmante;
                            n.NumeroDocumento = p.NroDocumento;
                            n.Torre = p.Torre;
                            blcppf.Insert(n);
                        }

                        SSITSolicitudesTitularesPersonasJuridicasBL blPjs = new SSITSolicitudesTitularesPersonasJuridicasBL();
                        SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL blcppjpf = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL();
                        var listPj = blPjs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPj)
                        {
                            ConsultaPadronTitularesPersonasJuridicasDTO n = new ConsultaPadronTitularesPersonasJuridicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdConsultaPadron = idCP;
                            n.IdTipoSociedad = p.IdTipoSociedad;
                            n.RazonSocial = p.RazonSocial;
                            n.CUIT = p.CUIT;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.NumeroroIibb = p.NroIibb;
                            n.Calle = p.Calle;
                            n.NumeroPuerta = p.NroPuerta;
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
                            TransferenciaUbicacionesBL blUbiTR = new TransferenciaUbicacionesBL();
                            TransferenciasUbicacionesPropiedadHorizontalBL blHorTR = new TransferenciasUbicacionesPropiedadHorizontalBL();
                            TransferenciasUbicacionesPuertasBL blPuerTR = new TransferenciasUbicacionesPuertasBL();

                            var lubi = blUbiTR.GetByFKIdSolicitud(id_solicitud);
                            foreach (var ubi in lubi)
                            {
                                ConsultaPadronUbicacionesDTO u = new ConsultaPadronUbicacionesDTO();
                                u.DeptoLocalConsultaPadronUbicacion = ubi.DeptoLocalTransferenciaUbicacion;
                                u.Depto = ubi.Depto;
                                u.Local = ubi.Local;
                                u.Torre = ubi.Torre;
                                u.IdConsultaPadron = idCP;
                                u.IdSubTipoUbicacion = ubi.IdSubTipoUbicacion;
                                u.IdUbicacion = ubi.IdUbicacion;
                                u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                                u.LocalSubTipoUbicacion = ubi.LocalSubTipoUbicacion;
                                var lhor = blHorTR.GetByFKIdSolicitudUbicacion(ubi.IdTransferenciaUbicacion);
                                u.PropiedadesHorizontales = new List<ConsultaPadronUbicacionPropiedadHorizontalDTO>();
                                foreach (var hor in lhor)
                                {
                                    ConsultaPadronUbicacionPropiedadHorizontalDTO h = new ConsultaPadronUbicacionPropiedadHorizontalDTO();
                                    h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                    u.PropiedadesHorizontales.Add(h);
                                }
                                var lpuer = blPuerTR.GetByFKIdTransferenciaUbicacion(ubi.IdTransferenciaUbicacion);
                                u.Puertas = new List<ConsultaPadronUbicacionesPuertasDTO>();
                                foreach (var puer in lpuer)
                                {
                                    ConsultaPadronUbicacionesPuertasDTO p = new ConsultaPadronUbicacionesPuertasDTO();
                                    p.CodigoCalle = puer.CodigoCalle;
                                    p.NumeroPuerta = puer.NumeroPuerta;
                                    u.Puertas.Add(p);
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
                            //TransferenciaUbicacionesBL blUbiTR = new TransferenciaUbicacionesBL();
                            ConsultaPadronUbicacionPropiedadHorizontalBL blHorCP = new ConsultaPadronUbicacionPropiedadHorizontalBL();
                            ConsultaPadronUbicacionesPuertasBL blPuerCP = new ConsultaPadronUbicacionesPuertasBL();

                            var lubi = bltu.GetByFKIdConsultaPadron(idCpadron);
                            foreach (var ubi in lubi)
                            {
                                ConsultaPadronUbicacionesDTO u = new ConsultaPadronUbicacionesDTO();
                                u.DeptoLocalConsultaPadronUbicacion = ubi.DeptoLocalConsultaPadronUbicacion;
                                u.Depto = ubi.Depto;
                                u.Local = ubi.Local;
                                u.Torre = ubi.Torre;
                                u.IdConsultaPadron = idCP;
                                u.IdSubTipoUbicacion = ubi.IdSubTipoUbicacion;
                                u.IdUbicacion = ubi.IdUbicacion;
                                u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                                u.LocalSubTipoUbicacion = ubi.LocalSubTipoUbicacion;
                                var lhor = blHorCP.GetByFKIdConsultaPadronUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.PropiedadesHorizontales = new List<ConsultaPadronUbicacionPropiedadHorizontalDTO>();
                                foreach (var hor in lhor)
                                {
                                    ConsultaPadronUbicacionPropiedadHorizontalDTO h = new ConsultaPadronUbicacionPropiedadHorizontalDTO();
                                    h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                                    u.PropiedadesHorizontales.Add(h);
                                }
                                var lpuer = blPuerCP.GetByFKIdConsultaPadronUbicacion(ubi.IdConsultaPadronUbicacion);
                                u.Puertas = new List<ConsultaPadronUbicacionesPuertasDTO>();
                                foreach (var puer in lpuer)
                                {
                                    ConsultaPadronUbicacionesPuertasDTO p = new ConsultaPadronUbicacionesPuertasDTO();
                                    p.CodigoCalle = puer.CodigoCalle;
                                    p.NumeroPuerta = puer.NumeroPuerta;
                                    u.Puertas.Add(p);
                                }
                                u.CreateDate = DateTime.Now;
                                u.CreateUser = userid;
                                bltu.Insert(u);
                            }
                            // unitOfWork.Commit();
                            #endregion

                            #region Plantas                            
                            ConsultaPadronPlantasBL blPlantasT = new ConsultaPadronPlantasBL();

                            var listPlantas = blPlantasT.GetByFKIdConsultaPadron(idCpadron);
                            var listPlantasNew = new List<ConsultaPadronPlantasDTO>();
                            foreach (var planta in listPlantas)
                            {
                                ConsultaPadronPlantasDTO p = new ConsultaPadronPlantasDTO();
                                p.IdConsultaPadron = idCP;
                                p.IdTipoSector = planta.IdTipoSector;
                                p.DetalleConsultaPadronTipoSector = planta.DetalleConsultaPadronTipoSector;
                                blPlantasT.Insert(p);
                                listPlantasNew.Add(p);
                            }
                            #endregion

                            #region Datos Del Local
                            ConsultaPadronDatosLocalBL blDatosT = new ConsultaPadronDatosLocalBL();

                            var dato = blDatosT.GetByFKIdConsultaPadron(idCpadron);

                            foreach (var item in dato)
                            {
                                ConsultaPadronDatosLocalDTO d = new ConsultaPadronDatosLocalDTO();
                                d.CantidadOperariosDl = item.CantidadOperariosDl;
                                d.CantidadSanitariosDl = item.CantidadSanitariosDl;
                                d.CreateDate = DateTime.Now;
                                d.CreateUser = userid;
                                d.CroquisUbicacionDl = item.CroquisUbicacionDl;
                                d.DimesionFrenteDl = item.DimesionFrenteDl;
                                d.EstacionamientoDl = item.EstacionamientoDl;
                                d.FondoDl = item.FondoDl;
                                d.FrenteDl = item.FrenteDl;
                                d.IdConsultaPadron = idCP;
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

                            #region Rubros
                            ConsultaPadronRubrosBL blRubrost = new ConsultaPadronRubrosBL();
                            var lstRubros = blRubrost.GetByFKIdConsultaPadron(idCpadron);
                            foreach (var rub in lstRubros)
                            {
                                ConsultaPadronRubrosDTO r = new ConsultaPadronRubrosDTO();
                                r.CodidoRubro = rub.CodidoRubro;
                                r.CreateDate = DateTime.Now;
                                r.DescripcionRubro = rub.DescripcionRubro;
                                r.EsAnterior = rub.EsAnterior;
                                r.IdConsultaPadron = idCP;
                                r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                                r.IdTipoActividad = rub.IdTipoActividad;
                                r.IdTipoDocumentoReq = rub.IdTipoDocumentoReq;
                                r.LocalVenta = rub.LocalVenta;
                                r.RestriccionSup = rub.RestriccionSup;
                                r.RestriccionZona = rub.RestriccionZona;
                                r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                blRubrost.Insert(r);
                            }

                            #endregion

                            #region Normativas
                            ConsultaPadronNormativasBL blNor = new ConsultaPadronNormativasBL();
                            var listNor = blNor.GetByFKIdConsultaPadron(idCpadron);
                            foreach (var nor in listNor)
                            {
                                ConsultaPadronNormativasDTO n = new ConsultaPadronNormativasDTO();
                                n.CreateDate = DateTime.Now;
                                n.CreateUser = userid;
                                n.IdConsultaPadron = idCP;
                                n.IdEntidadNormativa = nor.IdEntidadNormativa;
                                n.IdTipoNormativa = nor.IdTipoNormativa;
                                n.NumeroNormativa = nor.NumeroNormativa;
                                blNor.Insert(n);
                            }
                            #endregion
                        }
                        #region Titulares CP
                        TransferenciasTitularesPersonasFisicasBL blPfs = new TransferenciasTitularesPersonasFisicasBL();

                        var listPf = blPfs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPf)
                        {
                            ConsultaPadronTitularesPersonasFisicasDTO n = new ConsultaPadronTitularesPersonasFisicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdConsultaPadron = idCP;
                            n.Apellido = p.Apellido;
                            n.Nombres = p.Nombres;
                            n.IdTipoDocumentoPersonal = p.IdTipodocPersonal;
                            n.Cuit = p.Cuit;
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
                            //n.Sms = p.Sms;
                            n.Email = p.Email;
                            n.MismoFirmante = p.MismoFirmante;
                            n.NumeroDocumento = p.NumeroDocumento;
                            n.Torre = p.Torre;
                            blcppf.Insert(n);
                        }

                        TransferenciasTitularesPersonasJuridicasBL blPjs = new TransferenciasTitularesPersonasJuridicasBL();
                        TransferenciasTitularesPersonasJuridicasPersonasFisicasBL blcppjpf = new TransferenciasTitularesPersonasJuridicasPersonasFisicasBL();
                        var listPj = blPjs.GetByFKIdSolicitud(id_solicitud);
                        foreach (var p in listPj)
                        {
                            ConsultaPadronTitularesPersonasJuridicasDTO n = new ConsultaPadronTitularesPersonasJuridicasDTO();
                            n.CreateDate = DateTime.Now;
                            n.CreateUser = userid;
                            n.IdConsultaPadron = idCP;
                            n.IdTipoSociedad = p.IdTipoSociedad;
                            n.RazonSocial = p.RazonSocial;
                            n.CUIT = p.Cuit;
                            n.IdTipoiibb = p.IdTipoiibb;
                            n.NumeroroIibb = p.NumeroIibb;
                            n.Calle = p.Calle;
                            n.NumeroPuerta = p.NumeroPuerta;
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

                            blcppj.Insert(n);
                        }
                        #endregion
                    }

                    if (idEncomienda > 0)
                    {
                        #region Plantas
                        EncomiendaPlantasBL blPlantas = new EncomiendaPlantasBL();
                        ConsultaPadronPlantasBL blPlantasT = new ConsultaPadronPlantasBL();

                        var listPlantas = blPlantas.GetByFKIdEncomienda(idEncomienda);
                        var listPlantasNew = new List<ConsultaPadronPlantasDTO>();
                        foreach (var planta in listPlantas)
                        {
                            ConsultaPadronPlantasDTO p = new ConsultaPadronPlantasDTO();
                            p.IdConsultaPadron = idCP;
                            p.IdTipoSector = planta.IdTipoSector;
                            p.DetalleConsultaPadronTipoSector = planta.detalle_encomiendatiposector;
                            blPlantasT.Insert(p);
                            listPlantasNew.Add(p);
                        }
                        #endregion

                        #region Datos Del Local                    
                        EncomiendaDatosLocalBL blDatos = new EncomiendaDatosLocalBL();
                        ConsultaPadronDatosLocalBL blDatosT = new ConsultaPadronDatosLocalBL();

                        var dato = blDatos.GetByFKIdEncomienda(idEncomienda);

                        if (dato != null)
                        {
                            ConsultaPadronDatosLocalDTO d = new ConsultaPadronDatosLocalDTO();
                            d.CantidadOperariosDl = dato.cantidad_operarios_dl;
                            d.CantidadSanitariosDl = dato.cantidad_sanitarios_dl;
                            d.CreateDate = DateTime.Now;
                            d.CreateUser = userid;
                            d.CroquisUbicacionDl = dato.croquis_ubicacion_dl;
                            d.DimesionFrenteDl = dato.dimesion_frente_dl;
                            d.EstacionamientoDl = dato.estacionamiento_dl;
                            d.FondoDl = dato.fondo_dl;
                            d.FrenteDl = dato.frente_dl;
                            d.IdConsultaPadron = idCP;
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

                        #region Rubros
                        EncomiendaRubrosBL blRubros = new EncomiendaRubrosBL();
                        ConsultaPadronRubrosBL blRubrost = new ConsultaPadronRubrosBL();
                        var lstRubros = blRubros.GetByFKIdEncomienda(idEncomienda);
                        foreach (var rub in lstRubros)
                        {
                            ConsultaPadronRubrosDTO r = new ConsultaPadronRubrosDTO();
                            r.CodidoRubro = rub.CodigoRubro;
                            r.CreateDate = DateTime.Now;
                            r.DescripcionRubro = rub.DescripcionRubro;
                            r.EsAnterior = rub.EsAnterior.Value;
                            r.IdConsultaPadron = idCP;
                            r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                            r.IdTipoActividad = rub.IdTipoActividad;
                            r.IdTipoDocumentoReq = rub.IdTipoDocumentoRequerido;
                            r.LocalVenta = rub.LocalVenta;
                            r.RestriccionSup = rub.RestriccionSup;
                            r.RestriccionZona = rub.RestriccionZona;
                            r.SuperficieHabilitar = rub.SuperficieHabilitar;
                            blRubrost.Insert(r);
                        }

                        #endregion

                        #region Normativas
                        ConsultaPadronNormativasBL blNor = new ConsultaPadronNormativasBL();
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
                            n.NumeroNormativa = (nor.NroNormativa).Substring(0,15);
                            blNor.Insert(n);
                        }
                        #endregion
                    }
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

