using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
    public class TransferenciasUbicacionesPropiedadHorizontalBL : ITransferenciasUbicacionesPropiedadHorizontalBL<TransferenciasUbicacionPropiedadHorizontalDTO>
    {
        private TransferenciasUbicacionPropiedadHorizontalRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciasUbicacionesPropiedadHorizontalBL()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Transf_Ubicaciones_PropiedadHorizontal, TransferenciasUbicacionPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdTranferenciaPropiedadHorizontal, source => source.MapFrom(p => p.id_transfprophorizontal))
                    .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));
            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasUbicacionPropiedadHorizontalDTO> GetByFKIdSolicitudUbicacion(int IdTransferenciaUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasUbicacionPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTransferenciaUbicacion(IdTransferenciaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones_PropiedadHorizontal>, IEnumerable<TransferenciasUbicacionPropiedadHorizontalDTO>>(elements);
            return elementsDto;
        }
    }
}
