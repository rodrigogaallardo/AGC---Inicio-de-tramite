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

namespace BusinesLayer.Implementation
{
    public class UbicacionesCatalogoDistritosSubzonasBL : IUbicacionesCatalogoDistritosSubzonasBL<UbicacionesCatalogoDistritosSubzonasDTO>
    {
        private UbicacionesCatalogoDistritosSubzonasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public UbicacionesCatalogoDistritosSubzonasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UbicacionesCatalogoDistritosSubzonasDTO, Ubicaciones_CatalogoDistritos_Subzonas>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public UbicacionesCatalogoDistritosSubzonasDTO GetSubZona(int IdDistrito)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosSubzonasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetSubZona(IdDistrito);
                var entityDto = mapperBase.Map<Ubicaciones_CatalogoDistritos_Subzonas, UbicacionesCatalogoDistritosSubzonasDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSubZonaByIdZona(int zona)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosSubzonasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetSubZonas(zona);

                return String.Join(" ", entity.Select(x => x.CodigoSubZona).ToArray());              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
