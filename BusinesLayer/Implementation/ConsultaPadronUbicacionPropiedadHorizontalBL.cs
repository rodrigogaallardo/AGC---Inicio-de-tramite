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
    public class ConsultaPadronUbicacionPropiedadHorizontalBL : IConsultaPadronUbicacionPropiedadHorizontalBL<ConsultaPadronUbicacionPropiedadHorizontalDTO>
    {               
        private ConsultaPadronUbicacionPropiedadHorizontalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
         
        public ConsultaPadronUbicacionPropiedadHorizontalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<CPadron_Ubicaciones_PropiedadHorizontal, ConsultaPadronUbicacionPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdConsultaPadronPropiedadHorizontal, source => source.MapFrom(p => p.id_cpadronprophorizontal))
                    .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));
            });
            mapperBase = config.CreateMapper();
        }	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadronUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<ConsultaPadronUbicacionPropiedadHorizontalDTO> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadronUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronUbicacionPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdConsultaPadronUbicacion(IdConsultaPadronUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal>, IEnumerable<ConsultaPadronUbicacionPropiedadHorizontalDTO>>(elements);
            return elementsDto;
        }
    }
}

