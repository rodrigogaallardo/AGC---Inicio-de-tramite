using AutoMapper;
using BaseRepository;
using BaseRepository.Engine;
using BusinesLayer.MappingConfig;
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
    public class EncomiendaBuscadorBL : EncomiendaBL
    {
        public EncomiendaBuscadorBL()
            : base(false)
        { }
    }

    internal class RubrosAnterioresComparer : IEqualityComparer<RubrosDTO>
    {
        public bool Equals(RubrosDTO x, RubrosDTO y)
        {
            if (string.Equals(x.Codigo, y.Codigo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(RubrosDTO obj)
        {
            return obj.Codigo.GetHashCode();
        }
    }

    public class EncomiendaBL : IEncomiendaBL<EncomiendaDTO>
    {
        public static string encomienda_solicitud_encomienda_en_curso = "";
        public static string encomienda_superficie_rubro = "";
        public static string superficieRubroMayorASuperficieAHabilitar = "";
        private EncomiendaRepository repo = null;
        private EncomiendaEstadosRepository repoEstados = null;
        private IUnitOfWorkFactory uowF = null;
        private ItemDirectionRepository itemRepo = null;
        private TitularesRepository repoTit = null;
        private EncomiendaEstadosRepository repoEncEstados = null;
        private EncomiendaExternaRepository repoExt = null;
        private EncomiendaExternaHistorialEstadosRepository repoExtEstado = null;
        private AntenaEncomiendaRepository repoAnt = null;
        private SSITSolicitudesRepository repoSsit = null;
        private TransferenciasSolicitudesRepository repoTransf = null;
        private TipoTramiteRepository repoTipoTramite = null;
        private AntenasDocumentosAdjuntosRepository repoAntenasAdjuntos = null;
        private EncomiendaRubrosRepository repoRubros = null;
        private EncomiendaDocumentosAdjuntosRepository repoEncomiendaDocumentosAdjuntos = null;

        private EncomiendaTitularesPersonasFisicasRepository repoEncTitPF = null;
        private EncomiendaFirmantesPersonasFisicasRepository repoEncFirPF = null;

        private EncomiendaTitularesPersonasJuridicasRepository repoEncTitPJ = null;
        private EncomiendaFirmantesPersonasJuridicasRepository repoEncFirPJ = null;

        private EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository repoEncFirPJPF = null;
        private EngTareasRepository repoEngTareas = null;

        IMapper mapperBuscadorEncomiendaExterna;
        IMapper InstanceMapperBuscadorEncomiendaExterna
        {
            get
            {
                if (mapperBuscadorEncomiendaExterna == null)
                {
                    var configExterna = new MapperConfiguration(cfg =>
                    {
                        #region "Encomienda Externa"

                        cfg.CreateMap<EncomiendaExt, EncomiendaExternaDTO>()
                            .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                            .ForMember(dest => dest.NroEncomiendaConsejo, source => source.MapFrom(p => p.nroEncomiendaconsejo))
                            .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_consejo))
                            .ForMember(dest => dest.IdProfesional, source => source.MapFrom(p => p.id_profesional))
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.TipoTramite))
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            .ForMember(dest => dest.nroTramite, source => source.MapFrom(p => p.nroTramite))
                            .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.EncomiendaExt_Estados))
                            .ForMember(dest => dest.Consejo, source => source.Ignore())
                            .ForMember(dest => dest.ProfesionalDTO, source => source.MapFrom(p => p.Profesional))
                            .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasFisicas, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasJuridicas, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasJuridicasPersonasFisicas, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaExternaUbicaciones, source => source.Ignore())
                            .ForMember(dest => dest.ActasNotariales, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaConformacionLocalDTO, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaDatosLocalDTO, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaDocumentosAdjuntosDTO, source => source.Ignore());

                        cfg.CreateMap<EncomiendaExt_Estados, EncomiendaEstadosDTO>()
                          .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                          .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                          .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                          .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado));

                        #endregion

                        #region "EncomiendaExt_Estados"
                        cfg.CreateMap<EncomiendaExt_Estados, EncomiendaEstadosDTO>()
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                            .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                            .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado));
                        #endregion

                        #region "Profesional"
                        cfg.CreateMap<Profesional, ProfesionalDTO>()
                            .ForMember(dest => dest.ConsejoProfesionalDTO, source => source.Ignore())
                            .ForMember(dest => dest.UserAspNet, source => source.Ignore());

                        #endregion

                    });
                    mapperBuscadorEncomiendaExterna = configExterna.CreateMapper();
                }
                return mapperBuscadorEncomiendaExterna;
            }
        }
        IMapper mapperBase;
        IMapper mapperEncomiendaExterna;
        IMapper InstanceMapperEncomiendaExterna
        {
            get
            {
                if (mapperEncomiendaExterna == null)
                {
                    var configExterna = new MapperConfiguration(cfg =>
                    {
                        #region "Encomienda Externa"

                        cfg.CreateMap<EncomiendaExt, EncomiendaExternaDTO>()
                            .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                            .ForMember(dest => dest.NroEncomiendaConsejo, source => source.MapFrom(p => p.nroEncomiendaconsejo))
                            .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_consejo))
                            .ForMember(dest => dest.IdProfesional, source => source.MapFrom(p => p.id_profesional))
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.TipoTramite))
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            .ForMember(dest => dest.nroTramite, source => source.MapFrom(p => p.nroTramite))
                            .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.EncomiendaExt_Estados))
                            .ForMember(dest => dest.Consejo, source => source.MapFrom(p => p.ConsejoProfesional))
                            .ForMember(dest => dest.ProfesionalDTO, source => source.MapFrom(p => p.Profesional))
                            .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasFisicas, source => source.MapFrom(p => p.EncomiendaExt_Titulares_PersonasFisicas))
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasJuridicas, source => source.MapFrom(p => p.EncomiendaExt_Titulares_PersonasJuridicas))
                            .ForMember(dest => dest.EncomiendaExternaTitularesPersonasJuridicasPersonasFisicas, source => source.MapFrom(p => p.EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas))
                            .ForMember(dest => dest.EncomiendaExternaUbicaciones, source => source.MapFrom(p => p.EncomiendaExt_Ubicaciones));

                        cfg.CreateMap<EncomiendaExt_Titulares_PersonasFisicas, EncomiendaExternaTitularesPersonasFisicasDTO>();
                        cfg.CreateMap<EncomiendaExt_Titulares_PersonasJuridicas, EncomiendaExternaTitularesPersonasJuridicasDTO>();
                        cfg.CreateMap<EncomiendaExt_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaExternaTitularesPersonasJuridicasPersonasFisicasDTO>();

                        cfg.CreateMap<EncomiendaExt_Estados, EncomiendaEstadosDTO>()
                          .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                          .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                          .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                          .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado));

                        cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                            .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                            .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                            .ForMember(dest => dest.CodTipoTramiteWs, source => source.MapFrom(p => p.cod_tipotramite_ws));

                        cfg.CreateMap<EncomiendaExt_Ubicaciones, EncomiendaExternaUbicacionesDTO>()
                            .ForMember(dest => dest.EncomiendaExternaUbicacionesPropiedadHorizontal, source => source.MapFrom(p => p.EncomiendaExt_Ubicaciones_PropiedadHorizontal));

                        cfg.CreateMap<EncomiendaExt_Ubicaciones_PropiedadHorizontal, EncomiendaExternaUbicacionesPropiedadHorizontalDTO>().ReverseMap();

                        #endregion

                        #region "EncomiendaExt_Estados"
                        cfg.CreateMap<EncomiendaEstadosDTO, EncomiendaExt_Estados>().ReverseMap()
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                            .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                            .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado));

                        cfg.CreateMap<EncomiendaExt_Estados, EncomiendaEstadosDTO>().ReverseMap()
                            .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                            .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                            .ForMember(dest => dest.nom_estado, source => source.MapFrom(p => p.NomEstado))
                            .ForMember(dest => dest.nom_estado_consejo, source => source.MapFrom(p => p.NomEstadoConsejo));

                        #endregion

                        #region "EncomiendaExt_Estados_entity"
                        cfg.CreateMap<EncomiendaExternaHistorialEstadosEntity, EncomiendaExternaHistorialEstadosDTO>().ReverseMap();
                        #endregion

                        #region "Profesional"
                        cfg.CreateMap<Profesional, ProfesionalDTO>()
                            .ForMember(dest => dest.ConsejoProfesionalDTO, source => source.MapFrom(p => p.ConsejoProfesional));

                        cfg.CreateMap<ProfesionalDTO, Profesional>()
                            .ForMember(dest => dest.ConsejoProfesional, source => source.MapFrom(p => p.ConsejoProfesionalDTO));

                        #endregion

                        #region  consejo profesional

                        cfg.CreateMap<ConsejoProfesionalDTO, ConsejoProfesional>();

                        cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>();

                        #endregion

                        #region "Ubicaciones "
                        cfg.CreateMap<EncomiendaExt_Ubicaciones, EncomiendaExternaUbicacionesDTO>()
                           .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones));

                        cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                            .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                        #endregion
                    });
                    mapperEncomiendaExterna = configExterna.CreateMapper();
                }
                return mapperEncomiendaExterna;

            }
        }
        IMapper mapperEncomiendaAntenas;
        IMapper InstanceMapperEncomiendaAntena
        {
            get
            {
                if (mapperEncomiendaAntenas == null)
                {
                    var configMapperAntenas = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<EncomiendaAntenasEntity, EncomiendaAntenasGrillaDTO>();

                        cfg.CreateMap<ANT_Encomiendas_Estados, AntEncomiendaEstadosDTO>();

                        #region "Encomienda Antenas"
                        cfg.CreateMap<ANT_Encomiendas, EncomiendaAntenasDTO>()
                            .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            .ForMember(dest => dest.IdTipoCertificado, source => source.MapFrom(p => p.id_tipocertificado))
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                            .ForMember(dest => dest.ZonaDeclarada, source => source.MapFrom(p => p.ZonaDeclarada))
                            .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                            .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.Estado));
                        #endregion

                        cfg.CreateMap<APRA_TiposDeTramite, TipoTramiteDTO>()
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                            .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite));

                        cfg.CreateMap<ANT_Encomiendas_Estados, EncomiendaEstadosDTO>()
                           .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                           .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                           .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                           .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo));

                    });
                    mapperEncomiendaAntenas = configMapperAntenas.CreateMapper();

                }
                return mapperEncomiendaAntenas;
            }
        }

        IMapper mapperBuscador;
        IMapper InstanceMapperBuscador
        {

            get
            {

                if (mapperBuscador == null)
                {
                    var configBuscador = new MapperConfiguration(cfg =>
                    {
                        #region "Encomienda"
                        cfg.CreateMap<Encomienda, EncomiendaDTO>()
                            .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                            .ForMember(dest => dest.NroEncomiendaConsejo, source => source.MapFrom(p => p.nroEncomiendaconsejo))
                            .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_consejo))
                            .ForMember(dest => dest.IdProfesional, source => source.MapFrom(p => p.id_profesional))
                            .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                            .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                            .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                            .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                            // .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                            .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                            .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.Encomienda_Estados))
                            .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                            .ForMember(dest => dest.SubTipoExpediente, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.Ignore())
                            .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.Ignore());
                        #endregion

                        #region "TipoTramite"
                        cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                        .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                        .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                        .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                        .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite_ws));
                        #endregion

                        #region "Estado"
                        cfg.CreateMap<Encomienda_Estados, EncomiendaEstadosDTO>()
                          .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                          .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                          .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                          .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo));
                        #endregion
                    });

                    mapperBuscador = configBuscador.CreateMapper();
                }

                return mapperBuscador;
            }
        }

        IMapper mapperTransf;
        IMapper InstanceMapperTransf
        {

            get
            {
                var configTransf = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Transf_Solicitudes, TransferenciasSolicitudesDTO>()
                .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado));

                    cfg.CreateMap<TransferenciasSolicitudesDTO, Transf_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado));
                });

                mapperTransf = configTransf.CreateMapper();

                return mapperTransf;
            }
        }

        public EncomiendaBL(bool loadMapper)
        {

        }
        public EncomiendaBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
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
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.tipo_anexo, source => source.MapFrom(p => p.tipoAnexo))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasFisicasDTO))
                    .ForMember(dest => dest.Profesional, source => source.MapFrom(p => p.ProfesionalDTO))
                    .ForMember(dest => dest.Encomienda_Rubros, source => source.MapFrom(p => p.EncomiendaRubrosDTO))
                    .ForMember(dest => dest.Encomienda_ConformacionLocal, source => source.MapFrom(p => p.EncomiendaConformacionLocalDTO))
                    .ForMember(dest => dest.Encomienda_DatosLocal, source => source.MapFrom(p => p.EncomiendaDatosLocalDTO))
                    .ForMember(dest => dest.Encomienda_DocumentosAdjuntos, source => source.MapFrom(p => p.EncomiendaDocumentosAdjuntosDTO))
                    .ForMember(dest => dest.Encomienda_Planos, source => source.MapFrom(p => p.EncomiendaPlanosDTO))
                    .ForMember(dest => dest.Encomienda_Plantas, source => source.MapFrom(p => p.EncomiendaPlantasDTO))
                    .ForMember(dest => dest.Encomienda_Rectificatoria, source => source.MapFrom(p => p.EncomiendaRectificatoriaDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones, source => source.MapFrom(p => p.EncomiendaUbicacionesDTO))
                    .ForMember(dest => dest.Encomienda_Normativas, source => source.MapFrom(p => p.EncomiendaNormativasDTO))
                    .ForMember(dest => dest.SubtipoExpediente, source => source.MapFrom(p => p.SubTipoExpediente))
                    .ForMember(dest => dest.Encomienda_Estados, source => source.Ignore())
                    .ForMember(dest => dest.wsEscribanos_ActaNotarial, source => source.Ignore())
                    .ForMember(dest => dest.Encomienda_RubrosCN, source => source.MapFrom(p => p.EncomiendaRubrosCNDTO))
                    .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.MapFrom(p => p.EncomiendaSSITSolicitudesDTO))
                    .ForMember(dest => dest.Encomienda_Transf_Solicitudes, source => source.MapFrom(p => p.EncomiendaTransfSolicitudesDTO))
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
                    //.ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.Encomienda_Estados))
                    .ForMember(dest => dest.tipoAnexo, source => source.MapFrom(p => p.tipo_anexo))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasFisicas))
                    .ForMember(dest => dest.ProfesionalDTO, source => source.MapFrom(p => p.Profesional))
                    .ForMember(dest => dest.EncomiendaRubrosDTO, source => source.MapFrom(p => p.Encomienda_Rubros))
                    .ForMember(dest => dest.EncomiendaConformacionLocalDTO, source => source.MapFrom(p => p.Encomienda_ConformacionLocal))
                    .ForMember(dest => dest.EncomiendaDatosLocalDTO, source => source.MapFrom(p => p.Encomienda_DatosLocal))
                    .ForMember(dest => dest.EncomiendaDocumentosAdjuntosDTO, source => source.MapFrom(p => p.Encomienda_DocumentosAdjuntos))
                    .ForMember(dest => dest.EncomiendaPlanosDTO, source => source.MapFrom(p => p.Encomienda_Planos))
                    .ForMember(dest => dest.EncomiendaPlantasDTO, source => source.MapFrom(p => p.Encomienda_Plantas))
                    .ForMember(dest => dest.EncomiendaRectificatoriaDTO, source => source.MapFrom(p => p.Encomienda_Rectificatoria))
                    .ForMember(dest => dest.EncomiendaUbicacionesDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones))
                    .ForMember(dest => dest.EncomiendaNormativasDTO, source => source.MapFrom(p => p.Encomienda_Normativas))
                    .ForMember(dest => dest.SubTipoExpediente, source => source.MapFrom(p => p.SubtipoExpediente))
                    .ForMember(dest => dest.ActasNotariales, source => source.MapFrom(p => p.wsEscribanos_ActaNotarial))
                    .ForMember(dest => dest.EncomiendaRubrosCNDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN))
                    .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes))
                    .ForMember(dest => dest.Encomienda_RubrosCN_DepositoDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN_Deposito))
                    ;

                #endregion
                #region encomienda solicitud
                cfg.CreateMap<Encomienda_SSIT_Solicitudes, EncomiendaSSITSolicitudesDTO>()
                .ForMember(dest => dest.SSITSolicitudesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes));

                cfg.CreateMap<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>()
                .ForMember(dest => dest.SSIT_Solicitudes, source => source.MapFrom(p => p.SSITSolicitudesDTO));

                cfg.CreateMap<Encomienda_Transf_Solicitudes, EncomiendaTransfSolicitudesDTO>()
                .ForMember(dest => dest.TransferenciasSolicitudesDTO, source => source.MapFrom(p => p.Transf_Solicitudes));

                cfg.CreateMap<EncomiendaTransfSolicitudesDTO, Encomienda_Transf_Solicitudes>()
                .ForMember(dest => dest.Transf_Solicitudes, source => source.MapFrom(p => p.TransferenciasSolicitudesDTO));
                #endregion
                #region "Encomienda_Certificado_Sobrecarga"
                cfg.CreateMap<Encomienda_Certificado_Sobrecarga, EncomiendaCertificadoSobrecargaDTO>()
                    .ForMember(dest => dest.EncomiendaTiposSobrecargasDTO, source => source.MapFrom(p => p.Encomienda_Tipos_Sobrecargas))
                    .ForMember(dest => dest.EncomiendaTiposCertificadosSobrecargaDTO, source => source.MapFrom(p => p.Encomienda_Tipos_Certificados_Sobrecarga));

                cfg.CreateMap<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>()
                    .ForMember(dest => dest.Encomienda_Tipos_Sobrecargas, source => source.MapFrom(p => p.EncomiendaTiposSobrecargasDTO))
                    .ForMember(dest => dest.Encomienda_Tipos_Certificados_Sobrecarga, source => source.MapFrom(p => p.EncomiendaTiposCertificadosSobrecargaDTO));
                #endregion
                #region "Encomienda_Tipos_Certificados_Sobrecarga"
                cfg.CreateMap<EncomiendaTiposCertificadosSobrecargaDTO, Encomienda_Tipos_Certificados_Sobrecarga>();

                cfg.CreateMap<Encomienda_Tipos_Certificados_Sobrecarga, EncomiendaTiposCertificadosSobrecargaDTO>();
                #endregion
                #region "Encomienda_ConformacionLocal"
                cfg.CreateMap<Encomienda_ConformacionLocal, EncomiendaConformacionLocalDTO>()
                    .ForMember(dest => dest.EncomiendaPlantasDTO, source => source.MapFrom(p => p.Encomienda_Plantas))
                    .ForMember(dest => dest.TipoSuperficieDTO, source => source.MapFrom(p => p.TipoSuperficie))
                    .ForMember(dest => dest.TipoVentilacionDTO, source => source.MapFrom(p => p.tipo_ventilacion))
                    .ForMember(dest => dest.TipoDestinoDTO, source => source.MapFrom(p => p.TipoDestino))
                    .ForMember(dest => dest.TipoIluminacionDTO, source => source.MapFrom(p => p.tipo_iluminacion));

                cfg.CreateMap<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>()
                    .ForMember(dest => dest.Encomienda_Plantas, source => source.MapFrom(p => p.EncomiendaPlantasDTO))
                    .ForMember(dest => dest.TipoSuperficie, source => source.MapFrom(p => p.TipoSuperficieDTO))
                    .ForMember(dest => dest.tipo_ventilacion, source => source.MapFrom(p => p.TipoVentilacionDTO))
                    .ForMember(dest => dest.TipoDestino, source => source.MapFrom(p => p.TipoDestinoDTO))
                    .ForMember(dest => dest.tipo_iluminacion, source => source.MapFrom(p => p.TipoIluminacionDTO));
                #endregion
                #region "Encomienda_DatosLocal"
                cfg.CreateMap<Encomienda_DatosLocal, EncomiendaDatosLocalDTO>()
                    .ForMember(dest => dest.EncomiendaCertificadoSobrecargaDTO, source => source.MapFrom(p => p.Encomienda_Certificado_Sobrecarga));

                cfg.CreateMap<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>()
                    .ForMember(dest => dest.Encomienda_Certificado_Sobrecarga, source => source.MapFrom(p => p.EncomiendaCertificadoSobrecargaDTO));

                cfg.CreateMap<Encomienda_DatosLocal, EncomiendaDatosLocalDTO>()
                    .ForMember(dest => dest.dj_certificado_sobrecarga, source => source.MapFrom(p => p.dj_certificado_sobrecarga));

                cfg.CreateMap<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>()
                    .ForMember(dest => dest.dj_certificado_sobrecarga, source => source.MapFrom(p => p.dj_certificado_sobrecarga));
                #endregion
                #region "Encomienda_DocumentosAdjuntos"
                cfg.CreateMap<Encomienda_DocumentosAdjuntos, EncomiendaDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistemaDTO, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridosDTO))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistemaDTO));
                #endregion
                #region "Encomienda_Estados"
                cfg.CreateMap<Encomienda_Estados, EncomiendaEstadosDTO>()
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                    .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                    .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado));

                cfg.CreateMap<EncomiendaEstadosDTO, Encomienda_Estados>()
                  .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                  .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                  .ForMember(dest => dest.nom_estado, source => source.MapFrom(p => p.NomEstado))
                  .ForMember(dest => dest.nom_estado_consejo, source => source.MapFrom(p => p.NomEstadoConsejo));
                #endregion
                #region "Encomienda_Titulares_PersonasFisicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasFisicas, EncomiendaTitularesPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasFisicas))
                    .ForMember(dest => dest.LocalidadDTO, source => source.MapFrom(p => p.Localidad))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeIngresosBrutosDTO, source => source.MapFrom(p => p.TiposDeIngresosBrutos));

                cfg.CreateMap<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasFisicasDTO))
                    .ForMember(dest => dest.Localidad, source => source.MapFrom(p => p.LocalidadDTO))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeIngresosBrutos, source => source.MapFrom(p => p.TiposDeIngresosBrutosDTO));
                #endregion
                #region "Encomienda_Titulares_PersonasJuridicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas, EncomiendaTitularesPersonasJuridicasDTO>()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.NroIb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.LocalidadDTO, source => source.MapFrom(p => p.Localidad))
                    .ForMember(dest => dest.TiposDeIngresosBrutosDTO, source => source.MapFrom(p => p.TiposDeIngresosBrutos))
                    .ForMember(dest => dest.TipoSociedadDTO, source => source.MapFrom(p => p.TipoSociedad))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas));

                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NroIb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Localidad, source => source.MapFrom(p => p.LocalidadDTO))
                    .ForMember(dest => dest.TiposDeIngresosBrutos, source => source.MapFrom(p => p.TiposDeIngresosBrutosDTO))
                    .ForMember(dest => dest.TipoSociedad, source => source.MapFrom(p => p.TipoSociedadDTO))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO));

                #endregion
                #region "Encomienda_Titulares_PersonasJuridicas_PersonasFisicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasDTO))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO));
                #endregion
                #region "Encomienda_Firmantes_PersonasJuridicas"
                cfg.CreateMap<Encomienda_Firmantes_PersonasJuridicas, EncomiendaFirmantesPersonasJuridicasDTO>()
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas));

                cfg.CreateMap<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePj))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegalDTO));
                #endregion
                #region "Encomienda_Firmantes_PersonasFisicas"
                cfg.CreateMap<Encomienda_Firmantes_PersonasFisicas, EncomiendaFirmantesPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasFisicas));

                cfg.CreateMap<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePf))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegalDTO));
                #endregion
                #region "Encomienda_Normativas"
                cfg.CreateMap<Encomienda_Normativas, EncomiendaNormativasDTO>()
                    .ForMember(dest => dest.IdEncomiendaNormativa, source => source.MapFrom(p => p.id_encomiendatiponormativa))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoNormativa, source => source.MapFrom(p => p.id_tiponormativa))
                    .ForMember(dest => dest.IdEntidadNormativa, source => source.MapFrom(p => p.id_entidadnormativa))
                    .ForMember(dest => dest.NroNormativa, source => source.MapFrom(p => p.nro_normativa))
                    .ForMember(dest => dest.TipoNormativaDTO, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativaDTO, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<EncomiendaNormativasDTO, Encomienda_Normativas>()
                    .ForMember(dest => dest.id_encomiendatiponormativa, source => source.MapFrom(p => p.IdEncomiendaNormativa))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tiponormativa, source => source.MapFrom(p => p.IdTipoNormativa))
                    .ForMember(dest => dest.id_entidadnormativa, source => source.MapFrom(p => p.IdEntidadNormativa))
                    .ForMember(dest => dest.nro_normativa, source => source.MapFrom(p => p.NroNormativa))
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativaDTO))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativaDTO));
                #endregion
                #region "Encomienda_Planos"
                cfg.CreateMap<Encomienda_Planos, EncomiendaPlanosDTO>()
                    .ForMember(dest => dest.TiposDePlanosDTO, source => source.MapFrom(p => p.TiposDePlanos));

                cfg.CreateMap<EncomiendaPlanosDTO, Encomienda_Planos>()
                    .ForMember(dest => dest.TiposDePlanos, source => source.MapFrom(p => p.TiposDePlanosDTO));
                #endregion
                #region "Encomienda_Plantas"
                cfg.CreateMap<Encomienda_Plantas, EncomiendaPlantasDTO>()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.detalle_encomiendatiposector))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.TipoSectorDTO, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<EncomiendaPlantasDTO, Encomienda_Plantas>()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.detalle_encomiendatiposector, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSectorDTO));
                #endregion
                #region "Encomienda_Rectificatoria"
                cfg.CreateMap<EncomiendaRectificatoriaDTO, Encomienda_Rectificatoria>();

                cfg.CreateMap<Encomienda_Rectificatoria, EncomiendaRectificatoriaDTO>();
                #endregion
                #region "Encomienda_Rubros"
                cfg.CreateMap<Encomienda_Rubros, EncomiendaRubrosDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.cod_rubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tipodocreq))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental))
                    .ForMember(dest => dest.ImpactoAmbientalDTO, source => source.MapFrom(p => p.ImpactoAmbiental))
                    .ForMember(dest => dest.TipoDocumentacionRequeridaDTO, source => source.MapFrom(p => p.Tipo_Documentacion_Req))
                    .ForMember(dest => dest.TipoActividadDTO, source => source.MapFrom(p => p.TipoActividad));

                cfg.CreateMap<EncomiendaRubrosDTO, Encomienda_Rubros>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.desc_rubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbientalDTO))
                    .ForMember(dest => dest.Tipo_Documentacion_Req, source => source.MapFrom(p => p.TipoDocumentacionRequeridaDTO))
                    .ForMember(dest => dest.TipoActividad, source => source.MapFrom(p => p.TipoActividadDTO));
                #endregion
                #region "Encomienda_Tipos_Sobrecargas"
                cfg.CreateMap<EncomiendaTiposSobrecargasDTO, Encomienda_Tipos_Sobrecargas>();

                cfg.CreateMap<Encomienda_Tipos_Sobrecargas, EncomiendaTiposSobrecargasDTO>();
                #endregion
                #region "Encomienda_Ubicaciones"
                cfg.CreateMap<Encomienda_Ubicaciones, EncomiendaUbicacionesDTO>()
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubtipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalEncomiendaUbicacion, source => source.MapFrom(p => p.deptoLocal_encomiendaubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.EncomiendaUbicacionesPropiedadHorizontalDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.EncomiendaUbicacionesPuertasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Puertas))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.ZonasPlaneamientoDTO, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.EncomiendaUbicacionesDistritosDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Distritos))
                    .ForMember(dest => dest.EncomiendaUbicacionesMixturasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Mixturas));

                cfg.CreateMap<EncomiendaUbicacionesDTO, Encomienda_Ubicaciones>()
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubtipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_encomiendaubicacion, source => source.MapFrom(p => p.DeptoLocalEncomiendaUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.EncomiendaUbicacionesPropiedadHorizontalDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Puertas, source => source.MapFrom(p => p.EncomiendaUbicacionesPuertasDTO))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO))
                    .ForMember(dest => dest.Zonas_Planeamiento, source => source.MapFrom(p => p.ZonasPlaneamientoDTO))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Ubicacion))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Distritos, source => source.MapFrom(p => p.EncomiendaUbicacionesDistritosDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Mixturas, source => source.MapFrom(p => p.EncomiendaUbicacionesMixturasDTO));
                #endregion
                #region "Encomienda_Ubicaciones_PropiedadHorizontal"
                cfg.CreateMap<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdEncomiendaPropiedadHorizontal, source => source.MapFrom(p => p.id_encomiendaprophorizontal))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>()
                  .ForMember(dest => dest.id_encomiendaprophorizontal, source => source.MapFrom(p => p.IdEncomiendaPropiedadHorizontal))
                  .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                  .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));
                #endregion
                #region "Encomienda_Ubicaciones_Puertas"
                cfg.CreateMap<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdEncomiendaPuerta, source => source.MapFrom(p => p.id_encomiendapuerta))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>()
                    .ForMember(dest => dest.id_encomiendapuerta, source => source.MapFrom(p => p.IdEncomiendaPuerta))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));
                #endregion
                #region Encomienda_Ubicaciones_Distritos
                cfg.CreateMap<Encomienda_Ubicaciones_Distritos, Encomienda_Ubicaciones_DistritosDTO>().ReverseMap()
                .ForMember(dest => dest.Ubicaciones_CatalogoDistritos, source => source.MapFrom(p => p.UbicacionesCatalogoDistritosDTO));
                #endregion
                #region Encomienda_Ubicaciones_Mixturas
                cfg.CreateMap<Encomienda_Ubicaciones_Mixturas, Encomienda_Ubicaciones_MixturasDTO>().ReverseMap()
                .ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO));
                #endregion
                #region "SSIT_Solicitudes"
                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Telefono, source => source.MapFrom(p => p.telefono));

                cfg.CreateMap<SSITSolicitudesDTO, SSIT_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.telefono, source => source.MapFrom(p => p.Telefono));
                #endregion
                #region Transf
                cfg.CreateMap<Transf_Solicitudes, TransferenciasSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado));

                cfg.CreateMap<TransferenciasSolicitudesDTO, Transf_Solicitudes>()
                .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado));
                #endregion
                #region "ConsejoProfesional"
                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, source => source.MapFrom(p => p.GrupoConsejos));

                cfg.CreateMap<ConsejoProfesionalDTO, ConsejoProfesional>()
                    .ForMember(dest => dest.GrupoConsejos, source => source.MapFrom(p => p.GrupoConsejosDTO));
                #endregion
                #region "EntidadNormativa"
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                #endregion
                #region "ImpactoAmbiental"
                cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>();

                cfg.CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>();
                #endregion
                #region "Localidad"
                cfg.CreateMap<LocalidadDTO, Localidad>()
                    .ForMember(dest => dest.Provincia, source => source.MapFrom(p => p.ProvinciaDTO));

                cfg.CreateMap<Localidad, LocalidadDTO>()
                    .ForMember(dest => dest.ProvinciaDTO, source => source.MapFrom(p => p.Provincia));
                #endregion
                #region "Profesional"
                cfg.CreateMap<Profesional, ProfesionalDTO>()
                    .ForMember(dest => dest.ConsejoProfesionalDTO, source => source.MapFrom(p => p.ConsejoProfesional));

                cfg.CreateMap<ProfesionalDTO, Profesional>()
                    .ForMember(dest => dest.ConsejoProfesional, source => source.MapFrom(p => p.ConsejoProfesionalDTO));
                #endregion
                #region "ConsejoProfesional"
                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, source => source.MapFrom(p => p.GrupoConsejos));

                cfg.CreateMap<ConsejoProfesionalDTO, ConsejoProfesional>()
                    .ForMember(dest => dest.GrupoConsejos, source => source.MapFrom(p => p.GrupoConsejosDTO));
                #endregion
                #region "GrupoConsejos"
                cfg.CreateMap<GrupoConsejos, GrupoConsejosDTO>()
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.descripcion_grupoconsejo))
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_grupoconsejo))
                    .ForMember(dest => dest.LogoImpresion, source => source.MapFrom(p => p.logo_impresion_grupoconsejo))
                    .ForMember(dest => dest.LogoPantalla, source => source.MapFrom(p => p.logo_pantalla_grupoconsejo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nombre_grupoconsejo));

                cfg.CreateMap<GrupoConsejosDTO, GrupoConsejos>()
                    .ForMember(dest => dest.descripcion_grupoconsejo, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.id_grupoconsejo, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.logo_impresion_grupoconsejo, source => source.MapFrom(p => p.LogoImpresion))
                    .ForMember(dest => dest.logo_pantalla_grupoconsejo, source => source.MapFrom(p => p.LogoPantalla))
                    .ForMember(dest => dest.nombre_grupoconsejo, source => source.MapFrom(p => p.Nombre));
                #endregion
                #region "Provincia"
                cfg.CreateMap<ProvinciaDTO, Provincia>();

                cfg.CreateMap<Provincia, ProvinciaDTO>();
                #endregion
                #region "SubTiposDeUbicacion"
                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<SubTipoUbicacionesDTO, SubTiposDeUbicacion>()
                    .ForMember(dest => dest.TiposDeUbicacion, source => source.MapFrom(p => p.TiposDeUbicacionDTO));
                #endregion
                #region "Tipo_Documentacion_Req"
                cfg.CreateMap<TipoDocumentacionRequeridaDTO, Tipo_Documentacion_Req>();

                cfg.CreateMap<Tipo_Documentacion_Req, TipoDocumentacionRequeridaDTO>();
                #endregion
                #region "tipo_ventilacion"
                cfg.CreateMap<TipoVentilacionDTO, tipo_ventilacion>();

                cfg.CreateMap<tipo_ventilacion, TipoVentilacionDTO>();
                #endregion
                #region "TipoActividad"
                cfg.CreateMap<TipoActividadDTO, TipoActividad>();

                cfg.CreateMap<TipoActividad, TipoActividadDTO>();
                #endregion
                #region "TipoDestino"
                cfg.CreateMap<TipoDestinoDTO, TipoDestino>();

                cfg.CreateMap<TipoDestino, TipoDestinoDTO>();
                #endregion
                #region "TipoDocumentoPersonal"
                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>();

                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>();
                #endregion
                #region "TipoIluminasion"
                cfg.CreateMap<TipoIluminacionDTO, tipo_iluminacion>();

                cfg.CreateMap<tipo_iluminacion, TipoIluminacionDTO>();
                #endregion
                #region "TipoNormativa"
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                #endregion
                #region "TipoSector"
                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
                #endregion
                #region "TiposDeCaracterLegal"
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
                #endregion
                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();
                #endregion
                #region "TiposDeDocumentosSistema"
                cfg.CreateMap<TiposDeDocumentosSistemaDTO, TiposDeDocumentosSistema>();

                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>();
                #endregion
                #region "TiposDeIngresosBrutos"
                cfg.CreateMap<TiposDeIngresosBrutos, TiposDeIngresosBrutosDTO>()
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.CodTipoIb, source => source.MapFrom(p => p.cod_tipoibb))
                    .ForMember(dest => dest.NomTipoIb, source => source.MapFrom(p => p.nom_tipoiibb))
                    .ForMember(dest => dest.FormatoTipoIb, source => source.MapFrom(p => p.formato_tipoiibb));

                cfg.CreateMap<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>()
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.cod_tipoibb, source => source.MapFrom(p => p.CodTipoIb))
                    .ForMember(dest => dest.nom_tipoiibb, source => source.MapFrom(p => p.NomTipoIb))
                    .ForMember(dest => dest.formato_tipoiibb, source => source.MapFrom(p => p.FormatoTipoIb));
                #endregion
                #region "TiposDePlanos"
                cfg.CreateMap<TiposDePlanosDTO, TiposDePlanos>();

                cfg.CreateMap<TiposDePlanos, TiposDePlanosDTO>();
                #endregion
                #region "TiposDeUbicacion"
                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO));
                #endregion
                #region "TipoSociedad"
                cfg.CreateMap<TipoSociedadDTO, TipoSociedad>();

                cfg.CreateMap<TipoSociedad, TipoSociedadDTO>();
                #endregion
                #region "TipoSuperficie"
                cfg.CreateMap<TipoSuperficieDTO, TipoSuperficie>();

                cfg.CreateMap<TipoSuperficie, TipoSuperficieDTO>();
                #endregion
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
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite_ws));
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
                     .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                     .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura));
                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                     .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                     .ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO));
                #endregion
                #region "Ubicaciones_CatalogoDistritos"
                cfg.CreateMap<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>().ReverseMap();
                #endregion
                #region "Ubicaciones_ZonasMixtura"
                cfg.CreateMap<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>().ReverseMap();
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
                #region "Zonas_Planeamiento"
                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>()
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento));

                #endregion                
                #region "Encomienda_RubrosCN"
                cfg.CreateMap<Encomienda_RubrosCN, EncomiendaRubrosCNDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.NombreRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.ImpactoAmbientalDTO, source => source.MapFrom(p => p.ImpactoAmbiental));


                cfg.CreateMap<EncomiendaRubrosCNDTO, Encomienda_RubrosCN>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.NombreRubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbientalDTO));
                #endregion

                #region Impacto Ambiental
                cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>();
                cfg.CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>();
                #endregion

                #region
                cfg.CreateMap<wsEscribanosActaNotarialDTO, wsEscribanos_ActaNotarial>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano));

                cfg.CreateMap<wsEscribanos_ActaNotarial, wsEscribanosActaNotarialDTO>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano));


                cfg.CreateMap<Escribano, EscribanoDTO>();

                cfg.CreateMap<EscribanoDTO, Escribano>();
                #endregion

                cfg.CreateMap<TramitesDTO, TramitesEntity>().ReverseMap();

                cfg.CreateMap<Encomienda_RubrosCN_DepositoDTO, Encomienda_RubrosCN_Deposito>()
                .ForMember(dest => dest.RubrosDepositosCN, source => source.MapFrom(p => p.RubrosDepositosCNDTO))
                .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosCNDTO));

                cfg.CreateMap<Encomienda_RubrosCN_Deposito, Encomienda_RubrosCN_DepositoDTO>()
               .ForMember(dest => dest.RubrosDepositosCNDTO, source => source.MapFrom(p => p.RubrosDepositosCN))
               .ForMember(dest => dest.RubrosCNDTO, source => source.MapFrom(p => p.RubrosCN));

                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<RubrosCN, RubrosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();

            });
            mapperBase = config.CreateMapper();
        }

        public void EncomiendaActualizarTipoSubtipoExpediente(EncomiendaDTO encomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                unitOfWork.Db.SPEncomiendaActualizarTipoSubtipoExpediente(encomienda.IdEncomienda, encomienda.IdTipoExpediente, encomienda.IdSubTipoExpediente);
                unitOfWork.Commit();
            }
        }

        internal void Encomienda_Actualizar_TipoSubtipo_Expediente(Encomienda encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    unitOfWork.Db.Encomienda_Actualizar_TipoSubtipo_Expediente(encomienda.id_encomienda);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateTitularesFromSSIT(int idSolicitud, EncomiendaDTO encDTO, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSsit = new SSITSolicitudesRepository(unitOfWork);
                    repoEncTitPF = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    repoEncFirPF = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);

                    repoEncTitPJ = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJ = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);


                    EncomiendaTitularesPersonasFisicasBL blEncTPF = new EncomiendaTitularesPersonasFisicasBL();
                    EncomiendaFirmantesPersonasFisicasBL blEncFPF = new EncomiendaFirmantesPersonasFisicasBL();
                    EncomiendaTitularesPersonasJuridicasBL blEncTPJ = new EncomiendaTitularesPersonasJuridicasBL();
                    EncomiendaFirmantesPersonasJuridicasBL blEncFPJ = new EncomiendaFirmantesPersonasJuridicasBL();
                    EncomiendaTitularesPersonasJuridicasPersonasFisicasBL blEncTPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasBL();

                    var firPFDel = blEncFPF.GetByFKIdEncomienda(encDTO.IdEncomienda);
                    foreach (var tit in firPFDel)
                        blEncFPF.Delete(tit);

                    var titPFDel = blEncTPF.GetByFKIdEncomienda(encDTO.IdEncomienda);
                    foreach (var tit in titPFDel)
                        blEncTPF.Delete(tit);

                    var titPJPFDel = blEncTPJPF.GetByFKIdEncomienda(encDTO.IdEncomienda);
                    foreach (var tit in titPJPFDel)
                        blEncTPJPF.Delete(tit);

                    var firPJDel = blEncFPJ.GetByFKIdEncomienda(encDTO.IdEncomienda);
                    foreach (var tit in firPJDel)
                        blEncFPJ.Delete(tit);

                    var titPJDel = blEncTPJ.GetByFKIdEncomienda(encDTO.IdEncomienda);
                    foreach (var tit in titPJDel)
                        blEncTPJ.Delete(tit);

                    var id_encomienda = encDTO.IdEncomienda;

                    var ssitEntity = repoSsit.Single(idSolicitud);

                    #region Titulares fisicos
                    foreach (var titPF in ssitEntity.SSIT_Solicitudes_Titulares_PersonasFisicas)
                    {
                        EncomiendaTitularesPersonasFisicasDTO encTitPFDTO = new EncomiendaTitularesPersonasFisicasDTO();
                        encTitPFDTO.IdEncomienda = id_encomienda;
                        encTitPFDTO.Apellido = titPF.Apellido;
                        encTitPFDTO.Nombres = titPF.Nombres;
                        encTitPFDTO.IdTipoDocPersonal = titPF.id_tipodoc_personal;
                        encTitPFDTO.NroDocumento = titPF.Nro_Documento;
                        encTitPFDTO.Cuit = titPF.Cuit;
                        encTitPFDTO.IdTipoiibb = titPF.id_tipoiibb;
                        encTitPFDTO.IngresosBrutos = titPF.Ingresos_Brutos;
                        encTitPFDTO.Calle = titPF.Calle;
                        encTitPFDTO.NroPuerta = titPF.Nro_Puerta;
                        encTitPFDTO.Piso = titPF.Piso;
                        encTitPFDTO.Depto = titPF.Depto;
                        encTitPFDTO.IdLocalidad = titPF.Id_Localidad;
                        encTitPFDTO.CodigoPostal = titPF.Codigo_Postal;
                        encTitPFDTO.Telefono = titPF.Telefono;
                        encTitPFDTO.TelefonoMovil = titPF.TelefonoMovil;
                        encTitPFDTO.Sms = titPF.Sms;
                        encTitPFDTO.Email = titPF.Email;
                        encTitPFDTO.MismoFirmante = titPF.MismoFirmante;
                        encTitPFDTO.CreateUser = userid;
                        encTitPFDTO.CreateDate = DateTime.Now;
                        encTitPFDTO.Torre = titPF.Torre;

                        var titPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(encTitPFDTO);
                        repoEncTitPF.Insert(titPFEntity);

                        foreach (var firPF in titPF.SSIT_Solicitudes_Firmantes_PersonasFisicas)
                        {
                            EncomiendaFirmantesPersonasFisicasDTO encFirPFDTO = new EncomiendaFirmantesPersonasFisicasDTO();
                            encFirPFDTO.IdEncomienda = id_encomienda;
                            encFirPFDTO.IdPersonaFisica = titPFEntity.id_personafisica;
                            encFirPFDTO.Apellido = firPF.Apellido;
                            encFirPFDTO.Nombres = firPF.Nombres;
                            encFirPFDTO.IdTipodocPersonal = firPF.id_tipodoc_personal;
                            encFirPFDTO.NroDocumento = firPF.Nro_Documento;
                            encFirPFDTO.IdTipoCaracter = firPF.id_tipocaracter;
                            encFirPFDTO.Email = firPF.Email;

                            var firPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(encFirPFDTO);
                            repoEncFirPF.Insert(firPFEntity);
                        }
                    }

                    #endregion

                    #region Titulares juridicos
                    foreach (var titPJ in ssitEntity.SSIT_Solicitudes_Titulares_PersonasJuridicas)
                    {
                        EncomiendaTitularesPersonasJuridicasDTO encTitPJDTO = new EncomiendaTitularesPersonasJuridicasDTO();
                        encTitPJDTO.IdEncomienda = id_encomienda;
                        encTitPJDTO.IdTipoSociedad = titPJ.Id_TipoSociedad;
                        encTitPJDTO.RazonSocial = titPJ.Razon_Social;
                        encTitPJDTO.CUIT = titPJ.CUIT;
                        encTitPJDTO.IdTipoIb = titPJ.id_tipoiibb;
                        encTitPJDTO.NroIb = titPJ.Nro_IIBB;
                        encTitPJDTO.Calle = titPJ.Calle;
                        encTitPJDTO.NroPuerta = titPJ.NroPuerta;
                        encTitPJDTO.Piso = titPJ.Piso;
                        encTitPJDTO.Depto = titPJ.Depto;
                        encTitPJDTO.IdLocalidad = titPJ.id_localidad;
                        encTitPJDTO.CodigoPostal = titPJ.Codigo_Postal;
                        encTitPJDTO.Telefono = titPJ.Telefono;
                        encTitPJDTO.Email = titPJ.Email;
                        encTitPJDTO.CreateUser = userid;
                        encTitPJDTO.CreateDate = DateTime.Now;
                        encTitPJDTO.Torre = titPJ.Torre;

                        var titPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(encTitPJDTO);
                        repoEncTitPJ.Insert(titPJEntity);

                        foreach (var firPJDTO in titPJ.SSIT_Solicitudes_Firmantes_PersonasJuridicas)
                        {
                            EncomiendaFirmantesPersonasJuridicasDTO encFirPJDTO = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPJDTO.IdEncomienda = id_encomienda;
                            encFirPJDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                            encFirPJDTO.Apellido = firPJDTO.Apellido;
                            encFirPJDTO.Nombres = firPJDTO.Nombres;
                            encFirPJDTO.IdTipoDocPersonal = firPJDTO.id_tipodoc_personal;
                            encFirPJDTO.NroDocumento = firPJDTO.Nro_Documento;
                            encFirPJDTO.IdTipoCaracter = firPJDTO.id_tipocaracter;
                            encFirPJDTO.CargoFirmantePj = firPJDTO.cargo_firmante_pj;
                            encFirPJDTO.Email = firPJDTO.Email;

                            var firPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(encFirPJDTO);
                            repoEncFirPJ.Insert(firPJEntity);

                            foreach (var titPJPFDTO in firPJDTO.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas)
                            {
                                EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encTitPJPFDTO = new EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO();
                                encTitPJPFDTO.IdEncomienda = id_encomienda;
                                encTitPJPFDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                                encTitPJPFDTO.Apellido = titPJPFDTO.Apellido;
                                encTitPJPFDTO.Nombres = titPJPFDTO.Nombres;
                                encTitPJPFDTO.IdTipoDocPersonal = titPJPFDTO.id_tipodoc_personal;
                                encTitPJPFDTO.NroDocumento = titPJPFDTO.Nro_Documento;
                                encTitPJPFDTO.Email = titPJPFDTO.Email;
                                encTitPJPFDTO.IdFirmantePj = firPJEntity.id_firmante_pj;
                                encTitPJPFDTO.FirmanteMismaPersona = titPJPFDTO.firmante_misma_persona;

                                var firPJPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(encTitPJPFDTO);
                                repoEncFirPJPF.Insert(firPJPFEntity);
                            }
                        }
                    }
                    unitOfWork.Commit();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool ValidarRequerimientosDocumentosRubros(int id_solicitud, List<int> listRubros)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);

            return repo.ValidarRequerimientosDocumentosRubros(id_solicitud, listRubros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public EncomiendaDTO Single(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repo = new EncomiendaRepository(unitOfWork);
                var encomiendaEntity = repo.Single(IdEncomienda);

                var encomiendaDTO = AutoMapperConfig.MapperBaseEncomienda.Map<Encomienda, EncomiendaDTO>(encomiendaEntity);
                var direcciones = GetDireccionesEncomienda(new List<int>() { IdEncomienda });


                if (direcciones.Any())
                    encomiendaDTO.Direccion = direcciones.FirstOrDefault();

                switch (encomiendaDTO.IdTipoTramite)
                {
                    case (int)Constantes.TipoTramite.HABILITACION:
                        encomiendaDTO.TipoTramiteDescripcion = Constantes.TipoTramiteDescripcion.Habilitacion;
                        break;
                    case (int)Constantes.TipoTramite.TRANSFERENCIA:
                        encomiendaDTO.TipoTramiteDescripcion = Constantes.TipoTramiteDescripcion.Transferencia;
                        break;
                    case (int)Constantes.TipoTramite.AMPLIACION:
                        encomiendaDTO.TipoTramiteDescripcion = Constantes.TipoTramiteDescripcion.Ampliacion_Unificacion;
                        break;
                    case (int)Constantes.TipoTramite.REDISTRIBUCION_USO:
                        encomiendaDTO.TipoTramiteDescripcion = Constantes.TipoTramiteDescripcion.RedistribucionDeUso;
                        break;
                }

                switch (encomiendaDTO.IdTipoExpediente)
                {
                    case (int)Constantes.TipoDeExpediente.Simple:
                        encomiendaDTO.TipoExpedienteDescripcion = Constantes.TipoExpedienteDescripcion.Simple;
                        break;
                    case (int)Constantes.TipoDeExpediente.Especial:
                        encomiendaDTO.TipoExpedienteDescripcion = Constantes.TipoExpedienteDescripcion.Especial;
                        break;
                    case (int)Constantes.TipoDeExpediente.NoDefinido:
                        encomiendaDTO.TipoExpedienteDescripcion = Constantes.TipoExpedienteDescripcion.NoDefinido;
                        break;
                }
                if (encomiendaDTO.IdSubTipoExpediente != (int)Constantes.SubtipoDeExpediente.NoDefinido)
                {
                    switch (encomiendaDTO.IdSubTipoExpediente)
                    {
                        case (int)Constantes.SubtipoDeExpediente.InspeccionPrevia:
                            encomiendaDTO.TipoExpedienteDescripcion += Constantes.SubtipoDeExpedienteDescripcion.InspeccionPrevia;
                            break;
                        case (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia:
                            encomiendaDTO.TipoExpedienteDescripcion += Constantes.SubtipoDeExpedienteDescripcion.HabilitacionPrevia;
                            break;
                        case (int)Constantes.SubtipoDeExpediente.ConPlanos:
                            encomiendaDTO.TipoExpedienteDescripcion += Constantes.SubtipoDeExpedienteDescripcion.ConPlanos;
                            break;
                        case (int)Constantes.SubtipoDeExpediente.SinPlanos:
                            encomiendaDTO.TipoExpedienteDescripcion += Constantes.SubtipoDeExpedienteDescripcion.SinPlanos;
                            break;
                    }

                }
                return encomiendaDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetEstadosByUserId(Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetEstadosByUserId(userid);
            return elements;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id_encomienda"></param>
        /// <param name="id_tipotramite"></param>
        /// <param name="id_estado"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public IEnumerable<TramitesDTO> GetByTramiteEstado(Guid UserId, int id_encomienda, int id_tipotramite, int id_estado, int id_solicitud, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByTramiteEstado(UserId);

            if (id_encomienda > 0)
                elements = elements.Where(x => x.id_encomienda == id_encomienda);

            if (id_tipotramite > -1)
                elements = elements.Where(x => x.IdTipoTramite == id_tipotramite);

            if (id_estado > -1)
                elements = elements.Where(x => x.IdEstado == id_estado);

            if (id_solicitud > 0)
            {
                elements = elements.Where(x => x.IdTramite == id_solicitud);
            }

            totalRowCount = elements.Count();
            elements = elements.OrderByDescending(o => o.CreateDate).Skip(startRowIndex).Take(maximumRows);

            //var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
            var elementsDto = mapperBase.Map<IEnumerable<TramitesEntity>, IEnumerable<TramitesDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        public void MailAnularAnexoTecnico(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                    EmailEntity emailEntity = new EmailEntity();
                    MailMessages mailer = new MailMessages();

                    SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();
                    int idMotivoNotificacion = (int)Constantes.MotivosNotificaciones.AnexoTecnicoAnulado;
                    string motivo = notifBL.getMotivoById(idMotivoNotificacion);

                    List<int> lstIdSolicitudes = new List<int>();
                    string htmlBody;
                    string Email;
                    string Asunto;
                    int IdTipoEmail = (int)ExternalService.TiposDeMail.AnulacionAnexoTecnico;
                    var encEntity = repo.Single(id_encomienda);
                    int id_solicitud = encEntity.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault();
                    List<ItemDirectionDTO> lstDireccionesSSIT;
                    if (id_solicitud < 200000)
                    {
                        TransferenciasSolicitudesBL transfBL = new TransferenciasSolicitudesBL();
                        id_solicitud = encEntity.Encomienda_Transf_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault();
                        lstIdSolicitudes.Add(id_solicitud);
                        lstDireccionesSSIT = transfBL.GetDireccionesTransf(lstIdSolicitudes).ToList();
                    }
                    else
                    {
                        lstIdSolicitudes.Add(id_solicitud);
                        lstDireccionesSSIT = solBL.GetDireccionesSSIT(lstIdSolicitudes).ToList();
                    }

                    Email = encEntity.Profesional.Email;
                    Asunto = "";
                    Asunto = Asunto + "Sol: " + id_solicitud.ToString();
                    Asunto = Asunto + " - " + motivo + " - ";
                    Asunto = Asunto + " - AT: " + id_encomienda.ToString();
                    Asunto = Asunto + " - Ubicación: " + lstDireccionesSSIT[0].direccion;

                    emailEntity.Asunto = Asunto;
                    emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                    emailEntity.IdTipoEmail = IdTipoEmail;
                    emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                    emailEntity.CantIntentos = 3;
                    emailEntity.CantMaxIntentos = 3;
                    emailEntity.FechaAlta = DateTime.Now;
                    emailEntity.Prioridad = 1;

                    //Profesional
                    htmlBody = mailer.MailProfesionalAnulacionAnexoTecnico(id_encomienda);
                    emailEntity.Email = Email;
                    emailEntity.Html = htmlBody;

                    EmailServiceBL serviceMail = new EmailServiceBL();
                    var idMail = serviceMail.SendMail(emailEntity);

                    if (id_solicitud < 200000)
                    {
                        TransferenciasNotificacionesBL notiTRBL = new TransferenciasNotificacionesBL();
                        notiTRBL.InsertNotificacionByIdSolicitud(id_solicitud, idMail, idMotivoNotificacion);
                    }
                    else
                    {
                        SSITSolicitudesBL SolBL = new SSITSolicitudesBL();
                        var sol = SolBL.Single(id_solicitud);
                        notifBL.InsertNotificacionByIdSolicitud(sol, idMail, idMotivoNotificacion);
                    }

                    //Titular
                    repoTit = new TitularesRepository(unitOfWork);
                    htmlBody = mailer.MailTitularAnulacionAnexoTecnico(encEntity.id_profesional, encEntity.Profesional.Apellido, encEntity.Profesional.Nombre);
                    emailEntity.Html = htmlBody;

                    var lstTitulares = repoTit.GetTitularesSolicitud(id_solicitud);
                    string mailTitulares = "";
                    foreach (var mailItem in lstTitulares)
                        mailTitulares = mailTitulares + mailItem.Email + "; ";

                    emailEntity.Email = mailTitulares;

                    idMail = serviceMail.SendMail(emailEntity);
                    //notifBL.InsertNotificacionByIdSolicitud(sol, idMail);
                }
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
        public int Insert(EncomiendaDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    var elementDto = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaDTO, Encomienda>(objectDto);
                    var insertOk = repo.Insert(elementDto);
                    unitOfWork.Commit();
                    return elementDto.id_encomienda;
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
        public void Update(EncomiendaDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRepository(unitOfWork);

                    if (objectDTO.IdEstado != (int)Constantes.Encomienda_Estados.Completa && objectDTO.IdEstado != (int)Constantes.Encomienda_Estados.Incompleta
                        && objectDTO.IdEstado != (int)Constantes.Encomienda_Estados.Confirmada && objectDTO.IdEstado != (int)Constantes.Encomienda_Estados.Anulada)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var elementDTO = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaDTO, Encomienda>(objectDTO);
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
        public void Delete(EncomiendaDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    var elementDto = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaDTO, Encomienda>(objectDto);
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
        /// <param name="id_consejo"></param>
        /// <returns></returns>
        public IList<EncomiendaDTO> TraerEncomiendasConsejos(int id_grupoconsejo, string matricula, string Apenom, string cuit, string estados, int pIdEncomienda, int pIdEncomiendaConsejo, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.TraerEncomiendasConsejos(id_grupoconsejo, matricula, Apenom, cuit, estados, pIdEncomienda, pIdEncomiendaConsejo);

                totalRowCount = elements.Count();
                var elementsTake = elements.Skip(startRowIndex).Take(maximumRows).ToList();

                var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IList<Encomienda>, IList<EncomiendaDTO>>(elementsTake);

                var direcciones = GetDireccionesEncomienda(elementsDto.Select(p => p.IdEncomienda).ToList());

                foreach (var element in elementsDto)
                {
                    element.Direccion = direcciones.FirstOrDefault(p => p.id_solicitud == element.IdEncomienda);

                    if (element.EsECI)
                        element.TipoTramiteDescripcion = (element.IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion ? StaticClass.Constantes.TipoTramiteDescripcion.HabilitacionECI : StaticClass.Constantes.TipoTramiteDescripcion.AdecuacionECI);
                    else
                        element.TipoTramiteDescripcion = element.TipoTramite.DescripcionTipoTramite;
                }

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="id_estado"></param>
        /// <param name="nroTramite"></param>
        /// <param name="id_tipocertificado"></param>
        /// <returns></returns>
        public List<EncomiendaExternaDTO> TraerEncomiendasDirectorObra(int BusIdGrupoConsejo,
            string BusMatricula,
            string BusApenom,
            string BusCuit,
            List<int> BusListEstados,
            int BusNroTramite,
            int BusTipoTramite,
            int startRowIndex,
            int maximumRows,
            string sortByExpression,
            out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();

                repo = new EncomiendaRepository(unitOfWork);
                itemRepo = new ItemDirectionRepository(unitOfWork);

                var elements = repo.TraerEncomiendasDirectorObra(BusIdGrupoConsejo, BusMatricula, BusApenom, BusCuit, BusListEstados, BusNroTramite, BusTipoTramite);

                totalRowCount = elements.Count();

                var rq = elements.OrderByDescending(o => o.TipoTramite).ThenByDescending(p => p.FechaEncomienda).Skip(startRowIndex).Take(maximumRows).ToList();

                List<EncomiendaExternaDTO> elementsDto = InstanceMapperBuscadorEncomiendaExterna.Map<List<EncomiendaExt>, List<EncomiendaExternaDTO>>(rq);

                var direcciones = GetDireccionesEncomienda(elementsDto.Select(p => p.IdEncomienda).ToList(), true);

                foreach (var element in elementsDto)
                {
                    element.Direccion = direcciones.First(p => p.id_solicitud == element.IdEncomienda);
                    if (element.IdTipoTramite == (int)Constantes.TipoCertificado.Ligue)
                        element.TipoTramiteDescripcion = Constantes.EncomiendaDirectorObra.LigueDeObra;
                    else if (element.IdTipoTramite == (int)Constantes.TipoCertificado.Desligue)
                        element.TipoTramiteDescripcion = Constantes.EncomiendaDirectorObra.DesligueDeObra;
                }

                return elementsDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="estados"></param>
        /// <param name="tipoTramite"></param>
        /// <param name="nroTramite"></param>
        /// <param name="pIdEncomiendaConsejo"></param>
        /// <returns></returns>
        public IList<EncomiendaExternaDTO> TraerEncomiendasExConsejos(int id_grupoconsejo, string matricula, string Apenom, string cuit, string estados, int tipoTramite, int nroTramite, int pIdEncomiendaConsejo, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.TraerEncomiendasExConsejos(id_grupoconsejo, matricula, Apenom, cuit, estados, tipoTramite, nroTramite, pIdEncomiendaConsejo);
                totalRowCount = elements.Count();

                elements = elements.Skip(startRowIndex).Take(maximumRows);

                var elementsDto = InstanceMapperEncomiendaExterna.Map<IEnumerable<EncomiendaExt>, IEnumerable<EncomiendaExternaDTO>>(elements).ToList();

                var direcciones = GetDireccionesEncomienda(elementsDto.Select(p => p.IdEncomienda).ToList(), true);

                foreach (var element in elementsDto)
                    element.Direccion = direcciones.First(p => p.id_solicitud == element.IdEncomienda);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AntEncomiendaEstadosDTO> GetAntEstados()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAntEstados();
                var elementsDto = InstanceMapperEncomiendaAntena.Map<IEnumerable<ANT_Encomiendas_Estados>, IEnumerable<AntEncomiendaEstadosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdEncomienda"></param>
        ///// <param name="Zona"></param>
        public void ActualizarZonaDeclarada(int IdEncomienda, string Zona)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    var encomienda = repo.Single(IdEncomienda);

                    if (encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);


                    encomienda.ZonaDeclarada = Zona;

                    repo.Update(encomienda);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        public IList<ItemDirectionDTO> GetDireccionesEncomienda(List<int> lstSolicitudes)
        {
            return GetDireccionesEncomienda(lstSolicitudes, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        public IList<ItemDirectionDTO> GetDireccionesEncomiendaExt(List<int> lstSolicitudes)
        {
            return GetDireccionesEncomienda(lstSolicitudes, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstSolicitudes"></param>
        /// <returns></returns>
        private IList<ItemDirectionDTO> GetDireccionesEncomienda(List<int> lstSolicitudes, bool Externas)
        {
            try
            {
                if (uowF == null)
                    uowF = new TransactionScopeUnitOfWorkFactory();
                if (itemRepo == null)
                    itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());

                List<ItemPuertaEntity> LstDoorsDirection = null;
                if (Externas)
                    LstDoorsDirection = itemRepo.GetDireccionesExt(lstSolicitudes).ToList();
                else
                    LstDoorsDirection = itemRepo.GetDirecciones(lstSolicitudes).ToList();

                List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

                int id_solicitud_ant = 0;
                string calle_ant = "";
                string Direccion_armada = "";

                if (LstDoorsDirection.Count() > 0)
                {
                    id_solicitud_ant = LstDoorsDirection[0].id_solicitud;
                    calle_ant = LstDoorsDirection[0].calle;
                }

                foreach (var puerta in LstDoorsDirection)
                {
                    int id = puerta.idUbicacion ?? 0;
                    if (esUbicacionEspecialConObjetoTerritorialByIdUbicacion(id))
                    {
                        puerta.puerta += "t";
                    }

                    if (id_solicitud_ant != puerta.id_solicitud)
                    {
                        ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                        itemDireccion.id_solicitud = id_solicitud_ant;
                        itemDireccion.direccion = Direccion_armada;
                        lstDirecciones.Add(itemDireccion);
                        Direccion_armada = "";
                        id_solicitud_ant = puerta.id_solicitud;
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

        private bool esUbicacionEspecialConObjetoTerritorialByIdUbicacion(int idUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            return repo.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idUbicacion);
        }

        private IList<ItemDirectionDTO> GetDireccionesEncomiendaAntenas(List<int> lstSolicitudes)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = null;
                LstDoorsDirection = itemRepo.GetDireccionesAntenas(lstSolicitudes).ToList();

                List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

                int id_solicitud_ant = 0;
                string calle_ant = "";
                string Direccion_armada = "";

                if (LstDoorsDirection.Count() > 0)
                {
                    id_solicitud_ant = LstDoorsDirection[0].id_solicitud;
                    calle_ant = LstDoorsDirection[0].calle;
                }

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstEncomiendas"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaDTO> GetByFKListIdEncomienda(List<int> lstEncomiendas)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKListIdEncomienda(lstEncomiendas);
            var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public DateTime GetFechaCertificacion(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                return repo.GetFechaCertificacion(id_encomienda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool isRubroAutomatico(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var unitOfWork = this.uowF.GetUnitOfWork();
            repoRubros = new EncomiendaRubrosRepository(unitOfWork);

            var elements = repoRubros.GetRubrosByIdEncomienda(IdEncomienda);
            return elements.All(x => x.Circuito_Automatico == false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);

            repoRubros = new EncomiendaRubrosRepository(unitOfWork);
            var rubros = repoRubros.GetRubros(elementsDto.Select(s => s.IdEncomienda).ToList());

            foreach (var enc in elementsDto)
            {
                if (enc.EsECI)
                    enc.TipoTramiteDescripcion = (enc.IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion ? StaticClass.Constantes.TipoTramiteDescripcion.HabilitacionECI : StaticClass.Constantes.TipoTramiteDescripcion.AdecuacionECI);
                else
                    enc.TipoTramiteDescripcion = enc.TipoTramite.DescripcionTipoTramite;

                foreach (var rubro in enc.EncomiendaRubrosDTO)
                {
                    var rubroEnc = rubros.Where(p => p.IdEncomiendaRubro == rubro.IdEncomiendaRubro).FirstOrDefault();
                    if (rubroEnc != null)
                    {
                        rubro.RestriccionZona = rubroEnc.RestriccionZona;
                        rubro.RestriccionSup = rubroEnc.RestriccionSup;
                    }
                }
            }

            return elementsDto;
        }


        public IEnumerable<EncomiendaDTO> GetByFKIdSolicitudTransf(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);
            var elements = repo.GetByFKIdSolicitudTransf(IdSolicitud);
            var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);

            repoRubros = new EncomiendaRubrosRepository(unitOfWork);
            var rubros = repoRubros.GetRubros(elementsDto.Select(s => s.IdEncomienda).ToList());

            foreach (var enc in elementsDto)
            {
                foreach (var rubro in enc.EncomiendaRubrosDTO)
                {
                    var rubroEnc = rubros.Where(p => p.IdEncomiendaRubro == rubro.IdEncomiendaRubro).FirstOrDefault();
                    if (rubroEnc != null)
                    {
                        rubro.RestriccionZona = rubroEnc.RestriccionZona;
                        rubro.RestriccionSup = rubroEnc.RestriccionSup;
                    }
                }
            }

            return elementsDto;
        }
        /// <summary>
        /// Compara encomienda contra encomienda
        /// </summary>
        /// <param name="idEncomienda1">Numero de primer encomienda</param>
        /// <param name="idEncomienda2">Numero Encomienda 2</param>
        /// <returns>True si son IGUALES </returns>
        public bool CompareBetween(int idEncomienda1, int idEncomienda2)
        {
            bool arEquals = false;

            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());

                EncomiendaUbicacionesBL ubicacionesBL = new EncomiendaUbicacionesBL();
                arEquals = ubicacionesBL.CompareEntreEncomienda(idEncomienda1, idEncomienda2);

                arEquals = arEquals && repo.Compare(idEncomienda1, idEncomienda2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arEquals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="CodigoSeguridad"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int CrearEncomienda(int id_solicitud, string CodigoSeguridad, Guid userid)
        {
            int id_encomienda = 0;

            ValidacionCrearAnexo(id_solicitud, CodigoSeguridad, userid);

            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new EncomiendaRepository(unitOfWork);
                var repoProf = new ProfesionalesRepository(unitOfWork);
                var repoSol = new SSITSolicitudesRepository(unitOfWork);
                var repoTransf = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
                var sol = (dynamic)null;
                var prof = repoProf.Get(userid);

                sol = repoSol.Single(id_solicitud);

                if (sol == null)
                    sol = repoTransf.Single(id_solicitud);

                int nroEncomiendaconsejo = repo.GetMaxNumeroEncomiendaConsejo(prof.IdConsejo.Value);
                IEnumerable<Encomienda> listEnco = null;

                int id_s_origen = 0;
                if (sol.id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                {

                    int id_sol_transf_origen = sol.idSolicitudRef != null ? sol.idSolicitudRef : 0;

                    if (sol.SSIT_Solicitudes_Origen.Count != 0)
                    {
                        id_s_origen = sol.SSIT_Solicitudes_Origen?.id_solicitud_origen ?? 0;
                    }
                }
                else
                {
                    if (sol.SSIT_Solicitudes_Origen != null)
                    {
                        id_s_origen = sol.SSIT_Solicitudes_Origen?.id_solicitud_origen ?? 0;

                        if (id_s_origen == 0)
                            id_s_origen = sol.SSIT_Solicitudes_Origen?.id_transf_origen ?? 0;
                    }
                }

                //Si se trata de una rectificatoria de un tramite de ampliación, y la solicitud hereda de otra anterior,
                //se deben considerar los rubros originales de la solicitud de habilitación de referencia (SSIT_Solicitudes_Origen)
                if (sol.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION && sol.SSIT_Solicitudes_Origen != null)
                {
                    int id_sol_o = sol.SSIT_Solicitudes_Origen?.id_solicitud_origen ?? 0;
                    if (id_sol_o == 0)
                    {
                        // Cuando es una ampliacion que tiene como referencia una transferencia
                        id_sol_o = sol.SSIT_Solicitudes_Origen?.id_transf_origen ?? 0;

                        listEnco = repo.GetByFKIdSolicitudTransf(id_sol_o).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        || x.id_estado == (int)Constantes.Encomienda_Estados.Vencida);
                    }
                    else
                    {
                        listEnco = repo.GetByFKIdSolicitud(id_sol_o).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        || x.id_estado == (int)Constantes.Encomienda_Estados.Vencida);
                    }
                }
                else if (sol.id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    listEnco = repo.GetByFKIdSolicitudTransf(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        || x.id_estado == (int)Constantes.Encomienda_Estados.Vencida);
                }
                else
                {
                    listEnco = repo.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        || x.id_estado == (int)Constantes.Encomienda_Estados.Vencida);
                }

                int id_encomienda_ant = 0;
                string ObsRubros = null;
                int id_subtipoexpediente = sol.id_subtipoexpediente;
                if (listEnco != null && listEnco.Count() > 0)
                {
                    var encAnt = listEnco.OrderByDescending(x => x.id_encomienda).FirstOrDefault();
                    id_encomienda_ant = encAnt.id_encomienda;
                    id_subtipoexpediente = encAnt.id_subtipoexpediente;
                    ObsRubros = encAnt.Observaciones_rubros;
                }

                #region inserto la nueva encomienda
                string CodigoSeguridadEnc = Funciones.getGenerarCodigoSeguridadEncomiendas();


                EncomiendaDTO encDTO = new EncomiendaDTO();
                encDTO.IdEncomienda = 0;
                encDTO.FechaEncomienda = DateTime.Now;
                encDTO.NroEncomiendaConsejo = nroEncomiendaconsejo;
                encDTO.IdConsejo = prof.IdConsejo.Value;
                encDTO.IdProfesional = prof.Id;
                encDTO.IdTipoTramite = sol.id_tipotramite;
                encDTO.IdTipoExpediente = 0;
                encDTO.IdSubTipoExpediente = id_subtipoexpediente;
                encDTO.IdEstado = (int)Constantes.Encomienda_Estados.Incompleta;
                encDTO.CodigoSeguridad = CodigoSeguridadEnc;
                encDTO.CreateDate = DateTime.Now;
                encDTO.CreateUser = userid;
                encDTO.LastUpdateDate = null;
                encDTO.LastUpdateUser = null;
                encDTO.tipoAnexo = Constantes.TipoAnexo_A;
                encDTO.ObservacionesRubros = ObsRubros;

                if (listEnco != null && listEnco.Count() > 0)
                {
                    var encAnt = listEnco.OrderByDescending(x => x.id_encomienda).FirstOrDefault();
                    if (encAnt.InformaModificacion.HasValue)
                    {
                        encDTO.InformaModificacion = encAnt.InformaModificacion.Value;
                    }
                }

                if (sol.id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia)
                {
                    if (sol.Transf_Ubicaciones.Count > 1)
                        encDTO.Servidumbre_paso = true;
                    else
                        encDTO.Servidumbre_paso = false;

                    EncomiendaTransfSolicitudesDTO trDto = new EncomiendaTransfSolicitudesDTO();
                    encDTO.EncomiendaTransfSolicitudesDTO = new List<EncomiendaTransfSolicitudesDTO>();
                    trDto.id_solicitud = id_solicitud;
                    encDTO.EncomiendaTransfSolicitudesDTO.Add(trDto);
                }
                else
                {
                    if ((sol.id_tipotramite == (int)Constantes.TipoDeTramite.Ampliacion ||
                    sol.id_tipotramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso) &&
                    sol.SSIT_Solicitudes_Ubicaciones.Count > 1)
                        encDTO.Servidumbre_paso = true;
                    else
                        encDTO.Servidumbre_paso = sol.Servidumbre_paso;
                    EncomiendaSSITSolicitudesDTO solDTO = new EncomiendaSSITSolicitudesDTO();
                    encDTO.EncomiendaSSITSolicitudesDTO = new List<EncomiendaSSITSolicitudesDTO>();
                    solDTO.id_solicitud = id_solicitud;
                    encDTO.EncomiendaSSITSolicitudesDTO.Add(solDTO);
                }
                var elementDto = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaDTO, Encomienda>(encDTO);
                repo.Insert(elementDto);
                id_encomienda = elementDto.id_encomienda;

                #endregion

                if (id_encomienda_ant == 0)
                {
                    int id_encomienda_solicitud_anterior = 0;

                    if (sol.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION ||
                        sol.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO ||
                        sol.id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                    {
                        int? id_solicitud_origen = null;

                        if (sol.id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                        {
                            if (sol.SSIT_Solicitudes_Origen.Count == 0)
                            {
                                id_solicitud_origen = sol.idSolicitudRef;
                            }
                            else
                            {
                                id_solicitud_origen = sol.idSolicitudRef || sol.SSIT_Solicitudes_Origen?.id_solicitud_origen;
                            }
                        }
                        else
                        {
                            if (sol.SSIT_Solicitudes_Origen != null)
                            {
                                id_solicitud_origen = sol.SSIT_Solicitudes_Origen?.id_solicitud_origen ?? 0;
                                if (id_s_origen == 0)
                                    id_s_origen = sol.SSIT_Solicitudes_Origen?.id_transf_origen ?? 0;
                            }

                        }

                        if (id_solicitud_origen.HasValue)
                        {
                            var EncSolAnt = repo.GetByFKIdSolicitud(id_solicitud_origen.Value).Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                        .OrderByDescending(o => o.CreateDate).FirstOrDefault();

                            if (EncSolAnt != null)
                                id_encomienda_solicitud_anterior = EncSolAnt.id_encomienda;
                        }
                    }


                    if (id_encomienda_solicitud_anterior > 0)
                    {
                        // pone el cumple en los rubros cuando se copia de una encomienda anterior
                        elementDto.CumpleArticulo521 = true;
                        repo.Update(elementDto);
                        copiarDatos(id_solicitud, id_encomienda, id_encomienda_solicitud_anterior, sol.id_tipotramite, userid, id_s_origen);
                    }
                    else
                    {
                        if (sol.id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                            copiarDatosTransf(id_solicitud, id_encomienda, userid);
                        else
                            copiarDatos(id_solicitud, id_encomienda, userid, sol.id_tipotramite);
                    }
                }
                else
                {
                    copiarDatos(id_solicitud, id_encomienda, id_encomienda_ant, sol.id_tipotramite, userid, id_s_origen);
                    //creo la relacion
                    var blRec = new EncomiendaRectificatoriaBL();
                    var rel = new EncomiendaRectificatoriaDTO();
                    rel.id_encomienda_anterior = id_encomienda_ant;
                    rel.id_encomienda_nueva = id_encomienda;
                    blRec.Insert(rel);
                }
                unitOfWork.Commit();
            }
            return id_encomienda;
        }

        private bool ValidacionCrearAnexo(int id_solicitud, string CodigoSeguridad, Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var repoSol = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var repoTransf = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());

            #region Validaciones
            var sol = (dynamic)null;
            sol = repoSol.Single(id_solicitud);
            if (sol == null)
            {
                sol = repoTransf.Single(id_solicitud);
            }

            if (sol == null || sol.CodigoSeguridad.ToUpper() != CodigoSeguridad)
                throw new Exception(Errors.ENCOMIENDA_SOLICITUD_DATOS_INVALIDOS);

            if (sol.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF &&
                sol.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO &&
                sol.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN &&
                sol.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.PING)
                throw new Exception(Errors.ENCOMIENDA_SOLICITUD_ESTADO_ERRONEO);

            var listEncomiendas = repo.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_estado != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Vencida &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Anulada).
                                      Union(repo.GetByFKIdSolicitudTransf(id_solicitud).Where(x => x.id_estado != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Vencida &&
                                      x.id_estado != (int)Constantes.Encomienda_Estados.Anulada));
            //var listEncoEnCurso = listEncomiendas.Where(x => x.IdEstado != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo &&
            //                          x.IdEstado != (int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo &&
            //                          x.IdEstado != (int)Constantes.Encomienda_Estados.Anulada);

            if (listEncomiendas.Count() > 0)
            {
                encomienda_solicitud_encomienda_en_curso = string.Format(Errors.ENCOMIENDA_SOLICITUD_ENCOMIENDA_EN_CURSO, listEncomiendas.First().id_encomienda);
                throw new Exception(encomienda_solicitud_encomienda_en_curso);
            }

            var repoProf = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
            var prof = repoProf.Get(userid);
            if (prof == null)
                throw new Exception(Errors.ENCOMIENDA_PROFESIONAL_INEXISTENTE_USUARIO);
            if (prof.InhibidoBit == true)
                throw new Exception(Errors.ENCOMIENDA_PROFESIONAL_INHIBIDO);
            #endregion
            return true;
        }

        private void copiarDatos(int id_solicitud, int id_encomienda, Guid userid, int id_tipotramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    copyTitularesFromEncomienda(id_solicitud, id_encomienda, userid);

                    #region ubicacion
                    UbicacionesBL blUbic = new UbicacionesBL();
                    SSITSolicitudesUbicacionesBL blUbi = new SSITSolicitudesUbicacionesBL();
                    SSITSolicitudesUbicacionesPropiedadHorizontalBL blHor = new SSITSolicitudesUbicacionesPropiedadHorizontalBL();
                    SSITSolicitudesUbicacionesPuertasBL blPuer = new SSITSolicitudesUbicacionesPuertasBL();
                    SSITSolicitudesUbicacionesDistritoBL blDistrito = new SSITSolicitudesUbicacionesDistritoBL();
                    SSITSolicitudesUbicacionesMixturasBL blMixtura = new SSITSolicitudesUbicacionesMixturasBL();
                    UbicacionesBL blUbicacion = new UbicacionesBL();


                    EncomiendaUbicacionesBL bleu = new EncomiendaUbicacionesBL();
                    var lubi = blUbi.GetByFKIdSolicitud(id_solicitud);

                    foreach (var ubi in lubi)
                    {
                        var ubic = blUbic.Single((int)ubi.IdUbicacion);
                        EncomiendaUbicacionesDTO u = new EncomiendaUbicacionesDTO();
                        u.DeptoLocalEncomiendaUbicacion = ubi.DeptoLocalUbicacion;
                        u.Depto = ubi.Depto;
                        u.Local = ubi.Local;
                        u.Torre = ubi.Torre;
                        u.IdEncomienda = id_encomienda;
                        u.IdSubtipoUbicacion = ubi.IdSubtipoUbicacion;
                        u.IdUbicacion = ubi.IdUbicacion;
                        u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                        u.LocalSubtipoUbicacion = ubi.LocalSubtipoUbicacion;
                        u.InmuebleCatalogado = ubic.EsUbicacionProtegida;

                        var lhor = blHor.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                        u.PropiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
                        foreach (var hor in lhor)
                        {
                            UbicacionesPropiedadhorizontalDTO h = new UbicacionesPropiedadhorizontalDTO();
                            h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                            u.PropiedadesHorizontales.Add(h);
                        }

                        var lpuer = blPuer.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                        u.Puertas = new List<UbicacionesPuertasDTO>();
                        foreach (var puer in lpuer)
                        {
                            UbicacionesPuertasDTO p = new UbicacionesPuertasDTO();
                            p.CodigoCalle = puer.CodigoCalle;
                            p.NroPuertaUbic = puer.NroPuerta;
                            u.Puertas.Add(p);
                        }

                        var ldistrito = blDistrito.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                        u.EncomiendaUbicacionesDistritosDTO = new List<Encomienda_Ubicaciones_DistritosDTO>();
                        foreach (var distri in ldistrito)
                        {
                            Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                            d.IdDistrito = distri.IdDistrito;
                            d.IdZona = distri.IdZona;
                            d.IdSubZona = distri.IdSubZona;
                            u.EncomiendaUbicacionesDistritosDTO.Add(d);
                        }

                        var lmixtura = blMixtura.GetByFKIdSolicitudUbicacion(ubi.IdSolicitudUbicacion);
                        u.EncomiendaUbicacionesMixturasDTO = new List<Encomienda_Ubicaciones_MixturasDTO>();
                        foreach (var mixtu in lmixtura)
                        {
                            Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                            m.IdZonaMixtura = mixtu.IdZonaMixtura;
                            u.EncomiendaUbicacionesMixturasDTO.Add(m);
                        }

                        #region Redistribuciones de Uso
                        //Para Redistribuciones de Uso verificamos si se generó alguna Mixtura o Distrito
                        //Si no generó busco esos datos directamente desde la ubicación
                        if ((id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO) && (ldistrito.Count() == 0) && (lmixtura.Count() == 0))
                        {
                            //Busco Distritos a partir de la Ubicacion
                            var ludistrito = blUbicacion.Single(ubic.IdUbicacion).UbicacionesDistritosDTO.ToList();
                            u.EncomiendaUbicacionesDistritosDTO = new List<Encomienda_Ubicaciones_DistritosDTO>();
                            foreach (var distri in ludistrito)
                            {
                                Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                                d.IdDistrito = distri.IdDistrito;
                                d.IdZona = distri.IdZona;
                                d.IdSubZona = distri.IdSubZona;
                                u.EncomiendaUbicacionesDistritosDTO.Add(d);
                            }

                            //Busco Mixturas a partir de la Ubicacion
                            var lumixtura = blUbicacion.Single(ubic.IdUbicacion).UbicacionesZonasMixturasDTO.ToList();
                            u.EncomiendaUbicacionesMixturasDTO = new List<Encomienda_Ubicaciones_MixturasDTO>();
                            foreach (var mixtu in lumixtura)
                            {
                                Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                                m.IdZonaMixtura = mixtu.IdZona;
                                u.EncomiendaUbicacionesMixturasDTO.Add(m);
                            }
                        }
                        #endregion

                        u.CreateDate = DateTime.Now;
                        u.CreateUser = userid;
                        bleu.Insert(u);
                    }
                    unitOfWork.Commit();
                    #endregion
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void copiarDatosTransf(int id_solicitud, int id_encomienda, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    copyTitularesTransfFromEncomienda(id_solicitud, id_encomienda, userid);

                    #region ubicacion
                    UbicacionesBL blUbic = new UbicacionesBL();
                    TransferenciaUbicacionesBL blUbi = new TransferenciaUbicacionesBL();
                    TransferenciasUbicacionesPropiedadHorizontalBL blHor = new TransferenciasUbicacionesPropiedadHorizontalBL();
                    TransferenciasUbicacionesPuertasBL blPuer = new TransferenciasUbicacionesPuertasBL();
                    TransferenciaUbicacionesDistritosBL blDistrito = new TransferenciaUbicacionesDistritosBL();
                    TransferenciaUbicacionesMixturasBL blMixtura = new TransferenciaUbicacionesMixturasBL();

                    EncomiendaUbicacionesBL bleu = new EncomiendaUbicacionesBL();
                    var lubi = blUbi.GetByFKIdSolicitud(id_solicitud);

                    foreach (var ubi in lubi)
                    {
                        var ubic = blUbic.Single((int)ubi.IdUbicacion);
                        EncomiendaUbicacionesDTO u = new EncomiendaUbicacionesDTO();
                        u.DeptoLocalEncomiendaUbicacion = ubi.DeptoLocalTransferenciaUbicacion;
                        u.Depto = ubi.Depto;
                        u.Local = ubi.Local;
                        u.Torre = ubi.Torre;
                        u.IdEncomienda = id_encomienda;
                        u.IdSubtipoUbicacion = ubi.IdSubTipoUbicacion;
                        u.IdUbicacion = ubi.IdUbicacion;
                        u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                        u.LocalSubtipoUbicacion = ubi.LocalSubTipoUbicacion;
                        u.InmuebleCatalogado = ubic.EsUbicacionProtegida;
                        var lhor = blHor.GetByFKIdSolicitudUbicacion(ubi.IdTransferenciaUbicacion);
                        u.PropiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
                        foreach (var hor in lhor)
                        {
                            UbicacionesPropiedadhorizontalDTO h = new UbicacionesPropiedadhorizontalDTO();
                            h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                            u.PropiedadesHorizontales.Add(h);
                        }
                        var lpuer = blPuer.GetByFKIdTransferenciaUbicacion(ubi.IdTransferenciaUbicacion);
                        u.Puertas = new List<UbicacionesPuertasDTO>();
                        foreach (var puer in lpuer)
                        {
                            UbicacionesPuertasDTO p = new UbicacionesPuertasDTO();
                            p.CodigoCalle = puer.CodigoCalle;
                            p.NroPuertaUbic = puer.NumeroPuerta;
                            u.Puertas.Add(p);
                        }
                        var ldistrito = blDistrito.GetByFKIdSolicitudUbicacion(ubi.IdTransferenciaUbicacion);
                        u.EncomiendaUbicacionesDistritosDTO = new List<Encomienda_Ubicaciones_DistritosDTO>();
                        foreach (var distri in ldistrito)
                        {
                            Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                            d.IdDistrito = distri.IdDistrito;
                            u.EncomiendaUbicacionesDistritosDTO.Add(d);
                        }
                        var lmixtura = blMixtura.GetByFKIdSolicitudUbicacion(ubi.IdTransferenciaUbicacion);
                        u.EncomiendaUbicacionesMixturasDTO = new List<Encomienda_Ubicaciones_MixturasDTO>();
                        foreach (var mixtu in lmixtura)
                        {
                            Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                            m.IdZonaMixtura = mixtu.IdZonaMixtura;
                            u.EncomiendaUbicacionesMixturasDTO.Add(m);
                        }

                        u.CreateDate = DateTime.Now;
                        u.CreateUser = userid;
                        bleu.Insert(u);
                    }
                    unitOfWork.Commit();
                    #endregion
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void copyTitularesFromEncomienda(int id_solicitud, int id_encomienda, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSsit = new SSITSolicitudesRepository(unitOfWork);
                    repoEncTitPF = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    repoEncFirPF = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);

                    repoEncTitPJ = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJ = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);


                    var ssitEntity = repoSsit.Single(id_solicitud);

                    #region Titulares fisicos
                    foreach (var titPF in ssitEntity.SSIT_Solicitudes_Titulares_PersonasFisicas)
                    {
                        EncomiendaTitularesPersonasFisicasDTO encTitPFDTO = new EncomiendaTitularesPersonasFisicasDTO();
                        encTitPFDTO.IdEncomienda = id_encomienda;
                        encTitPFDTO.Apellido = titPF.Apellido;
                        encTitPFDTO.Nombres = titPF.Nombres;
                        encTitPFDTO.IdTipoDocPersonal = titPF.id_tipodoc_personal;
                        encTitPFDTO.NroDocumento = titPF.Nro_Documento;
                        encTitPFDTO.Cuit = titPF.Cuit;
                        encTitPFDTO.IdTipoiibb = titPF.id_tipoiibb;
                        encTitPFDTO.IngresosBrutos = titPF.Ingresos_Brutos;
                        encTitPFDTO.Calle = titPF.Calle;
                        encTitPFDTO.NroPuerta = titPF.Nro_Puerta;
                        encTitPFDTO.Piso = titPF.Piso;
                        encTitPFDTO.Depto = titPF.Depto;
                        encTitPFDTO.IdLocalidad = titPF.Id_Localidad;
                        encTitPFDTO.CodigoPostal = titPF.Codigo_Postal;
                        encTitPFDTO.Telefono = titPF.Telefono;
                        encTitPFDTO.TelefonoMovil = titPF.TelefonoMovil;
                        encTitPFDTO.Sms = titPF.Sms;
                        encTitPFDTO.Email = titPF.Email;
                        encTitPFDTO.MismoFirmante = titPF.MismoFirmante;
                        encTitPFDTO.CreateUser = userid;
                        encTitPFDTO.CreateDate = DateTime.Now;
                        encTitPFDTO.Torre = titPF.Torre;

                        var titPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(encTitPFDTO);
                        repoEncTitPF.Insert(titPFEntity);

                        foreach (var firPF in titPF.SSIT_Solicitudes_Firmantes_PersonasFisicas)
                        {
                            EncomiendaFirmantesPersonasFisicasDTO encFirPFDTO = new EncomiendaFirmantesPersonasFisicasDTO();
                            encFirPFDTO.IdEncomienda = id_encomienda;
                            encFirPFDTO.IdPersonaFisica = titPFEntity.id_personafisica;
                            encFirPFDTO.Apellido = firPF.Apellido;
                            encFirPFDTO.Nombres = firPF.Nombres;
                            encFirPFDTO.IdTipodocPersonal = firPF.id_tipodoc_personal;
                            encFirPFDTO.NroDocumento = firPF.Nro_Documento;
                            encFirPFDTO.IdTipoCaracter = firPF.id_tipocaracter;
                            encFirPFDTO.Email = firPF.Email;

                            var firPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(encFirPFDTO);
                            repoEncFirPF.Insert(firPFEntity);
                        }
                    }

                    #endregion

                    #region Titulares juridicos
                    foreach (var titPJ in ssitEntity.SSIT_Solicitudes_Titulares_PersonasJuridicas)
                    {
                        EncomiendaTitularesPersonasJuridicasDTO encTitPJDTO = new EncomiendaTitularesPersonasJuridicasDTO();
                        encTitPJDTO.IdEncomienda = id_encomienda;
                        encTitPJDTO.IdTipoSociedad = titPJ.Id_TipoSociedad;
                        encTitPJDTO.RazonSocial = titPJ.Razon_Social;
                        encTitPJDTO.CUIT = titPJ.CUIT;
                        encTitPJDTO.IdTipoIb = titPJ.id_tipoiibb;
                        encTitPJDTO.NroIb = titPJ.Nro_IIBB;
                        encTitPJDTO.Calle = titPJ.Calle;
                        encTitPJDTO.NroPuerta = titPJ.NroPuerta;
                        encTitPJDTO.Piso = titPJ.Piso;
                        encTitPJDTO.Depto = titPJ.Depto;
                        encTitPJDTO.IdLocalidad = titPJ.id_localidad;
                        encTitPJDTO.CodigoPostal = titPJ.Codigo_Postal;
                        encTitPJDTO.Telefono = titPJ.Telefono;
                        encTitPJDTO.Email = titPJ.Email;
                        encTitPJDTO.CreateUser = userid;
                        encTitPJDTO.CreateDate = DateTime.Now;
                        encTitPJDTO.Torre = titPJ.Torre;

                        var titPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(encTitPJDTO);
                        repoEncTitPJ.Insert(titPJEntity);

                        foreach (var firPJDTO in titPJ.SSIT_Solicitudes_Firmantes_PersonasJuridicas)
                        {
                            EncomiendaFirmantesPersonasJuridicasDTO encFirPJDTO = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPJDTO.IdEncomienda = id_encomienda;
                            encFirPJDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                            encFirPJDTO.Apellido = firPJDTO.Apellido;
                            encFirPJDTO.Nombres = firPJDTO.Nombres;
                            encFirPJDTO.IdTipoDocPersonal = firPJDTO.id_tipodoc_personal;
                            encFirPJDTO.NroDocumento = firPJDTO.Nro_Documento;
                            encFirPJDTO.IdTipoCaracter = firPJDTO.id_tipocaracter;
                            encFirPJDTO.CargoFirmantePj = firPJDTO.cargo_firmante_pj;
                            encFirPJDTO.Email = firPJDTO.Email;

                            var firPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(encFirPJDTO);
                            repoEncFirPJ.Insert(firPJEntity);

                            foreach (var titPJPFDTO in firPJDTO.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas)
                            {
                                EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encTitPJPFDTO = new EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO();
                                encTitPJPFDTO.IdEncomienda = id_encomienda;
                                encTitPJPFDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                                encTitPJPFDTO.Apellido = titPJPFDTO.Apellido;
                                encTitPJPFDTO.Nombres = titPJPFDTO.Nombres;
                                encTitPJPFDTO.IdTipoDocPersonal = titPJPFDTO.id_tipodoc_personal;
                                encTitPJPFDTO.NroDocumento = titPJPFDTO.Nro_Documento;
                                encTitPJPFDTO.Email = titPJPFDTO.Email;
                                encTitPJPFDTO.IdFirmantePj = firPJEntity.id_firmante_pj;
                                encTitPJPFDTO.FirmanteMismaPersona = titPJPFDTO.firmante_misma_persona;

                                var firPJPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(encTitPJPFDTO);
                                repoEncFirPJPF.Insert(firPJPFEntity);
                            }
                        }
                    }
                    unitOfWork.Commit();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void copyTitularesTransfFromEncomienda(int id_solicitud, int id_encomienda, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoTransf = new TransferenciasSolicitudesRepository(unitOfWork);
                    repoEncTitPF = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    repoEncFirPF = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);

                    repoEncTitPJ = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJ = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);
                    repoEncFirPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);


                    var transfEntity = repoTransf.Single(id_solicitud);

                    #region Titulares fisicos
                    foreach (var titPF in transfEntity.Transf_Titulares_PersonasFisicas)
                    {
                        EncomiendaTitularesPersonasFisicasDTO encTitPFDTO = new EncomiendaTitularesPersonasFisicasDTO();
                        encTitPFDTO.IdEncomienda = id_encomienda;
                        encTitPFDTO.Apellido = titPF.Apellido;
                        encTitPFDTO.Nombres = titPF.Nombres;
                        encTitPFDTO.IdTipoDocPersonal = titPF.id_tipodoc_personal;
                        encTitPFDTO.NroDocumento = titPF.Nro_Documento;
                        encTitPFDTO.Cuit = titPF.Cuit;
                        encTitPFDTO.IdTipoiibb = titPF.id_tipoiibb;
                        encTitPFDTO.IngresosBrutos = titPF.Ingresos_Brutos;
                        encTitPFDTO.Calle = titPF.Calle;
                        encTitPFDTO.NroPuerta = titPF.Nro_Puerta;
                        encTitPFDTO.Piso = titPF.Piso;
                        encTitPFDTO.Depto = titPF.Depto;
                        encTitPFDTO.IdLocalidad = titPF.id_Localidad;
                        encTitPFDTO.CodigoPostal = titPF.Codigo_Postal;
                        encTitPFDTO.Telefono = titPF.Telefono;
                        encTitPFDTO.TelefonoMovil = titPF.Celular;
                        //encTitPFDTO.Sms = titPF.sms;
                        encTitPFDTO.Email = titPF.Email;
                        encTitPFDTO.MismoFirmante = titPF.MismoFirmante;
                        encTitPFDTO.CreateUser = userid;
                        encTitPFDTO.CreateDate = DateTime.Now;
                        //encTitPFDTO.Torre = titPF.Torre;

                        var titPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(encTitPFDTO);
                        repoEncTitPF.Insert(titPFEntity);

                        foreach (var firPF in titPF.Transf_Firmantes_PersonasFisicas)
                        {
                            EncomiendaFirmantesPersonasFisicasDTO encFirPFDTO = new EncomiendaFirmantesPersonasFisicasDTO();
                            encFirPFDTO.IdEncomienda = id_encomienda;
                            encFirPFDTO.IdPersonaFisica = titPFEntity.id_personafisica;
                            encFirPFDTO.Apellido = firPF.Apellido;
                            encFirPFDTO.Nombres = firPF.Nombres;
                            encFirPFDTO.IdTipodocPersonal = firPF.id_tipodoc_personal;
                            encFirPFDTO.NroDocumento = firPF.Nro_Documento;
                            encFirPFDTO.IdTipoCaracter = firPF.id_tipocaracter;
                            encFirPFDTO.Email = firPF.Email;

                            var firPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(encFirPFDTO);
                            repoEncFirPF.Insert(firPFEntity);
                        }
                    }

                    #endregion

                    #region Titulares juridicos
                    foreach (var titPJ in transfEntity.Transf_Titulares_PersonasJuridicas)
                    {
                        EncomiendaTitularesPersonasJuridicasDTO encTitPJDTO = new EncomiendaTitularesPersonasJuridicasDTO();
                        encTitPJDTO.IdEncomienda = id_encomienda;
                        encTitPJDTO.IdTipoSociedad = titPJ.Id_TipoSociedad;
                        encTitPJDTO.RazonSocial = titPJ.Razon_Social;
                        encTitPJDTO.CUIT = titPJ.CUIT;
                        encTitPJDTO.IdTipoIb = titPJ.id_tipoiibb;
                        encTitPJDTO.NroIb = titPJ.Nro_IIBB;
                        encTitPJDTO.Calle = titPJ.Calle;
                        encTitPJDTO.NroPuerta = titPJ.NroPuerta;
                        encTitPJDTO.Piso = titPJ.Piso;
                        encTitPJDTO.Depto = titPJ.Depto;
                        encTitPJDTO.IdLocalidad = titPJ.id_localidad;
                        encTitPJDTO.CodigoPostal = titPJ.Codigo_Postal;
                        encTitPJDTO.Telefono = titPJ.Telefono;
                        encTitPJDTO.Email = titPJ.Email;
                        encTitPJDTO.CreateUser = userid;
                        encTitPJDTO.CreateDate = DateTime.Now;
                        //encTitPJDTO.Torre = titPJ.Torre;

                        var titPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(encTitPJDTO);
                        repoEncTitPJ.Insert(titPJEntity);

                        foreach (var firPJDTO in titPJ.Transf_Firmantes_PersonasJuridicas)
                        {
                            EncomiendaFirmantesPersonasJuridicasDTO encFirPJDTO = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPJDTO.IdEncomienda = id_encomienda;
                            encFirPJDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                            encFirPJDTO.Apellido = firPJDTO.Apellido;
                            encFirPJDTO.Nombres = firPJDTO.Nombres;
                            encFirPJDTO.IdTipoDocPersonal = firPJDTO.id_tipodoc_personal;
                            encFirPJDTO.NroDocumento = firPJDTO.Nro_Documento;
                            encFirPJDTO.IdTipoCaracter = firPJDTO.id_tipocaracter;
                            encFirPJDTO.CargoFirmantePj = firPJDTO.cargo_firmante_pj;
                            encFirPJDTO.Email = firPJDTO.Email;

                            var firPJEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(encFirPJDTO);
                            repoEncFirPJ.Insert(firPJEntity);

                            foreach (var titPJPFDTO in firPJDTO.Transf_Titulares_PersonasJuridicas_PersonasFisicas)
                            {
                                EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encTitPJPFDTO = new EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO();
                                encTitPJPFDTO.IdEncomienda = id_encomienda;
                                encTitPJPFDTO.IdPersonaJuridica = titPJEntity.id_personajuridica;
                                encTitPJPFDTO.Apellido = titPJPFDTO.Apellido;
                                encTitPJPFDTO.Nombres = titPJPFDTO.Nombres;
                                encTitPJPFDTO.IdTipoDocPersonal = titPJPFDTO.id_tipodoc_personal;
                                encTitPJPFDTO.NroDocumento = titPJPFDTO.Nro_Documento;
                                encTitPJPFDTO.Email = titPJPFDTO.Email;
                                encTitPJPFDTO.IdFirmantePj = firPJEntity.id_firmante_pj;
                                encTitPJPFDTO.FirmanteMismaPersona = titPJPFDTO.firmante_misma_persona;

                                var firPJPFEntity = AutoMapperConfig.MapperBaseEncomienda.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(encTitPJPFDTO);
                                repoEncFirPJPF.Insert(firPJPFEntity);
                            }
                        }
                    }
                    unitOfWork.Commit();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void copiarDatos(int id_solicitud, int id_encomienda, int id_encomienda_ant, int id_tipotramite, Guid userid, int id_s_origen)
        {
            int nroSolReferencia = 0;
            try
            {
                var param = new ParametrosBL();
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    #region Titulares
                    if (id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia)
                    {
                        nroSolReferencia = (int)param.GetParametros("NroTransmisionReferencia").ValornumParam;
                        copyTitularesTransfFromEncomienda(id_solicitud, id_encomienda, userid);
                    }
                    else
                    {
                        nroSolReferencia = (int)param.GetParametros("NroSolicitudReferencia").ValornumParam;
                        copyTitularesFromEncomienda(id_solicitud, id_encomienda, userid);
                    }
                    #endregion

                    #region Ubicacion
                    EncomiendaUbicacionesBL blUbi = new EncomiendaUbicacionesBL();
                    EncomiendaUbicacionesPropiedadHorizontalBL blHor = new EncomiendaUbicacionesPropiedadHorizontalBL();
                    EncomiendaUbicacionesPuertasBL blPuer = new EncomiendaUbicacionesPuertasBL();
                    EncomiendaUbicacionesMixturasBL blMixturas = new EncomiendaUbicacionesMixturasBL();
                    EncomiendaUbicacionesDistritoBL blDistrito = new EncomiendaUbicacionesDistritoBL();
                    UbicacionesCatalogoDistritosBL blUbiDis = new UbicacionesCatalogoDistritosBL();
                    UbicacionesZonasMixturasBL blUbiMix = new UbicacionesZonasMixturasBL();

                    TransferenciaUbicacionesBL blTransfUbic = new TransferenciaUbicacionesBL();
                    TransferenciaUbicacionesMixturasBL blTransfubicMixturas = new TransferenciaUbicacionesMixturasBL();
                    TransferenciaUbicacionesDistritosBL blTransfUbicDistritos = new TransferenciaUbicacionesDistritosBL();

                    var encAnt = Single(id_encomienda_ant);
                    var lubi = blUbi.GetByFKIdEncomienda(id_encomienda_ant);
                    foreach (var ubi in lubi)
                    {
                        EncomiendaUbicacionesDTO u = new EncomiendaUbicacionesDTO();
                        u.DeptoLocalEncomiendaUbicacion = ubi.DeptoLocalEncomiendaUbicacion;
                        u.Depto = ubi.Depto;
                        u.Local = ubi.Local;
                        u.Torre = ubi.Torre;
                        u.IdEncomienda = id_encomienda;
                        u.IdSubtipoUbicacion = ubi.IdSubtipoUbicacion;
                        u.IdUbicacion = ubi.IdUbicacion;
                        u.IdZonaPlaneamiento = ubi.IdZonaPlaneamiento;
                        u.LocalSubtipoUbicacion = ubi.LocalSubtipoUbicacion;
                        var lhor = blHor.GetByFKIdEncomiendaUbicacion(ubi.IdEncomiendaUbicacion);
                        u.PropiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
                        foreach (var hor in lhor)
                        {
                            UbicacionesPropiedadhorizontalDTO h = new UbicacionesPropiedadhorizontalDTO();
                            h.IdPropiedadHorizontal = hor.IdPropiedadHorizontal.Value;
                            u.PropiedadesHorizontales.Add(h);
                        }
                        var lpuer = blPuer.GetByFKIdEncomiendaUbicacion(ubi.IdEncomiendaUbicacion);
                        u.Puertas = new List<UbicacionesPuertasDTO>();
                        foreach (var puer in lpuer)
                        {
                            UbicacionesPuertasDTO p = new UbicacionesPuertasDTO();
                            p.CodigoCalle = puer.CodigoCalle;
                            p.NroPuertaUbic = puer.NroPuerta;
                            u.Puertas.Add(p);
                        }

                        u.EncomiendaUbicacionesMixturasDTO = new List<Encomienda_Ubicaciones_MixturasDTO>();
                        u.EncomiendaUbicacionesDistritosDTO = new List<Encomienda_Ubicaciones_DistritosDTO>();
                        if (encAnt.IdSolicitud > nroSolReferencia)
                        {
                            var lmixturas = blMixturas.GetByFKIdEncomiendaUbicacion(ubi.IdEncomiendaUbicacion);
                            foreach (var mixtura in lmixturas)
                            {
                                Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                                m.IdZonaMixtura = mixtura.IdZonaMixtura;
                                u.EncomiendaUbicacionesMixturasDTO.Add(m);
                            }


                            var ldistrito = blDistrito.GetByFKIdEncomiendaUbicacion(ubi.IdEncomiendaUbicacion);
                            foreach (var distrito in ldistrito)
                            {
                                Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                                d.IdDistrito = distrito.IdDistrito;
                                d.IdZona = distrito.IdZona;
                                d.IdSubZona = distrito.IdSubZona;
                                u.EncomiendaUbicacionesDistritosDTO.Add(d);
                            }

                            //Si no existen Distritos y Mixturas en la encomienda de la solicitud de la cual hereda --> verifico en la solicitud actual.
                            // Mantis 0164787: JADHE 63917 - AT - Error al crear AT
                            if (!lmixturas.ToList().Any() && !ldistrito.ToList().Any())
                            {
                                var TransfUbic = blTransfUbic.GetByFKIdSolicitud(id_solicitud);
                                int idSolicitudUbicacion = TransfUbic.FirstOrDefault().IdTransferenciaUbicacion;
                                int idUbicacion = Convert.ToInt32(TransfUbic.FirstOrDefault().IdUbicacion);

                                var lmixturasTransfSoli = blTransfubicMixturas.GetByFKIdSolicitudUbicacion(idSolicitudUbicacion);
                                foreach (var mixtura in lmixturasTransfSoli)
                                {
                                    Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                                    m.IdZonaMixtura = mixtura.IdZonaMixtura;
                                    u.IdUbicacion = idUbicacion;
                                    u.EncomiendaUbicacionesMixturasDTO.Add(m);
                                }

                                var lDistritoTransfSoli = blTransfUbicDistritos.GetByFKIdSolicitudUbicacion(idSolicitudUbicacion);
                                foreach (var distrito in lDistritoTransfSoli)
                                {
                                    Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                                    d.IdDistrito = distrito.IdDistrito;
                                    d.IdZona = distrito.IdZona;
                                    d.IdSubZona = distrito.IdSubZona;
                                    u.IdUbicacion = idUbicacion;
                                    u.EncomiendaUbicacionesDistritosDTO.Add(d);
                                }
                            }
                        }
                        else
                        {
                            //busco la mixtura en base al id de ubicacion
                            UbicacionesZonasMixturasBL blZonaMixturas = new UbicacionesZonasMixturasBL();
                            UbicacionesCatalogoDistritosBL blDistritoMixturas = new UbicacionesCatalogoDistritosBL();

                            var lmixturas = blZonaMixturas.GetZonasUbicacion(ubi.IdUbicacion.Value);

                            foreach (var mixtura in lmixturas)
                            {
                                Encomienda_Ubicaciones_MixturasDTO m = new Encomienda_Ubicaciones_MixturasDTO();
                                m.IdZonaMixtura = mixtura.IdZona;
                                u.EncomiendaUbicacionesMixturasDTO.Add(m);
                            }

                            var ldistrito = blDistritoMixturas.GetDistritosUbicacion(ubi.IdUbicacion.Value);
                            foreach (var distrito in ldistrito)
                            {
                                Encomienda_Ubicaciones_DistritosDTO d = new Encomienda_Ubicaciones_DistritosDTO();
                                d.IdDistrito = distrito.IdDistrito;
                                d.IdZona = blDistritoMixturas.GetIdZonaByUbicacion(ubi.IdUbicacion.Value);
                                d.IdSubZona = blDistritoMixturas.GetIdSubZonaByUbicacion(ubi.IdUbicacion.Value);
                                u.EncomiendaUbicacionesDistritosDTO.Add(d);
                            }
                        }
                        u.CreateDate = DateTime.Now;
                        u.CreateUser = userid;
                        blUbi.Insert(u);
                    }
                    #endregion

                    #region Plantas
                    EncomiendaPlantasBL blPlantas = new EncomiendaPlantasBL();
                    var listPlantas = blPlantas.GetByFKIdEncomienda(id_encomienda_ant);
                    var listPlantasNew = new List<EncomiendaPlantasDTO>();
                    foreach (var planta in listPlantas)
                    {
                        EncomiendaPlantasDTO p = new EncomiendaPlantasDTO();
                        p.id_encomienda = id_encomienda;
                        p.IdTipoSector = planta.IdTipoSector;
                        p.Descripcion = planta.Descripcion;
                        p.id_encomiendatiposector = blPlantas.Insert(p);
                        listPlantasNew.Add(p);
                    }
                    #endregion

                    #region Datos Del Local
                    EncomiendaDatosLocalBL blDatos = new EncomiendaDatosLocalBL();
                    var dato = blDatos.GetByFKIdEncomienda(id_encomienda_ant);
                    if (dato != null)
                    {
                        EncomiendaDatosLocalDTO d = new EncomiendaDatosLocalDTO();
                        d.cantidad_operarios_dl = dato.cantidad_operarios_dl;
                        d.cantidad_sanitarios_dl = dato.cantidad_sanitarios_dl;
                        d.CreateDate = DateTime.Now;
                        d.CreateUser = userid;
                        d.croquis_ubicacion_dl = dato.croquis_ubicacion_dl;
                        d.dimesion_frente_dl = dato.dimesion_frente_dl;
                        d.estacionamiento_dl = dato.estacionamiento_dl;
                        d.fondo_dl = dato.fondo_dl;
                        d.frente_dl = dato.frente_dl;
                        d.id_encomienda = id_encomienda;
                        d.lateral_derecho_dl = dato.lateral_derecho_dl;
                        d.lateral_izquierdo_dl = dato.lateral_izquierdo_dl;
                        d.local_venta = dato.local_venta;
                        d.lugar_carga_descarga_dl = dato.lugar_carga_descarga_dl;
                        d.materiales_paredes_dl = dato.materiales_paredes_dl;
                        d.materiales_pisos_dl = dato.materiales_pisos_dl;
                        d.materiales_revestimientos_dl = dato.materiales_revestimientos_dl;
                        d.materiales_techos_dl = dato.materiales_techos_dl;
                        d.red_transito_pesado_dl = dato.red_transito_pesado_dl;
                        d.sanitarios_distancia_dl = dato.sanitarios_distancia_dl;
                        d.sanitarios_ubicacion_dl = dato.sanitarios_ubicacion_dl;

                        d.dj_certificado_sobrecarga = dato.dj_certificado_sobrecarga;

                        d.sobrecarga_art813_inciso = dato.sobrecarga_art813_inciso;
                        d.sobrecarga_art813_item = dato.sobrecarga_art813_item;
                        d.sobrecarga_corresponde_dl = dato.sobrecarga_corresponde_dl;
                        d.sobrecarga_requisitos_opcion = dato.sobrecarga_requisitos_opcion;
                        d.sobrecarga_tipo_observacion = dato.sobrecarga_tipo_observacion;
                        d.sobre_avenida_dl = dato.sobre_avenida_dl;

                        if (dato.ampliacion_superficie.HasValue && dato.ampliacion_superficie.Value)
                        {
                            d.superficie_cubierta_dl = dato.superficie_descubierta_amp;
                            d.superficie_descubierta_dl = dato.superficie_descubierta_amp;
                        }
                        else
                        {
                            d.superficie_cubierta_dl = dato.superficie_cubierta_dl;
                            d.superficie_descubierta_dl = dato.superficie_descubierta_dl;
                        }
                        d.superficie_sanitarios_dl = dato.superficie_sanitarios_dl;
                        d.cumple_ley_962 = dato.cumple_ley_962;
                        d.eximido_ley_962 = dato.eximido_ley_962;

                        int id_encomiendadatoslocal = blDatos.Insert(d);

                        #region Sobrecargas
                        EncomiendaCertificadoSobrecargaBL blCertSobre = new EncomiendaCertificadoSobrecargaBL();
                        var certSobre = blCertSobre.GetByFKIdEncomiendaDatosLocal(dato.id_encomiendadatoslocal);
                        if (certSobre != null)
                        {
                            EncomiendaCertificadoSobrecargaDTO cs = new EncomiendaCertificadoSobrecargaDTO();
                            cs.CreateDate = DateTime.Now;
                            cs.id_encomienda_datoslocal = id_encomiendadatoslocal;
                            cs.id_tipo_certificado = certSobre.id_tipo_certificado;
                            cs.id_tipo_sobrecarga = certSobre.id_tipo_sobrecarga;
                            int id_sobrecarga = blCertSobre.Insert(cs);

                            //EncomiendaSobrecargaDetalle1BL blSob1 = new EncomiendaSobrecargaDetalle1BL();
                            //EncomiendaSobrecargaDetalle2BL blSob2 = new EncomiendaSobrecargaDetalle2BL();
                            //var listSob1 = blSob1.GetByFKIdSobrecarga(certSobre.id_sobrecarga);
                            //foreach (var sob1 in listSob1)
                            //{
                            //    EncomiendaSobrecargaDetalle1DTO s1 = new EncomiendaSobrecargaDetalle1DTO();
                            //    s1.detalle = sob1.detalle;
                            //    s1.id_sobrecarga = id_sobrecarga;
                            //    s1.id_tipo_destino = sob1.id_tipo_destino;
                            //    s1.id_tipo_uso = sob1.id_tipo_uso;
                            //    s1.losa_sobre = sob1.losa_sobre;
                            //    s1.valor = sob1.valor;
                            //    var pl = listPlantas.Where(x => x.id_encomiendatiposector == sob1.id_encomiendatiposector).First();
                            //    int id_encomiendatiposector = 0;
                            //    foreach (var p in listPlantasNew)
                            //    {
                            //        if (p.IdTipoSector == pl.IdTipoSector && p.Descripcion == pl.Descripcion)
                            //        {
                            //            id_encomiendatiposector = p.id_encomiendatiposector;
                            //            break;
                            //        }
                            //    }
                            //    s1.id_encomiendatiposector = id_encomiendatiposector;
                            //    int id_sobrecarga_detalle1 = blSob1.Insert(s1);
                            //    var listSob2 = blSob2.GetByFKIdSobrecargaDetalle1(sob1.id_sobrecarga_detalle1);
                            //    foreach (var sob2 in listSob2)
                            //    {
                            //        EncomiendaSobrecargaDetalle2DTO s2 = new EncomiendaSobrecargaDetalle2DTO();
                            //        s2.id_sobrecarga_detalle1 = id_sobrecarga_detalle1;
                            //        s2.id_tipo_uso_1 = sob2.id_tipo_uso_1;
                            //        s2.id_tipo_uso_2 = sob2.id_tipo_uso_2;
                            //        s2.valor_1 = sob2.valor_1;
                            //        s2.valor_2 = sob2.valor_2;
                            //        blSob2.Insert(s2);
                            //    }
                            //}

                        }
                        #endregion
                    }
                    #endregion

                    #region Rubros                    
                    if (encAnt.IdSolicitud < nroSolReferencia && encAnt.IdTipoTramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                    {
                        EncomiendaRubrosBL blRubros = new EncomiendaRubrosBL();
                        var lstRubros = blRubros.GetByFKIdEncomienda(id_encomienda_ant);

                        EncomiendaRubrosCNBL blRubrosCN = new EncomiendaRubrosCNBL();
                        var lstRubrosCN = blRubrosCN.GetByFKIdEncomienda(id_encomienda_ant);

                        if (id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION ||
                            id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA ||
                            id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        {
                            // por cada rubro codigo VIEJO lo copio y lo mando a la tabla rubros_AT_anterior
                            foreach (var rub in lstRubros)
                            {
                                EncomiendaRubrosDTO r = new EncomiendaRubrosDTO();
                                r.CodigoRubro = rub.CodigoRubro;
                                r.CreateDate = DateTime.Now;
                                r.DescripcionRubro = rub.DescripcionRubro;
                                r.EsAnterior = rub.EsAnterior;
                                r.IdEncomienda = id_encomienda;
                                r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                                r.IdTipoActividad = rub.IdTipoActividad;
                                r.IdTipoDocumentoRequerido = rub.IdTipoDocumentoRequerido;
                                r.LocalVenta = rub.LocalVenta;
                                r.RestriccionSup = rub.RestriccionSup;
                                r.RestriccionZona = rub.RestriccionZona;
                                r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                blRubros.InsertATAnterior(r);
                            }
                            // por cada rubro codigo NUEVO lo copio y lo mando a la tabla rubrosCN_AT_anterior
                            foreach (var rub in lstRubrosCN)
                            {
                                EncomiendaRubrosCNDTO r = new EncomiendaRubrosCNDTO();
                                r.CodigoRubro = rub.CodigoRubro;
                                r.CreateDate = DateTime.Now;
                                r.DescripcionRubro = rub.DescripcionRubro;
                                r.EsAnterior = rub.EsAnterior;
                                r.IdEncomienda = id_encomienda;
                                r.idImpactoAmbiental = rub.idImpactoAmbiental;
                                r.IdTipoActividad = rub.IdTipoActividad;
                                r.RestriccionSup = rub.RestriccionSup;
                                r.RestriccionZona = rub.RestriccionZona;
                                r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                r.IdTipoExpediente = rub.IdTipoExpediente;
                                blRubrosCN.InsertATAnterior(r);
                            }
                        }
                        else
                        {
                            foreach (var rub in lstRubros)
                            {
                                EncomiendaRubrosDTO r = new EncomiendaRubrosDTO();
                                r.CodigoRubro = rub.CodigoRubro;
                                r.CreateDate = DateTime.Now;
                                r.DescripcionRubro = rub.DescripcionRubro;
                                r.EsAnterior = rub.EsAnterior;
                                r.IdEncomienda = id_encomienda;
                                r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                                r.IdTipoActividad = rub.IdTipoActividad;
                                r.IdTipoDocumentoRequerido = rub.IdTipoDocumentoRequerido;
                                r.LocalVenta = rub.LocalVenta;
                                r.RestriccionSup = rub.RestriccionSup;
                                r.RestriccionZona = rub.RestriccionZona;
                                r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                blRubros.Insert(r, false);
                            }
                        }
                    }
                    else
                    {
                        EncomiendaRubrosCNBL blRubrosCN = new EncomiendaRubrosCNBL();
                        var lstRubros = blRubrosCN.GetByFKIdEncomienda(id_encomienda_ant);

                        //Valido si es una eci de tipo adecuacion y no inserto los rubros
                        SSITSolicitudesBL sol = new SSITSolicitudesBL();
                        var Solicitud = sol.Single(id_solicitud);
                        bool EsEci = (
                            Solicitud != null &&
                            Solicitud.IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIAdecuacion
                            && Solicitud.EsECI
                            );
                        if (!EsEci)
                        {
                            foreach (var rub in lstRubros)
                            {
                                EncomiendaRubrosCNDTO r = new EncomiendaRubrosCNDTO();
                                r.CodigoRubro = rub.CodigoRubro;
                                r.CreateDate = DateTime.Now;
                                r.DescripcionRubro = rub.DescripcionRubro;
                                r.EsAnterior = rub.EsAnterior;
                                r.IdEncomienda = id_encomienda;
                                r.IdTipoActividad = rub.IdTipoActividad;
                                r.RestriccionSup = rub.RestriccionSup;
                                r.RestriccionZona = rub.RestriccionZona;
                                r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                blRubrosCN.Insert(r, false, userid);

                                EncomiendaRubrosSubCNBL blSubRubrosCN = new EncomiendaRubrosSubCNBL();
                                var lstSubRubros = blSubRubrosCN.GetSubRubrosByEncomiendaRubroVigentes(rub.IdEncomiendaRubro, id_encomienda_ant);
                                if (lstSubRubros.Count() > 0)
                                {
                                    foreach (var item in lstSubRubros)
                                    {
                                        EncomiendaRubrosCNSubrubrosDTO subr = new EncomiendaRubrosCNSubrubrosDTO();
                                        subr.Id_rubrosubrubro = item.Id_rubrosubrubro;
                                        subr.Id_EncRubro = r.IdEncomiendaRubro; //
                                        subr.rubrosCNSubRubrosDTO = item.rubrosCNSubRubrosDTO;
                                        blSubRubrosCN.Insert(subr, userid);
                                    }
                                }
                            }

                        }
                        if (id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION ||
                            id_tipotramite == (int)Constantes.TipoTramite.TRANSFERENCIA ||
                            id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        {
                            if (lstRubros.Count() > 0)
                            {
                                foreach (var rub in lstRubros)
                                {
                                    EncomiendaRubrosCNDTO r = new EncomiendaRubrosCNDTO();
                                    r.CodigoRubro = rub.CodigoRubro;
                                    r.CreateDate = DateTime.Now;
                                    r.DescripcionRubro = rub.DescripcionRubro;
                                    r.EsAnterior = rub.EsAnterior;
                                    r.IdEncomienda = id_encomienda;
                                    r.idImpactoAmbiental = rub.idImpactoAmbiental;
                                    r.IdTipoActividad = rub.IdTipoActividad;
                                    r.RestriccionSup = rub.RestriccionSup;
                                    r.RestriccionZona = rub.RestriccionZona;
                                    r.SuperficieHabilitar = rub.SuperficieHabilitar;

                                    if (id_s_origen != 0 && id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION)
                                    {
                                        blRubrosCN.InsertATAnterior(r);
                                    }

                                }
                            }
                            else
                            {
                                EncomiendaRubrosBL blRubros = new EncomiendaRubrosBL();
                                var lstRubrosAnt = blRubros.GetByFKIdEncomienda(id_encomienda_ant);

                                foreach (var rub in lstRubrosAnt)
                                {
                                    EncomiendaRubrosDTO r = new EncomiendaRubrosDTO();
                                    r.CodigoRubro = rub.CodigoRubro;
                                    r.CreateDate = DateTime.Now;
                                    r.DescripcionRubro = rub.DescripcionRubro;
                                    r.EsAnterior = rub.EsAnterior;
                                    r.IdEncomienda = id_encomienda;
                                    r.IdImpactoAmbiental = rub.IdImpactoAmbiental;
                                    r.IdTipoActividad = rub.IdTipoActividad;
                                    r.IdTipoDocumentoRequerido = rub.IdTipoDocumentoRequerido;
                                    r.LocalVenta = rub.LocalVenta;
                                    r.RestriccionSup = rub.RestriccionSup;
                                    r.RestriccionZona = rub.RestriccionZona;
                                    r.SuperficieHabilitar = rub.SuperficieHabilitar;
                                    blRubros.InsertATAnterior(r);
                                }
                            }
                        }

                        // Copiamos los depositos
                        Encomienda_RubrosCN_DepositoBL depoBL = new Encomienda_RubrosCN_DepositoBL();
                        var lstDeposAnteiores = depoBL.GetByEncomienda(id_encomienda_ant);
                        foreach (var depo in lstDeposAnteiores)
                        {
                            Encomienda_RubrosCN_DepositoDTO d = new Encomienda_RubrosCN_DepositoDTO();
                            d.id_encomienda = id_encomienda;
                            d.IdDeposito = depo.IdDeposito;
                            d.IdRubro = depo.IdRubro;
                            depoBL.InsertRubDeposito(d);
                        }
                    }

                    #endregion

                    #region Normativas
                    EncomiendaNormativasBL blNor = new EncomiendaNormativasBL();
                    var listNor = blNor.GetByFKIdEncomienda(id_encomienda_ant);
                    foreach (var nor in listNor)
                    {
                        EncomiendaNormativasDTO n = new EncomiendaNormativasDTO();
                        n.CreateDate = DateTime.Now;
                        n.CreateUser = userid;
                        n.IdEncomienda = id_encomienda;
                        n.IdEntidadNormativa = nor.IdEntidadNormativa;
                        n.IdTipoNormativa = nor.IdTipoNormativa;
                        n.NroNormativa = nor.NroNormativa;
                        blNor.Insert(n);
                    }
                    #endregion

                    #region Conformación del Local
                    EncomiendaConformacionLocalBL blConf = new EncomiendaConformacionLocalBL();
                    var listConf = blConf.GetByFKIdEncomienda(id_encomienda_ant);
                    foreach (var conf in listConf)
                    {
                        EncomiendaConformacionLocalDTO c = new EncomiendaConformacionLocalDTO();
                        c.alto_conflocal = conf.alto_conflocal;
                        c.ancho_conflocal = conf.ancho_conflocal;
                        c.CreateDate = DateTime.Now;
                        c.CreateUser = userid;
                        c.Detalle_conflocal = conf.Detalle_conflocal;
                        c.Frisos_conflocal = conf.Frisos_conflocal;
                        c.id_destino = conf.id_destino;
                        c.id_encomienda = id_encomienda;
                        var pl = listPlantas.Where(x => x.id_encomiendatiposector == conf.id_encomiendatiposector).FirstOrDefault();
                        int? id_encomiendatiposector = null;

                        if (pl != null)
                        {
                            foreach (var p in listPlantasNew)
                            {
                                if (p.IdTipoSector == pl.IdTipoSector && p.Descripcion == pl.Descripcion)
                                {
                                    id_encomiendatiposector = p.id_encomiendatiposector;
                                    break;
                                }
                            }
                        }
                        c.id_encomiendatiposector = id_encomiendatiposector;
                        c.id_iluminacion = conf.id_iluminacion;
                        c.id_tiposuperficie = conf.id_tiposuperficie;
                        c.id_ventilacion = conf.id_ventilacion;
                        c.largo_conflocal = conf.largo_conflocal;
                        c.Observaciones_conflocal = conf.Observaciones_conflocal;
                        c.Paredes_conflocal = conf.Paredes_conflocal;
                        c.Pisos_conflocal = conf.Pisos_conflocal;
                        c.superficie_conflocal = conf.superficie_conflocal;
                        c.Techos_conflocal = conf.Techos_conflocal;
                        blConf.Insert(c);
                    }
                    #endregion

                    #region Planos
                    EncomiendaPlanosBL blPlanos = new EncomiendaPlanosBL();
                    var listPlanos = blPlanos.GetByFKIdEncomienda(id_encomienda_ant);
                    foreach (var plano in listPlanos)
                    {
                        EncomiendaPlanosDTO p = new EncomiendaPlanosDTO();

                        // En este caso se deja la fecha ya que no se permite eliminar los planos que se ingresaron en el trámite anterior.
                        // para realizar dicho análisis es necesario tener la fecha original que se subió el plano que se está copiando.
                        p.CreateDate = plano.CreateDate;
                        p.CreateUser = userid.ToString();
                        p.detalle = plano.detalle;
                        p.id_encomienda = id_encomienda;
                        p.id_file = plano.id_file;
                        p.id_tipo_plano = plano.id_tipo_plano;
                        p.nombre_archivo = plano.nombre_archivo;

                        blPlanos.Insert(p);
                    }
                    #endregion

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ConfirmarAnexoTecnico(int id_encomienda, Guid userid)
        {
            if (ValidacionAnexo(id_encomienda))
            {
                #region acciones
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    var repoTareas = new EngTareasRepository(unitOfWork);

                    // este SP se llama pq dentro hace la actualización de Tipo y subtipo de expediente en la encomienda
                    int id_circuito = repoTareas.GetIdCircuitoByIdEncomienda(id_encomienda);

                    var repo = new EncomiendaRepository(unitOfWork);
                    var repoSolHab = new SSITSolicitudesRepository(unitOfWork);
                    var repoSolTransf = new TransferenciasSolicitudesRepository(unitOfWork);
                    var encomiendaEntity = repo.Single(id_encomienda);
                    Transf_Solicitudes solicitudTransfEntity = null;
                    SSIT_Solicitudes solicitudHabEntity = null;
                    int id_solicitud = 0;
                    int CantidadEncomiendasxSolicitudHAB = 0;
                    int CantidadEncomiendasxSolicitudTransf = 0;

                    if (encomiendaEntity.id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia)
                    {
                        id_solicitud = encomiendaEntity.Encomienda_Transf_Solicitudes.FirstOrDefault().id_solicitud;
                        solicitudTransfEntity = repoSolTransf.Single(id_solicitud);
                        CantidadEncomiendasxSolicitudTransf = solicitudTransfEntity.Encomienda_Transf_Solicitudes.Count(x => x.Encomienda.id_estado != (int)Constantes.Encomienda_Estados.Anulada);
                    }
                    else
                    {
                        id_solicitud = encomiendaEntity.Encomienda_SSIT_Solicitudes.FirstOrDefault().id_solicitud;
                        solicitudHabEntity = repoSolHab.Single(id_solicitud);
                        CantidadEncomiendasxSolicitudHAB = solicitudHabEntity.Encomienda_SSIT_Solicitudes.Count(x => x.Encomienda.id_estado != (int)Constantes.Encomienda_Estados.Anulada);
                    }

                    repoEncEstados = new EncomiendaEstadosRepository(unitOfWork);

                    #region Tipo Anexo
                    if (CantidadEncomiendasxSolicitudHAB <= 1 && CantidadEncomiendasxSolicitudTransf <= 1)
                        encomiendaEntity.tipo_anexo = Constantes.TipoAnexo_A;
                    else
                    {
                        var repoRec = new EncomiendaRectificatoriaRepository(unitOfWork);
                        var EncAnt = repoRec.GetByFKIdEncomienda(encomiendaEntity.id_encomienda);

                        if (EncAnt != null)
                        {
                            if (CompareBetween(encomiendaEntity.id_encomienda, EncAnt.id_encomienda_anterior))
                                encomiendaEntity.tipo_anexo = Constantes.TipoAnexo_B;
                            else
                                encomiendaEntity.tipo_anexo = Constantes.TipoAnexo_A;
                        }
                    }
                    #endregion

                    #region actualizo la encomienda
                    encomiendaEntity.id_estado = (int)Constantes.Encomienda_Estados.Confirmada;
                    encomiendaEntity.LastUpdateUser = userid;
                    encomiendaEntity.LastUpdateDate = DateTime.Now;

                    repo.Update(encomiendaEntity);
                    unitOfWork.Commit();
                    #endregion
                }
                #endregion
                return true;
            }
            return false;
        }



        public void RegenerarPDFEncomienda(int id_encomienda, Guid userid)
        {
            int nroSolReferencia = 0;

            byte[] pdfEncomienda = new byte[0];

            List<string> lstCirSP = new List<string>();
            lstCirSP.Add("SSP");
            lstCirSP.Add("SSP-A");
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);

            ExternalService.Class.ReportingEntity ReportingEntity = new ExternalService.Class.ReportingEntity();

            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

            var enc = Single(id_encomienda);

            EngineBL blEng = new EngineBL();

            int idCir = blEng.GetIdCircuitoByIdEncomienda(id_encomienda);


            ExternalServiceReporting reportingService = new ExternalServiceReporting();

            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repoEngTareas = new EngTareasRepository(unitOfWork);
                string grupoCir = repoEngTareas.GetCircuito(idCir).nombre_grupo;
                if (idCir != (int)Constantes.ENG_Circuitos.TRANSF_NUEVO)
                {
                    if (enc.IdSolicitud > nroSolReferencia)
                    {
                        if (lstCirSP.Contains(grupoCir))
                            ReportingEntity = reportingService.GetPDFEncomiendaDDRRASP(id_encomienda, true);
                        else
                            ReportingEntity = reportingService.GetPDFEncomiendaDDRRPL(id_encomienda, true);
                    }
                    else
                    {
                        ReportingEntity = reportingService.GetPDFEncomiendaDDRRPL(id_encomienda, true);
                    }
                }
                else
                {
                    ReportingEntity = reportingService.GetPDFEncomiendaTransmision(id_encomienda, true);
                }
                pdfEncomienda = ReportingEntity.Reporte;
            }
            if (pdfEncomienda.Length == 0)
            {
                throw new Exception("El Anexo está en blanco.");
            }
            else
            {
                try
                {
                    EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    EncomiendaDocumentosAdjuntosDTO encDocDTO;

                    string FileName = ReportingEntity.FileName;
                    int id_file = ReportingEntity.Id_file;

                    if (id_file == 0)
                        throw new Exception("No se pudo guardar el file en el servicio, id_encomienda = " + id_encomienda.ToString());

                    encDocDTO = encDocBL.GetByFKIdEncomiendaTipoSis(id_encomienda, (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL).FirstOrDefault();

                    if (encDocDTO != null)
                    {
                        if (id_file != encDocDTO.id_file)
                            esf.deleteFile(encDocDTO.id_file);
                        encDocDTO.id_file = id_file;
                        encDocDTO.UpdateUser = userid;
                        encDocDTO.UpdateDate = DateTime.Now;
                        encDocBL.Update(encDocDTO);
                    }
                    else
                    {
                        encDocDTO = new EncomiendaDocumentosAdjuntosDTO();
                        encDocDTO.id_encomienda = id_encomienda;
                        encDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL;
                        encDocDTO.id_tdocreq = 0;
                        encDocDTO.generadoxSistema = true;
                        encDocDTO.CreateDate = DateTime.Now;
                        encDocDTO.CreateUser = userid;
                        encDocDTO.nombre_archivo = FileName;
                        encDocDTO.id_file = id_file;
                        encDocBL.Insert(encDocDTO);
                    }
                }
                catch (Exception ex)
                {
                    LogError.Write(ex, ex.Message);
                    throw;
                }
            }
        }

        private int getIdCirByEncomienda(int id_encomienda)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        private bool ValidacionAnexo(int id_encomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);

            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            List<string> listaErrores = new List<string>();
            var enc = repo.Single(id_encomienda);

            EncomiendaDTO encDTO = AutoMapperConfig.MapperBaseEncomienda.Map<Encomienda, EncomiendaDTO>(enc);

            if (enc.id_estado == (int)Constantes.Encomienda_Estados.Completa)
            {
                var repoRub = new EncomiendaRubrosRepository(unitOfWork);
                var repoRubCN = new EncomiendaRubrosCNRepository(unitOfWork);
                var repoSsit = new SSITSolicitudesRepository(unitOfWork);
                var datosLocal = enc.Encomienda_DatosLocal.FirstOrDefault();
                var rubros = repoRub.GetRubros(id_encomienda);
                var rubrosCN = repoRubCN.GetByFKIdEncomienda(id_encomienda);
                var planos = enc.Encomienda_Planos;
                decimal SuperficieTotal = 0;
                bool esAmpliacionSuperficie = (datosLocal.ampliacion_superficie.HasValue ? datosLocal.ampliacion_superficie.Value : false);

                if (esAmpliacionSuperficie)
                    SuperficieTotal = datosLocal.superficie_cubierta_amp.Value + datosLocal.superficie_descubierta_amp.Value;
                else
                    SuperficieTotal = datosLocal.superficie_cubierta_dl.Value + datosLocal.superficie_descubierta_dl.Value;



                #region Validaciones
                //Si es un edificio protegido debe adjuntar la dispo de DGUIUR                
                if (enc.Encomienda_SSIT_Solicitudes.Count > 0)
                {
                    var solDTO = repoSsit.Single(enc.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault());
                    if (enc.Encomienda_Ubicaciones.Where(x => x.Ubicaciones.EsUbicacionProtegida).Any() &&
                        (solDTO.SSIT_DocumentosAdjuntos.Count <= 0 ||
                        !solDTO.SSIT_DocumentosAdjuntos.Where(y => y.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Disposicion_DGIUR).Any()) &&
                        (enc.Encomienda_DocumentosAdjuntos.Count <= 0 || enc.Encomienda_DocumentosAdjuntos.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Disposicion_DGIUR).Count() <= 0))
                        listaErrores.Add(Errors.SSIT_SOLICITUD_UBICACION_PROTEGIDA);
                }

                #region validacion redistribucion de uso
                if (enc.id_tipotramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso
                    && enc.id_tipoexpediente == (int)Constantes.TipoDeExpediente.Simple
                    && enc.id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                    listaErrores.Add(Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP);
                #endregion

                #region validacion cantidad operario
                if (datosLocal.cantidad_operarios_dl <= 0)
                    listaErrores.Add(Errors.ENCOMIENDA_CANT_OPERARIOS);
                #endregion

                #region validacion superficie rubro


                bool valRubroHab = false;
                bool RubroSuperaMax = false;
                //EncomiendaRubrosEntity rubro = null;
                string rubroDesc = string.Empty;
                foreach (var rub in rubros)
                {
                    if (rub.SuperficieHabilitar == SuperficieTotal)
                        valRubroHab = true;

                    if (rub.SuperficieHabilitar > SuperficieTotal)
                    {
                        RubroSuperaMax = true;
                        rubroDesc = rub.DescripcionRubro;
                    }
                }
                foreach (var rub in rubrosCN)
                {
                    if (rub.SuperficieHabilitar == SuperficieTotal)
                        valRubroHab = true;

                    if (rub.SuperficieHabilitar > SuperficieTotal)
                    {
                        RubroSuperaMax = true;
                        rubroDesc = rub.NombreRubro;
                    }
                }
                if (valRubroHab == false)
                    listaErrores.Add(Errors.ENCOMIENDA_SUP_RUBROS);

                if (RubroSuperaMax)
                {
                    encomienda_superficie_rubro = string.Format(Errors.ENCOMIENDA_SUPERFICIE_RUBRO, rubroDesc);
                    listaErrores.Add(encomienda_superficie_rubro);
                }
                #endregion

                #region validacion rubros


                RubrosBL rubBL = new RubrosBL();
                var rubrosAnterioresDTO = rubBL.GetRubrosAnterioresByIdEncomienda(id_encomienda);
                var rubrosDTO = rubBL.GetRubrosByIdEncomienda(id_encomienda);

                // Para redistribuciones de uso no se debenn corroborar los rubros.
                if (enc.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO &&
                    enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    var normativa = enc.Encomienda_Normativas.FirstOrDefault();

                    if (normativa == null)
                    {
                        foreach (var rub in rubros)
                        {
                            if ((rub.RestriccionZona != "tilde.png" || rub.RestriccionSup != "tilde.png")
                                && !rubrosAnterioresDTO.Any(x => x.Codigo == rub.CodigoRubro))
                                listaErrores.Add(Errors.ENCOMIENDA_RUBROS_INVALIDOS);
                        }
                    }
                    else
                    {
                        var repoTipoDocReq = new TiposDeDocumentosRequeridosRepository(unitOfWork);
                        var repoDoc = new SSITDocumentosAdjuntosRepository(unitOfWork);

                        var listTipoDoc = repoTipoDocReq.GetByFKIdTipoNormativa(normativa.id_tiponormativa);

                        var listDocSsit = repoDoc.GetByFKIdSolicitud(enc.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault()).Where(x => listTipoDoc.Select(s => s.id_tdocreq).Contains(x.id_tdocreq)).Any();
                        var listDocEnc = enc.Encomienda_DocumentosAdjuntos.Where(x => listTipoDoc.Select(s => s.id_tdocreq).Contains(x.id_tdocreq)).Any();

                        if (!listDocEnc && !listDocSsit)
                            listaErrores.Add(Errors.SSIT_SOLICITUD_NORMATIVA_ANEXO_SIN_DOCUMENTO);
                    }


                    rubBL.ValidarRubrosIndividuales(enc.Encomienda_Rubros.Select(s => s.cod_rubro).ToList());

                    var arrRubrosNoVigentes = rubrosDTO.Where(x => x.EsAnterior == true)
                        .Except(rubrosAnterioresDTO, new RubrosAnterioresComparer())
                        .Select(s => s.Codigo);

                    string rubrosNoVigentes = string.Join(", ", arrRubrosNoVigentes);

                    if (arrRubrosNoVigentes.Count() > 0)
                        listaErrores.Add(string.Format(Errors.ENCOMIENDA_RUBROS_NO_VIGENTES, rubrosNoVigentes));

                }


                #endregion

                #region "Validaciones para SSP"

                if (enc.id_tipoexpediente == (int)Constantes.TipoDeExpediente.Simple &&
                    enc.id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                {
                    decimal superficieTotalSectorCargaDescarga = 0;
                    decimal AnchoFrente = 0;

                    bool tieneDestinoPlayaCargaDescarga;

                    #region "Conformacion del local y Rubros PlayaCargaDescarga"

                    tieneDestinoPlayaCargaDescarga = enc.Encomienda_ConformacionLocal
                                                            .Where(x => x.id_destino == (int)Constantes.TipoDestino.PlayaCargaDescarga)
                                                            .Any();

                    if (tieneDestinoPlayaCargaDescarga)
                    {
                        superficieTotalSectorCargaDescarga = enc.Encomienda_ConformacionLocal
                                                               .Where(x => x.id_destino == (int)Constantes.TipoDestino.PlayaCargaDescarga)
                                                               .Sum(s => s.superficie_conflocal).Value;

                        if (superficieTotalSectorCargaDescarga < 30)
                            listaErrores.Add(Errors.ENCOMIENDA_SECTORES_CARGA_DESCARGA_MAYOR_30MTS);
                    }
                    #endregion

                    #region "Validacion con Rubros"

                    if (rubrosDTO.Any())
                    {

                        #region "Validacion Rubros con deposito"
                        var RubroDeposito = rubrosDTO.Any(a => a.TieneDeposito == true);
                        var LocalConDeposito = enc.Encomienda_ConformacionLocal.Where(x => x.id_destino == (int)Constantes.TipoDestino.Deposito).Any();

                        if (RubroDeposito && LocalConDeposito)
                        {
                            decimal SumSuperficieDeposito = enc.Encomienda_ConformacionLocal
                                                                .Where(x => x.id_destino == (int)Constantes.TipoDestino.Deposito)
                                                                .Sum(s => s.superficie_conflocal).Value;

                            decimal totalPorcentaje = (SumSuperficieDeposito * 100) / SuperficieTotal;

                            if (totalPorcentaje > 60)
                                listaErrores.Add(Errors.ENCOMIENDA_SUMATORIA_DEPOSITO_MAYOR_TOTAL);
                        }

                        #endregion
                        decimal rubroCargaDescargaRefII = 0;
                        decimal rubroCargaDescarga = 0;

                        #region "Validacion Rubros Carga Descarga Segun Ref II"
                        bool tieneRubroCargaDescargaRefII = rubrosDTO.Where(x => x.SupMinCargaDescargaRefII.HasValue).Any();

                        if (tieneRubroCargaDescargaRefII)
                            rubroCargaDescargaRefII = rubrosDTO.Max(m => m.SupMinCargaDescargaRefII).Value;
                        #endregion

                        #region "Validacion Rubros Carga Descarga"
                        bool tieneRubroCargaDescarga = rubrosDTO.Where(x => x.SupMinCargaDescarga.HasValue).Any();

                        AnchoFrente = enc.Encomienda_DatosLocal.Select(s => s.frente_dl).FirstOrDefault().Value;

                        if (SuperficieTotal >= 300 && AnchoFrente >= 10)
                            if (tieneRubroCargaDescarga)
                                rubroCargaDescarga = rubrosDTO.Max(m => m.SupMinCargaDescarga).Value;
                        #endregion
                        if (!(enc.Contiene_galeria_paseo.HasValue ? enc.Contiene_galeria_paseo.Value : false))
                        {
                            if (tieneRubroCargaDescargaRefII || (SuperficieTotal >= 300 && AnchoFrente >= 10 && rubrosDTO.Where(x => x.ValidaCargaDescarga).Any()))
                            {
                                if (!tieneDestinoPlayaCargaDescarga)
                                    listaErrores.Add(Errors.ENCOMIENDA_CARGAR_DESTINO_CARGADESCARGA);

                                if (rubroCargaDescargaRefII > rubroCargaDescarga)
                                    rubroCargaDescarga = rubroCargaDescargaRefII;

                                if (superficieTotalSectorCargaDescarga < rubroCargaDescarga)
                                    listaErrores.Add(string.Format(Errors.ENCOMIENDA_SUP_CARGADESCARGA_MENOR_SUP_TOTAL, rubroCargaDescarga));
                            }
                        }
                        #region "Validacion Rubros Carga Descarga Segun Ref V"


                        if (SuperficieTotal >= 300)
                        {
                            bool tieneRubroCargaDescargaRefV = rubrosDTO.Where(x => x.SupMinCargaDescargaRefV.HasValue).Any();

                            decimal rubroCargaDescargaRefV = 0;

                            if (tieneRubroCargaDescargaRefV)
                                rubroCargaDescargaRefV = rubrosDTO.Where(x => x.SupMinCargaDescargaRefV.HasValue)
                                                                        .Max(m => m.SupMinCargaDescargaRefV).Value;

                            decimal porcentajeTotalPorRubroCargaDescargaRefV = SuperficieTotal * rubroCargaDescargaRefV / 100;

                            if (superficieTotalSectorCargaDescarga < porcentajeTotalPorRubroCargaDescargaRefV && tieneRubroCargaDescargaRefV)
                                listaErrores.Add(string.Format(Errors.ENCOMIENDA_SUP_CARGADESCARGA_PORCENTAJE, rubroCargaDescargaRefV));
                        }
                        #endregion

                    }
                    #endregion

                    #region "Validacion cantidad de operarios"

                    var encDatosLocalConSanitariosDentro = enc.Encomienda_DatosLocal.Where(x => x.sanitarios_ubicacion_dl == (int)Constantes.SanitariosUbicacion.DentroLocal).FirstOrDefault();

                    if (encDatosLocalConSanitariosDentro != null && enc.Encomienda_Rubros.Count > 0)    // Solo se valida  si posee rubros CPU, para rubros CUR no se debe validar
                    {
                        int CantBanos = enc.Encomienda_ConformacionLocal.Where(x => x.id_destino == (int)Constantes.TipoDestino.Baño).Count();
                        int CantOperarios = encDatosLocalConSanitariosDentro.cantidad_operarios_dl ?? 0;

                        if (CantOperarios <= 4 && CantBanos == 0)
                            listaErrores.Add(Errors.ENCOMIENDA_DEBE_EXISTIR_1_BANO);

                        if (CantOperarios > 5 && CantOperarios < 9 && CantBanos < 2)
                            listaErrores.Add(Errors.ENCOMIENDA_DEBE_EXISTIR_2_BANO);

                        bool eximido_ley_962 = encDatosLocalConSanitariosDentro.eximido_ley_962 ?? false;
                        bool salubridad_especial = encDatosLocalConSanitariosDentro.salubridad_especial ?? false;

                        if (CantOperarios >= 10 && (CantBanos < 2 || !(salubridad_especial || eximido_ley_962)))
                            listaErrores.Add(Errors.ENCOMIENDA_DEBE_EXISTIR_2_BANO_SALUBRIDAD);
                    }
                    #endregion
                }

                #endregion

                #region "Validacion eximido ley 962"
                bool eximidoLey962 = enc.Encomienda_DatosLocal.Where(x => x.eximido_ley_962 ?? false == true).Any();

                if (eximidoLey962 && enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    Ley962TiposDeDocumentosRequeridosBL ley962BL = new Ley962TiposDeDocumentosRequeridosBL();
                    var tipoDocReqLey962 = ley962BL.GetAll();
                    int docReqLey962Insertados = enc.Encomienda_DocumentosAdjuntos
                                                                        .Where(x => tipoDocReqLey962.Select(s => s.id_tdocreq).Distinct()
                                                                                    .Contains(x.id_tdocreq))
                                                                        .Select(s => s.id_tdocreq)
                                                                        .Distinct()
                                                                        .Count();

                    if (docReqLey962Insertados < tipoDocReqLey962.Count())
                        listaErrores.Add(Errors.ENCOMIENDA_ADJUNTAR_DOCUMENTOS_LEY_962);
                }
                #endregion

                #region "Validacion tipo doc requerido por zona por rubro"
                if (enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    List<int> documentosInsertados = enc.Encomienda_DocumentosAdjuntos.Select(s => s.id_tdocreq).ToList();

                    TiposDeDocumentosRequeridosBL tipoDocReqBL = new TiposDeDocumentosRequeridosBL();
                    var lstTipoDocReqDTO = tipoDocReqBL.GetAll().ToList();

                    foreach (var itemRubro in rubrosDTO)
                    {
                        if (itemRubro.RubrosTiposDeDocumentosRequeridosZonasDTO.Where(x => x.obligatorio_rubtdocreq && x.codZonaHab == enc.ZonaDeclarada).Any())
                        {
                            var documentosReqPorRubroPorZona = itemRubro.RubrosTiposDeDocumentosRequeridosZonasDTO.Where(x => x.obligatorio_rubtdocreq).ToList();
                            foreach (var docReq in documentosReqPorRubroPorZona.Where(x => !documentosInsertados.Contains(x.id_tdocreq)))
                            {
                                string nombreTipoDoc = lstTipoDocReqDTO.Where(x => x.id_tdocreq == docReq.id_tdocreq).Select(s => s.nombre_tdocreq).FirstOrDefault();
                                string codRubro = rubBL.Single(itemRubro.IdRubro).Codigo;
                                listaErrores.Add(string.Format(Errors.ENCOMIENDA_ADJUNTAR_DOCUMENTOS_REQUERIDO_RUBRO_ZONA, nombreTipoDoc, codRubro));
                            }
                        }
                    }
                }
                #endregion

                #region "Validacion declara que cumple con el artículo 5.2.1 inc (Zona residencial)"

                ParametrosDTO parametrosDTO = new ParametrosBL().GetParametros(ConfigurationManager.AppSettings["CodParam"]);

                if ((encDTO.EncomiendaSSITSolicitudesDTO?.FirstOrDefault()?.id_solicitud ?? 0) < parametrosDTO.ValornumParam)
                {
                    var zonPla = new ZonasPlaneamientoBL().GetZonaPlaneamientoByIdEncomienda(id_encomienda);

                    List<int> lstZonaPlaneamiento = enc.Encomienda_Ubicaciones.Select(s => s.id_zonaplaneamiento).ToList();
                    lstZonaPlaneamiento.AddRange(zonPla.Select(s => s.IdZonaPlaneamiento).ToList());

                    if (new EncomiendaUbicacionesBL().esZonaResidencial(lstZonaPlaneamiento) && !enc.CumpleArticulo521)
                    {
                        listaErrores.Add(Errors.ENCOMIENDA_DECLARAR_ARTICULO_521);
                    }
                }

                #endregion

                if (enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    #region "Validacion exigir certificado sobrecarga para cualquier planta != PB"

                    bool tienePlantasDiferentesPB = enc.Encomienda_Plantas.Where(x => x.id_tiposector != (int)Constantes.TipoSector.PB).Any();
                    bool tieneSobreCarga = enc.Encomienda_DatosLocal.Select(s => s.dj_certificado_sobrecarga).Any();

                    if (enc.id_tipotramite != (int)Constantes.TipoDeTramite.RedistribucionDeUso &&
                        tienePlantasDiferentesPB && !tieneSobreCarga)
                        listaErrores.Add(Errors.ENCOMIENDA_EXIGIR_SOBREGARGA);

                    #endregion

                    #region "Validacion con plantas declaradas consecutivas que superan 10 mts de altura"
                    bool Consecutiva_Supera_10 = enc.Consecutiva_Supera_10.HasValue ? enc.Consecutiva_Supera_10.Value : false;
                    if (Consecutiva_Supera_10 && !enc.Encomienda_Planos.Where(x => x.id_tipo_plano == (int)Constantes.TiposDePlanos.Contra_Incendio).Any())
                        listaErrores.Add(Errors.ENCOMIENDA_PLANTAS_CONSECUTIVAS);

                    #endregion
                }
                #region validacion transiciones estado
                //IF not Exists(
                //        SELECT 1 FROM Encomienda_TransicionEstados  est
                //        INNER JOIN aspnet_UsersInRoles usr_rol ON usr_rol.UserId = @userid
                //        INNER JOIN aspnet_Roles rol ON rol.RoleId = usr_rol.RoleId
                //        WHERE est.id_estado_actual = @enc_id_estado
                //        AND est.id_estado_siguiente = @id_estado
                //        AND est.rol = rol.RoleName
                //        )
                //BEGIN
                //    SET @msg = 'Cambio de estado invalido. Su perfil no permite realizar este cambio de estado.'
                //    RAISERROR(@msg, 16, 1)
                //    RETURN
                //END
                #endregion

                #region validacion local de venta
                if (enc.id_tipotramite != (int)Constantes.TipoDeTramite.RedistribucionDeUso &&
                    enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)

                {
                    bool tieneRubroVenta = false;
                    foreach (var rub in rubros)
                    {
                        if (rub.LocalVenta != null)
                        {
                            tieneRubroVenta = true;
                            break;
                        }
                    }

                    //Local de Ventas
                    if (tieneRubroVenta && datosLocal.local_venta == null)
                        listaErrores.Add(Errors.ENCOMIENDA_SALON_VENTA_SIN_ESP);
                }
                #endregion

                #region Validacion plano Habilitacion

                if (enc.id_subtipoexpediente != (int)Constantes.SubtipoDeExpediente.SinPlanos &&
                    enc.Encomienda_SSIT_Solicitudes.Count > 0 &&
                    !encDTO.EsECI)
                {
                    var soli = repoSsit.Single(enc.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault());
                    if (planos.Count() == 0 &&
                        (soli.id_tipotramite == (int)Constantes.TipoDeTramite.Habilitacion ||
                         (soli.id_tipotramite != (int)Constantes.TipoDeTramite.Habilitacion &&
                         soli.SSIT_Solicitudes_Origen != null && soli.SSIT_Solicitudes_Origen.id_solicitud_origen.HasValue)))
                        listaErrores.Add(Errors.ENCOMIENDA_SIN_PLANOS_HABILITACION);
                    else
                    {
                        // Si es Redistribución de Uso se busca el plano específico, sino se busca que tenga el de habilitación.
                        // --
                        if (enc.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO &&
                            planos.Count(x => x.id_tipo_plano == (int)Constantes.TiposDePlanos.PlanoRedistribuciónDeUso) <= 0)
                        {
                            listaErrores.Add(Errors.ENCOMIENDA_SIN_PLANOS_REDISTRIBUCION_USO);
                        }
                        else if (
                                enc.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION &&
                                planos.Count(x => x.id_tipo_plano == (int)Constantes.TiposDePlanos.Ampliacion) <= 0 &&
                                !encDTO.EsECI)
                        {
                            listaErrores.Add(Errors.ENCOMIENDA_SIN_PLANOS_AMPLIACION);
                        }

                    }
                }
                #endregion

                #region Validacion plancheta  
                if (enc.Encomienda_SSIT_Solicitudes != null)
                {
                    var sol = repoSsit.Single(enc.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault());

                    if (enc.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION &&
                        sol.SSIT_Solicitudes_Origen == null &&
                        enc.Encomienda_DocumentosAdjuntos.Count(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Plancheta) <= 0 &&
                        sol.SSIT_DocumentosAdjuntos.Count(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_JPG) <= 0)
                    {
                        listaErrores.Add(Errors.ENCOMIENDA_SIN_PLANCHETA);
                    }
                }
                #endregion

                #region validacion plano contra incendio
                if (enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    //Si hay un rubro con incendio verifico que exista el plano de contraincendio y los limites
                    bool tieneRubCI = false;
                    foreach (var plano in planos)
                        if (plano.id_tipo_plano == (int)Constantes.TiposDePlanos.Contra_Incendio)
                        {
                            tieneRubCI = true;
                            break;
                        }

                    bool validacionCI = true;
                    if (!tieneRubCI)
                    {
                        // Si la superficie del local es mayor a 1500m2 se debe pedir siempre
                        if (SuperficieTotal > 1500)
                            validacionCI = false;
                        else
                        {
                            var repoR = new RubrosRepository(unitOfWork);
                            var ruCI = repoR.getRubrosIncendioEncomienda(id_encomienda);
                            if (ruCI.Count() > 0)
                            {
                                foreach (var r in ruCI)
                                {
                                    if (SuperficieTotal > r.DesdeM2)
                                    {
                                        validacionCI = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (!validacionCI)
                        listaErrores.Add(Errors.ENCOMIENDA_SIN_PLANOS_CONTRA_INCENDIO);
                }
                #endregion

                #region validacion certificado pro teatro
                if (enc.id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.InspeccionPrevia && enc.Pro_teatro &&
                    enc.id_tipotramite != (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    foreach (var rub in rubros)
                    {
                        if (rub.CodigoRubro.Equals("800530"))
                        {
                            var repoTipoDocSis = new TiposDeDocumentosSistemaRepository(unitOfWork);
                            var docPT = repoTipoDocSis.GetByCodigo("CERTIFICADO_PRO_TEATRO");

                            var docs = enc.Encomienda_DocumentosAdjuntos.Where(p => p.id_tipodocsis == docPT.id_tipdocsis);
                            if (docs.Count() == 0)
                                listaErrores.Add(Errors.ENCOMIENDA_SIN_CERTIFICADO_PRO_TEATRO);

                            break;
                        }

                    }
                }
                #endregion

                #region "Validación de sobrecarga"

                //if (enc.id_tipotramite != (int)Constantes.TipoDeTramite.RedistribucionDeUso
                //    && datosLocal.sobrecarga_corresponde_dl && !datosLocal.Encomienda_Certificado_Sobrecarga.Any(x => x.Encomienda_Sobrecarga_Detalle1.Any()))
                //    listaErrores.Add(Errors.ENCOMIENDA_FALTA_SOBRECARGA);

                #endregion

                #region "Validación de Conformación del local"

                if (enc.Encomienda_ConformacionLocal.Count() > 0 &&
                    enc.id_tipotramite == (int)Constantes.TipoTramite.HABILITACION &&
                    enc.id_tipoexpediente == (int)Constantes.TipoDeExpediente.Simple &&
                    enc.id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos &&
                    enc.Encomienda_ConformacionLocal.Sum(s => s.superficie_conflocal) != SuperficieTotal)
                    listaErrores.Add(Errors.ENCOMIENDA_CONFORMACION_LOCAL_DISTINTA_SUPERFICIE);

                #endregion

                #endregion

                if (listaErrores.Count() > 0)
                    throw new Exception(Funciones.MensajeError(listaErrores));

                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EncomiendaAntenasGrillaDTO> Encomienda_TraerEncomiendasConsejo_ANT(string matricula, string Apenom, string cuit, string estados, int pIdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoAnt = new AntenaEncomiendaRepository(this.uowF.GetUnitOfWork());
                var encomiendas = repoAnt.GetAntenas(matricula, Apenom, cuit, estados, pIdEncomienda);
                var encomiendasDTO = InstanceMapperEncomiendaAntena.Map<IEnumerable<EncomiendaAntenasEntity>, IEnumerable<EncomiendaAntenasGrillaDTO>>(encomiendas);

                var direcciones = GetDireccionesEncomiendaAntenas(encomiendasDTO.Select(p => p.id_encomienda).ToList());

                foreach (var element in encomiendasDTO)
                    element.Direccion = direcciones.FirstOrDefault(p => p.id_solicitud == element.id_encomienda);

                return encomiendasDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <param name="IdTipoTramite"></param>
        /// <returns></returns>
        public EncomiendaExternaDTO GetEncomiendaExterna(int NroTramite, int IdTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repoExt = new EncomiendaExternaRepository(unitOfWork);
                var encomiendaEntity = repoExt.Get(NroTramite, IdTipoTramite);
                var encomiendaDTO = InstanceMapperEncomiendaExterna.Map<EncomiendaExt, EncomiendaExternaDTO>(encomiendaEntity);

                return encomiendaDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public EncomiendaExternaDTO GetEncomiendaExterna(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();

                repoExt = new EncomiendaExternaRepository(unitOfWork);
                repoTipoTramite = new TipoTramiteRepository(unitOfWork);

                var encomiendaEntity = repoExt.Single(IdEncomienda);
                var encomiendaDTO = InstanceMapperEncomiendaExterna.Map<EncomiendaExt, EncomiendaExternaDTO>(encomiendaEntity);

                var tramite = repoTipoTramite.Single(encomiendaEntity.TipoTramite);
                var tramiteDTO = InstanceMapperEncomiendaExterna.Map<TipoTramite, TipoTramiteDTO>(tramite);

                encomiendaDTO.Direccion = GetDireccionesEncomienda(new List<int>() { encomiendaDTO.IdEncomienda }, true).FirstOrDefault();
                if (encomiendaDTO.IdTipoTramite == (int)Constantes.TipoCertificado.Ligue)
                    encomiendaDTO.TipoTramiteDescripcion = Constantes.EncomiendaDirectorObra.LigueDeObra;
                else if (encomiendaDTO.IdTipoTramite == (int)Constantes.TipoCertificado.Desligue)
                    encomiendaDTO.TipoTramiteDescripcion = Constantes.EncomiendaDirectorObra.DesligueDeObra;

                List<EncomiendaExternaTitularesDTO> titulares = new List<EncomiendaExternaTitularesDTO>();

                foreach (var item in encomiendaDTO.EncomiendaExternaTitularesPersonasFisicas)
                {
                    EncomiendaExternaTitularesDTO titularesDTO = new EncomiendaExternaTitularesDTO();
                    titularesDTO.ApellidoNomRazon = item.Apellido + " " + item.Nombres;
                    titularesDTO.Apellido = item.Apellido;
                    titularesDTO.Calle = item.Calle;
                    titularesDTO.Codigo_Postal = item.Codigo_Postal;
                    titularesDTO.Cuit = item.Cuit;
                    titularesDTO.Depto = item.Depto;
                    titularesDTO.Email = item.Email;
                    titularesDTO.id_encomienda = item.id_encomienda;
                    titularesDTO.id_tipodoc_personal = item.id_tipodoc_personal;
                    titularesDTO.Localidad = item.Localidad;
                    titularesDTO.Nombres = item.Nombres;
                    titularesDTO.Nro_Documento = item.Nro_Documento;
                    titularesDTO.NroPuerta = item.NroPuerta;
                    titularesDTO.Piso = item.Piso;
                    titularesDTO.Torre = item.Torre;
                    titularesDTO.TipoPersona = Constantes.TipoPersonaFisica;
                    titulares.Add(titularesDTO);
                }
                foreach (var item in encomiendaDTO.EncomiendaExternaTitularesPersonasJuridicas)
                {
                    EncomiendaExternaTitularesDTO titularesDTO = new EncomiendaExternaTitularesDTO();
                    titularesDTO.ApellidoNomRazon = item.Razon_Social;
                    titularesDTO.Nombres = item.Razon_Social;
                    titularesDTO.Calle = item.Calle;
                    titularesDTO.Codigo_Postal = item.Codigo_Postal;
                    titularesDTO.Cuit = item.Cuit;
                    titularesDTO.Depto = item.Depto;
                    titularesDTO.Email = item.Email;
                    titularesDTO.id_encomienda = item.id_encomienda;
                    titularesDTO.Localidad = item.Localidad;
                    titularesDTO.NroPuerta = item.NroPuerta;
                    titularesDTO.Piso = item.Piso;
                    titularesDTO.Torre = item.Torre;
                    titularesDTO.TipoPersona = Constantes.TipoPersonaJuridica;
                    titulares.Add(titularesDTO);
                }
                encomiendaDTO.Titulares = titulares;
                return encomiendaDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIDEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaDTO> GetListaIdEncomienda(List<int> listIDEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetListaIdEncomienda(listIDEncomienda);
                var encomiendaDTO = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
                return encomiendaDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomiendaDesde"></param>
        /// <param name="id_encomiendaHasta"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaDTO> GetRangoIdEncomienda(int id_encomiendaDesde, int id_encomiendaHasta)
        {
            try
            {
                int inicio = id_encomiendaDesde;
                int fin = id_encomiendaHasta;

                if (id_encomiendaDesde >= id_encomiendaHasta)
                {
                    inicio = id_encomiendaHasta;
                    fin = id_encomiendaDesde;
                }

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRangoIdEncomienda(inicio, fin);
                var encomiendaDTO = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
                return encomiendaDTO;
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
        public EncomiendaAntenasDTO GetEncomiendaAntenas(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoAnt = new AntenaEncomiendaRepository(this.uowF.GetUnitOfWork());

                var encomiendaEntity = repoAnt.Get(IdEncomienda);
                var encomiendaDTO = InstanceMapperEncomiendaAntena.Map<ANT_Encomiendas, EncomiendaAntenasDTO>(encomiendaEntity);

                return encomiendaDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public int GetEncomiendaAntenasDocumentos(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoAntenasAdjuntos = new AntenasDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());

                var encomiendaEntity = repoAntenasAdjuntos.Get(IdEncomienda);

                if (encomiendaEntity.Any())
                    return encomiendaEntity.FirstOrDefault().id_file;

                return 0;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IList<EncomiendaExternaHistorialEstadosDTO> Traer_EncomiendaExt_HistorialEstados(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoExtEstado = new EncomiendaExternaHistorialEstadosRepository(this.uowF.GetUnitOfWork());
                var encomiendaEntity = repoExtEstado.GetHistorialEncomiendaExterna(IdEncomienda).ToList();

                var encomiendaDTO = InstanceMapperEncomiendaExterna.Map<IList<EncomiendaExternaHistorialEstadosEntity>, IList<EncomiendaExternaHistorialEstadosDTO>>(encomiendaEntity);

                return encomiendaDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IList<EncomiendaExternaHistorialEstadosDTO> GetHistorial(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoExtEstado = new EncomiendaExternaHistorialEstadosRepository(this.uowF.GetUnitOfWork());
                var encomiendaEntity = repoExtEstado.GetHistorial(IdEncomienda).ToList();

                var encomiendaDTO = InstanceMapperEncomiendaExterna.Map<IList<EncomiendaExternaHistorialEstadosEntity>, IList<EncomiendaExternaHistorialEstadosDTO>>(encomiendaEntity);

                return encomiendaDTO;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <param name="id_estado"></param>
        /// <param name="userid"></param>
        public void ActualizarEncomiendaEx_Estado(int id_encomienda, int id_estado, Guid userid, int? id_file = null)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoExt = new EncomiendaExternaRepository(unitOfWork);

                    var encomiendaEntity = repoExt.Single(id_encomienda);

                    if (encomiendaEntity.Bloqueada)
                        throw new Exception(Errors.ENCOMIENDA_TRAMITE_BLOQUEADO);

                    encomiendaEntity.LastUpdateUser = userid;
                    encomiendaEntity.LastUpdateDate = DateTime.Now;
                    encomiendaEntity.id_estado = id_estado;
                    encomiendaEntity.id_file = id_file;
                    repoExt.Update(encomiendaEntity);

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
        /// <param name="IdEncomienda"></param>
        /// <param name="IdEstado"></param>
        /// <param name="UserId"></param>
        public void ActualizarEstado(int IdEncomienda, int IdEstado, Guid UserId, EncomiendaDocumentosAdjuntosDTO encDocAdjuntosDTO = null)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();

            repo = new EncomiendaRepository(UnitOfWork);
            repoEstados = new EncomiendaEstadosRepository(UnitOfWork);
            decimal suma_superficieHabilitar = 0;

            var encomiendaEntity = repo.Single(IdEncomienda);

            if (IdEstado != (int)Constantes.Encomienda_Estados.Completa)
            {
                //142134: JADHE 55346 - SGI - Quitar validación SSP en consejos
                //if (encomiendaEntity.id_estado == (int)Constantes.Encomienda_Estados.Confirmada)
                //{
                //    if (encomiendaEntity.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO
                //        && encomiendaEntity.id_tipoexpediente == (int)Constantes.TipoDeExpediente.Simple
                //        && encomiendaEntity.id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                //        throw new Exception(Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP);
                //}

                if (encomiendaEntity.id_estado == (int)Constantes.Encomienda_Estados.Confirmada)
                {
                    var encomiendaDL = encomiendaEntity.Encomienda_DatosLocal.FirstOrDefault();

                    if (encomiendaDL == null)
                        throw new Exception(Errors.ENCOMIENDA_NO_DATOS_LOCAL);
                    if (encomiendaDL.cantidad_operarios_dl <= 0)
                        throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_OPERARIOS);

                    decimal SuperficieTotal = 0;
                    if (encomiendaDL.ampliacion_superficie.HasValue && encomiendaDL.ampliacion_superficie.Value)
                        SuperficieTotal = encomiendaDL.superficie_cubierta_amp.Value + encomiendaDL.superficie_descubierta_amp.Value;
                    else
                        SuperficieTotal = encomiendaDL.superficie_cubierta_dl.Value + encomiendaDL.superficie_descubierta_dl.Value;

                    #region ValidacionSegunRubros
                    if (encomiendaEntity.Encomienda_RubrosCN.Count > 0)
                    {
                        suma_superficieHabilitar = encomiendaEntity.Encomienda_RubrosCN.Sum(p => p.SuperficieHabilitar);
                        if (suma_superficieHabilitar < SuperficieTotal)
                            throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE);

                        var rubro = encomiendaEntity.Encomienda_RubrosCN.Where(p => p.SuperficieHabilitar > SuperficieTotal).Select(p => p).FirstOrDefault();
                        if (rubro != null)
                        {
                            superficieRubroMayorASuperficieAHabilitar = string.Format("La superficie del rubro " + rubro.NombreRubro + ", " + rubro.SuperficieHabilitar + " m2 es mayor a la superficie a habilitar.");
                            throw new Exception(superficieRubroMayorASuperficieAHabilitar);
                        }
                    }
                    else
                    {
                        suma_superficieHabilitar = encomiendaEntity.Encomienda_Rubros.Sum(p => p.SuperficieHabilitar);
                        if (suma_superficieHabilitar < SuperficieTotal)
                            throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE);

                        var rubro = encomiendaEntity.Encomienda_Rubros.Where(p => p.SuperficieHabilitar > SuperficieTotal).Select(p => p).FirstOrDefault();
                        if (rubro != null)
                        {
                            superficieRubroMayorASuperficieAHabilitar = string.Format("La superficie del rubro " + rubro.desc_rubro + ", " + rubro.SuperficieHabilitar + " m2 es mayor a la superficie a habilitar.");
                            throw new Exception(superficieRubroMayorASuperficieAHabilitar);
                        }
                    }
                    #endregion

                    var estadosEntity = repoEstados.TraerEncomiendaEstadosSiguientes(UserId, encomiendaEntity.id_estado).Any();
                    if (!estadosEntity)
                        throw new Exception(Errors.ENCOMIENDA_ESTADO_PERFIL_INVALIDO);

                }
            }

            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    encomiendaEntity = repo.Single(IdEncomienda);
                    encomiendaEntity.id_estado = IdEstado;
                    encomiendaEntity.LastUpdateUser = UserId;
                    encomiendaEntity.LastUpdateDate = DateTime.Now;

                    repo.Update(encomiendaEntity);

                    unitOfWork.Commit();
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="MotivoRechazo"></param>
        public void ActualizarDirectorObraMotivoRechazo(int IdEncomienda, string MotivoRechazo)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    repoExt = new EncomiendaExternaRepository(unitOfWork);
                    var encomiendaEntity = repoExt.Single(IdEncomienda);

                    encomiendaEntity.MotivoRechazo = MotivoRechazo;
                    encomiendaEntity.LastUpdateDate = DateTime.Now;

                    repoExt.Update(encomiendaEntity);

                    unitOfWork.Commit();
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="IdEstado"></param>
        /// <param name="UserId"></param>
        public void ActualizarEncomiendaAnt_Estado(int IdEncomienda, int IdEstado, Guid UserId)
        {
            bool result = false;
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();

                var repoParam = new ParametrosRepository(UnitOfWork);

                ws_Interface_AGC servicio = new ws_Interface_AGC();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                servicio.Url = repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                result = servicio.ActualizarEncomiendaAnt_Estado(repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User"), repoParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password"), IdEncomienda, IdEstado, UserId, ref ws_resultado_BUI);
                servicio.Dispose();
                if (ws_resultado_BUI.ErrorCode != 0)
                {
                    throw new Exception("No se ha podido Actualizar el estado de la Encomienda");
                }
            }
            catch
            {
                throw new Exception("No se ha podido Actualizar el estado de la Encomienda");
            }
        }
        public void Anular(int IdEncomienda, Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    repo = new EncomiendaRepository(unitOfWork);
                    var enc = repo.Single(IdEncomienda);

                    #region validacion transiciones estado
                    //IF not Exists(
                    //        SELECT 1 FROM Encomienda_TransicionEstados  est
                    //        INNER JOIN aspnet_UsersInRoles usr_rol ON usr_rol.UserId = @userid
                    //        INNER JOIN aspnet_Roles rol ON rol.RoleId = usr_rol.RoleId
                    //        WHERE est.id_estado_actual = @enc_id_estado
                    //        AND est.id_estado_siguiente = @id_estado
                    //        AND est.rol = rol.RoleName
                    //        )
                    //BEGIN
                    //    SET @msg = 'Cambio de estado invalido. Su perfil no permite realizar este cambio de estado.'
                    //    RAISERROR(@msg, 16, 1)
                    //    RETURN
                    //END
                    #endregion


                    enc.LastUpdateUser = userid;
                    enc.LastUpdateDate = DateTime.Now;
                    enc.id_estado = (int)Constantes.Encomienda_Estados.Anulada;
                    repo.Update(enc);

                    unitOfWork.Commit();
                }
                catch { }
            }
        }

        public EncomiendaDTO GetUltimaEncomiendaAprobada(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetUltimaEncomiendaAprobada(IdSolicitud).FirstOrDefault();
            var elementDto = mapperBase.Map<Encomienda, EncomiendaDTO>(elements);

            return elementDto;
        }

        public EncomiendaDTO GetUltimaEncomiendaAprobadaTR(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetUltimaEncomiendaAprobadaTR(IdSolicitud).FirstOrDefault();
            var elementDto = mapperBase.Map<Encomienda, EncomiendaDTO>(elements);

            return elementDto;
        }

        public bool PoseeHabilitacionConAnexoTecnicoAnterior(int IdEncomienda)
        {
            bool ret = false;

            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            TransferenciasSolicitudesRepository repoTransf = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
            //var sol = (dynamic)null; 
            var sol = repo.GetByFKIdEncomienda(IdEncomienda);
            if (sol == null)
            {
                var tr = repoTransf.GetByFKIdEncomienda(IdEncomienda);
                if (tr.idSolicitudRef != null)
                    ret = true;
            }
            else
            {
                // Se evalua que el anterior sea cualquier trámite distinto a transferencia ya que es la única que no tiene anexo técnico
                ret = sol.SSIT_Solicitudes_Origen != null && sol.SSIT_Solicitudes_Origen.id_solicitud_origen.HasValue;
            }
            return ret;
        }

        //public bool PoseeCertificadoDeSobreCarga(int IdEncomiendaDatosLocal)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    var repo = new EncomiendaCertificadoSobrecargaRepository(this.uowF.GetUnitOfWork());
        //    var cert = repo.GetByFKIdEncomiendaDatosLocal(IdEncomiendaDatosLocal);

        //    return cert != null;
        //}

        public void DescargarDispo(string nroDispo, int idEncomienda, Guid userid)
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

                    EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    EncomiendaDocumentosAdjuntosDTO encDocDTO;

                    int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION;
                    var DocAdj = encDocBL.GetByFKIdEncomiendaTipoSis(idEncomienda, id_tipodocsis).FirstOrDefault();

                    if (DocAdj == null)
                    {
                        int id_file = esf.addFile("Disposicion.pdf", docPdf);

                        encDocDTO = new EncomiendaDocumentosAdjuntosDTO();
                        encDocDTO.id_encomienda = idEncomienda;
                        encDocDTO.id_tipodocsis = id_tipodocsis;
                        encDocDTO.id_tdocreq = 0;
                        encDocDTO.generadoxSistema = true;
                        encDocDTO.CreateDate = DateTime.Now;
                        encDocDTO.CreateUser = userid;
                        encDocDTO.nombre_archivo = "Disposición";
                        encDocDTO.id_file = id_file;
                        encDocBL.Insert(encDocDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceEE.Dispose();
                throw new Exception("No se ha podido Descargar la Disposición");
            }
        }

        public void ActualizaTipoSubtipoExpToSSIT(int id_encomienda)
        {

            uowF = new TransactionScopeUnitOfWorkFactory();
            int tipoTramite = 0;
            var repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var repoTransf = new TransferenciasSolicitudesRepository(this.uowF.GetUnitOfWork());
            var entity = (dynamic)null;
            var entityDto = (dynamic)null;
            var repoenc = new EncomiendaRepository(this.uowF.GetUnitOfWork());
            Encomienda enc = repoenc.Single(id_encomienda);

            entity = repo.GetByFKIdEncomienda(id_encomienda);
            if (entity != null)
            {
                entityDto = mapperBase.Map<SSIT_Solicitudes, SSITSolicitudesDTO>(entity);
                tipoTramite = 1;
            }
            else
            {
                entity = repoTransf.GetByFKIdEncomienda(id_encomienda);
                entityDto = mapperBase.Map<Transf_Solicitudes, TransferenciasSolicitudesDTO>(entity);
                tipoTramite = 2;
            }
            try
            {
                if (enc != null)
                {
                    entityDto.IdTipoExpediente = enc.id_tipoexpediente;
                    entityDto.IdSubTipoExpediente = enc.id_subtipoexpediente;

                    if (tipoTramite == 1)
                    {
                        SSITSolicitudesBL blssit = new SSITSolicitudesBL();
                        blssit.Update(entityDto);
                    }
                    else
                    {
                        TransferenciasSolicitudesBL bltr = new TransferenciasSolicitudesBL();
                        bltr.Update(entityDto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProfesionalDTO GetProfesionalByTransf(int idSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);
            var elements = repo.GetByFKIdSolicitudTransf(idSolicitud);
            var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
            var profesional = elementsDto.LastOrDefault()?.ProfesionalDTO;
            return profesional;
        }

        public ProfesionalDTO GetProfesionalBySolicitud(int idSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var unitOfWork = this.uowF.GetUnitOfWork();
            repo = new EncomiendaRepository(unitOfWork);
            var elements = repo.GetByFKIdSolicitud(idSolicitud);
            var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda>, IEnumerable<EncomiendaDTO>>(elements);
            var profesional = elementsDto.LastOrDefault()?.ProfesionalDTO;
            return profesional;
        }
        /// <summary>
        /// Inserta en el CAA en la base de files y guarda el id_file en DGHP_Solicitudes
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="userid"></param>
        /// <param name="bytes"></param>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        public void InsertarCAA_DocAdjuntos_Hab(int id_solicitud, Guid userid, byte[] bytes, string filename, string extension)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    var repoDoc = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    string arch = filename + extension;
                    int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_CAA;

                    var DocAdj = repoDoc.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

                    if (DocAdj == null)
                    {
                        int id_file = esf.addFile(arch, bytes);
                        DocAdj = new SSIT_DocumentosAdjuntos();
                        DocAdj.id_solicitud = id_solicitud;
                        DocAdj.id_tdocreq = 16;
                        DocAdj.tdocreq_detalle = "";
                        DocAdj.generadoxSistema = true;
                        DocAdj.CreateDate = DateTime.Now;
                        DocAdj.CreateUser = userid;
                        DocAdj.nombre_archivo = arch;
                        DocAdj.id_file = id_file;
                        DocAdj.id_tipodocsis = id_tipodocsis;
                        repoDoc.Insert(DocAdj);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                throw ex;
            }
        }

    }
}

