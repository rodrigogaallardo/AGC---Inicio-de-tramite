using IBusinessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using DataTransferObject;
using AutoMapper;
using BaseRepository;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BusinesLayer.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class RubrosBL : IRubrosBL<RubrosDTO>
    {
        private RubrosRepository repo = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RubrosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosDTO, Rubros>().ReverseMap()
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
                    .ForMember(dest => dest.RubrosCircuitoAtomaticoZonasDTO, source => source.MapFrom(p => p.Rubros_CircuitoAtomatico_Zonas))
                    .ForMember(dest => dest.RubrosTiposDeDocumentosRequeridosZonasDTO, source => source.MapFrom(p => p.Rubros_TiposDeDocumentosRequeridos_Zonas));

                cfg.CreateMap<Rubros, RubrosDTO>().ReverseMap()
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
                    .ForMember(dest => dest.Rubros_CircuitoAtomatico_Zonas, source => source.MapFrom(p => p.RubrosCircuitoAtomaticoZonasDTO))
                    .ForMember(dest => dest.Rubros_TiposDeDocumentosRequeridos_Zonas, source => source.MapFrom(p => p.RubrosTiposDeDocumentosRequeridosZonasDTO));

                cfg.CreateMap<Rubros_CircuitoAtomatico_Zonas, RubrosCircuitoAtomaticoZonasDTO>();
                cfg.CreateMap<RubrosCircuitoAtomaticoZonasDTO, Rubros_CircuitoAtomatico_Zonas>();


                cfg.CreateMap<Rubros_TiposDeDocumentosRequeridos_Zonas, RubrosTiposDeDocumentosRequeridosZonasDTO>();
                cfg.CreateMap<RubrosTiposDeDocumentosRequeridosZonasDTO, Rubros_TiposDeDocumentosRequeridos_Zonas>();


                cfg.CreateMap<Rubros_Config_Incendio, RubrosIncendioDTO>();

            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RubrosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Rubros>, IEnumerable<RubrosDTO>>(entityRubros);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ImpactoAmbiental(int IdRubro, decimal Superficie)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                return repo.ImpactoAmbiental(IdRubro, Superficie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRubros"></param>
        /// <returns></returns>
        public int TipoDocumentoRequeridoMayor(int[] IdRubros)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                return repo.TipoDocumentoRequeridoMayor(IdRubros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public RubrosDTO Single(int IdRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.Single(IdRubro);
                var lstMenuesDto = mapperBase.Map<Rubros, RubrosDTO>(entityRubros);

                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarRubrosIndividuales(List<string> lstcod_rubro)
        {
            var listaRubrosDTO = GetByListCodigo(lstcod_rubro);

            var rubroIndividual = listaRubrosDTO
                                        .Where(x => x.EsRubroIndividual)
                                        .FirstOrDefault();

            if (listaRubrosDTO.Count() > 1 && rubroIndividual != null)
                throw new Exception(string.Format(Errors.SOLO_RUBRO_INDIVIDUAL, rubroIndividual.Nombre));
        }

        public IEnumerable<RubrosDTO> GetByListCodigo(List<string> lstcod_rubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetByListCodigo(lstcod_rubro);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Rubros>, IEnumerable<RubrosDTO>>(entityRubros);

                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDto"></param>
        /// <returns></returns>
        public bool Insert(RubrosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new RubrosRepository(unitOfWork);
                    var bafycoEntyties = mapperBase.Map<RubrosDTO, Rubros>(objectDto);
                    var insertOk = repo.Insert(bafycoEntyties);
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
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public IEnumerable<RubrosDTO> Get(string Codigo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());
                var entityDTO = repo.Get(Codigo);
                var elements = mapperBase.Map<IEnumerable<Rubros>, IEnumerable<RubrosDTO>>(entityDTO);

                return elements.Where(p => !p.VigenciaHasta.HasValue || p.VigenciaHasta > DateTime.Now);
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
        public IEnumerable<RubrosIncendioDTO> getRubrosIncendioEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.getRubrosIncendioEncomienda(IdEncomienda);
                var elementsDTO = mapperBase.Map<IEnumerable<Rubros_Config_Incendio>, IEnumerable<RubrosIncendioDTO>>(elements);

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
        /// <returns></returns>
        public IEnumerable<RubrosDTO> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetRubrosByIdEncomienda(IdEncomienda);
                var elementsDTO = mapperBase.Map<IEnumerable<Rubros>, IEnumerable<RubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetInfoAdicionalRubros(List<string> lstCodRubros)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());

                var ret = repo.GetInfoAdicionalRubros(lstCodRubros);
                
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<RubrosDTO> GetRubrosAnterioresByIdEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetRubrosAnterioresByIdEncomienda(IdEncomienda);
                var elementsDTO = mapperBase.Map<IEnumerable<Rubros>, IEnumerable<RubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CategorizaciónDeImpacto(int cod_rubro)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosRepository(this.uowF.GetUnitOfWork());

            return repo.CategorizaciónDeImpacto(cod_rubro);
        }
    }
}
