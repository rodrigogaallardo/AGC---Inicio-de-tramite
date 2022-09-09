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
	public class EncomiendaEstadosBL : IEncomiendaEstadosBL<EncomiendaEstadosDTO>
    {               
		private EncomiendaEstadosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaEstadosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaEstadosDTO, Encomienda_Estados>().ReverseMap()
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                    .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                    .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo));

                cfg.CreateMap<Encomienda_Estados, EncomiendaEstadosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                    .ForMember(dest => dest.nom_estado, source => source.MapFrom(p => p.NomEstado))
                    .ForMember(dest => dest.nom_estado_consejo, source => source.MapFrom(p => p.NomEstadoConsejo));

                cfg.CreateMap<EncomiendaEstadosDTO, EncomiendaExt_Estados>().ReverseMap()
                   .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                   .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                   .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado))
                   .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo));

                cfg.CreateMap<EncomiendaExt_Estados, EncomiendaEstadosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                    .ForMember(dest => dest.nom_estado, source => source.MapFrom(p => p.NomEstado))
                    .ForMember(dest => dest.nom_estado_consejo, source => source.MapFrom(p => p.NomEstadoConsejo));
            });
            mapperBase = config.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<EncomiendaEstadosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Estados>, IEnumerable<EncomiendaEstadosDTO>>(elements);
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
        /// <returns></returns>
        public IEnumerable<EncomiendaEstadosDTO> GetAllExt()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAllExt();
                var elementsDto = mapperBase.Map<IEnumerable<EncomiendaExt_Estados>, IEnumerable<EncomiendaEstadosDTO>>(elements);
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
		public EncomiendaEstadosDTO Single(int IdEstado )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaEstadosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEstado);
                var entityDto = mapperBase.Map<Encomienda_Estados, EncomiendaEstadosDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id_estado_actual"></param>
        /// <param name="tipoTramite"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaEstadosDTO> TraerEncomiendaExtEstadosSiguientes(Guid UserId, int id_estado_actual, int tipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.TraerEncomiendaExtEstadosSiguientes(UserId, id_estado_actual, tipoTramite);
                var elementsDto = mapperBase.Map<IEnumerable<EncomiendaExt_Estados>, IEnumerable<EncomiendaEstadosDTO>>(elements);
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
        /// <param name="UserId"></param>
        /// <param name="id_estado_actual"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaEstadosDTO> TraerEncomiendaEstadosSiguientes(Guid UserId, int id_estado_actual)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.TraerEncomiendaEstadosSiguientes(UserId, id_estado_actual);
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Estados>, IEnumerable<EncomiendaEstadosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

