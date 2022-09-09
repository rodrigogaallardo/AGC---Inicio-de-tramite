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
using System.Text.RegularExpressions;

namespace BusinesLayer.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class RubrosCNBL : IRubrosCNBL<RubrosCNDTO>
    {
        private RubrosCNRepository repo = null;

        private IUnitOfWorkFactory uowF = null;

        private EncomiendaUbicacionesMixturasRepository encomendaUbicacionMixturaRepository = null;

        private EncomiendaUbicacionesRepository encomendaUbicacionRepository = null;


        IMapper mapperBase;

        public RubrosCNBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
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
                #region subRubros
                cfg.CreateMap<RubrosCN_Subrubros, RubrosCNSubRubrosDTO>().ReverseMap();
                #endregion

            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RubrosCNDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<RubrosCN>, IEnumerable<RubrosCNDTO>>(entityRubros);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RubrosCNDTO> GetRubros(string codigo, decimal superficie)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetRubros(codigo, superficie).ToList();
                var lstMenuesDto = mapperBase.Map<List<RubrosCN>, IEnumerable<RubrosCNDTO>>(entityRubros);
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
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public RubrosCNDTO Single(int IdRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.Single(IdRubro);
                var lstMenuesDto = mapperBase.Map<RubrosCN, RubrosCNDTO>(entityRubros);

                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public IEnumerable<RubrosCNDTO> GetByListCodigo(List<string> lstcod_rubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetByListCodigo(lstcod_rubro);
                var lstMenuesDto = mapperBase.Map<IEnumerable<RubrosCN>, IEnumerable<RubrosCNDTO>>(entityRubros);

                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public IEnumerable<RubrosCNDTO> Get(string Codigo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                var entityDTO = repo.Get(Codigo);
                var elements = mapperBase.Map<IEnumerable<RubrosCN>, IEnumerable<RubrosCNDTO>>(entityDTO);

                return elements.Where(p => !p.VigenciaHasta.HasValue || p.VigenciaHasta > DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetLimiteMixtura(string Codigo, int encomiendaId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());
                encomendaUbicacionRepository = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork()); 
                encomendaUbicacionMixturaRepository = new EncomiendaUbicacionesMixturasRepository(this.uowF.GetUnitOfWork());

                var rubtoDTO = repo.Get(Codigo).FirstOrDefault();
                int encomiendaUbicacionId = encomendaUbicacionRepository.GetByFKIdEncomienda(encomiendaId).FirstOrDefault().id_encomiendaubicacion;
                int mixtura = encomendaUbicacionMixturaRepository.GetByFKIdEncomiendaUbicacion(encomiendaUbicacionId).FirstOrDefault().IdZonaMixtura;
                string result = rubtoDTO.ZonaMixtura1;

                switch (mixtura)
                {
                    case 2:
                        result = rubtoDTO.ZonaMixtura2;
                        break;
                    case 3:
                        result = rubtoDTO.ZonaMixtura3;
                        break;
                    case 4:
                        result = rubtoDTO.ZonaMixtura4;
                        break;
                    default:
                        break;
                }

                Regex reg = new Regex("[A-Za-z]");
                if ((reg. IsMatch(result)))
                {
                    result = (result.ToUpper() == "NO") ? "0" : "10000";
                }

                return result;
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
        /// <returns></returns>
        public IEnumerable<RubrosCNDTO> GetRubrosByIdSolicitud(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetRubrosByIdSolicitud(IdSolicitud);
                var elementsDTO = mapperBase.Map<IEnumerable<RubrosCN>, IEnumerable<RubrosCNDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RubrosCNDTO> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetRubrosByIdEncomienda(IdEncomienda);
                var elementsDTO = mapperBase.Map<IEnumerable<RubrosCN>, IEnumerable<RubrosCNDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CategorizaciónDeImpacto(string cod_rubro)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());

            return repo.CategorizaciónDeImpacto(cod_rubro);
        }

        internal string GetDescripcionDocumentosFaltantesByRubros(int id_solicitud,IEnumerable<RubrosCNDTO> lstRubrosCN)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosCNRepository(this.uowF.GetUnitOfWork());

            var list = lstRubrosCN.Select(x => x.IdRubro).ToList();

            return repo.GetDescripcionDocumentosFaltantesByRubros(id_solicitud, list);
        }
    }
}
