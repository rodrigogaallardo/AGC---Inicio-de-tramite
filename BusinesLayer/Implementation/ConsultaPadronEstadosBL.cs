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

namespace BusinesLayer.Implementation
{
    public class ConsultaPadronEstadosBL : IConsultaPadronEstadosBL<ConsultaPadronEstadosDTO>
    {
        private ConsultaPadronEstadosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public ConsultaPadronEstadosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronEstadosDTO, CPadron_Estados>().ReverseMap()
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                    .ForMember(dest => dest.NomEstadoUsuario, source => source.MapFrom(p => p.nom_estado_usuario))
                    .ForMember(dest => dest.NomEstadoInterno, source => source.MapFrom(p => p.nom_estado_interno));

                cfg.CreateMap<CPadron_Estados, ConsultaPadronEstadosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                    .ForMember(dest => dest.nom_estado_usuario, source => source.MapFrom(p => p.NomEstadoUsuario))
                    .ForMember(dest => dest.nom_estado_interno, source => source.MapFrom(p => p.NomEstadoInterno));
            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronEstadosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Estados>, IEnumerable<ConsultaPadronEstadosDTO>>(elements);
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
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public ConsultaPadronEstadosDTO Single(int IdEstado)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronEstadosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEstado);
                var entityDto = mapperBase.Map<CPadron_Estados, ConsultaPadronEstadosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

