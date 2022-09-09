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
    public class RubrosCNSubrubrosBL : IRubrosCNSubrubrosBL<RubrosCNSubRubrosDTO>
    {
        private RubrosCNSubRubrosRepository repo = null;

        private EncomiendaRubrosCNSubrubrosRepository repoRubroSubCN = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RubrosCNSubrubrosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosCNSubRubrosDTO, RubrosCN_Subrubros>().ReverseMap()
                .ForMember(dest => dest.RubrosDTO, source => source.MapFrom(p => p.RubrosCN));

                cfg.CreateMap<RubrosCN, RubrosCNDTO>().ReverseMap()
                .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.Codigo));

                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();
                #region Encomienda_RubrosCN_Subrubros
                cfg.CreateMap<EncomiendaRubrosCNSubrubrosDTO, Encomienda_RubrosCN_Subrubros>().ReverseMap()
                .ForMember(dest => dest.rubrosCNSubRubrosDTO, source => source.MapFrom(p => p.RubrosCN_Subrubros));

                cfg.CreateMap<Encomienda_RubrosCN_Subrubros, EncomiendaRubrosCNSubrubrosDTO>().ReverseMap();
                #endregion

                #region subRubros
                cfg.CreateMap<RubrosCN_Subrubros, RubrosCNSubRubrosDTO>().ReverseMap();
                cfg.CreateMap<RubrosCN_Subrubros, ItemRubrosCNSubRubrosDTO>().ReverseMap();
                #endregion

            });
            mapperBase = config.CreateMapper();
        }
        public IEnumerable<RubrosCNSubRubrosDTO> GetSubRubros(int idRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNSubRubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetSubRubros(idRubro);
                var elementsDTO = mapperBase.Map<IEnumerable<RubrosCN_Subrubros>, IEnumerable<RubrosCNSubRubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ItemRubrosCNSubRubrosDTO> GetSubRubrosVigentes(int idRubro, int idEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNSubRubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetSubRubrosVigentes(idRubro).ToList();

                return elements;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RubrosCNSubRubrosDTO Single(int idSubRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNSubRubrosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Single(idSubRubro);
                var elementsDTO = mapperBase.Map<RubrosCN_Subrubros, RubrosCNSubRubrosDTO>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RubrosCNSubRubrosDTO> GetSubRubrosByEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosCNSubRubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetSubRubrosCN(IdEncomienda);
                var elementsDTO = mapperBase.Map<IEnumerable<RubrosCN_Subrubros>, IEnumerable<RubrosCNSubRubrosDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void Insert(EncomiendaRubrosCNSubrubrosDTO SubRubroDTO, bool v, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoRubroSubCN = new EncomiendaRubrosCNSubrubrosRepository(unitOfWork);
                    //var entities = mapperBase.Map<EncomiendaRubrosCNSubrubrosDTO, RubrosCN_Subrubros>(SubRubroDTO);
                    Encomienda_RubrosCN_Subrubros entities = new Encomienda_RubrosCN_Subrubros();
                    entities.Id_EncRubro = SubRubroDTO.Id_EncRubro;
                    entities.Id_rubrosubrubro = SubRubroDTO.Id_rubrosubrubro;

                    var insertOk = repoRubroSubCN.Insert(entities);
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
