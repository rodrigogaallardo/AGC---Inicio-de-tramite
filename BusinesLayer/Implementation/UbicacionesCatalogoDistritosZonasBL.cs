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
    public class UbicacionesCatalogoDistritosZonasBL : IUbicacionesCatalogoDistritosZonasBL<Encomienda_Ubicaciones_DistritosDTO>
    {
        private UbicacionesCatalogoDistritosZonasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public UbicacionesCatalogoDistritosZonasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Encomienda_Ubicaciones_DistritosDTO, Ubicaciones_CatalogoDistritos_Zonas>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }


        public Encomienda_Ubicaciones_DistritosDTO GetZona(int IdDistrito)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosZonasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetZona(IdDistrito);
                var entityDto = mapperBase.Map<Ubicaciones_CatalogoDistritos_Zonas, Encomienda_Ubicaciones_DistritosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Encomienda_Ubicaciones_DistritosDTO GetZonaByIdDistrito(int idDistrito)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesCatalogoDistritosZonasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetZona(idDistrito);
                var entityDto = mapperBase.Map<Ubicaciones_CatalogoDistritos_Zonas, Encomienda_Ubicaciones_DistritosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
