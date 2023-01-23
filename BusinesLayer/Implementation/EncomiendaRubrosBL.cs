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
    public class EncomiendaRubrosBL : IEncomiendaRubrosBL<EncomiendaRubrosDTO>
    {
        private EncomiendaRubrosRepository repo = null;
        private EncomiendaRepository repoEncomienda = null;
        private RubrosRepository repoRubro = null;
       

        private SSITSolicitudesRepository repoSSIT = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBaseRubro;
        IMapper mapperEncomiendaRubro;

        public EncomiendaRubrosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Encomienda_Rubros, EncomiendaRubrosDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.cod_rubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tipodocreq))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental));

                cfg.CreateMap<EncomiendaRubrosDTO, Encomienda_Rubros>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.desc_rubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental));


                #region "Rubros"
                cfg.CreateMap<Rubros, RubrosDTO>()
                   .ForMember(dest => dest.IdRubro, source => source.MapFrom(p => p.id_rubro))
                   .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.cod_rubro))
                   .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nom_rubro))
                   .ForMember(dest => dest.Busqueda, source => source.MapFrom(p => p.bus_rubro))
                   .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                   .ForMember(dest => dest.IdTipoDocumentorRequerido, source => source.MapFrom(p => p.id_tipodocreq))
                   .ForMember(dest => dest.EsAnterior, source => source.MapFrom(p => p.EsAnterior_Rubro))
                   .ForMember(dest => dest.VigenciaDesde, source => source.MapFrom(p => p.VigenciaDesde_rubro))
                   .ForMember(dest => dest.VigenciaHasta, source => source.MapFrom(p => p.VigenciaHasta_rubro))
                   .ForMember(dest => dest.PregAntenaEmisora, source => source.MapFrom(p => p.PregAntenaEmisora))
                   .ForMember(dest => dest.Tooltip, source => source.MapFrom(p => p.tooltip_rubro))
                   .ForMember(dest => dest.LocalVenta, source => source.MapFrom(p => p.local_venta))
                   .ForMember(dest => dest.Ley105, source => source.MapFrom(p => p.ley105))
                   .ForMember(dest => dest.RubrosTiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.Rubros_TiposDeDocumentosRequeridos));

                cfg.CreateMap<RubrosDTO, Rubros>()
                    .ForMember(dest => dest.id_rubro, source => source.MapFrom(p => p.IdRubro))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.nom_rubro, source => source.MapFrom(p => p.Nombre))
                    .ForMember(dest => dest.bus_rubro, source => source.MapFrom(p => p.Busqueda))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentorRequerido))
                    .ForMember(dest => dest.EsAnterior_Rubro, source => source.MapFrom(p => p.EsAnterior))
                    .ForMember(dest => dest.VigenciaDesde_rubro, source => source.MapFrom(p => p.VigenciaDesde))
                    .ForMember(dest => dest.VigenciaHasta_rubro, source => source.MapFrom(p => p.VigenciaHasta))
                    .ForMember(dest => dest.PregAntenaEmisora, source => source.MapFrom(p => p.PregAntenaEmisora))
                    .ForMember(dest => dest.tooltip_rubro, source => source.MapFrom(p => p.Tooltip))
                    .ForMember(dest => dest.local_venta, source => source.MapFrom(p => p.LocalVenta))
                    .ForMember(dest => dest.ley105, source => source.MapFrom(p => p.Ley105))
                    .ForMember(dest => dest.Rubros_TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.RubrosTiposDeDocumentosRequeridosDTO));
                #endregion
                               

                #region "Rubros_TiposDeDocumentosRequeridos"
                cfg.CreateMap<RubrosTiposDeDocumentosRequeridosDTO, Rubros_TiposDeDocumentosRequeridos>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridosDTO));

                cfg.CreateMap<Rubros_TiposDeDocumentosRequeridos, RubrosTiposDeDocumentosRequeridosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));
                #endregion

                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();
                #endregion

                cfg.CreateMap<RubrosTipoDocReqDTO, RubrosTipoDocReqEntity>();

                cfg.CreateMap<RubrosTipoDocReqEntity, RubrosTipoDocReqDTO>();


                cfg.CreateMap<Encomienda_Rubros_AT_Anterior, EncomiendaRubrosDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.cod_rubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tipodocreq))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental));

                cfg.CreateMap<EncomiendaRubrosDTO, Encomienda_Rubros_AT_Anterior>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.desc_rubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental));



            });
            mapperBase = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosDTO, RubrosEntity>().ReverseMap();
            });

            mapperBaseRubro = config1.CreateMapper();

            var config2 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaRubrosDTO, EncomiendaRubrosEntity>().ReverseMap();
                cfg.CreateMap<EncomiendaRubrosDTO, EncomiendaRubrosATAnteriorEntity>().ReverseMap();

            });

            mapperEncomiendaRubro = config2.CreateMapper();
           
        }

        public IEnumerable<EncomiendaRubrosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rubros>, IEnumerable<EncomiendaRubrosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EncomiendaRubrosDTO Single(int IdEncomiendaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaRubro);
                var entityDto = mapperBase.Map<Encomienda_Rubros, EncomiendaRubrosDTO>(entity);

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
        public IEnumerable<RubrosTipoDocReqDTO> GetRubrosTdoReqByIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetRubrosTdoReqByIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<RubrosTipoDocReqEntity>, IEnumerable<RubrosTipoDocReqDTO>>(elements);
            return elementsDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaRubrosDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rubros>, IEnumerable<EncomiendaRubrosDTO>>(elements);
            return elementsDto;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdTipoActividad"></param>
        ///// <returns></returns>	
        //public IEnumerable<EncomiendaRubrosDTO> GetByFKIdTipoActividad(int IdTipoActividad)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
        //     var elements = repo.GetByFKIdTipoActividad(IdTipoActividad);
        //    var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rubros>, IEnumerable<EncomiendaRubrosDTO>>(elements);
        //    return elementsDto;				
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdTipoDocumentoRequerido"></param>
        ///// <returns></returns>	
        //public IEnumerable<EncomiendaRubrosDTO> GetByFKIdTipoDocumentoRequerido(int IdTipoDocumentoRequerido)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
        //     var elements = repo.GetByFKIdTipoDocumentoRequerido(IdTipoDocumentoRequerido);
        //    var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rubros>, IEnumerable<EncomiendaRubrosDTO>>(elements);
        //    return elementsDto;				
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdImpactoAmbiental"></param>
        ///// <returns></returns>	
        //public IEnumerable<EncomiendaRubrosDTO> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
        //     var elements = repo.GetByFKIdImpactoAmbiental(IdImpactoAmbiental);
        //    var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rubros>, IEnumerable<EncomiendaRubrosDTO>>(elements);
        //    return elementsDto;				
        //}
        #region Métodos de actualizacion e insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDto"></param>
        /// <returns></returns>
        public bool InsertRubroUsoNoContemplado(EncomiendaRubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                    if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    if (objectDto.IdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.DJ && objectDto.SuperficieHabilitar > 500)
                        objectDto.IdTipoDocumentoRequerido = (int)Constantes.TipoDocumentacionReq.PP;

                    repo = new EncomiendaRubrosRepository(unitOfWork);

                    objectDto.CodigoRubro = "888888";

                    objectDto.CreateDate = DateTime.Now;

                    objectDto.IdImpactoAmbiental = (int)Constantes.CAA_TipoCertificado.SujetoaCategorizacion;
                    var elementDto = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros>(objectDto);

                    repo.Insert(elementDto);
                    objectDto.IdEncomiendaRubro = elementDto.id_encomiendarubro;

                    ActualizarSubTipoExpediente(encomiendaEntity, unitOfWork);

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
        public bool Insert(EncomiendaRubrosDTO objectDto)
        {
            return Insert(objectDto, true);
        }
        /// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
        public bool Insert(EncomiendaRubrosDTO objectDto, bool Validar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    repo = new EncomiendaRubrosRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);

                    if (Validar)
                    {
                        if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                            throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                        var encomiendaDatosLocalEntity = encomiendaEntity.Encomienda_DatosLocal.FirstOrDefault();

                        if (encomiendaDatosLocalEntity == null)
                            throw new Exception(Errors.ENCOMIENDA_NO_DATOS_LOCAL);

                        bool esAmpliacionSuperficie = (encomiendaDatosLocalEntity.ampliacion_superficie.HasValue ? encomiendaDatosLocalEntity.ampliacion_superficie.Value : false);

                        if (esAmpliacionSuperficie)
                        {
                            if (objectDto.SuperficieHabilitar > encomiendaDatosLocalEntity.superficie_cubierta_amp + encomiendaDatosLocalEntity.superficie_descubierta_amp)
                                throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO_AMP);
                        }
                        else if (objectDto.SuperficieHabilitar > encomiendaDatosLocalEntity.superficie_cubierta_dl + encomiendaDatosLocalEntity.superficie_descubierta_dl)
                            throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO);

                        var encomiendaRubrosEntity = encomiendaEntity.Encomienda_Rubros;

                        if (encomiendaRubrosEntity.Any(p => p.cod_rubro == objectDto.CodigoRubro))
                            throw new Exception(Errors.ENCOMIENDA_RUBRO_EXISTENTE);
                    }


                    objectDto.CreateDate = DateTime.Now;

                    if (objectDto.CodigoRubro != Constantes.RubroNoContemplado)
                    {
                        repoRubro = new RubrosRepository(unitOfWork);
                        var rubroEntity = repoRubro.Get(objectDto.CodigoRubro).FirstOrDefault();

                        if (rubroEntity == null)
                            throw new Exception(Errors.ENCOMIENDA_RUBRO);

                        if (rubroEntity.id_tipodocreq == (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaSinPlano && objectDto.SuperficieHabilitar > 500)
                            objectDto.IdTipoDocumentoRequerido = (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaConPlano;

                        RubrosImpactoAmbientalBL rubrosImpactoAmbiental = new RubrosImpactoAmbientalBL();

                        var rubrosImpactoAmbientalDTO = rubrosImpactoAmbiental.GetImpactoAmbiental(objectDto.SuperficieHabilitar, rubroEntity.id_rubro);
                        if (rubrosImpactoAmbientalDTO == null)
                            //Mantis 0126558: JADHE 47329 - SGI
                            //Si no tiene categorización de impacto, por defecto es "Sujeto a Categorización",
                            //throw new Exception(Errors.ENCOMIENDA_NO_CATEGORIZADO_AMBIENTALMENTE);
                            objectDto.IdImpactoAmbiental = (int)Constantes.ImpactoAmbiental.SujetoACategorización;
                        else
                        {
                            objectDto.IdImpactoAmbiental = rubrosImpactoAmbientalDTO.IdImpactoAmbiental;
                        }

                        objectDto.DescripcionRubro = rubroEntity.nom_rubro;
                        objectDto.EsAnterior = rubroEntity.EsAnterior_Rubro;
                        objectDto.IdTipoActividad = rubroEntity.id_tipoactividad;
                        objectDto.IdTipoDocumentoRequerido = rubroEntity.id_tipodocreq;
                    
                    }

		            var elementDto = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros>(objectDto);                   

                    repo.Insert(elementDto);

                    objectDto.IdEncomiendaRubro = elementDto.id_encomiendarubro;


                    ActualizarSubTipoExpediente(encomiendaEntity, unitOfWork);

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


        /// <summary>
        /// /
        /// </summary>
        /// <param name="encomiendaEntity"></param>
        /// <param name="unitOfWork"></param>
        private void ActualizarSubTipoExpediente(Encomienda encomiendaEntity, IUnitOfWork unitOfWork)
        {
            int MaxIdTipoDocumentoRequerido = 0;

            if (encomiendaEntity.Encomienda_Rubros.Any())
                MaxIdTipoDocumentoRequerido = encomiendaEntity.Encomienda_Rubros.Max(p => p.id_tipodocreq);

            var encomiendaDatosLocalEntity = encomiendaEntity.Encomienda_DatosLocal.FirstOrDefault();

            if (encomiendaDatosLocalEntity == null)
                throw new Exception(Errors.ENCOMIENDA_NO_DATOS_LOCAL);

            var sup_rub_local_venta = repo.GetByFKIdEncomiendaCodigo(encomiendaEntity.id_encomienda) ?? 999999;
            var sup_enc_local_venta = encomiendaDatosLocalEntity.local_venta ?? 0;

            if (sup_enc_local_venta > sup_rub_local_venta)
                MaxIdTipoDocumentoRequerido = (int)Constantes.TipoDocumentacionReq.HP;

            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.DJ &&
                encomiendaDatosLocalEntity.superficie_cubierta_dl + encomiendaDatosLocalEntity.superficie_descubierta_dl > 500)
                MaxIdTipoDocumentoRequerido = (int)Constantes.TipoDocumentacionReq.PP;

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
                throw new Exception("El tipo definido para el rubro fue dado de baja. Comuniquese con el administrador");
            }
            //--Habilitacion Previa
            if (MaxIdTipoDocumentoRequerido == (int)Constantes.TipoDocumentacionReq.HP)
            {
                id_tipoexpediente = (int)Constantes.TipoDeExpediente.Especial;
                id_subtipoexpediente = (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia;
            }

            encomiendaEntity.id_tipoexpediente = id_tipoexpediente;
            encomiendaEntity.id_subtipoexpediente = id_subtipoexpediente;

            repoSSIT = new SSITSolicitudesRepository(unitOfWork);
            var ssits = new List<SSIT_Solicitudes>(); 

            foreach (var solicitud in ssits)
            {
                solicitud.id_subtipoexpediente = id_subtipoexpediente;
                solicitud.id_tipoexpediente = id_tipoexpediente;
                repoSSIT.Update(solicitud);
            }
        }

        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaRubrosDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros>(objectDTO);
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
        public void Delete(EncomiendaRubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                    if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    repo = new EncomiendaRubrosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros>(objectDto);

                    repo.Delete(elementDto);

                    ActualizarSubTipoExpediente(encomiendaEntity, unitOfWork);

                    repoEncomienda.Update(encomiendaEntity);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteATAnterior(EncomiendaRubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaRubrosATAnteriorRepository repo = new EncomiendaRubrosATAnteriorRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);

                    var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                    if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var elementDto = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros_AT_Anterior>(objectDto);

                    repo.Delete(elementDto);

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
        public IEnumerable<EncomiendaRubrosDTO> GetRubros(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubros(IdEncomienda);
                var elementsDTO = mapperEncomiendaRubro.Map<IEnumerable<EncomiendaRubrosEntity>, IEnumerable<EncomiendaRubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<EncomiendaRubrosDTO> GetRubrosATAnterior(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosATAnterior(IdEncomienda);
                var elementsDTO = mapperEncomiendaRubro.Map<IEnumerable<EncomiendaRubrosATAnteriorEntity>, IEnumerable<EncomiendaRubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public string GetCodigoZonaEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();
                repoRubro = new RubrosRepository(UnitOfWork);
                repoEncomienda = new EncomiendaRepository(UnitOfWork);

                UbicacionesRepository repositoryUbicaciones = new UbicacionesRepository(UnitOfWork);
                string CodZona = repositoryUbicaciones.GetCodigoZonaEncomienda(IdEncomienda);


                return CodZona;
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
        public IEnumerable<RubrosDTO> GetRubros(int IdEncomienda, decimal Superficie, string CodigoRubro, string CodZona)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();
                repoRubro = new RubrosRepository(UnitOfWork);
                repoEncomienda = new EncomiendaRepository(UnitOfWork);

                var encomiendaEntity = repoEncomienda.Single(IdEncomienda);

                var TieneNormativa = encomiendaEntity.Encomienda_Normativas.Any();

                var rubrosEntity = repoRubro.GetRubros(CodZona, Superficie, CodigoRubro, TieneNormativa, encomiendaEntity.id_tipotramite);

                return mapperBaseRubro.Map<IEnumerable<RubrosEntity>, IEnumerable<RubrosDTO>>(rubrosEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public IEnumerable<RubrosDTO> GetRubrosHistoricos(int IdEncomienda, decimal Superficie, string CodigoRubro, string CodZona)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();
                repoRubro = new RubrosRepository(UnitOfWork);
                repoEncomienda = new EncomiendaRepository(UnitOfWork);

                var encomiendaEntity = repoEncomienda.Single(IdEncomienda);

                var TieneNormativa = encomiendaEntity.Encomienda_Normativas.Any();

                var rubrosEntity = repoRubro.GetRubrosHistoricos(CodZona, Superficie, CodigoRubro, TieneNormativa, encomiendaEntity.id_tipotramite);

                return mapperBaseRubro.Map<IEnumerable<RubrosEntity>, IEnumerable<RubrosDTO>>(rubrosEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Encomienda_ValidarCargaProfesional_porRubro(int id_encomienda)
        {
            try
            {
                bool cargar_prof = true;

                string[] rubros_vivienda = new string[2] { "VIND", "VCOL" };
                bool es_rubro_vivienda = false;
                bool es_rubro_otros = false;
                string cod_rubro = "";
                decimal sup_vivienda = 0;
                decimal SuperficieHabilitar = 0;

                var lstrub = repo.GetByFKIdEncomienda(id_encomienda);

                foreach (var item in lstrub)
                {
                    cod_rubro = item.cod_rubro;
                    SuperficieHabilitar = item.SuperficieHabilitar;

                    if (!string.IsNullOrEmpty((string)rubros_vivienda.Where(x => x == cod_rubro).FirstOrDefault()))
                    {
                        sup_vivienda = sup_vivienda + SuperficieHabilitar;
                        es_rubro_vivienda = true;
                    }
                    else
                        es_rubro_otros = true;
                }


                if (es_rubro_vivienda && !es_rubro_otros && sup_vivienda <= 5000)
                {
                    cargar_prof = false;
                }


                return cargar_prof;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSubTipoExpediente(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    repo = new EncomiendaRubrosRepository(unitOfWork);
                    var encomiendaEntity = repoEncomienda.Single(IdEncomienda);

                    ActualizarSubTipoExpediente(encomiendaEntity, unitOfWork);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public bool InsertATAnterior(EncomiendaRubrosDTO objectDto)
        {

            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repoEncomienda = new EncomiendaRepository(unitOfWork);
                EncomiendaRubrosATAnteriorRepository repo = new BaseRepository.EncomiendaRubrosATAnteriorRepository(unitOfWork);
                repoRubro = new RubrosRepository(unitOfWork);

                var encomiendaEntity = repoEncomienda.Single(objectDto.IdEncomienda);
                var encomiendaRubrosEntity = encomiendaEntity.Encomienda_Rubros_AT_Anterior;

                if (encomiendaRubrosEntity.Any(p => p.cod_rubro == objectDto.CodigoRubro))
                    throw new Exception(Errors.ENCOMIENDA_RUBRO_EXISTENTE);

                var rubroEntity = repoRubro.Get(objectDto.CodigoRubro).FirstOrDefault();

                if (rubroEntity == null)
                    throw new Exception(Errors.ENCOMIENDA_RUBRO);

                if (rubroEntity.id_tipodocreq == (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaSinPlano && objectDto.SuperficieHabilitar > 500)
                    objectDto.IdTipoDocumentoRequerido = (int)Constantes.TipoDocumentoRequerido.DeclaracionJuradaConPlano;
                RubrosImpactoAmbientalBL rubrosImpactoAmbiental = new RubrosImpactoAmbientalBL();

                var rubrosImpactoAmbientalDTO = rubrosImpactoAmbiental.GetImpactoAmbiental(objectDto.SuperficieHabilitar, rubroEntity.id_rubro);

                objectDto.CreateDate = DateTime.Now;
                objectDto.DescripcionRubro = rubroEntity.nom_rubro.Substring(0, 200);
                objectDto.EsAnterior = rubroEntity.EsAnterior_Rubro;
                if (rubrosImpactoAmbientalDTO != null)
                {
                    objectDto.IdImpactoAmbiental = rubrosImpactoAmbientalDTO?.IdImpactoAmbiental;
                }
                else
                {
                    //Si no tiene categorización de impacto, por defecto es "Sujeto a Categorización", agregar la condición gral.
                    objectDto.IdImpactoAmbiental = (int)Constantes.ImpactoAmbiental.SujetoACategorización;
                }
                objectDto.IdTipoActividad = rubroEntity.id_tipoactividad;
                objectDto.IdTipoDocumentoRequerido = rubroEntity.id_tipodocreq;

                var elementDto = mapperBase.Map<EncomiendaRubrosDTO, Encomienda_Rubros_AT_Anterior>(objectDto);

                repo.Insert(elementDto);

                objectDto.IdEncomiendaRubro = elementDto.id_encomiendarubro;

                unitOfWork.Commit();
                return true;
            }

        }
        public bool ValidarSuperficie(string CodigoRubro, string CodZonaPla, decimal Superficie, bool TieneNormativa)
        {

            bool ret = TieneNormativa;
            
            if(!ret)
            {

                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork UnitOfWork = this.uowF.GetUnitOfWork();
                RubrosRepository repo = new RubrosRepository(UnitOfWork);
                ret = repo.ValidarSuperficie(CodigoRubro, CodZonaPla, Superficie);
            }

            return ret;
        }
        public bool ProvieneSolicitudAnterior(int id_encomienda)
        {
            bool ret = false;

            uowF = new TransactionScopeUnitOfWorkFactory();
            var repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var sol = repo.GetByFKIdEncomienda(id_encomienda);
            ret = sol.SSIT_Solicitudes_Origen != null;

            return ret;
        }

    }
}

