using IBusinessLayer;
using DataTransferObject;
using DataAcess;
using BaseRepository;
using UnitOfWork;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace BusinesLayer.Implementation
{
    public class SubTipoUbicacionesBL : ISubTipoUbicacionesBL<SubTipoUbicacionesDTO>
    {
        private SubTipoUbicacioneRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SubTipoUbicacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubTipoUbicacionesDTO, SubTiposDeUbicacion>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<SubTipoUbicacionesDTO> Get(int IdTipoUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SubTipoUbicacioneRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Get(IdTipoUbicacion);
                var lstDto = mapperBase.Map<IEnumerable<SubTiposDeUbicacion>, IEnumerable<SubTipoUbicacionesDTO>>(entity);
                return lstDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SubTipoUbicacionesDTO Single(int IdSubTipoUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SubTipoUbicacioneRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSubTipoUbicacion);
                var entityDto = mapperBase.Map<SubTiposDeUbicacion, SubTipoUbicacionesDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
