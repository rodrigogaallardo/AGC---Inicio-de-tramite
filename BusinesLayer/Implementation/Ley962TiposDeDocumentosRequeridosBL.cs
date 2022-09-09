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
	public class Ley962TiposDeDocumentosRequeridosBL : ILey962TiposDeDocumentosRequeridosBL<Ley962TiposDeDocumentosRequeridosDTO>
    {
        
		private Ley962TiposDeDocumentosRequeridosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public Ley962TiposDeDocumentosRequeridosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ley962TiposDeDocumentosRequeridosDTO, Ley962_TiposDeDocumentosRequeridos>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridosDTO));

                cfg.CreateMap<Ley962_TiposDeDocumentosRequeridos, Ley962TiposDeDocumentosRequeridosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));
                    

                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>().ReverseMap();

            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<Ley962TiposDeDocumentosRequeridosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new Ley962TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ley962_TiposDeDocumentosRequeridos>, IEnumerable<Ley962TiposDeDocumentosRequeridosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Ley962TiposDeDocumentosRequeridosDTO Single(int Id)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new Ley962TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<Ley962_TiposDeDocumentosRequeridos, Ley962TiposDeDocumentosRequeridosDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
