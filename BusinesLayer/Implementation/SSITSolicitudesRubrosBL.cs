using AutoMapper;
using BaseRepository;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesRubrosBL
    {
        private SSITSolicitudesRubrosCNRepository repo = null;
        private SSITSolicitudesRepository repoSol = null;
        private RubrosCNRepository repoRubroCN = null;
        private EncomiendaRubrosCNSubrubrosRepository repoEncRubSubrub = null;


        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public SSITSolicitudesRubrosBL()
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
                        .ForMember(dest => dest.SSITSolicitudesRubrosCNDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_RubrosCN))
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
                    .ForMember(dest => dest.SSIT_Solicitudes_RubrosCN, source => source.Ignore())
                    // .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.Ignore())
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
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    ;

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

                #region "RubrosCN"
                cfg.CreateMap<RubrosCNDTO, RubrosCN>().ReverseMap()
                    .ForMember(dest => dest.IdRubro, source => source.MapFrom(p => p.IdRubro))
                    .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.Nombre))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.VigenciaDesde, source => source.MapFrom(p => p.VigenciaDesde_rubro))
                    .ForMember(dest => dest.VigenciaHasta, source => source.MapFrom(p => p.VigenciaHasta_rubro))
                    .ForMember(dest => dest.RubrosCN_SubrubrosDTO, source => source.MapFrom(p => p.RubrosCN_Subrubros));

                cfg.CreateMap<RubrosCN, RubrosCNDTO>().ReverseMap()
                    .ForMember(dest => dest.IdRubro, source => source.MapFrom(p => p.IdRubro))
                    .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.Nombre))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.VigenciaDesde_rubro, source => source.MapFrom(p => p.VigenciaDesde))
                    .ForMember(dest => dest.VigenciaHasta_rubro, source => source.MapFrom(p => p.VigenciaHasta))
                    .ForMember(dest => dest.RubrosCN_Subrubros, source => source.MapFrom(p => p.RubrosCN_SubrubrosDTO));

                cfg.CreateMap<RubrosCNDTO, RubrosCNEntity>().ReverseMap();

                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();

                #endregion

                cfg.CreateMap<SSIT_Solicitudes_RubrosCN, SSITSolicitudesRubrosCNDTO>()
                    .ForMember(dest=> dest.DescripcionRubro , source => source.MapFrom(m=> m.NombreRubro))
                ;

                cfg.CreateMap<SSITSolicitudesRubrosCNDTO, SSIT_Solicitudes_RubrosCN>()
                .ForMember(dest => dest.NombreRubro, source => source.MapFrom(m => m.DescripcionRubro))
                    .ForMember(dest => dest.SSIT_Solicitudes, source => source.Ignore())
                    .ForMember(dest => dest.RubrosCN, source => source.Ignore())
                    .ForMember(dest => dest.TipoActividad, source => source.Ignore())
                    .ForMember(dest => dest.aspnet_Users, source => source.Ignore())
                ;

            });

            mapperBase = config.CreateMapper();

        }

        public IEnumerable<SSITSolicitudesRubrosCNDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesRubrosCNRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_RubrosCN>, IEnumerable<SSITSolicitudesRubrosCNDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<SSITSolicitudesRubrosCNDTO> GetRubros(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesRubrosCNRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosCN(IdSolicitud);
                var elementsDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes_RubrosCN>, IEnumerable<SSITSolicitudesRubrosCNDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RubrosCNDTO> GetRubros(int IdSolicitud, decimal Superficie, string Rubro, List<ZonasDistritosDTO> listZD)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                var UnitOfWork = this.uowF.GetUnitOfWork();
                repo = new SSITSolicitudesRubrosCNRepository(UnitOfWork);
                repoSol = new SSITSolicitudesRepository(UnitOfWork);

                var solicitudEntity = repoSol.Single(IdSolicitud);
                bool TieneNormativa = solicitudEntity.SSIT_Solicitudes_Normativas != null;

                var rubrosEntity = repo.GetRubros(listZD.Where(x => x.IdTipo == 1).Select(y => y.Codigo).ToList(), listZD.Where(x => x.IdTipo == 2).Select(y => y.Codigo).ToList()
                                                        , Superficie, Rubro, TieneNormativa);

                return mapperBase.Map<IEnumerable<RubrosCNEntity>, IEnumerable<RubrosCNDTO>>(rubrosEntity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(SSITSolicitudesRubrosCNDTO objectDto, bool Validar, Guid userLogued)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (var unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSol = new SSITSolicitudesRepository(unitOfWork);
                    repo = new SSITSolicitudesRubrosCNRepository(unitOfWork);

                    var solEntity = repoSol.Single(objectDto.IdSolicitud);

                    if (Validar)
                    {
                        if (solEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && solEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                            throw new Exception(Errors.SSIT_SOLICITUDES_CAMBIOS);

                        var solDatosLocalEntity = solEntity.SSIT_Solicitudes_DatosLocal;

                        if (solDatosLocalEntity == null)
                            throw new Exception(Errors.SSIT_SOLICITUD_NO_DATOS_LOCAL);

                        var solRubrosEntity = solEntity.SSIT_Solicitudes_RubrosCN;

                        if (solRubrosEntity.Any(p => p.CodigoRubro == objectDto.CodigoRubro))
                            throw new Exception(Errors.SSIT_SOLICITUD_RUBRO_EXISTENTE);
                    }


                    objectDto.CreateDate = DateTime.Now;
                    objectDto.CreateUser = userLogued;

                    if (objectDto.CodigoRubro != Constantes.RubroNoContemplado)
                    {

                        repoRubroCN = new RubrosCNRepository(unitOfWork);
                        var rubroEntity = repoRubroCN.Get(objectDto.CodigoRubro).FirstOrDefault();

                        if (rubroEntity == null)
                            throw new Exception(Errors.SSIT_SOLICITUD_RUBRO);

                        RubrosImpactoAmbientalCNBL rubrosImpactoAmbientalCN = new RubrosImpactoAmbientalCNBL();

                        var rubrosImpactoAmbientalCNDTO = rubrosImpactoAmbientalCN.GetImpactoAmbiental(objectDto.SuperficieHabilitar, rubroEntity.IdRubro);
                        if (rubrosImpactoAmbientalCNDTO == null)
                            objectDto.idImpactoAmbiental = (int)Constantes.ImpactoAmbiental.SujetoACategorización;
                        else
                        {
                            objectDto.idImpactoAmbiental = rubrosImpactoAmbientalCNDTO.id_tipocertificado;
                        }
                        objectDto.IdRubro = rubroEntity.IdRubro;
                        objectDto.DescripcionRubro = rubroEntity.Nombre;
                        objectDto.IdTipoActividad = rubroEntity.IdTipoActividad;
                        objectDto.IdTipoExpediente = rubroEntity.IdTipoExpediente;
                    }

                    var elementDto = mapperBase.Map<SSITSolicitudesRubrosCNDTO, SSIT_Solicitudes_RubrosCN>(objectDto);

                    repo.Insert(elementDto);

                    objectDto.IdSolicitudRubro = elementDto.IdSolicitudRubro;

                    repoSol.Update(solEntity);

                    unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(SSITSolicitudesRubrosCNDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (var unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRubrosCNRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesRubrosCNDTO, SSIT_Solicitudes_RubrosCN>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(SSITSolicitudesRubrosCNDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (var unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesRubrosCNRepository(unitOfWork);
                    repoSol = new SSITSolicitudesRepository(unitOfWork);
                    repoEncRubSubrub = new EncomiendaRubrosCNSubrubrosRepository(unitOfWork);

                    var solicitudEntity = repoSol.Single(objectDto.IdSolicitud);
                    if (solicitudEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && solicitudEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.SSIT_SOLICITUDES_CAMBIOS);

                    repo = new SSITSolicitudesRubrosCNRepository(unitOfWork);
                    var elemnt = repo.Single(objectDto.IdSolicitudRubro);

                    //repoEncRubSubrub.RemoveRange(elemnt.Encomienda_RubrosCN_Subrubros);

                    repo.Delete(elemnt);

                    solicitudEntity.id_tipoexpediente = 0;
                    solicitudEntity.id_subtipoexpediente = 0;

                    repoSol.Update(solicitudEntity);

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
