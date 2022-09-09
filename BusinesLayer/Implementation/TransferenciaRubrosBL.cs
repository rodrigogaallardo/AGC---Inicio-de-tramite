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

namespace BusinesLayer.Implementation
{
    public class TransferenciasRubrosBL : ITransferenciasRubrosBL<TransferenciasRubrosDTO>
    {
        private TransferenciasRubrosRepository repo = null;
        private RubrosRepository repoRubro = null;
        private TransferenciasConformacionLocalRepository repoConformacionLocal = null;
        private SSITSolicitudesRepository repoSoli = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper maperBaseEntity;
        IMapper mapperBaseRubro;

        public TransferenciasRubrosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasRubrosDTO, Transf_Rubros>().ReverseMap()
                    .ForMember(dest => dest.IdTransferenciaRubro, source => source.MapFrom(p => p.id_transfrubro))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.CodidoRubro, source => source.MapFrom(p => p.cod_rubro))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                    .ForMember(dest => dest.IdTipoDocumentoReq, source => source.MapFrom(p => p.id_tipodocreq))
                    .ForMember(dest => dest.SuperficieHabilitar, source => source.MapFrom(p => p.SuperficieHabilitar))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental))
                    .ForMember(dest => dest.TipoActividad, source => source.MapFrom(p => p.TipoActividad))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbiental))
                    .ForMember(dest => dest.TipoDocumentacionRequerida, source => source.MapFrom(p => p.Tipo_Documentacion_Req)); 

                cfg.CreateMap<Transf_Rubros, TransferenciasRubrosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_transfrubro, source => source.MapFrom(p => p.IdTransferenciaRubro))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
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
            });
            mapperBase = config.CreateMapper();

            var configEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasRubrosDTO, TransferenciaRubrosEntity>().ReverseMap();
            });

            maperBaseEntity = configEntity.CreateMapper();


            var configBaseRubro = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosDTO, RubrosEntity>().ReverseMap();
            });

            mapperBaseRubro = configBaseRubro.CreateMapper(); 
        }

        public IEnumerable<TransferenciasRubrosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasRubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Rubros>, IEnumerable<TransferenciasRubrosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TransferenciasRubrosDTO Single(int IdTransferenciaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasRubrosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTransferenciaRubro);
                var entityDto = mapperBase.Map<Transf_Rubros, TransferenciasRubrosDTO>(entity);

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
        public IEnumerable<TransferenciasRubrosDTO> GetByFKIdTransferencia(int IdTransferencia)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasRubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdTransferencia);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Rubros>, IEnumerable<TransferenciasRubrosDTO>>(elements);
            return elementsDto;
        }     
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(TransferenciasRubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                   
                    TransferenciasDatosLocalBL trDatosLocalBL = new TransferenciasDatosLocalBL();
                    var trDatosLocalDTO = trDatosLocalBL.Single(objectDto.IdSolicitud);

                    
                    repo = new TransferenciasRubrosRepository(unitOfWork);


                    RubrosBL rubrosBL = new RubrosBL();
                    var rubroDTO = rubrosBL.Get(objectDto.CodidoRubro).FirstOrDefault();
                    

                    if (rubroDTO.IdTipoDocumentorRequerido == (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaSinPlano && objectDto.SuperficieHabilitar > 500)
                    {
                        objectDto.IdTipoDocumentoReq = (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaConPlano;
                    }
                    RubrosImpactoAmbientalBL rubrosImpactoAmbiental = new RubrosImpactoAmbientalBL();

                    var rubrosImpactoAmbientalDTO = rubrosImpactoAmbiental.GetImpactoAmbiental(objectDto.SuperficieHabilitar, rubroDTO.IdRubro);
                    

                    objectDto.CreateDate = DateTime.Now;
                    objectDto.DescripcionRubro = rubroDTO.Nombre;
                    objectDto.EsAnterior = rubroDTO.EsAnterior;
                    objectDto.IdImpactoAmbiental = rubrosImpactoAmbientalDTO.IdImpactoAmbiental;
                    objectDto.IdTipoActividad = rubroDTO.IdTipoActividad;
                    objectDto.IdTipoDocumentoReq = rubroDTO.IdTipoDocumentorRequerido;

                    repo = new TransferenciasRubrosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasRubrosDTO, Transf_Rubros>(objectDto);
                    repo.Insert(elementDto);
                    objectDto.IdTransferenciaRubro = elementDto.id_transfrubro;
                    ////unitOfWork.SaveChangeNoUsar();

                    //ActualizarSubTipoExpediente(TransferenciasRubrosDTO, objectDto, consultaPadronDatosLocalDTO, consultaPadronDTO, consultaPadronSolicitudesBL, unitOfWork);

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
        public void Update(ConsultaPadronRubrosDTO objectDTO)
        {
           /* try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronRubrosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<ConsultaPadronRubrosDTO, CPadron_Rubros>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }*/
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(ConsultaPadronRubrosDTO objectDto)
        {
            /*try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                    var consultaPadronDTO = consultaPadronSolicitudesBL.Single(objectDto.IdConsultaPadron);

                    if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);


                    repo = new ConsultaPadronRubrosRepository(unitOfWork);

                    var elementDto = mapperBase.Map<ConsultaPadronRubrosDTO, CPadron_Rubros>(objectDto);
                    var insertOk = repo.Delete(elementDto);

                    ConsultaPadronDatosLocalBL consultaPadronDatosLocalBL = new ConsultaPadronDatosLocalBL();
                    var consultaPadronDatosLocalDTO = consultaPadronDatosLocalBL.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron).FirstOrDefault();
                    var consultaPadronRubrosDTO = repo.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron);

                    ActualizarSubTipoExpediente(consultaPadronRubrosDTO, objectDto, consultaPadronDatosLocalDTO, consultaPadronDTO, consultaPadronSolicitudesBL, unitOfWork);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }*/
        }
        public void DeleteByFKIdConsultaPadron(int IdConsultaPadron)
        {
           /* try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronRubrosRepository(unitOfWork);
                    var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
                    foreach (var element in elements)
                        repo.Delete(element);

                    unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }*/
        }

        
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        /*public IEnumerable<ConsultaPadronRubrosDTO> GetRubros(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronRubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetRubros(IdSolicitud);
                var elementsDto = maperBaseEntity.Map<IEnumerable<ConsultaPadronRubrosEntity>, IEnumerable<ConsultaPadronRubrosDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="Superficie"></param>
        /// <param name="CodigoRubro"></param>
        /// <returns></returns>
        /*public IEnumerable<RubrosDTO> GetRubrosCpadron(int IdConsultaPadron, decimal Superficie, string CodigoRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repoRubro = new RubrosRepository(unitOfWork);
                repoConsulta = new ConsultaPadronSolicitudesRepository(unitOfWork);
                var consulta = repoConsulta.Single(IdConsultaPadron);
                var TieneNormativa = consulta.CPadron_Normativas.Any();
                
                UbicacionesRepository repositoryUbicaciones = new UbicacionesRepository(unitOfWork);
                string CodZona = repositoryUbicaciones.GetCodigoZonaConsultaPadron(IdConsultaPadron);

                var rubrosEntity = repoRubro.GetRubrosHistoricos(CodZona, Superficie, CodigoRubro, TieneNormativa, consulta.id_tipotramite);
                return mapperBaseRubro.Map<IEnumerable<RubrosEntity>, IEnumerable<RubrosDTO>>(rubrosEntity);
     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        //// <summary>
        /// 
        /// </summary>
        /// <param name="objectDto"></param>
        /// <returns></returns>
        /*public bool InsertRubroUsoNoContemplado(ConsultaPadronRubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                    var consultaPadronDTO = consultaPadronSolicitudesBL.Single(objectDto.IdConsultaPadron);

                    if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

                    if (objectDto.IdTipoDocumentoReq == (int)Constantes.TipoDocumentacionReq.DJ && objectDto.SuperficieHabilitar > 500)
                        objectDto.IdTipoDocumentoReq = (int)Constantes.TipoDocumentacionReq.PP;


                    repo = new ConsultaPadronRubrosRepository(unitOfWork);

                    objectDto.CodidoRubro = "888888";

                    objectDto.CreateDate = DateTime.Now;
                    objectDto.IdImpactoAmbiental = 3;//buscar enumeracion-- Sujeto a Categorización
                    var elementDto = mapperBase.Map<ConsultaPadronRubrosDTO, CPadron_Rubros>(objectDto);

                    repo.Insert(elementDto);
                    objectDto.IdConsultaPadronRubro = elementDto.id_cpadronrubro;
                    ActualizarSubTipoExpediente(null, objectDto, null, consultaPadronDTO, consultaPadronSolicitudesBL, unitOfWork);

                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        /*private void ActualizarSubTipoExpediente(
           IEnumerable<CPadron_Rubros> consultaPadronRubrosDTO,
           ConsultaPadronRubrosDTO objectDto,
           ConsultaPadronDatosLocalDTO consultaPadronDatosLocalDTO,
           ConsultaPadronSolicitudesDTO consultaPadronDTO,
           ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL,
           IUnitOfWork unitOfWork)
        {
            int MaxIdTipoDocumentoRequerido = 0;

            if (consultaPadronRubrosDTO == null)
            {
                consultaPadronRubrosDTO = repo.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron);
            }
            if (consultaPadronRubrosDTO.Any())
                MaxIdTipoDocumentoRequerido = consultaPadronRubrosDTO.Max(p => p.id_tipodocreq);

            if (consultaPadronDatosLocalDTO == null)
            {
                ConsultaPadronDatosLocalBL consultaPadronDatosLocalBL = new ConsultaPadronDatosLocalBL();
                consultaPadronDatosLocalDTO = consultaPadronDatosLocalBL.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron).FirstOrDefault();
            }

            if (consultaPadronDatosLocalDTO != null)
            {
                if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.DJ &&
                   consultaPadronDatosLocalDTO.SuperficieCubiertaDl + consultaPadronDatosLocalDTO.SuperficieDescubiertaDl > 500)
                    MaxIdTipoDocumentoRequerido = (int)Constantes.TipoDocumentacionReq.PP;
            }
            int id_tipoexpediente = 0;
            int id_subtipoexpediente = 0;
            //-- Simple Sin Planos
            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.DJ)
            {
                id_tipoexpediente = (int)Constantes.TipoDeExpediente.Simple;
                id_subtipoexpediente = (int)Constantes.SubtipoDeExpediente.SinPlanos;
            }
            //-- Simple Con Planos
            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.PP)
            {
                id_tipoexpediente = (int)Constantes.TipoDeExpediente.Simple;
                id_subtipoexpediente = (int)Constantes.SubtipoDeExpediente.ConPlanos;
            }
            //-- Especiales
            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.IP)
            {
                id_tipoexpediente = (int)Constantes.TipoDeExpediente.Especial;
                id_subtipoexpediente = (int)Constantes.SubtipoDeExpediente.InspeccionPrevia;
            }
            //--Habilitacion Previa
            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.HP)
            {
                id_tipoexpediente = (int)Constantes.TipoDeExpediente.Especial;
                id_subtipoexpediente = (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia;
            }
            consultaPadronDTO.IdTipoExpediente = id_tipoexpediente;
            consultaPadronDTO.IdSubTipoExpediente = id_subtipoexpediente;

            consultaPadronSolicitudesBL.Update(consultaPadronDTO);

            if (consultaPadronDTO.IdSubTipoExpediente != (int)Constantes.SubtipoDeExpediente.SinPlanos)
            {
                repoConformacionLocal = new ConsultaPadronConformacionLocalRepository(unitOfWork);
                var elements = repoConformacionLocal.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron);
                foreach (var element in elements)
                    repoConformacionLocal.Delete(element);
            }            
        }*/
    }
}

