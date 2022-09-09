using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAcess;
using BaseRepository;
using Dal.UnitOfWork;
using UnitOfWork;
using AutoMapper;
using System.Data;
using System.Transactions;
using DataAcess.EntityCustom;


namespace BusinesLayer.Implementation
{
    public class EncomiendaConformacionLocalBL : IEncomiendaConformacionLocalBL<EncomiendaConformacionLocalDTO>
    {
        private EncomiendaConformacionLocalRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaConformacionLocalBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region "Encomienda_ConformacionLocal"
                cfg.CreateMap<Encomienda_ConformacionLocal, EncomiendaConformacionLocalDTO>()
                    .ForMember(dest => dest.EncomiendaPlantasDTO, source => source.MapFrom(p => p.Encomienda_Plantas))
                    .ForMember(dest => dest.TipoSuperficieDTO, source => source.MapFrom(p => p.TipoSuperficie))
                    .ForMember(dest => dest.TipoVentilacionDTO, source => source.MapFrom(p => p.tipo_ventilacion))
                    .ForMember(dest => dest.TipoDestinoDTO, source => source.MapFrom(p => p.TipoDestino))
                    .ForMember(dest => dest.TipoIluminacionDTO, source => source.MapFrom(p => p.tipo_iluminacion));

                cfg.CreateMap<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>()
                    .ForMember(dest => dest.Encomienda_Plantas, source => source.Ignore())
                    .ForMember(dest => dest.tipo_ventilacion, source => source.Ignore())
                    .ForMember(dest => dest.TipoDestino, source => source.Ignore())
                    .ForMember(dest => dest.tipo_iluminacion, source => source.Ignore())
                    .ForMember(dest => dest.TipoSuperficie, source => source.Ignore());
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

                #region "TipoSuperficie"
                cfg.CreateMap<TipoSuperficieDTO, TipoSuperficie>();

                cfg.CreateMap<TipoSuperficie, TipoSuperficieDTO>();
                #endregion

                #region "tipo_ventilacion"
                cfg.CreateMap<TipoIluminacionDTO, tipo_iluminacion>();

                cfg.CreateMap<tipo_iluminacion, TipoIluminacionDTO>();
                #endregion

                #region "TipoIluminacion"
                cfg.CreateMap<TipoVentilacionDTO, tipo_ventilacion>();

                cfg.CreateMap<tipo_ventilacion, TipoVentilacionDTO>();
                #endregion

                #region "TipoDestino"
                cfg.CreateMap<TipoDestinoDTO, TipoDestino>();

                cfg.CreateMap<TipoDestino, TipoDestinoDTO>();
                #endregion

                #region "TipoSector"
                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
                #endregion


            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaConformacionLocalDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaConformacionLocalRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetByFKIdEncomienda(IdEncomienda);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Encomienda_ConformacionLocal>, IEnumerable<EncomiendaConformacionLocalDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EncomiendaConformacionLocalDTO Single(int id_encomiendaconflocal)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaConformacionLocalRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.Single(id_encomiendaconflocal);
                var lstMenuesDto = mapperBase.Map<Encomienda_ConformacionLocal, EncomiendaConformacionLocalDTO>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(EncomiendaConformacionLocalDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaConformacionLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>(objectDto);
                    repo.Insert(elementDto);

                    unitOfWork.Commit();

                    return elementDto.id_encomiendaconflocal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EncomiendaConformacionLocalDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaConformacionLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>(objectDto);
                    repo.Update(elementDto);

                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(EncomiendaConformacionLocalDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaConformacionLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>(objectDto);
                    var insertOk = repo.Delete(elementDto);
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
