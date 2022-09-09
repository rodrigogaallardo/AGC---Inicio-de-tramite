using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaRubrosSubCNBL : IEncomiendaRubrosSubCNBL<EncomiendaRubrosCNSubrubrosDTO>
    {
        private EncomiendaRubrosCNSubrubrosRepository repo = null;

        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBaseRubro;
        IMapper mapperEncomiendaRubro;

        public EncomiendaRubrosSubCNBL()
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
                #endregion

                #region subRubros
                cfg.CreateMap<RubrosCN_Subrubros, RubrosCNSubRubrosDTO>().ReverseMap();
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

        public void Delete(EncomiendaRubrosCNSubrubrosDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EncomiendaRubrosCNSubrubrosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN_Subrubros>, IEnumerable<EncomiendaRubrosCNSubrubrosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EncomiendaRubrosCNSubrubrosDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            throw new NotImplementedException();
        }

        public bool Insert(EncomiendaRubrosCNSubrubrosDTO objectDto, Guid userlogued)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
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

        public EncomiendaRubrosCNSubrubrosDTO Single(int IdEncomiendaRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaRubro);
                var entityDto = mapperBase.Map<Encomienda_RubrosCN_Subrubros, EncomiendaRubrosCNSubrubrosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EncomiendaRubrosCNSubrubrosDTO> GetSubRubrosByEncomienda(int id_encomienda_ant)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetSubRubrosByEncomienda(id_encomienda_ant);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN_Subrubros>, IEnumerable<EncomiendaRubrosCNSubrubrosDTO>>(elements);
            return elementsDto;
        }

        public void Update(EncomiendaRubrosCNSubrubrosDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EncomiendaRubrosCNSubrubrosDTO> GetSubRubrosByEncomiendaRubro(int idEncomiendaRubro, int id_encomienda_ant)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetSubRubrosByEncomiendaRubro(idEncomiendaRubro, id_encomienda_ant);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN_Subrubros>, IEnumerable<EncomiendaRubrosCNSubrubrosDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<EncomiendaRubrosCNSubrubrosDTO> GetSubRubrosByEncomiendaRubroVigentes(int idEncomiendaRubro, int id_encomienda_ant)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRubrosCNSubrubrosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetRubrosSubrubrosCNVigentes(idEncomiendaRubro, id_encomienda_ant);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_RubrosCN_Subrubros>, IEnumerable<EncomiendaRubrosCNSubrubrosDTO>>(elements);
            return elementsDto;
        }
    }
}
