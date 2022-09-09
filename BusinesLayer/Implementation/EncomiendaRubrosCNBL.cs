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
using BaseRepository.Engine;
using System.Web.Security;
using System.Linq.Expressions;

namespace BusinesLayer.Implementation
{
    public class EncomiendaRubrosCNBL : IEncomiendaRubrosCNBL<EncomiendaRubrosCNDTO>
    {
        private EncomiendaRubrosCNRepository repo = null;
        private EncomiendaRepository repoEncomienda = null;
        private RubrosCNRepository repoRubroCN = null;
        private EncomiendaRubrosCNSubrubrosRepository repoEncRubSubrub = null;

        private SSITSolicitudesRepository repoSSIT = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBaseRubro;
        IMapper mapperEncomiendaRubro;

        public EncomiendaRubrosCNBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Encomienda_RubrosCN, EncomiendaRubrosCNDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.NombreRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.encomiendaRubrosCNSubrubrosDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN_Subrubros))
                    .ForMember(dest => dest.RubrosDTO, source => source.MapFrom(p => p.RubrosCN));


                cfg.CreateMap<EncomiendaRubrosCNDTO, Encomienda_RubrosCN>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.NombreRubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.Encomienda_RubrosCN_Subrubros, source => source.MapFrom(p => p.encomiendaRubrosCNSubrubrosDTO))
                    .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosDTO));


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

                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();
                #endregion

                #region Encomienda_RubrosCN_Subrubros
                cfg.CreateMap<EncomiendaRubrosCNSubrubrosDTO, Encomienda_RubrosCN_Subrubros>().ReverseMap()
                .ForMember(dest => dest.rubrosCNSubRubrosDTO, source => source.MapFrom(p => p.RubrosCN_Subrubros));
                // cfg.CreateMap<Encomienda_RubrosCN_Subrubros, EncomiendaRubrosCNSubrubrosDTO>().ReverseMap();
                #endregion

                #region subRubros
                cfg.CreateMap<RubrosCN_Subrubros, RubrosCNSubRubrosDTO>().ReverseMap();
                #endregion
                #region rubroscn at anterior
                cfg.CreateMap<Encomienda_RubrosCN_AT_Anterior, EncomiendaRubrosCNDTO>()
                   .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                   .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                   .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                   .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                   .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.NombreRubro))
                   .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                   .ForMember(dest => dest.RubrosDTO, source => source.MapFrom(p => p.RubrosCN));

                cfg.CreateMap<EncomiendaRubrosCNDTO, Encomienda_RubrosCN_AT_Anterior>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.NombreRubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosDTO));
                #endregion
            });
            mapperBase = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosCNDTO, RubrosCNEntity>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();
            });

            mapperBaseRubro = config1.CreateMapper();

            var config2 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaRubrosCNDTO, EncomiendaRubrosCNEntity>().ReverseMap();

            });

            mapperEncomiendaRubro = config2.CreateMapper();

        }

        public IEnumerable<EncomiendaRubrosCNDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN>, IEnumerable<EncomiendaRubrosCNDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EncomiendaRubrosCNDTO Single(int IdEncomiendaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaRubro);
                var entityDto = mapperBase.Map<Encomienda_RubrosCN, EncomiendaRubrosCNDTO>(entity);

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
        public IEnumerable<EncomiendaRubrosCNDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN>, IEnumerable<EncomiendaRubrosCNDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de actualizacion e insert

        public bool Insert(EncomiendaRubrosCNDTO objectDto, Guid userLogued)
        {
            return Insert(objectDto, true, userLogued);
        }
        /// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
        public bool Insert(EncomiendaRubrosCNDTO objectDto, bool Validar, Guid userLogued)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    repo = new EncomiendaRubrosCNRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);

                    if (Validar)
                    {
                        if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                            throw new ValidationException(Errors.ENCOMIENDA_CAMBIOS);

                        var encomiendaDatosLocalEntity = encomiendaEntity.Encomienda_DatosLocal.FirstOrDefault();

                        if (encomiendaDatosLocalEntity == null)
                            throw new ValidationException(Errors.ENCOMIENDA_NO_DATOS_LOCAL);

                        bool esAmpliacionSuperficie = (encomiendaDatosLocalEntity.ampliacion_superficie.HasValue ? encomiendaDatosLocalEntity.ampliacion_superficie.Value : false);

                        if (esAmpliacionSuperficie)
                        {
                            if (objectDto.SuperficieHabilitar > encomiendaDatosLocalEntity.superficie_cubierta_amp + encomiendaDatosLocalEntity.superficie_descubierta_amp)
                                throw new ValidationException(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO_AMP);
                        }
                        else if (objectDto.SuperficieHabilitar > encomiendaDatosLocalEntity.superficie_cubierta_dl + encomiendaDatosLocalEntity.superficie_descubierta_dl)
                            throw new ValidationException(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO);

                        var encomiendaRubrosEntity = encomiendaEntity.Encomienda_RubrosCN;

                        if (encomiendaRubrosEntity.Any(p => p.CodigoRubro == objectDto.CodigoRubro))
                            throw new ValidationException(Errors.ENCOMIENDA_RUBRO_EXISTENTE);
                    }


                    objectDto.CreateDate = DateTime.Now;
                    objectDto.CreateUser = userLogued;

                    if (objectDto.CodigoRubro != Constantes.RubroNoContemplado)
                    {
                        repoRubroCN = new RubrosCNRepository(unitOfWork);
                        var rubroEntity = repoRubroCN.Get(objectDto.CodigoRubro).FirstOrDefault();

                        if (rubroEntity == null)
                            throw new Exception(Errors.ENCOMIENDA_RUBRO);

                        //if (rubroEntity.id_tipodocreq == (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaSinPlano && objectDto.SuperficieHabilitar > 500)
                        //objectDto.IdTipoDocumentoRequerido = (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaConPlano;

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

                    var elementDto = mapperBase.Map<EncomiendaRubrosCNDTO, Encomienda_RubrosCN>(objectDto);

                    repo.Insert(elementDto);

                    objectDto.IdEncomiendaRubro = elementDto.id_encomiendarubro;

                    //ActualizarSubTipoExpediente(encomiendaEntity, unitOfWork);

                    repoEncomienda.Update(encomiendaEntity);

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
        public void Update(EncomiendaRubrosCNDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosCNRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaRubrosCNDTO, Encomienda_RubrosCN>(objectDTO);
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
        public void Delete(EncomiendaRubrosCNDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosCNRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    repoEncRubSubrub = new EncomiendaRubrosCNSubrubrosRepository(unitOfWork);
                    
                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                    if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    repo = new EncomiendaRubrosCNRepository(unitOfWork);
                    var elemnt = repo.Single(objectDto.IdEncomiendaRubro);
                    
                    repoEncRubSubrub.RemoveRange(elemnt.Encomienda_RubrosCN_Subrubros);
                    
                    repo.Delete(elemnt);

                    encomiendaEntity.id_tipoexpediente = 0;
                    encomiendaEntity.id_subtipoexpediente = 0;

                    repoEncomienda.Update(encomiendaEntity);

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
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaRubrosCNDTO> GetRubros(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosCN(IdEncomienda);
                var elementsDTO = mapperEncomiendaRubro.Map<IEnumerable<EncomiendaRubrosCNEntity>, IEnumerable<EncomiendaRubrosCNDTO>>(elements);

                return elementsDTO;
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
        /// <param name="Superficie"></param>
        /// <param name="CodigoRubro"></param>
        /// <returns></returns>
        public IEnumerable<RubrosCNDTO> GetRubros(int IdEncomienda, decimal Superficie, string Rubro, List<ZonasDistritosDTO> listZD)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();
                var repoRubro = new RubrosCNRepository(UnitOfWork);
                repoEncomienda = new EncomiendaRepository(UnitOfWork);

                var rubrosEntity = repoRubro.GetRubros(listZD.Where(x => x.IdTipo == 1).Select(y => y.Codigo).ToList(), listZD.Where(x => x.IdTipo == 2).Select(y => y.Codigo).ToList()
                                                        , Superficie, Rubro, IdEncomienda);

                return mapperBaseRubro.Map<IEnumerable<RubrosCNEntity>, IEnumerable<RubrosCNDTO>>(rubrosEntity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSubTipoExpediente(int idencomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EngineBL engBL = new EngineBL();
                    EngTareasRepository repoEngTareas = new EngTareasRepository(unitOfWork);
                    ENGGruposCircuitosTipoTramiteRepository gcttRepo = new ENGGruposCircuitosTipoTramiteRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(idencomienda);

                    int id_tipoexpediente = 0;
                    int id_subtipoexpediente = 0;

                    var encomiendaDatosLocalEntity = encomiendaEntity.Encomienda_DatosLocal.FirstOrDefault();

                    if (encomiendaDatosLocalEntity == null)
                        throw new Exception(Errors.ENCOMIENDA_NO_DATOS_LOCAL);

                    int idCir = engBL.GetIdCircuitoByEncomienda(idencomienda);
                    var circuito = repoEngTareas.GetCircuito(idCir);
                    var grupoTipoT = gcttRepo.GetByFKIdGrupo(circuito.nombre_grupo);

                    if (grupoTipoT != null)
                    {
                        id_tipoexpediente = grupoTipoT.id_tipo_expediente;
                        id_subtipoexpediente = grupoTipoT.id_sub_tipo_expediente;
                        encomiendaEntity.id_tipoexpediente = grupoTipoT.id_tipo_expediente;
                        encomiendaEntity.id_subtipoexpediente = grupoTipoT.id_sub_tipo_expediente;
                    }
                    else if (encomiendaEntity.Encomienda_Transf_Solicitudes != null)
                    {
                        encomiendaEntity.id_tipoexpediente = 0;
                        encomiendaEntity.id_subtipoexpediente = 0;
                    }
                    else
                    {
                        repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                        var ssits = repoSSIT.GetByFKIdEncomienda(encomiendaEntity.id_encomienda);

                        ssits.id_subtipoexpediente = id_subtipoexpediente;
                        ssits.id_tipoexpediente = id_tipoexpediente;
                        repoSSIT.Update(ssits);
                    }
                    repoEncomienda.Update(encomiendaEntity);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertSubRub(EncomiendaRubrosCNSubrubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaRubrosCNSubrubrosRepository repo = new EncomiendaRubrosCNSubrubrosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaRubrosCNSubrubrosDTO, Encomienda_RubrosCN_Subrubros>(objectDto);
                    repo.Insert(elementDto);
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertATAnterior(EncomiendaRubrosCNDTO objectDto)
        {

            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repoEncomienda = new EncomiendaRepository(unitOfWork);
                EncomiendaRubrosCNATAnteriorRepository repo = new BaseRepository.EncomiendaRubrosCNATAnteriorRepository(unitOfWork);
                repoRubroCN = new RubrosCNRepository(unitOfWork);

                var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                var encomiendaRubrosCNEntity = encomiendaEntity.Encomienda_RubrosCN_AT_Anterior;

                if (encomiendaRubrosCNEntity.Any(p => p.CodigoRubro == objectDto.CodigoRubro))
                    throw new Exception(Errors.ENCOMIENDA_RUBRO_EXISTENTE);

                var rubroEntity = (dynamic)null;
                rubroEntity = repoRubroCN.Get(objectDto.CodigoRubro).FirstOrDefault();

                if (rubroEntity == null)
                    throw new Exception(Errors.ENCOMIENDA_RUBRO);

                //if (rubroEntity.id_tipodocreq == (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaSinPlano && objectDto.SuperficieHabilitar > 500)
                //objectDto.IdTipoDocumentoRequerido = (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaConPlano;
                RubrosImpactoAmbientalCNBL rubrosImpactoAmbiental = new RubrosImpactoAmbientalCNBL();

                var rubrosImpactoAmbientalDTO = rubrosImpactoAmbiental.GetImpactoAmbiental(objectDto.SuperficieHabilitar, rubroEntity.IdRubro);

                objectDto.CreateDate = DateTime.Now;
                objectDto.DescripcionRubro = rubroEntity.Nombre;
                //objectDto.EsAnterior = rubroEntity.;
                if (rubrosImpactoAmbientalDTO != null)
                {
                    objectDto.idImpactoAmbiental = rubrosImpactoAmbientalDTO.id_tipocertificado;
                }
                else
                {
                    //Si no tiene categorización de impacto, por defecto es "Sujeto a Categorización", agregar la condición gral.
                    objectDto.idImpactoAmbiental = (int)Constantes.ImpactoAmbiental.SujetoACategorización;
                }
                objectDto.IdTipoActividad = rubroEntity.IdTipoActividad;
                objectDto.IdRubro = rubroEntity.IdRubro;

                var elementDto = mapperBase.Map<EncomiendaRubrosCNDTO, Encomienda_RubrosCN_AT_Anterior>(objectDto);

                repo.Insert(elementDto);

                objectDto.IdEncomiendaRubro = elementDto.id_encomiendarubro;

                unitOfWork.Commit();
                return true;
            }

        }

        internal bool TieneSubRubros(int idEncomiendaRubro, int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
                int elements = repo.GetRubrosSubrubrosCN(idEncomiendaRubro, id_encomienda);

                return elements > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<EncomiendaRubrosCNDTO> GetRubrosCNATAnterior(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosCNATAnterior(IdEncomienda);
                var elementsDTO = mapperEncomiendaRubro.Map<IEnumerable<EncomiendaRubrosCNEntity>, IEnumerable<EncomiendaRubrosCNDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

