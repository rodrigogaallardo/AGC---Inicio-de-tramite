using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;

namespace BusinesLayer.Implementation
{
    public class UbicacionesCatalogoDistritosBL : IUbicacionesCatalogoDistritosBL<UbicacionesCatalogoDistritosDTO>
    {
        private UbicacionesCatalogoDistritosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public UbicacionesCatalogoDistritosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>().ReverseMap()
                    .ForMember(dest => dest.IdDistrito, source => source.MapFrom(p => p.IdDistrito))
                    .ForMember(dest => dest.IdGrupoDistrito, source => source.MapFrom(p => p.IdGrupoDistrito))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.CreateDate, source => source.MapFrom(p => p.CreateDate))
                    ;

                cfg.CreateMap<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>().ReverseMap()
                    .ForMember(dest => dest.IdDistrito, source => source.MapFrom(p => p.IdDistrito))
                    .ForMember(dest => dest.IdGrupoDistrito, source => source.MapFrom(p => p.IdGrupoDistrito))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.CreateDate, source => source.MapFrom(p => p.CreateDate));
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<UbicacionesCatalogoDistritosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_CatalogoDistritos>, IEnumerable<UbicacionesCatalogoDistritosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UbicacionesCatalogoDistritosDTO Single(int IdZona)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdZona);
                var entityDto = mapperBase.Map<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(UbicacionesCatalogoDistritosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesCatalogoDistritosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>(objectDto);
                    var insertOk = repo.Insert(elementDto);
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
        public void Update(UbicacionesCatalogoDistritosDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesCatalogoDistritosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>(objectDTO);
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
        public void Delete(UbicacionesCatalogoDistritosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesCatalogoDistritosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>(objectDto);
                    var insertOk = repo.Delete(elementDto);
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
        public IEnumerable<UbicacionesCatalogoDistritosDTO> GetDistritosEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetDistritosEncomienda(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_CatalogoDistritos>, IEnumerable<UbicacionesCatalogoDistritosDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UbicacionesCatalogoDistritosDTO> GetDistritosUbicacion(int IdUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetDistritosUbicacion(IdUbicacion);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_CatalogoDistritos>, IEnumerable<UbicacionesCatalogoDistritosDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UbicacionesCatalogoDistritosDTO> GetDistritosUbicacion(List<int> lstUbi)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetDistritosUbicacion(lstUbi);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_CatalogoDistritos>, IEnumerable<UbicacionesCatalogoDistritosDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetIdSubZonaByUbicacion(int idUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                return repo.GetIdSubZonaByUbicacion(idUbicacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetIdZonaByUbicacion(int id)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosRepository(this.uowF.GetUnitOfWork());
                int? result =  repo.GetIdZonaByUbicacion(id);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
