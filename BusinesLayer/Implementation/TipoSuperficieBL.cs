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
	public class TipoSuperficieBL : ITipoSuperficieBL<TipoSuperficieDTO>
    {               
		private TipoSuperficieRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TipoSuperficieBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
	                cfg.CreateMap<TipoSuperficieDTO, TipoSuperficie>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TipoSuperficieDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoSuperficieRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TipoSuperficie>, IEnumerable<TipoSuperficieDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TipoSuperficieDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoSuperficieRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<TipoSuperficie, TipoSuperficieDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

