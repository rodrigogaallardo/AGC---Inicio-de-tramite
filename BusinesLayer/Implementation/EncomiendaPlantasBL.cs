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
using StaticClass;


namespace BusinesLayer.Implementation
{
    public class EncomiendaPlantasBL : IEncomiendaPlantasBL<EncomiendaPlantasDTO>
    {
        private EncomiendaPlantasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPlanta;

        public EncomiendaPlantasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaPlantasDTO, Encomienda_Plantas>().ReverseMap()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.detalle_encomiendatiposector))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.TipoSectorDTO, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<Encomienda_Plantas, EncomiendaPlantasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.detalle_encomiendatiposector, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSectorDTO));

                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
            });

            mapperBase = config.CreateMapper();

            var configPlanta = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaPlantasDTO, EncomiendaPlantasEntity>().ReverseMap();
            });
            mapperPlanta = configPlanta.CreateMapper();
        }

        public IEnumerable<EncomiendaPlantasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaPlantasRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Plantas>, IEnumerable<EncomiendaPlantasDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<EncomiendaPlantasDTO> Get(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaPlantasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.Get(IdEncomienda);
                var elementsDto = mapperPlanta.Map<IEnumerable<EncomiendaPlantasEntity>, IEnumerable<EncomiendaPlantasDTO>>(elements);

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
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaPlantasDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaPlantasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Plantas>, IEnumerable<EncomiendaPlantasDTO>>(elements);

            foreach (EncomiendaPlantasDTO item in elementsDto)
            {
                if (item.IdTipoSector == (int)Constantes.TipoSector.Piso || item.IdTipoSector == (int)Constantes.TipoSector.Otro)
                    item.Detalle = item.TipoSectorDTO.Descripcion + " " + item.detalle_encomiendatiposector;
                else
                    item.Detalle = item.TipoSectorDTO.Descripcion;
            }

            return elementsDto;
        }
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(EncomiendaPlantasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlantasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaPlantasDTO, Encomienda_Plantas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
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
        /// <param name="objectDto"></param>
        public int Insert(EncomiendaPlantasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlantasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaPlantasDTO, Encomienda_Plantas>(objectDto);
                    repo.Insert(elementDto);
                    unitOfWork.Commit();
                    return elementDto.id_encomiendatiposector;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(EncomiendaPlantasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlantasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaPlantasDTO, Encomienda_Plantas>(objectDto);
                    repo.Update(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public EncomiendaPlantasDTO GetByFKIdEncomiendaIdEncomiendaTiposector(int IdEncomienda, int IdEncomiendaTiposector)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaPlantasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdEncomiendaIdEncomiendaTiposector(IdEncomienda, IdEncomiendaTiposector);
                var elementsDto = mapperBase.Map<Encomienda_Plantas, EncomiendaPlantasDTO>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
